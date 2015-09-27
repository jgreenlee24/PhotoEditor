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
    public partial class EditForm : Form
    {
        public EditForm(Bitmap image)
        {
            InitializeComponent();

            this.Icon = Icon.FromHandle(PhotoEditor.Properties.Resources.PhotoEditorIcon.GetHicon());

            pictureBox.Image = originalImage = CurrentImage = image;


            progressDialog = new TransformDialog(image.Height * image.Width);
            progressDialog.TransformationCancelled += TransformCancelled;
        }

        //This image stores the original, unaltered image.
        private Bitmap originalImage;

        //This image stores the current working copy.
        public Bitmap CurrentImage { get; private set; }

        //Use this in the ValueChanged listener to determine whether to
        //update immediately, or wait for a mouse release.
        private Boolean draggingBrightnessBar = false;

        //This is used to determine if a mouse drag actually moved the slider.
        private int originalBrightnessValue;

        //This stores the information needed by the BackgroundWorker to transform the image.
        private struct Transformation
        {
            public TransformationType type;
            public object argument;
        }

        private enum TransformationType { BRIGHTNESS_CHANGE, TINTING, INVERSION };

        private TransformDialog progressDialog;

        private void TransformImage(Transformation transform)
        {
            progressDialog.ResetProgress();
            progressDialog.Show();
            this.Enabled = false;
            imageTransformer.RunWorkerAsync(transform);
        }

        private void TransformCancelled()
        {
            imageTransformer.CancelAsync();
        }

        private double getNormalizedBrightnessLevel()
        {
            double range = brightnessBar.Maximum - brightnessBar.Minimum;
            double normalizedValue = 2 * ((double)(brightnessBar.Value - brightnessBar.Minimum)) / range - 1;
            return normalizedValue;
        }

        private void BrightnessBarChanged()
        {
            Transformation transform = new Transformation();
            transform.type = TransformationType.BRIGHTNESS_CHANGE;
            transform.argument = getNormalizedBrightnessLevel();
            TransformImage(transform);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (!draggingBrightnessBar)
            {
                BrightnessBarChanged();
            }
        }

        private void brightnessBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggingBrightnessBar = true;
                originalBrightnessValue = brightnessBar.Value;
            }
        }

        private void brightnessBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (draggingBrightnessBar && brightnessBar.Value != originalBrightnessValue)
            {
                draggingBrightnessBar = false;
                BrightnessBarChanged();
            }
        }

        private void imageTransformer_DoWork(object sender, DoWorkEventArgs e)
        {
            Transformation transform = (Transformation)e.Argument;
            switch (transform.type)
            {
                case TransformationType.BRIGHTNESS_CHANGE:
                    TransformBrightness((double)transform.argument);
                    break;
                case TransformationType.TINTING:
                    TintImage((Color)transform.argument);
                    break;
                case TransformationType.INVERSION:
                    InvertImage();
                    break;
            }
            if (imageTransformer.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void TransformBrightness(double brightnessFactor)
        {
            Bitmap workingImage = CurrentImage.Clone() as Bitmap;

            for (int i = 0; i < workingImage.Width; i++)
            {
                for (int j = 0; j < workingImage.Height; j++)
                {
                    if (imageTransformer.CancellationPending)
                    {
                        return;
                    }

                    Color oldColor = workingImage.GetPixel(i, j);
                    int newRed, newGreen, newBlue;
                    if (brightnessFactor > 0)
                    {
                        //Increase Brightness
                        newRed = (int)((255 - oldColor.R) * brightnessFactor) + oldColor.R;
                        newGreen = (int)((255 - oldColor.G) * brightnessFactor) + oldColor.G;
                        newBlue = (int)((255 - oldColor.B) * brightnessFactor) + oldColor.B;
                    }
                    else
                    {
                        //Decrease Brightness
                        newRed = (int)(oldColor.R * (brightnessFactor + 1));
                        newGreen = (int)(oldColor.G * (brightnessFactor + 1));
                        newBlue = (int)(oldColor.B * (brightnessFactor + 1));
                    }
                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    workingImage.SetPixel(i, j, newColor);
                    progressDialog.SetProgress(i * workingImage.Height + j + 1);
                }
            }

            CurrentImage = workingImage;
        }

        private void TintImage(Color tintColor)
        {
            Bitmap workingImage = CurrentImage.Clone() as Bitmap;

            for (int i = 0; i < workingImage.Width; i++)
            {
                for (int j = 0; j < workingImage.Height; j++)
                {
                    if (imageTransformer.CancellationPending)
                    {
                        return;
                    }

                    Color oldColor = workingImage.GetPixel(i, j);

                    double scaleFactor = ((double)(oldColor.R + oldColor.G + oldColor.B)) / (3 * 255);
                    int newRed = (int)(scaleFactor * tintColor.R);
                    int newGreen = (int)(scaleFactor * tintColor.G);
                    int newBlue = (int)(scaleFactor * tintColor.B);

                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    workingImage.SetPixel(i, j, newColor);
                    imageTransformer.ReportProgress(i * workingImage.Height + j + 1);
                }
            }

            CurrentImage = workingImage;
        }

        private void InvertImage()
        {
            Bitmap workingImage = CurrentImage.Clone() as Bitmap;

            for (int i = 0; i < workingImage.Width; i++)
            {
                for (int j = 0; j < workingImage.Height; j++)
                {
                    if (imageTransformer.CancellationPending)
                    {
                        return;
                    }

                    Color oldColor = workingImage.GetPixel(i, j);

                    int newRed = 255 - oldColor.R;
                    int newGreen = 255 - oldColor.G;
                    int newBlue = 255 - oldColor.B;

                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    workingImage.SetPixel(i, j, newColor);
                    progressDialog.SetProgress(i * workingImage.Height + j + 1);
                }
            }

            CurrentImage = workingImage;
        }

        private void imageTransformer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressDialog.SetProgress(e.ProgressPercentage);
        }

        private void imageTransformer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            progressDialog.Hide();
            pictureBox.Image = CurrentImage;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            DialogResult result = colorPicker.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Transformation transform = new Transformation();
                transform.type = TransformationType.TINTING;
                transform.argument = colorPicker.Color;
                TransformImage(transform);
            }
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            Transformation transform = new Transformation();
            transform.type = TransformationType.INVERSION;
            TransformImage(transform);
        }

        
    }
}
