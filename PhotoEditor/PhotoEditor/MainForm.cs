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

namespace PhotoEditor
{
    public partial class MainForm : Form
    {
        // private variables
        private string rootDirectory;
        private FolderBrowserDialog folderBrowserDialog1;

        // constructor
        public MainForm()
        {
            InitializeComponent();
            folderBrowserDialog1 = new FolderBrowserDialog();
        }

        // main form load event
        private void Form1_Load(object sender, EventArgs e)
        {
            rootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ListDirectory(this.treeView1, rootDirectory);
        }
        #region View Event Handlers
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView1.Clear();
            // TODO: display progress bar while loop occurs

            // append selected path to current path
            string path = rootDirectory;
            if (e.Node.Level != 0)
            {
                string nodePath = e.Node.FullPath;
                int i = nodePath.IndexOf("\\");
                path += nodePath.Substring(i, nodePath.Length - i);
            }

            // initialize directory and image list
            var directory = new DirectoryInfo(path);
            ImageList images = new ImageList();
            images.ImageSize = new Size(32, 32);
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = images;

            // Load Images for Selected Directory (in a separate thread)
            int index = 0;
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension == ".jpg")
                {
                    images.Images.Add(file.Name, Image.FromFile(file.FullName));
                    listView1.Items.Add(new ListViewItem(file.Name, index));
                    index++;
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

        }
        #endregion
    }
}