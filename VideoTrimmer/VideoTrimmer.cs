using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace VideoTrimmer
{
    public partial class VideoTrimmer : Form
    {
        public VideoTrimmer()
        {
            InitializeComponent();
            progressBar1.Maximum = 100;
            AppDataFolder();
            FFMPEGCheck();
        }

        public static string userprofilepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string VideoTrimmerFolder = userprofilepath + "\\AppData\\Local\\VideoTrimmer\\";

        private void OpenButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName.Trim().ToString();
            Console.WriteLine("Selected file: " + file);
            //openFileDialog1.OpenFile();
            SelectedFileDisplay.Text = "Selected File: " + file.ToLower();
            FileAttributes fileattributes = File.GetAttributes(file);

            //ffmpeg example - ffmpeg -i movie.mp4 -ss 00:00:03 -t 00:00:08 -async 1 cut.mp4

        }

        public void AppDataFolder()
        {
            progressBar1.Value = 10;
            SelectedFileDisplay.Text = "User Profile Path: " + userprofilepath;
            SelectedFileDisplay.Text = "App Path: " + userprofilepath + "\\AppData\\Local\\VideoTrimmer\\";
            progressBar1.Value = 20;
            Boolean videotrimmerfolderexists = Directory.Exists(VideoTrimmerFolder);
            if (!videotrimmerfolderexists)
            {
                progressBar1.Value = 30;
                SelectedFileDisplay.Text = "VideoTrimmer folder doesn't exist, creating";
                Directory.CreateDirectory(VideoTrimmerFolder);
                progressBar1.Value = 40;
                Directory.CreateDirectory(VideoTrimmerFolder + "\\inputfile\\");
                progressBar1.Value = 50;
                Directory.CreateDirectory(VideoTrimmerFolder + "\\clips\\");
                progressBar1.Value = 60;
                Directory.CreateDirectory(VideoTrimmerFolder + "\\output\\");
                progressBar1.Value = 70;
            }
        }

        public void FFMPEGCheck()
        {
            Boolean ffmpegfolderexists = Directory.Exists(VideoTrimmerFolder + "\\ffmpeg");
            if (!ffmpegfolderexists)
            {
                SelectedFileDisplay.Text = "FFMpeg folder doesn't exist, creating";
                Directory.CreateDirectory(VideoTrimmerFolder + "\\ffmpeg\\");
                progressBar1.Value = 80;
            }

            Boolean ffmpegexists = File.Exists(VideoTrimmerFolder + "\\ffmpeg\\ffmpeg.exe");
            if (!ffmpegexists)
            {
                SelectedFileDisplay.Text = "FFMpeg not installed, downloading...";
                File.Copy("ffmpeg.exe", VideoTrimmerFolder + "\\ffmpeg\\ffmpeg.exe");
                progressBar1.Value = 90;
            }

            ffmpegfolderexists = Directory.Exists(VideoTrimmerFolder + "\\ffmpeg");
            ffmpegexists = File.Exists(VideoTrimmerFolder + "\\ffmpeg\\ffmpeg.exe");

            if (ffmpegexists && ffmpegfolderexists)
            {
                progressBar1.Value = 100;
                SelectedFileDisplay.Text = "Ready";
            }
        }
    }
}
