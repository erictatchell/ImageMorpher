using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;

namespace ImageMorpher
{
    public partial class ImageBase : Form
    {
        private List<Line> lines;
        protected Line? currentLine;
        protected Line? selectedLine;
        protected List<Bitmap> frames;
        protected Bitmap? backgroundImage;
        protected int type;
        protected bool deleting;
        public ImageBase(int type)
        {
            InitializeComponent();
            lines = new List<Line>();
            currentLine = null;
            selectedLine = null;
            backgroundImage = null;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            deleting = false;

            this.type = type;
            Text = TypeToString();
        }

        public void SetImage(Bitmap image, Bitmap loaded )
        {
            backgroundImage = image;
            //ResizeBitmap(loaded, ClientSize.Width, ClientSize.Height);
            Refresh();
        }

        private string TypeToString()
        {
            if (type != ImageBaseType.TRANSITION)
            {
                return type == ImageBaseType.SOURCE ? ImageBaseType.SOURCE_STR : ImageBaseType.DESTINATION_STR;
            }
            else return ImageBaseType.TRANSITION_STR;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (backgroundImage != null)
            {
                e.Graphics.DrawImage(backgroundImage, 0, 0, ClientSize.Width, ClientSize.Height);
            }

            foreach (Line line in lines)
            {
                line.Draw(e);
            }

            if (currentLine != null)
            {
                currentLine.Draw(e);
            }
        }

        private void ImageBase_MouseDown(object sender, MouseEventArgs e)
        {
            if (type != ImageBaseType.TRANSITION)
            {
                bool hoveringOverLine = false;

                foreach (Line line in lines)
                {
                    if (line.GetUserIntention(e) != Intention.CREATING_NEW_LINE)
                    {
                        hoveringOverLine = true;
                        selectedLine = line;
                        if (e.Button == MouseButtons.Right)
                        {
                            deleting = true;
                        }
                    }
                }

                if (!hoveringOverLine)
                {
                    if (backgroundImage != null && e.Button == MouseButtons.Left)
                    {
                        currentLine = new Line(e.Location.X, e.Location.Y);
                    }
                }
            }
            else return;
        }



        private void ImageBase_MouseMove(object sender, MouseEventArgs e)
        {
            // creating new line
            if (currentLine != null)
            {
                currentLine.UpdateEndPoints(e.Location.X, e.Location.Y);
                Refresh();
            }

            // resizing existing line
            else if (selectedLine != null)
            {
                selectedLine.Resize(e);
                Refresh();
            }
        }



        private void ImageBase_MouseUp(object sender, MouseEventArgs e)
        {
            if (backgroundImage != null && e.Button == MouseButtons.Left && currentLine != null && !deleting)
            {
                lines.Add(currentLine);
                ((Parent)MdiParent).Reflect(currentLine, this.type, Intention.CREATING_NEW_LINE);
                Debug.WriteLine("P Source: (" + currentLine.StartX + ", " + currentLine.StartY + ")");
                Debug.WriteLine("Q Source: (" + currentLine.EndX + ", " + currentLine.EndY + ")");
                currentLine = null;
                selectedLine = null;
                Refresh();
            }
            else if (selectedLine != null && !deleting)
            {
                Debug.WriteLine("P Destination: (" + selectedLine.StartX + ", " + selectedLine.StartY + ")");
                Debug.WriteLine("Q Destination: (" + selectedLine.EndX + ", " + selectedLine.EndY + ")");
                selectedLine = null;
                
                Refresh();
            }
        }

        public Line getLine(int lineId)
        {
            return lines.Find(line => line.getId() == lineId);
        }


        public void AddLines(Line line)
        {
            lines.Add(line);
        }
        private Bitmap ResizeBitmap(Bitmap originalBitmap, int width, int height)
        {
            Bitmap resizedBitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedBitmap))
            {
                g.DrawImage(originalBitmap, new Rectangle(0, 0, width, height));
            }
            return resizedBitmap;
        }

        private void ImageBase_Load(object sender, EventArgs e)
        {

        }

        public void Interpolate()
        {

        }

        public void Morph(List<Line> sourceLines)
        {
            Bitmap transition = new Bitmap(backgroundImage.Width, backgroundImage.Height);
            List<Vector2> sourcePoints = new List<Vector2>();
            List<Color> colors = new List<Color>();
            for (int y = 0; y < backgroundImage.Height; ++y)
            {
                for (int x = 0; x < backgroundImage.Width; ++x)
                {
                    for (int k = 0; k < lines.Count; k++)
                    {
                        Line line = lines[k];

                        Vector2 PQ = new Vector2(line.EndX - line.StartX, line.EndY - line.StartY);
                        Vector2 n = new Vector2(-PQ.Y, PQ.X);
                        Vector2 XP = new Vector2(line.StartX - x, line.StartY - y);
                        Vector2 PX = new Vector2(x - line.StartX, y - line.StartY);

                        float d = Vector2.Dot(XP, n) / n.Length();

                        float f = Vector2.Dot(PX, PQ) / PQ.Length();
                        
                        float fl = f / PQ.Length();

                        Line sourceLine = sourceLines[k];

                        Vector2 PPrime = new Vector2(sourceLine.StartX, sourceLine.StartY);
                        Vector2 NPrime = new Vector2(-1 * (sourceLine.EndY - sourceLine.StartY), sourceLine.EndX - sourceLine.StartX);
                        Vector2 PQPrime = new Vector2(sourceLine.EndX - sourceLine.StartX, sourceLine.EndY - sourceLine.StartY);

                        Vector2 XPrime = PPrime + Vector2.Multiply(fl, PQPrime) - Vector2.Multiply(d, Vector2.Divide(NPrime, NPrime.Length()));

                        Vector2 v = validatePixel(XPrime, backgroundImage.Width, backgroundImage.Height);
                        sourcePoints.Add(v);
                        transition.SetPixel(x, y, backgroundImage.GetPixel((int)v.X, (int)v.Y));
                    }
                }
            }
            ((Parent)MdiParent).UpdateTransition(transition, transition);
        }

        private Vector2 validatePixel(Vector2 coord, int width, int height)
        {
            if (coord.X < 0)
            {
                coord.X = 0;
            }
            else if (coord.X >= width)
            {
                coord.X = width - 1;
            }
            if (coord.Y < 0)
            {
                coord.Y = 0;
            }
            else if (coord.Y >= height)
            {
                coord.Y = height - 1;
            }
            return coord;
        }

        public void ReverseMap(Bitmap transition, List<Vector2> sourcePoints, List<Color> colors)
        {
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                // Ensure that the coordinates are within bounds
                float x = sourcePoints[i].X;
                float y = sourcePoints[i].Y;

                // Set the pixel color
                
            }
        }


        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All files|*.*";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap loadedBitmap = new Bitmap(openFileDialog.FileName);

                    backgroundImage = ResizeBitmap(loadedBitmap, ClientSize.Width, ClientSize.Height);
                    if (type == ImageBaseType.SOURCE) ((Parent)MdiParent).UpdateTransition(backgroundImage, loadedBitmap);
                    Refresh();
                }
            }
        }

        public void DeleteLines(Line line)
        {
            lines.Remove(line);
            deleting = false;
            Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedLine != null)
            {
                ((Parent)MdiParent).Reflect(selectedLine, type, Intention.DELETING);
                Refresh();
            }
        }

        public List<Line> GetLines()
        {
            return lines;
        }
    }
}
