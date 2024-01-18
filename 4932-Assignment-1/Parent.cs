namespace ImageMorpher
{
    public partial class Parent : Form
    {
        private ImageBase destination;
        private ImageBase source;

        public Parent()
        {
            InitializeComponent();

            source = new ImageBase(ImageBaseType.SOURCE);
            source.MdiParent = this;
            source.Show();

            destination = new ImageBase(ImageBaseType.DESTINATION);
            destination.MdiParent = this;
            destination.Show();
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

        private void menu_strip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
