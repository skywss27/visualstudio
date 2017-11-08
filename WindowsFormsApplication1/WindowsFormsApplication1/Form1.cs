using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ComputerVision : Form
    {
        public ComputerVision()
        {
            InitializeComponent();
        }

        private void ChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "图片|*.jpg";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FilePath.Text = openFileDialog.FileName;
            }
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FilePath.Text))
                MessageBox.Show("file path could not empty!");
            else 
            {
                this.pictureBox1.Image = Image.FromFile(this.FilePath.Text.Trim(),false);

                Mat original_ptr = CvInvoke.Imread(this.FilePath.Text.Trim(), ImreadModes.Color);

                if (original_ptr == null)
                {
                    MessageBox.Show("没有此图像");
                    return;
                }
                Mat gray_ptr = new Mat(original_ptr.Size, DepthType.Cv8U,1);
                //Mat gray_ptr = CvInvoke.cvCreateMat(original_ptr.Rows, original_ptr.Cols,Emgu.CV.CvEnum.IplDepth.IplDepth_8U );
                CvInvoke.CvtColor(original_ptr, gray_ptr, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

               
                this.pictureBox2.Image = gray_ptr.ToImage<Gray, Single>().ToBitmap();

                Mat thred_ptr = new Mat(gray_ptr.Size, DepthType.Cv8U, 1);
                CvInvoke.AdaptiveThreshold(gray_ptr, thred_ptr, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary,23,5);
                this.pictureBox3.Image = thred_ptr.ToImage<Gray, Single>().ToBitmap();

            }
        }
    }
    
}
