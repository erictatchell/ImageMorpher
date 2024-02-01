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

            controller = new Controller(frames, transition);
            controller.MdiParent = this;
            controller.Show();
            controller.Location = new Point(0, Math.Max(source.Bottom, Math.Max(destination.Bottom, transition.Bottom)) + offsetY);
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
        public void GenerateIntermediateFrames(List<Vector2> dest_points, List<Vector2> source_points, Bitmap final_image, Bitmap destination_image, List<Color> dest_colors, List<Color> source_colors)
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
