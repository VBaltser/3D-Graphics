using System;
using System.Drawing;
using System.Windows.Forms;

namespace _3D_Graph
{
    public partial class Form1 : Form
    {
        Lab app;
        Bitmap bitmap = null;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        
        
        public Form1()
        {
            InitializeComponent();
            app = Lab.CreateBuilder().SetWindowSize(700, 400).Build();
        }

        private void createGraph_Click(object sender, EventArgs e)
        {
            if (bitmap == null)
            {
                MessageBox.Show("Изображение не загружено!");
                return;
            }

            app.Run(bitmap);
        }

        private void CloseGraph_Click(object sender, EventArgs e)
        {
            app.ShutDown();
        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.Cancel)
            return;
            
            string filename = openFileDialog.FileName;
            bitmap = new Bitmap(Image.FromFile(filename));

            pictureBox1.Image = bitmap;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

    }
}
