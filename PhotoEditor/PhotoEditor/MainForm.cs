using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Authors: Justin Greenlee & Adam Hammond
// Date: September 26th, 2015
// Class: GUI
// Assignment: Photo Editor

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
        // private variables
        private string rootDirectory;
        private string currentPath;
        private FolderBrowserDialog folderBrowserDialog1;
        private OpenFileDialog openFileDialog1;
        private AboutForm aboutForm1;
        private ImageList images;
        private bool detailsView = false;

        #region Constructor & Load
        public MainForm()
        {
            InitializeComponent();
            folderBrowserDialog1 = new FolderBrowserDialog();
            openFileDialog1 = new OpenFileDialog();
            aboutForm1 = new AboutForm();
            images = new ImageList();
            images.ImageSize = new Size(32, 32);
        }

        // main form load event
        private void Form1_Load(object sender, EventArgs e)
        {
            rootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ListDirectory(this.treeView1, rootDirectory);
        }
        #endregion
        #region TreeView Event Handlers
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView1.Clear();

            // append selected path to current path
            string path = currentPath = rootDirectory;
            if (e.Node.Level != 0)
            {
                string nodePath = e.Node.FullPath;
                int i = nodePath.IndexOf("\\");
                path += nodePath.Substring(i, nodePath.Length - i);
                currentPath = path;
            }

            // initialize directory and image list
            listView1.View = View.LargeIcon;
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            else
            {
                if (backgroundWorker1.WorkerSupportsCancellation)
                {
                    backgroundWorker1.CancelAsync(); // cancels and restart for new selection
                }
            }
        }
        #endregion
        #region Directory Functions
        // Recursively fill the treeView with new DirectoryInfo Nodes
        private void ListDirectory(TreeView treeView, String path)
        {
            treeView.Nodes.Clear();
            var rootInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootInfo));
        }

        // Creates TreeNode, Adds Files, Loops for Sub-directories
        private static TreeNode CreateDirectoryNode(DirectoryInfo parentDir)
        {
            var directoryNode = new TreeNode(parentDir.Name);
            
            // Loop for Sub-directories
            foreach (var directory in parentDir.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));

            return directoryNode;
        }
        #endregion
        #region Menu Event Handlers
        private void selectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                rootDirectory = folderBrowserDialog1.SelectedPath;
                ListDirectory(treeView1, rootDirectory);
            }
        }
        private void locateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
            openFileDialog1.FileName = null;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Open Photo Editor for the selected file
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            detailsView = true;
            listView1.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("Date", 150);
            listView1.Columns.Add("Size", 100);
            listView1.Dock = DockStyle.Fill;
            images.ImageSize = new Size(32, 32);
            listView1.SmallImageList = images;

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            else backgroundWorker1.CancelAsync();
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            detailsView = false;
            listView1.Clear();
            listView1.View = View.SmallIcon;
            images.ImageSize = new Size(32, 32);
            listView1.SmallImageList = images;

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            else backgroundWorker1.CancelAsync();
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            detailsView = false;
            listView1.Clear();
            listView1.View = View.LargeIcon;
            images.ImageSize = new Size(64, 64);
            listView1.LargeImageList = images;

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            else backgroundWorker1.CancelAsync();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the AboutForm.
            DialogResult result = aboutForm1.ShowDialog();
        }
        #endregion
        #region Background Worker Event Handling
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar1.Value != progressBar1.Maximum)
                progressBar1.Value++;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                listView1.Clear(); // clear and restart for new selection
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var directory = new DirectoryInfo(currentPath);
            SetImageList(images);

            // Initialize Progress Bar, Count JPG Images in Directory
            int imageCount = 0;
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension == ".jpg") imageCount++;
            }
            InitializeProgressBar(imageCount);

            // Loop through files in currently selected directory
            int index = 0;
            foreach (var file in directory.GetFiles())
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                if (file.Extension == ".jpg")
                {
                    images.Images.Add(file.Name, Image.FromFile(file.FullName));
                    if (!detailsView)
                    {
                        var item = new ListViewItem(file.Name, index);
                        AppendItemToListView(item);
                    } else
                    {
                        var details = new string[]  { file.Name,
                                                      file.LastWriteTime.ToString(),
                                                      file.Length.ToString() };
                        var item = new ListViewItem(details, index);
                        AppendItemToListView(item);
                    }
                    worker.ReportProgress(1);
                    index++;
                }
            }
        }

        #endregion
        #region Delegate Methods (UI stuff)
        private void AppendItemToListView(ListViewItem item)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => AppendItemToListView(item)));
            else
                listView1.Items.Add(item);
        }
        public void SetImageList(ImageList images)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => SetImageList(images)));
            else
            {
                images.Images.Clear();
                listView1.SmallImageList = images;
                listView1.LargeImageList = images;
            }    
        }
        public void InitializeProgressBar(int max)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => InitializeProgressBar(max)));
            else
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = max;
                progressBar1.Visible = true;
            }
        }
        #endregion

    }
}