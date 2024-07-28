using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.Globalization;
using Shell32;
using System.Text.RegularExpressions;
using CsvHelper.Configuration;

namespace FrameSlice
{

    public partial class PhotoCutter : Form
    {
        private int captureCount = 0; // Initialize a variable to keep track of the capture count
        private const int FrameIntervalMilliseconds = 33;
        private bool isPlaying = false;
        private int currentRowIndex = 0; // Initialize a variable to keep track of the current row indexrowIndex
        private List<string> mp4Files = new List<string>(); // List to store selected MP4 files
        List<string> videoDurations = new List<string>();// List to store Durations of MP4 files
        List<string> mediaCreatedTime = new List<string>();// List to store Media created time
        private int currentMP4Index = 0; // Index of the currently playing MP4 file
        private string timeStartMP4 = null;
        Dictionary<string, SnapshotInfo> ImagesTimes = new Dictionary<string, SnapshotInfo>(); // Define a dictionary to store the Images url and loccal_time
        Dictionary<string, SnapshotInfo> DeltaTimes = new Dictionary<string, SnapshotInfo>(); // Initialize DeltaTimes as a Dictionary<string, SnapshotInfo>
        string previousTime = null;   // Iterate over the dictionary to calculate and store the time differences
        private string MP4fileDuration = null;
        string firstImageTime = null;
        string lastImageTime = null;
        Dictionary<string, string> FinalUrlImages = new Dictionary<string, string>();
        private Shell shell;  // Declare Shell object as a class-level field
        private string currentImagePath = null;

        // Class-level fields for images
        private Image orangeLock;
        private Image redLock;
        private Image play;
        private Image playClick;
        private Image pause;
        private Image leftArrowClick;
        private Image leftArrow;
        private Image rightArrowClick;
        private Image rightArrow;

        private bool syncButtonClicked = false;
        private bool playButtonClicked = false;
        private bool lockButtonRed = false;

        public PhotoCutter()
        {
            InitializeComponent();
            InitializeVScrollBar();

            vlcControl1.EndReached += vlcControl1_EndReached;

            // Set ComboBox style and add items programmatically
            comboBoxCamera.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCamera.Items.AddRange(new object[] { "front", "rear", "right", "left" });

            // Set the default selected item (e.g., "Front")
            comboBoxCamera.SelectedItem = "front"; // Default item value

            // Subscribe to the TimeChanged event of the VLC control
            vlcControl1.TimeChanged += VlcControl1_TimeChanged;

            // Subscribe to the Click events of the new buttons
            nextFrameButton.Click += NextFrameButton_Click;
            previousFrameButton.Click += PreviousFrameButton_Click;


            // Hide the progress bar initially
            progressBar.Visible = false;

            // initialization the Shell object
            shell = new Shell();


            // Load images at the class level
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            orangeLock = Image.FromFile(Path.Combine(basePath, "Images", "padlock.png"));
            redLock = Image.FromFile(Path.Combine(basePath, "Images", "padlockred.png"));
            play = Image.FromFile(Path.Combine(basePath, "Images", "play.png"));
            pause = Image.FromFile(Path.Combine(basePath, "Images", "pause.png"));
            leftArrowClick = Image.FromFile(Path.Combine(basePath, "Images", "left-arrow-click.png"));
            leftArrow = Image.FromFile(Path.Combine(basePath, "Images", "left-arrow.png"));
            rightArrow = Image.FromFile(Path.Combine(basePath, "Images", "right-arrow.png"));
            rightArrowClick = Image.FromFile(Path.Combine(basePath, "Images", "right-arrow-click.png"));
            playClick = Image.FromFile(Path.Combine(basePath, "Images", "play-click.png"));

            goButton.Enabled = false;  // Start with the GO button disabled

            // Set the text for the tooltip
            // Initialize tooltips for each control
            toolTip.SetToolTip(this.BrowseVideo, "Click to select the folder containing your GoPro video files.");
            toolTip.SetToolTip(this.syncButton, "Synchronize the selected video with its corresponding data. Ensure all required files are loaded before syncing.");
            toolTip.SetToolTip(this.muteButton, "Toggle the sound on or off during video playback.");
            toolTip.SetToolTip(this.previousFrameButton, "Move to the previous frame in the video.");
            toolTip.SetToolTip(this.StartStopButton, "Play or pause the video playback.");
            toolTip.SetToolTip(this.nextFrameButton, "Move to the next frame in the video.");
            toolTip.SetToolTip(this.loadStriderCSVButton, "Load a CSV file containing metadata for synchronization with video files.");
            toolTip.SetToolTip(this.goButton, "Execute the video processing task based on the current settings and loaded data.");
            toolTip.SetToolTip(this.BrowseButtonImage, "Select a destination folder where the screenshots from the video will be saved.");
            toolTip.SetToolTip(this.BrowseButtonCSV, "Choose a destination folder where the new CSV file, including updated paths and data, will be stored.");

        }



        public class SnapshotInfo
        {
            public string Id { get; set; }
            public string Road { get; set; }
            public string Lane { get; set; }
            public string Camera { get; set; }
            public string Time { get; set; }
            public int Milliseconds { get; set; }

            // Constructor to initialize the properties
            public SnapshotInfo(string id, string road, string lane, string camera, string time, int milliseconds)
            {
                Id = id;
                Road = road;
                Lane = lane;
                Camera = camera;
                Time = time;
                Milliseconds = milliseconds;
            }
        }

        private void PhotoCutter_Load(object sender, EventArgs e)
        {
            textBoxURLVideo.ReadOnly = true;
            tblImageDestination.ReadOnly = true;
            tblCSVDestination.ReadOnly = true;
        }

        private void Playbutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxURLVideo.Text))
            {
                if (!isPlaying)
                {
                    vlcControl1.Play();
                    StartStopButton.BackgroundImage = pause;
                    StartStopButton.BackgroundImageLayout = ImageLayout.Stretch;
                    isPlaying = true;
                }
                else
                {
                    vlcControl1.Pause();
                    StartStopButton.BackgroundImage = play;
                    StartStopButton.BackgroundImageLayout = ImageLayout.Stretch;
                    isPlaying = false;
                }

                playButtonClicked = true;
                UpdateGoButtonState();

            }
            else
            {
                MessageBox.Show("Please Insert a MP4 File", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Event handler for the "Next Frame" button
        private void NextFrameButton_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                // Get the current playback position
                long currentPosition = vlcControl1.Time;

                // Seek forward by the frame interval
                long newPosition = currentPosition + FrameIntervalMilliseconds;

                // Update the playback position
                vlcControl1.Time = newPosition;

                // Manually update labelCurrentTime
                TimeSpan newTimeSpan = TimeSpan.FromMilliseconds(newPosition);
                string formattedTime = newTimeSpan.ToString(@"hh\:mm\:ss\.fff");
                labelCurrentTime.Text = formattedTime; // Update the label with the new time
            }
        }

        // Event handler for the "Previous Frame" button
        private void PreviousFrameButton_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                // Get the current playback position
                long currentPosition = vlcControl1.Time;
                // Calculate the new position
                long newPosition = currentPosition - FrameIntervalMilliseconds;

                // Ensure newPosition is not negative
                if (newPosition < 0)
                    newPosition = 0;

                // Update the playback position
                vlcControl1.Time = newPosition;

                // Manually update labelCurrentTime
                TimeSpan newTimeSpan = TimeSpan.FromMilliseconds(newPosition);
                string formattedTime = newTimeSpan.ToString(@"hh\:mm\:ss\.fff");
                labelCurrentTime.Text = formattedTime; // Update the label with the new time
            }
        }

        // Method to browse a folder and load all MP4 files within it
        private void BrowseVideo_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "Select Folder Containing MP4 Files";

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderBrowser.SelectedPath;
                    string[] mp4FilesInFolder = Directory.GetFiles(selectedFolder, "*.mp4");

                    if (mp4FilesInFolder.Length > 0)
                    {
                        mp4Files = mp4FilesInFolder.ToList(); // Store selected MP4 files in the list
                        currentMP4Index = 0; // Reset current MP4 index to the first file
                        DisplayCurrentMP4Index(); // Display the current MP4 index

                        // Start playing the selected MP4 immediately
                        PlaySelectedMP4(false);
                    }
                    else
                    {
                        MessageBox.Show("No MP4 files found in the selected folder.", "Information");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }


        static string ExtractTime(string input)
        {
            // Regular expression pattern to match time in format HH:MM
            string pattern = @"(?<hour>\d{2}):(?<minute>\d{2})";

            // Match the pattern in the input string
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                // Extract hour and minute from the match
                string hour = match.Groups["hour"].Value;
                string minute = match.Groups["minute"].Value;

                // Return formatted time string
                return $"{hour}:{minute}";
            }

            return null; // No match found
        }

        // Method to play the currently selected MP4 file
        private void PlaySelectedMP4(bool startPlayingImmediately)
        {
            if (mp4Files.Count > 0 && currentMP4Index < mp4Files.Count)
            {
                string selectedMP4 = mp4Files[currentMP4Index];

                if (currentMP4Index == 0)
                {
                    // Initialize media info for all MP4 files
                    for (int i = 0; i < mp4Files.Count; i++)
                    {
                        if (System.IO.File.Exists(mp4Files[i]))
                        {
                            Folder folder = shell.NameSpace(Path.GetDirectoryName(mp4Files[i]));
                            FolderItem folderItem = folder.ParseName(Path.GetFileName(mp4Files[i]));
                            string mediaCreated = folder.GetDetailsOf(folderItem, 208);
                            string timePart = ExtractTime(mediaCreated);
                            if (!string.IsNullOrEmpty(timePart))
                            {
                                timeStartMP4 = timePart;
                                mediaCreatedTime.Add(timePart);
                            }
                            string durationString = folder.GetDetailsOf(folderItem, 27);
                            TimeSpan duration;
                            if (TimeSpan.TryParse(durationString, out duration))
                            {
                                MP4fileDuration = duration.ToString(@"hh\:mm\:ss\.fff"); // Using format with milliseconds
                                videoDurations.Add(MP4fileDuration);
                            }
                        }
                    }

                    // Print all videoDurations values
                    Console.WriteLine("Video Durations:");
                    foreach (var duration in videoDurations)
                    {
                        Console.WriteLine(duration);
                    }
                }

                MP4fileDuration = videoDurations[currentMP4Index];
                timeStartMP4 = mediaCreatedTime[currentMP4Index];

                BeginInvoke(new MethodInvoker(() =>
                {
                    textBoxURLVideo.Text = selectedMP4; // Update the textbox with the new video URL
                    DisplayCurrentMP4Index(); // Update the display of current MP4 index

                    if (vlcControl1.IsPlaying)
                    {
                        vlcControl1.Pause(); // Ensure the previous video is paused
                    }

                    vlcControl1.SetMedia(new Uri(selectedMP4)); // Set the new media

                    // Event to ensure the media is loaded
                    vlcControl1.MediaChanged += (s, e) =>
                    {
                        vlcControl1.Play(); // Play to ensure media is loaded

                        // Wait until the time is not -1, indicating the media is ready
                        Task.Delay(100).ContinueWith(t =>
                        {
                            if (!startPlayingImmediately)
                            {
                                vlcControl1.Pause(); // Pause the video if not supposed to play immediately
                            }
                            vlcControl1.Time = 0; // Reset to the beginning to ensure it's ready to play
                        });
                    };
                }));
            }
        }




        // Method to display the current MP4 index
        private void DisplayCurrentMP4Index()
        {
            // Display the current index and total count of MP4 files
            labelMP4Index.Text = $"{currentMP4Index + 1}/{mp4Files.Count}";
        }


        private void MuteButton_Click(object sender, EventArgs e)
        {
            if (vlcControl1.Audio.IsMute)
            {
                vlcControl1.Audio.IsMute = false;
                muteButton.Text = "Mute";
            }
            else
            {
                vlcControl1.Audio.IsMute = true;
                muteButton.Text = "Unmute";
            }
        }


        private string selectedFolderPathForImages = ""; // Store the selected folder path

        private void BrowseButtonImage_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize a new FolderBrowserDialog
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Select the folder to save images";

                // Show the FolderBrowserDialog
                DialogResult result = folderBrowserDialog.ShowDialog();

                // If the user selects a folder and clicks OK
                if (result == DialogResult.OK)
                {
                    // Get the selected folder path
                    selectedFolderPathForImages = folderBrowserDialog.SelectedPath;
                    tblImageDestination.Text = selectedFolderPathForImages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private string selectedFolderPathForCSV = ""; // Store the selected folder path
        private void BrowseButtonCSV_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize a new FolderBrowserDialog
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Select the folder to save CSV file";

                // Show the FolderBrowserDialog
                DialogResult result = folderBrowserDialog.ShowDialog();

                // If the user selects a folder and clicks OK
                if (result == DialogResult.OK)
                {
                    // Get the selected folder path
                    selectedFolderPathForCSV = folderBrowserDialog.SelectedPath;
                    tblCSVDestination.Text = selectedFolderPathForCSV;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        // Event handler for the EndReached event of the VLC control
        private void vlcControl1_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            Console.WriteLine($"End reached for MP4 index: {currentMP4Index + 1}");

            // Update the label displaying the current MP4 index
            UpdateCurrentMP4IndexLabel();

            // Check if there are more MP4 files to play
            if (currentMP4Index + 1 < mp4Files.Count)
            {
                currentMP4Index++;
                // Play the next MP4 file immediately
                Console.WriteLine($"Playing next MP4 file: {mp4Files[currentMP4Index]}");
                PlaySelectedMP4(false);
            }
            else
            {
                // If there are no more MP4 files, display a message or perform any other action
                //MessageBox.Show("All MP4 files have been played.", "Information");
            }
        }

        // Method to update the label displaying the current MP4 index
        private void UpdateCurrentMP4IndexLabel()
        {
            // Ensure this operation is executed on the main UI thread
            BeginInvoke(new MethodInvoker(() =>
            {
                labelMP4Index.Text = $"{currentMP4Index + 1}/{mp4Files.Count}";
            }));
        }


        private string selectedValuecolumnName = "front";

        private string previousSelectedValue; // Declare a field to store the previous selected value

        private void comboBoxCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentSelectedValue = comboBoxCamera.SelectedItem.ToString();

            try
            {
                selectedValuecolumnName = currentSelectedValue;

                if (!string.IsNullOrEmpty(textBoxStriderCSV.Text))
                {
                    string filename = textBoxStriderCSV.Text;
                    string value = GetValueFromCsv(filename, selectedValuecolumnName, 0);
                    csvImage.Image = Image.FromFile(value);
                    csvImage.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                // Update the previous selected value and currentRowIndex only if the selection change is successful
                previousSelectedValue = currentSelectedValue;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");

                // Revert back to the previous selected value
                if (!string.IsNullOrEmpty(previousSelectedValue))
                {
                    comboBoxCamera.SelectedItem = previousSelectedValue;
                }
                else
                {
                    comboBoxCamera.SelectedIndex = 0;
                }
            }
        }

        private void VlcControl1_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
        {
            // Get the current playback time
            TimeSpan currentTime = TimeSpan.FromMilliseconds(e.NewTime);

            // Format the time as HH:mm:ss:fff (hours:minutes:seconds:milliseconds)
            string formattedTime = currentTime.ToString(@"hh\:mm\:ss\.fff");

            // Update the label text with the formatted time on the main UI thread
            Invoke((Action)(() =>
            {
                labelCurrentTime.Text = formattedTime;

                // Assuming vlcControl1.Length is the total length of the video
                vScrollBar1.Value = (int)(100 * e.NewTime / vlcControl1.Length);
            }));
        }



        private void nextImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxStriderCSV.Text))
                {
                    currentRowIndex++;
                    string filename = textBoxStriderCSV.Text;
                    string value = GetValueFromCsv(filename, selectedValuecolumnName, currentRowIndex);
                    string ImageTime = GetValueFromCsv(filename, "local_time", currentRowIndex);
                    ImageTimeLabel.Text = ImageTime;
                    csvImage.Image = Image.FromFile(value);
                    csvImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    currentImagePath = value; // Store the current image path
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                currentRowIndex--;
            }
        }

        private void previousImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxStriderCSV.Text))
                {
                    currentRowIndex--;
                    string filename = textBoxStriderCSV.Text;
                    string value = GetValueFromCsv(filename, selectedValuecolumnName, currentRowIndex);
                    string ImageTime = GetValueFromCsv(filename, "local_time", currentRowIndex);
                    ImageTimeLabel.Text = ImageTime;
                    csvImage.Image = Image.FromFile(value);
                    csvImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    currentImagePath = value; // Store the current image path
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                currentRowIndex++;
            }
        }

        private void loadStriderCSVButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    string filename = fd.FileName;
                    // Do something with the selected CSV file path, such as displaying it in a textbox
                    textBoxStriderCSV.Text = filename;

                    // Now that you have the CSV file path, you can call the method to extract the value from the CSV
                    // For example, let's assume you want to extract the value from column "ColumnName"
                    string columnName = "front"; // Change this to the name of the column you want to extract the value from
                    string value = GetValueFromCsv(filename, columnName, currentRowIndex);
                    string ImageTime = GetValueFromCsv(filename, "local_time", currentRowIndex);
                    ImageTimeLabel.Text = ImageTime;
                    csvImage.Image = Image.FromFile(value);
                    csvImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    currentImagePath = value; // Store the current image path
                }
                else
                {
                    MessageBox.Show("No Selection", "Empty");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        public string GetValueFromCsv(string filePath, string columnName, int rowIndex)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read(); // Read the header row
                csv.ReadHeader(); // Read the header names

                // Find the index of the column by name
                int columnIndex = -1;
                foreach (string header in csv.HeaderRecord)
                {
                    if (header.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        columnIndex = Array.IndexOf(csv.HeaderRecord, header);
                        break;
                    }
                }

                if (columnIndex == -1)
                {
                    throw new ArgumentException($"Column '{columnName}' not found.");
                }

                // Read the CSV records
                int currentRowIndex = -1;
                while (csv.Read())
                {
                    currentRowIndex++;

                    // Check if the current row matches the desired index
                    if (currentRowIndex == rowIndex)
                    {
                        return csv.GetField(columnIndex);
                    }
                }

                throw new ArgumentException($"Row {rowIndex} not found.");
            }
        }


        private async void goButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(timeStartMP4))
                {
                    MessageBox.Show("Please select MP4 file.", "Information");
                    return;
                }

                if (string.IsNullOrEmpty(selectedFolderPathForImages))
                {
                    MessageBox.Show("Please select a folder to save images.", "Information");
                    return;
                }

                //TimeSpan targetTime = TimeSpan.Parse(AddTimes(timeStartMP4, labelCurrentTime.Text)); // Convert timeStartMP4 to TimeSpan

                //await TakeSnapshotAtTime(targetTime);
                
                // Build ImagesTimes if it's empty
                if (ImagesTimes.Count == 0)
                {
                    BuildImagesTimesFromImageUrl();
                }

                ProcessImage.Visible = true;
                BuildDictionaries();

                ExportCSVs();

                await TakeSnapshotAtTime();

                MessageBox.Show("Screenshots captured successfully.", "Information");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            Console.WriteLine(FinalUrlImages.Values);
            try
            {
                // Add a new column named with the new Images url
                string newCsvPath = CreateNewCSV(textBoxStriderCSV.Text, comboBoxCamera.SelectedItem.ToString(), FinalUrlImages, tblCSVDestination.Text);

                Console.WriteLine($"New CSV file with added column created: {newCsvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        //private async Task TakeSnapshotAtTime(TimeSpan targetTime)
        private async Task TakeSnapshotAtTime()
        {
            int i = 1;
            int totalSnapshots = DeltaTimes.Count;
            progressBar.Visible = true;
            ImagesFromImagesLabel.Visible = true;
            estimatedTimeLabel.Visible = true;
            DateTime startTime = DateTime.Now;

            foreach (var pair in DeltaTimes)
            {
                if (pair.Key == (currentMP4Index + 1).ToString())
                {
                    currentMP4Index++;
                    PlaySelectedMP4(false);
                    i++;
                    continue;
                }

                double progressPercentage = (double)i / totalSnapshots * 100;
                progressBar.Value = (int)progressPercentage;

                string fileName = $"{pair.Value.Road}_{pair.Value.Lane}_{pair.Value.Id}.png";
                string fullPath = Path.Combine(selectedFolderPathForImages, pair.Value.Road.ToString(), pair.Value.Lane.ToString(), pair.Value.Camera.ToString());

                Directory.CreateDirectory(fullPath);

                string filePath = Path.Combine(fullPath, fileName);

                try
                {
                    await Task.Run(() => vlcControl1.TakeSnapshot(filePath));
                    // Check if the snapshot file was created
                    if (File.Exists(filePath))
                    {
                        FinalUrlImages.Add(pair.Key.ToString(), filePath);
                    }
                    else
                    {
                        Console.WriteLine("Snapshot could not be taken.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error taking snapshot: {ex.Message}");
                }



                DateTime currentTime = DateTime.Now;
                TimeSpan elapsedTime = currentTime - startTime;

                double estimatedTimeRemainingT = (double)i / totalSnapshots;
                TimeSpan estimatedTimeRemaining = TimeSpan.FromTicks((long)(elapsedTime.Ticks / estimatedTimeRemainingT * (1 - estimatedTimeRemainingT)));

                estimatedTimeLabel.Text = $"Estimated Time Remaining: {estimatedTimeRemaining.ToString(@"hh\:mm\:ss")}";

                long newPosition = vlcControl1.Time + pair.Value.Milliseconds;
                Console.WriteLine("vlcControl1.Time - " + vlcControl1.Time);
                Console.WriteLine("pair.Value.Milliseconds - " + pair.Value.Milliseconds);
                vlcControl1.Time = newPosition;
                Console.WriteLine("vlcControl1.Time - " + vlcControl1.Time);
                ImagesFromImagesLabel.Text = $"{i}/{totalSnapshots}";
                i++;
            }

            progressBar.Value = 100;
        }


        public static int CountColonsAndDot(string input)
        {
            int count = 0;

            foreach (char c in input)
            {
                if (c == ':' || c == '.')
                {
                    count++;
                }
            }

            return count;
        }

        // Function to convert time string in the format "hh:mm:ss.ffffffff" to milliseconds
        public static int TimeToMilliseconds(string timeStr)
        {
            string[] parts = timeStr.Split(':');
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            int seconds = int.Parse(parts[2]);
            int milliseconds = int.Parse(parts[3]);

            return (hours * 3600 + minutes * 60 + seconds) * 1000 + milliseconds;
        }



        private void InitializeVScrollBar()
        {
            vScrollBar1.Dock = DockStyle.None;

            vScrollBar1.Scroll += VScrollBar1_Scroll;

        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (vlcControl1.Length > 0)
            {
                // Calculate the new time based on the scrollbar value
                long newTime = (long)(e.NewValue / 100.0 * vlcControl1.Length);

                // Set the VLC player's time
                vlcControl1.Time = newTime;

                // Update labelCurrentTime on the main UI thread
                Invoke((MethodInvoker)(() =>
                {
                    labelCurrentTime.Text = TimeSpan.FromMilliseconds(newTime).ToString(@"hh\:mm\:ss\.fff");
                }));

            }
        }

        static bool StartsWith(string mainString, string prefix)
        {
            return mainString.StartsWith(prefix);
        }

        static string AddTimes(string time1Str, string time2Str, bool includeSeconds = true)
        {
            try
            {
                // Parse the time strings
                TimeSpan time1 = TimeSpan.Parse(time1Str);
                TimeSpan time2 = TimeSpan.Parse(time2Str);

                // Add the times together
                TimeSpan sum = time1.Add(time2);

                // Format the result based on includeSeconds parameter
                string format = includeSeconds ? "{0:hh\\:mm\\:ss}" : "{0:hh\\:mm}";
                string result = string.Format(format, sum);

                return result;
            }
            catch (FormatException ex)
            {
                // Handle the parsing error
                Console.WriteLine("Error: Invalid time format.");
                MessageBox.Show("Error: " + ex.Message, "Error");
                return null; // Or any other action you want to take when parsing fails
            }

        }

        public static int SubtractTimeStrings(string time1, string time2)
        {
            int milliseconds1 = TimeToMilliseconds(time1);
            int milliseconds2 = TimeToMilliseconds(time2);

            int differenceMilliseconds = milliseconds1 - milliseconds2;

            if (differenceMilliseconds < 0)
            {
                differenceMilliseconds = 0;
            }

            return differenceMilliseconds;
        }


        private void syncButton_Click(object sender, EventArgs e)
        {
            if (textBoxURLVideo.Text == "" || textBoxStriderCSV.Text == "")
            {
                MessageBox.Show("Please Insert a MP4 & CSV Files ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lockButton.Enabled = true;
            lockButton.Visible = true;

            bool found = FindImageByTime();
            if (!found)
            {
                MessageBox.Show("No matching images found.", "Search Result");
            }
            else
            {
                syncButtonClicked = true;
                UpdateGoButtonState();
                
                
                // Print ImagesTimes dictionary
                Console.WriteLine("ImagesTimes Dictionary:");
                foreach (var pair in ImagesTimes)
                {
                    Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value.Time}");
                }
            }
        }


        static string CreateNewCSV(string csvPath, string columnName, Dictionary<string, string> ImagesPath, string destinationDirectory)
        {
            // Check if the file exists
            if (!System.IO.File.Exists(csvPath))
            {
                throw new FileNotFoundException("The specified CSV file does not exist.", csvPath);
            }

            // Create a new file name for the modified CSV
            string newCsvPath = Path.Combine(destinationDirectory, "newCsv.csv");

            // Read the original CSV file and create a new file with the added column
            using (var reader = new StreamReader(csvPath))
            using (var writer = new StreamWriter(newCsvPath))
            {
                string headerLine = reader.ReadLine();
                string[] headers = headerLine.Split(',');
                int ColumnIndex = Array.IndexOf(headers, columnName);

                // Write the header line with the new column name
                writer.WriteLine($"{headerLine},{"new_images_path"}");

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    string Value = values[ColumnIndex];

                    // Determine the value for the new column
                    string newValue = ImagesPath.ContainsKey(Value) ? ImagesPath[Value] : "";

                    // Write the row with the new column value
                    writer.WriteLine($"{line},{newValue}");
                }
            }

            return newCsvPath;
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            // Toggle the enabled state of syncButton
            syncButton.Enabled = !syncButton.Enabled;

            // Toggle the background image of lockButton based on the state of syncButton
            if (syncButton.Enabled)
            {
                lockButton.BackgroundImage = orangeLock;
                lockButtonRed = false;  // Red image is not shown
                // Disable the next and previous image buttons when lockButton is not red
                nextImageButton.BackgroundImage = rightArrow;
                previousImageButton.BackgroundImage = leftArrow;
                nextFrameButton.BackgroundImage = rightArrow;
                previousFrameButton.BackgroundImage = leftArrow;
                nextImageButton.Enabled = true;
                previousImageButton.Enabled = true;
                nextFrameButton.Enabled = true;
                previousFrameButton.Enabled = true;
                StartStopButton.Enabled = true;
                StartStopButton.BackgroundImage = play;
            }
            else
            {
                lockButton.BackgroundImage = redLock;
                lockButtonRed = true;  // Red image is shown
                // Enable the next and previous image buttons when lockButton is red
                nextImageButton.BackgroundImage = rightArrowClick;
                previousImageButton.BackgroundImage = leftArrowClick;
                nextFrameButton.BackgroundImage = rightArrowClick;
                previousFrameButton.BackgroundImage = leftArrowClick;
                nextImageButton.Enabled = false;
                previousImageButton.Enabled = false;
                nextFrameButton.Enabled = false;
                previousFrameButton.Enabled = false;
                StartStopButton.Enabled = false;
                StartStopButton.BackgroundImage = playClick;

                if (isPlaying)
                {
                    vlcControl1.Pause();
                    StartStopButton.BackgroundImage = playClick;
                    StartStopButton.BackgroundImageLayout = ImageLayout.Stretch;
                    isPlaying = false;
                }
            }
            UpdateGoButtonState();  // Update the GO button state

            // Optional: adjust the button's BackgroundImageLayout if needed
            lockButton.BackgroundImageLayout = ImageLayout.Stretch; // This stretches the image to fill the button
        }

        private void BuildDictionaries()
        {
            // Assume ImagesTimes has been populated already, or pass it as a parameter
            double millisecondsMP4 = TimeSpan.Parse(MP4fileDuration).TotalMilliseconds - TimeSpan.Parse(labelCurrentTime.Text).TotalMilliseconds;

            var keys = new List<string>(ImagesTimes.Keys);
            for (int i = 0; i < keys.Count - 1; i++) // Loop until the second last key
            {
                string currentKey = keys[i];
                string nextKey = keys[i + 1];

                // Calculate time difference between next key time and current key time
                int milliseconds = SubtractTimeStrings(ImagesTimes[nextKey].Time, ImagesTimes[currentKey].Time);

                int deltamillisecondsMP4 = (int)(milliseconds - millisecondsMP4);
                millisecondsMP4 -= milliseconds;


                if (millisecondsMP4 < 0 && currentMP4Index + 1 < mp4Files.Count)
                {
                    currentMP4Index++;
                    millisecondsMP4 += TimeSpan.Parse(videoDurations[currentMP4Index]).TotalMilliseconds;
                    Console.WriteLine("millisecondsMP4" + millisecondsMP4);
                    SnapshotInfo snapshotInfo = new SnapshotInfo(ImagesTimes[currentKey].Id, ImagesTimes[currentKey].Road, ImagesTimes[currentKey].Lane, selectedValuecolumnName, ImagesTimes[currentKey].Time, 0);
                    DeltaTimes.Add(currentMP4Index.ToString(), snapshotInfo); // sign that I need switch to new MP4 file
                    SnapshotInfo snapshotInfo1 = new SnapshotInfo(ImagesTimes[currentKey].Id, ImagesTimes[currentKey].Road, ImagesTimes[currentKey].Lane, selectedValuecolumnName, ImagesTimes[currentKey].Time, deltamillisecondsMP4);
                    DeltaTimes.Add(currentKey, snapshotInfo1);
                }
                else
                {
                    // Store the time difference in the dictionary
                    SnapshotInfo snapshotInfo = new SnapshotInfo(ImagesTimes[currentKey].Id, ImagesTimes[currentKey].Road, ImagesTimes[currentKey].Lane, selectedValuecolumnName, ImagesTimes[currentKey].Time, milliseconds);
                    DeltaTimes.Add(currentKey, snapshotInfo);
                }
            }

            // Add the last key with 0 milliseconds since there's no next key to compare to
            string lastKey = keys[keys.Count - 1];
            SnapshotInfo lastSnapshotInfo = new SnapshotInfo(ImagesTimes[lastKey].Id, ImagesTimes[lastKey].Road, ImagesTimes[lastKey].Lane, selectedValuecolumnName, ImagesTimes[lastKey].Time, 0);
            DeltaTimes.Add(lastKey, lastSnapshotInfo);

            currentMP4Index = 0;
        }




        private void BuildImagesTimesFromImageUrl()
        {
            using (var reader = new StreamReader(textBoxStriderCSV.Text))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read(); // Read the header row
                csv.ReadHeader(); // Read the header names

                // Find the index of the column by name
                int columnIndex = -1;
                foreach (string header in csv.HeaderRecord)
                {
                    if (header.Equals(selectedValuecolumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        columnIndex = Array.IndexOf(csv.HeaderRecord, header);
                        break;
                    }
                }

                if (columnIndex == -1)
                {
                    throw new ArgumentException($"Column '{selectedValuecolumnName}' not found.");
                }

                // Read the CSV records
                int findLine = 0;
                bool foundImage = false;
                while (csv.Read())
                {
                    string value = csv.GetField(selectedValuecolumnName);
                    if (value == currentImagePath)
                    {
                        // If the current image path matches, set the relevant properties
                        currentRowIndex = findLine;
                        string ImageTime = csv.GetField("local_time");
                        firstImageTime = ImageTime;
                        ImageTimeLabel.Text = ImageTime;
                        foundImage = true; // Indicate that the image has been found
                    }

                    if (foundImage)
                    {
                        // Add data to ImagesTimes dictionary
                        SnapshotInfo snapshotInfo = new SnapshotInfo(
                            csv.GetField("id"),
                            csv.GetField("road"),
                            csv.GetField("lane"),
                            selectedValuecolumnName,
                            csv.GetField("local_time"),
                            0
                        );

                        // Ensure unique keys in ImagesTimes dictionary
                        string key = value ?? findLine.ToString();
                        if (!ImagesTimes.ContainsKey(key))
                        {
                            ImagesTimes.Add(key, snapshotInfo);
                        }
                        else
                        {
                            // Handle the case where the key already exists
                            Console.WriteLine($"Key '{key}' already exists in ImagesTimes. Skipping duplicate entry.");
                        }
                    }

                    findLine++;
                }

                // If no matching image was found, throw an exception
                if (!foundImage)
                {
                    throw new ArgumentException($"Image with path '{currentImagePath}' not found in column '{selectedValuecolumnName}'.");
                }
            }
        }



        private bool FindImageByTime()
        {
            bool found = false; // Flag to track if a match is found
            using (var reader = new StreamReader(textBoxStriderCSV.Text))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read(); // Read the header row
                csv.ReadHeader(); // Read the header names

                // Find the index of the column by name
                int columnIndex = -1;
                foreach (string header in csv.HeaderRecord)
                {
                    if (header.Equals(selectedValuecolumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        columnIndex = Array.IndexOf(csv.HeaderRecord, header);
                        break;
                    }
                }

                if (columnIndex == -1)
                {
                    throw new ArgumentException($"Column '{selectedValuecolumnName}' not found.");
                }

                // Read the CSV records
                string value = null;
                int findLine = 0;

                ImagesTimes.Clear();
                DeltaTimes.Clear();

                previousTime = null;
                firstImageTime = null;
                lastImageTime = null;

                double millisecondsMP4 = TimeSpan.Parse(MP4fileDuration).TotalMilliseconds - TimeSpan.Parse(labelCurrentTime.Text).TotalMilliseconds;// The amount of milliseconds I have to take into account in the length of the remaining MP4 file
                Console.WriteLine($"{millisecondsMP4}");
                while (csv.Read())
                {
                    // Parse the value in the 'local_time' column
                    string currentTime = csv.GetField("local_time");

                    // Check if the current time matches the target time in the full format
                    if (StartsWith(currentTime, AddTimes(timeStartMP4, labelCurrentTime.Text)) && !found)
                    {
                        value = csv.GetField(selectedValuecolumnName);
                        currentRowIndex = findLine;
                        string filename = textBoxStriderCSV.Text;
                        string ImageTime = GetValueFromCsv(filename, "local_time", currentRowIndex);
                        firstImageTime = ImageTime;
                        ImageTimeLabel.Text = ImageTime;
                        csvImage.Image = Image.FromFile(value);
                        csvImage.SizeMode = PictureBoxSizeMode.StretchImage;
                        found = true; // Set the flag to true indicating a match is found
                    }
                    if (found)
                    {
                        //Store the value from selectedValuecolumnName column as the key
                        //and the corresponding value from local_time column as the value in the dictionary
                        value = csv.GetField(selectedValuecolumnName);
                        SnapshotInfo snapshotInfo = new SnapshotInfo(csv.GetField("id"), csv.GetField("road"), csv.GetField("lane"), selectedValuecolumnName, csv.GetField("local_time"), 0);
                        ImagesTimes.Add(value, snapshotInfo);
                    }

                    findLine++;
                }

                // If not found in full format, try searching with hours and minutes only
                if (!found)
                {
                    // Reset the file pointer to the beginning of the CSV file
                    reader.DiscardBufferedData();
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    csv.Read(); // Skip the header row

                    findLine = 0; // Reset the line counter

                    while (csv.Read())
                    {
                        // Parse the value in the 'local_time' column
                        string currentTime = csv.GetField("local_time");

                        // Check if the current time matches the target time in the abbreviated format
                        if (StartsWith(currentTime, AddTimes(timeStartMP4, labelCurrentTime.Text.Substring(0, labelCurrentTime.Text.Length - 4), false)) && !found)
                        {
                            value = csv.GetField(selectedValuecolumnName);
                            currentRowIndex = findLine;
                            int lastIndex = currentTime.LastIndexOf(':');
                            if (lastIndex != -1)
                            {
                                currentTime = currentTime.Substring(0, lastIndex);
                            }
                            string filename = textBoxStriderCSV.Text;
                            string ImageTime = GetValueFromCsv(filename, "local_time", currentRowIndex);
                            firstImageTime = ImageTime;
                            lastImageTime = AddTimes(ImageTime.Substring(0, Math.Min(ImageTime.Length, 8)), MP4fileDuration, true);
                            Console.WriteLine($"Last Image Time : {lastImageTime} ");
                            ImageTimeLabel.Text = ImageTime;
                            timeStartMP4 = currentTime;
                            csvImage.Image = Image.FromFile(value);
                            csvImage.SizeMode = PictureBoxSizeMode.StretchImage;
                            found = true; // Set the flag to true indicating a match is found
                        }
                        if (found)
                        {
                            //Store the value from selectedValuecolumnName column as the key
                            //and the corresponding value from local_time column as the value in the dictionary
                            value = csv.GetField(selectedValuecolumnName);
                            SnapshotInfo snapshotInfo = new SnapshotInfo(csv.GetField("id"), csv.GetField("road"), csv.GetField("lane"), selectedValuecolumnName, csv.GetField("local_time"), 0);
                            ImagesTimes.Add(value, snapshotInfo);
                        }

                        findLine++;
                    }
                    Console.WriteLine(findLine.ToString());
                }
            }
            return found;
        }

        private void UpdateGoButtonState()
        {
            goButton.Enabled =  playButtonClicked && lockButtonRed;
        }

        private void ExportCSVs()
        {
            string imagesTimesPath = Path.Combine(selectedFolderPathForCSV, "ImagesTimes.csv");
            string deltaTimesPath = Path.Combine(selectedFolderPathForCSV, "DeltaTimes.csv");

            try
            {
                // Export ImagesTimes to CSV
                using (var writer = new StreamWriter(imagesTimesPath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteHeader<SnapshotInfo>();
                    csv.NextRecord();
                    foreach (var pair in ImagesTimes)
                    {
                        csv.WriteRecord(pair.Value);
                        csv.NextRecord();
                    }
                }

                // Export DeltaTimes to CSV
                using (var writer = new StreamWriter(deltaTimesPath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteHeader<SnapshotInfo>();
                    csv.NextRecord();
                    foreach (var pair in DeltaTimes)
                    {
                        csv.WriteRecord(pair.Value);
                        csv.NextRecord();
                    }
                }

                MessageBox.Show("CSV files exported successfully.", "Information");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting CSV files: " + ex.Message, "Error");
            }
        }
    }
}

