using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageMorpher
{
    public partial class Controller : Form
    {
        private List<Bitmap> frames;
        private ImageBase transition;
        private int count = 0;
        public Controller(List<Bitmap> frames, ImageBase transition)
        {
            InitializeComponent();
            this.frames = frames;
            this.transition = transition;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transition.SetImage(frames[count]);
            count++;
        }

        private void Controller_Load(object sender, EventArgs e)
        {

        }
    }
}
