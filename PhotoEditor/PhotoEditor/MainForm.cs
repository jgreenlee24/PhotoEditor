using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Change 16x16 (small) application icon (top-left corner of window)
            this.Icon = Icon.FromHandle(PhotoEditor.Properties.Resources.PhotoEditorIcon.GetHicon());
            aboutForm = new AboutForm(this);
            AddFolderIconToDirectoryTreeViewNodes();
            this.detailToolStripMenuItem.Click += viewToolStripMenuItem_Click;
            this.smallToolStripMenuItem.Click += viewToolStripMenuItem_Click;
            this.largeToolStripMenuItem.Click += viewToolStripMenuItem_Click;
        }

        private HashSet<TreeNode> nodesVisited = new HashSet<TreeNode>();

        private AboutForm aboutForm;

        private void AddFolderIconToDirectoryTreeViewNodes()
        {
            // create an ImageList to hold our new Folder Icon
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(16, 16);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.Images.Add(PhotoEditor.Properties.Resources.FolderIcon);

            treeView1.ImageList = imageList;
        }

        private string rootDirectory;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                toolStripProgressBar1.Visible = false;
            }

            ChangeRootFolder(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)));

        }

        private void ChangeRootFolder(DirectoryInfo rootDir)
        {
            nodesVisited.Clear();

            var rootNode = new TreeNode(rootDir.Name);
            rootNode.Tag = rootDir;

            foreach (var dir in rootDir.GetDirectories())
            {
                var currentNode = new TreeNode(dir.Name);
                currentNode.Tag = dir;

                foreach (var subDir in dir.GetDirectories())
                {
                    var childNode = new TreeNode(subDir.Name);
                    childNode.Tag = subDir;
                    currentNode.Nodes.Add(childNode);
                }

                rootNode.Nodes.Add(currentNode);
            }

            rootNode.Expand();
            nodesVisited.Add(rootNode);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(rootNode);
        }

        private void locateOnDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", ((DirectoryInfo)treeView1.SelectedNode.Tag).FullName);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (backgroundWorker1.IsBusy && backgroundWorker1.WorkerSupportsCancellation)
            {
                backgroundWorker1.CancelAsync();
                while (backgroundWorker1.IsBusy)
                {
                    Application.DoEvents();
                }
            }

            toolStripProgressBar1.Visible = true;
            backgroundWorker1.RunWorkerAsync(e.Node.Tag);

        }

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
            directoryNode.Tag = parentDir;

            // Loop for Sub-directories
            foreach (var directory in parentDir.GetDirectories())
            {
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            }

            return directoryNode;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            AsyncUtilities.InvokeIfRequired(this, (o) =>
           {
               toolStripStatusLabel1.Text = "Working...";
           });

            AsyncUtilities.InvokeIfRequired(this, (o) =>
                {
                    o.listView1.Items.Clear();
                    ImageList smallImageList = new ImageList();
                    ImageList largeImageList = new ImageList();
                    smallImageList.ImageSize = new Size(48, 48);
                    largeImageList.ImageSize = new Size(128, 128);
                    smallImageList.ColorDepth = ColorDepth.Depth32Bit;
                    largeImageList.ColorDepth = ColorDepth.Depth32Bit;
                    o.listView1.SmallImageList = smallImageList;
                    o.listView1.LargeImageList = largeImageList;
                });

            DirectoryInfo dir = (DirectoryInfo)e.Argument;

            var imageFilesInDir = new List<FileInfo>();

            foreach (var file in dir.GetFiles())
            {
                if (file.Extension.ToLower().Equals(".jpg") || file.Extension.ToLower().Equals(".jpeg"))
                {
                    imageFilesInDir.Add(file);
                }
            }

            foreach (var file in imageFilesInDir)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    var image = Image.FromFile(file.FullName);
                    var item = new ListViewItem(
                                new string[] { file.Name, file.LastWriteTime.ToString(), BytesToString(file.Length) },
                                imageFilesInDir.IndexOf(file));
                    item.Tag = file;

                    AddItemToListView(item, image);
                }
                catch
                {
                    // do nothing
                }



            }
        }

        private void AddItemToListView(ListViewItem item, Image image)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => AddItemToListView(item, image)));
            }
            else
            {
                listView1.SmallImageList.Images.Add(image);
                listView1.LargeImageList.Images.Add(image);
                listView1.Items.Add(item);
            }
        }

        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            toolStripProgressBar1.Visible = false;
        }

        private void selectRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog rootFolderDialog = new FolderBrowserDialog();
            rootFolderDialog.ShowNewFolderButton = false;
            DialogResult result = rootFolderDialog.ShowDialog(this);

            //As long as the user didn't cancel, switch the root folder.
            if (result == DialogResult.OK)
            {
                ChangeRootFolder(new DirectoryInfo(rootFolderDialog.SelectedPath));
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutForm.ShowDialog();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.GetNodeCount(false) != 0 && !nodesVisited.Contains(e.Node))
            {
                foreach (TreeNode dirNode in e.Node.Nodes)
                {
                    DirectoryInfo dir = dirNode.Tag as DirectoryInfo;

                    foreach (DirectoryInfo subdir in dir.GetDirectories())
                    {
                        TreeNode subdirNode = new TreeNode(subdir.Name);
                        subdirNode.Tag = subdir;
                        dirNode.Nodes.Add(subdirNode);
                    }
                }

                nodesVisited.Add(e.Node);
            }
        }        

        private void viewChildMenuItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem viewMenuItem = sender as ToolStripMenuItem;

            //Do nothing if the item was already selected.
            if (viewMenuItem.CheckState == CheckState.Checked)
            {
                return;
            }
            
            detailToolStripMenuItem.CheckState = CheckState.Unchecked;
            smallToolStripMenuItem.CheckState = CheckState.Unchecked;
            largeToolStripMenuItem.CheckState = CheckState.Unchecked;
            viewMenuItem.CheckState = CheckState.Checked;

            //Pass on the selected view mode to the imageListView.
            if (viewMenuItem == detailToolStripMenuItem)
            {
                listView1.View = View.Details;
            }
            else if (viewMenuItem == smallToolStripMenuItem)
            {
                listView1.View = View.SmallIcon;
            }
            else if (viewMenuItem == largeToolStripMenuItem)
            {
                listView1.View = View.LargeIcon;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Get the item targetted by the double click.
            ListViewItem selectedItem = listView1.GetItemAt(e.X, e.Y);

            //Make sure the user actually targetted an item, then open the editor.
            if (selectedItem != null)
            {
                FileInfo imageFile = selectedItem.Tag as FileInfo;
                Image image = Image.FromFile(imageFile.FullName);
                EditForm imageEditor = new EditForm(new Bitmap(image));
                DialogResult result = imageEditor.ShowDialog(this);

                //If the user hit the 'save' button, save the image to disk.
                if (result == DialogResult.OK)
                {
                    imageEditor.CurrentImage.Save(imageFile.FullName, ImageFormat.Jpeg);
                }
            }
        }




    }
}
