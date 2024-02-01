using System.Numerics;

namespace ImageMorpher
{
    public partial class Parent : Form
    {
        private ImageBase destination;
        private ImageBase transition;
        private ImageBase source;
        private Controller controller;
        private List<Bitmap> frames;
        private int num_frames;
        public Parent()
        {
            InitializeComponent();
            frames = new List<Bitmap>();
            num_frames = 5;
            frames5.Checked = true;

            source = new ImageBase(ImageBaseType.SOURCE);
            source.MdiParent = this;
            source.Show();

            destination = new ImageBase(ImageBaseType.DESTINATION);
            destination.MdiParent = this;
            destination.Show();

            transition = new ImageBase(ImageBaseType.TRANSITION);
            transition.MdiParent = this;
            transition.Show();
        }

        public List<Bitmap> GetFrames()
        {
            return frames;
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

        public void UpdateTransition(int frame)
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
        public void GenerateIntermediateFrames(List<Point> dest_points, List<Point> source_points, List<Color> dest_colors, List<Color> source_colors)
        {



            for (int j = 0; j < num_frames; j++)
            {
                Bitmap frame = new Bitmap(destination.GetImage().Width, destination.GetImage().Height);
                for (int y = 0; y < frame.Height; y++)
                {
                    for (int x = 0; x < frame.Width; x++)
                    {
                        float diff_x = dest_points[x].X - source_points[x].X;
                        float diff_y = dest_points[x].Y - source_points[x].Y;
                        frame.SetPixel(x + diff_x, )
                    }
                }
                frames.Add(frame);
            }
        }

        private void beginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            destination.Morph(source.GetLines(), num_frames);
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
    }
}
