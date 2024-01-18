namespace ImageMorpher
{
    public partial class ImageBase : Form
    {
        private List<Line> lines;
        protected Line? currentLine;
        protected Line? selectedLine;
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

        private string TypeToString()
        {
            return type == ImageBaseType.SOURCE ? ImageBaseType.SOURCE_STR : ImageBaseType.DESTINATION_STR;
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
                currentLine = null;
                selectedLine = null;
                Refresh();
            }
            else if (selectedLine != null && !deleting)
            {
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
    }
}
