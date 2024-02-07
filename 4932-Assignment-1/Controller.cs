using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ImageMorpher
{
    public partial class Controller : Form
    {
        private ImageBase transition;
        private int count = 0;
        private int count2 = 0;
        private List<Bitmap> frames;
        public Controller(List<Bitmap> frames, ImageBase transition)
        {
            InitializeComponent();
            this.transition = transition;
            this.frames = frames;
            Location = new Point(ClientSize.Width - this.Width, ClientSize.Height - this.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (count2 != frames.Count - 1)
            {
                count2++;
                transition.SetImage(frames[count2]);
            }
        }

        private void Cycle()
        {

            if (count + 1 == ((Parent)MdiParent).GetFrames().Count)
            {
                count = 0;
                timer1.Stop();
            }
            else
            {
                count++;
                transition.SetImage(((Parent)MdiParent).GetFrames()[count]);
            }

        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 100; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cycle();
        }


        private void Controller_Load(object sender, EventArgs e)
        {
            label1.Text = ((Parent)MdiParent).GetThreads() + " threads: " + ((Parent)MdiParent).GetTime() + " seconds";
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if (count2 != 0)
            {
                count2--;
                transition.SetImage(frames[count2]);
            }
            else return;
        }

        private void Play_Click(object sender, EventArgs e)
        {
            if (((Parent)MdiParent).GetFrames().Count == 0)
            {
                return;
            }
            else InitTimer();
        }
    }
}
