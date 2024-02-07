using System.Diagnostics;
using System.Numerics;

namespace ImageMorpher
{
    public partial class Parent : Form
    {
        private ImageBase destination;
        private ImageBase transition;
        private ImageBase source;
        private Controller controller;
        private List<Bitmap> forwardframes;
        private List<Bitmap> backwardframes;
        private List<Bitmap> frames;
        private int num_frames;
        private int num_threads;
        private double time;
        private double time2;
        public Parent()
        {
            InitializeComponent();
            forwardframes = new List<Bitmap>();
            backwardframes = new List<Bitmap>();
            frames = new List<Bitmap>();
            num_frames = 5;
            num_threads = 2;
            t2.Checked = true;
            frames5.Checked = true;

            int offsetX = 10;
            int offsetY = 10;

            source = new ImageBase(ImageBaseType.SOURCE, 0);
            source.MdiParent = this;
            source.Show();
            source.Location = new Point(0, 0);

            destination = new ImageBase(ImageBaseType.DESTINATION, 1);
            destination.MdiParent = this;
            destination.Show();
            destination.Location = new Point(source.Right + offsetX, 0);

            transition = new ImageBase(ImageBaseType.TRANSITION, 2);
            transition.MdiParent = this;
            transition.Show();
            transition.Location = new Point(destination.Right + offsetX, 0);


        }


        public List<Bitmap> GetFrames()
        {
            return frames;
        }

        public List<Bitmap> GetForwardFrames()
        {
            return forwardframes;
        }

        public List<Bitmap> GetBackwardFrames()
        {
            return backwardframes;
        }

        public List<Bitmap> crossDissolve(List<Bitmap> a, List<Bitmap> b)
        {
            List<Bitmap> result = new List<Bitmap>();
            for (int i = 0; i < a.Count; i++)
            {
                result.Add(new Bitmap(a[i].Width, a[i].Height));
                for (int x = 0; x < a[i].Width; x++)
                {
                    for (int y = 0; y < a[i].Height; y++)
                    {
                        double t = (double)(i) / (a.Count - 1);
                        Color colorA = a[i].GetPixel(x, y);
                        Color colorB = b[i].GetPixel(x, y);
                        int newR = (int)Math.Round((1 - t) * colorA.R + t * colorB.R);
                        int newG = (int)Math.Round((1 - t) * colorA.G + t * colorB.G);
                        int newB = (int)Math.Round((1 - t) * colorA.B + t * colorB.B);
                        int newA = (int)Math.Round((1 - t) * colorA.A + t * colorB.A);
                        result[i].SetPixel(x, y, Color.FromArgb(newA, newR, newG, newB));
                    }
                }
            }
            return result;
        }

        public void Reflect(Line line, int origin, int intention)
        {
            if (intention == Intention.DELETING)
            {
                if (origin == ImageBaseType.SOURCE)
                {
                    Line destinationLine = destination.getLine(line.getId());
                    destination.DeleteLines(destinationLine);
                    source.DeleteLines(line);
                }
                else if (origin == ImageBaseType.DESTINATION)
                {
                    Line sourceLine = source.getLine(line.getId());
                    source.DeleteLines(sourceLine);
                    destination.DeleteLines(line);
                }
                return;
            }

            Line copiedLine = new Line(line.StartX, line.StartY, line.EndX, line.EndY);

            if (origin == ImageBaseType.SOURCE)
            {
                destination.AddLines(copiedLine);
            }
            else if (origin == ImageBaseType.DESTINATION)
            {
                source.AddLines(copiedLine);
            }
            source.Invalidate();
            destination.Invalidate();
        }


        private void Main_Load(object sender, EventArgs e)
        {
        }

        public void UpdateTransition(List<Bitmap> frames, int frame)
        {
            if (transition == null) return;
            transition.SetImage(frames[frame]);

        }

        public void TransitionInit(Bitmap img)
        {
            transition.SetImage(img);
        }

        private void menu_strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public List<Bitmap> GenerateIntermediateFrames(List<Bitmap> frames, List<Vector2> dest_points, List<Vector2> source_points, Bitmap final_image, Bitmap destination_image, List<Color> dest_colors, List<Color> source_colors)
        {
            List<Vector2> new_dest_points = new List<Vector2>(dest_points);
            frames.Add(destination_image);
            for (int frameIndex = 0; frameIndex < num_frames - 1; frameIndex++)
            {
                Bitmap frame = new Bitmap(destination_image.Width, destination_image.Height);

                for (int i = 0; i < dest_points.Count; i++)
                {
                    float diff_X = (dest_points[i].X - source_points[i].X) / num_frames;
                    float diff_Y = (dest_points[i].Y - source_points[i].Y) / num_frames;
                    Vector2 diffVector = new Vector2(diff_X, diff_Y);

                    new_dest_points[i] = Vector2.Subtract(new_dest_points[i], diffVector);
                    new_dest_points[i] = destination.validatePixel(new_dest_points[i], frame.Width, frame.Height);

                    frame.SetPixel((int)dest_points[i].X, (int)dest_points[i].Y, destination_image.GetPixel((int)new_dest_points[i].X, (int)new_dest_points[i].Y));
                }

                frames.Add(frame);
            }
            frames.Add(final_image);
            return frames;
        }

        public int GetThreads()
        {
            return num_threads;
        }

        public double GetThreadedTime()
        {
            return time;
        }

        public double Get1ThreadedTime()
        {
            return time2;
        }

        private System.Timers.Timer clock;

        private void beginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            List<Bitmap> dummy = new List<Bitmap>();

            sw.Start();
            dummy = destination.Morph(dummy, source.GetLines(), num_frames, 1);
            dummy = source.Morph(dummy, destination.GetLines(), num_frames, 1);
            sw.Stop();
            time2 = sw.Elapsed.TotalSeconds;

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            backwardframes = destination.Morph(backwardframes, source.GetLines(), num_frames, num_threads);
            forwardframes = source.Morph(forwardframes, destination.GetLines(), num_frames, num_threads);
            sw2.Stop();
            time = sw2.Elapsed.TotalSeconds;

            

            backwardframes.Reverse();
            frames = crossDissolve(forwardframes, backwardframes);

            controller = new Controller(frames, transition);
            controller.MdiParent = this;
            controller.Show();
            controller.Location = new Point(0, Math.Max(source.Bottom, Math.Max(destination.Bottom, transition.Bottom)) + 10);
        }

        private void frames5_Click(object sender, EventArgs e)
        {
            frames5.Checked = true;
            frames10.Checked = false;
            num_frames = 5;
        }

        private void frames10_Click(object sender, EventArgs e)
        {
            frames5.Checked = false;
            frames10.Checked = true;
            num_frames = 10;
        }

        private void t1_Click(object sender, EventArgs e)
        {
            t1.Checked = true;
            t2.Checked = false;
            t3.Checked = false;
            t4.Checked = false;
            t8.Checked = false;
            num_threads = 1;
        }

        private void t2_Click(object sender, EventArgs e)
        {
            t1.Checked = false;
            t2.Checked = true;
            t3.Checked = false;
            t4.Checked = false;
            t8.Checked = false;
            num_threads = 2;
        }

        private void t3_Click(object sender, EventArgs e)
        {
            t1.Checked = false;
            t2.Checked = false;
            t3.Checked = true;
            t4.Checked = false;
            t8.Checked = false;
            num_threads = 3;
        }

        private void t4_Click(object sender, EventArgs e)
        {
            t1.Checked = false;
            t2.Checked = false;
            t3.Checked = false;
            t4.Checked = true;
            t8.Checked = false;
            num_threads = 4;
        }

        private void t8_Click(object sender, EventArgs e)
        {
            t1.Checked = false;
            t2.Checked = false;
            t3.Checked = false;
            t4.Checked = false;
            t8.Checked = true;
            num_threads = 8;
        }
    }
}
