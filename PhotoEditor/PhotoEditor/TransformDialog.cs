using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEditor
{
    public partial class TransformDialog : Form
    {
        public TransformDialog(int progressMaximum)
        {
            InitializeComponent();
            progressBar.Maximum = progressMaximum;
        }

        public event TransformCancelledHandler TransformationCancelled;

        public delegate void TransformCancelledHandler();

        public void SetProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetProgress(progress)));
            }
            else
            {
                progressBar.Value = progress;
            }
        }

        public void ResetProgress()
        {
            progressBar.Value = 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (TransformationCancelled != null)
            {
                TransformationCancelled();
            }
        }
    }
}
