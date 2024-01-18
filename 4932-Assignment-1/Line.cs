namespace ImageMorpher
{
    public class Line
    {
        private int startX;
        private int startY;
        private int endX;
        private int endY;
        private int id;
        private static int counter = 0;
        private bool moving;
        private bool resizingStart;
        private bool resizingEnd;
        private Pen pen;
        private Pen pen_1;

        public Line(int startX, int startY, int endX = 0, int endY = 0)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
            id = counter / 2;
            counter++;
            pen = new Pen(Color.Black, 3F);
            pen_1 = new Pen(Color.White, 2F);
        }

        public void UpdateEndPoints(int endX, int endY)
        {
            this.endX = endX;
            this.endY = endY;
        }

        public void UpdateStartPoints(int startX, int startY)
        {
            this.startX = startX;
            this.startY = startY;
        }



        public void Draw(PaintEventArgs e)
        {
            Point p1 = new Point(startX, startY);
            Point p2 = new Point(endX, endY);

            e.Graphics.DrawLine(pen, p1, p2);

            e.Graphics.DrawCircle(pen_1, startX, startY, 3.5F);
            e.Graphics.DrawCircle(pen_1, (startX + endX) / 2, (startY + endY) / 2, 3.5F);
            e.Graphics.DrawCircle(pen_1, endX, endY, 3.5F);

        }

        public int GetUserIntention(MouseEventArgs e)
        {
            double lineCenterX = (startX + endX) / 2;
            double lineCenterY = (startY + endY) / 2;

            // user grabbing the center of the line, move
            if (Math.Abs(e.Location.X - lineCenterX) < 5 && Math.Abs(e.Location.Y - lineCenterY) < 5)
            {
                moving = true;
                resizingStart = false;
                resizingEnd = false;

                return Intention.MOVING;
            }
            // user grabbing the starting end of the line, resize
            else if (Math.Abs(e.Location.X - startX) < 6 && Math.Abs(e.Location.Y - startY) < 6)
            {
                moving = false;
                resizingStart = true;
                resizingEnd = false;

                return Intention.RESIZING_START;
            }
            // user grabbing the end of the line, resize
            else if (Math.Abs(e.Location.X - endX) < 6 && Math.Abs(e.Location.Y - endY) < 6)
            {
                moving = false;
                resizingStart = false;
                resizingEnd = true;

                return Intention.RESIZING_END;
            }

            // user is not resizing or moving an existing line, they are creating a new one
            else return Intention.CREATING_NEW_LINE;
        }

        public int getId()
        {
            return id;
        }

        public void Resize(MouseEventArgs e)
        {
            if (moving)
            {
                int offsetX = e.Location.X - (StartX + EndX) / 2;
                int offsetY = e.Location.Y - (StartY + EndY) / 2;

                StartX += offsetX;
                StartY += offsetY;
                EndX += offsetX;
                EndY += offsetY;
            }
            else if (resizingStart)
            {
                StartX = e.Location.X;
                StartY = e.Location.Y;
            } else
            {
                EndX = e.Location.X;
                EndY = e.Location.Y;
            }
        }

        public int StartX
        {
            get { return startX; }
            set { startX = value; }
        }

        public int StartY
        {
            get { return startY; }
            set { startY = value; }
        }

        public int EndX
        {
            get { return endX; }
            set { endX = value; }
        }

        public int EndY
        {
            get { return endY; }
            set { endY = value; }
        }

    }
}
