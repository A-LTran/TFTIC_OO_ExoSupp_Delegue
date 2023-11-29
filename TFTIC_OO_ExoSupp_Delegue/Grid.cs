namespace TFTIC_OO_ExoSupp_Delegue
{
    internal class Grid 
    {
        #region Constructors
        public Grid()
        {
            Width = rand.Next(3, 21);
            Height = rand.Next(3, 21);
            FinalX = rand.Next(0, Width + 1);
            FinalY = rand.Next(0, Height + 1);
        }

        //public Grid(int width, int height, int finalX, int finalY)
        //{
        //    Width = width;
        //    Height = height;
        //    FinalX = finalX;
        //    FinalY = finalY;
        //} 
        #endregion


        #region Properties
        private int _width;
        public int Width
        {
            get { return _width; }
            private set
            {
                if (value <= 0) return;
                _width = value;
            }
        }
        private int _height;

        public int Height
        {
            get { return _height; }
            private set
            {
                if (value <= 0) return;
                _height = value;
            }
        }
        private int _finalX;
        public int FinalX
        {
            get { return _finalX; }
            private set
            {
                if (!(value >= 0 && value <= _width)) return;
                _finalX = value;
            }
        }
        private int _finalY;
        public int FinalY
        {
            get { return _finalY; }
            private set
            {
                if (!(value >= 0 && value <= _height)) return;
                _finalY = value;
            }
        }

        Random rand = new Random();
        public Robot robot { get; set; }
        #endregion

        #region Methods
        public void InitGame()
        {
            robot = new Robot(this, rand.Next(0, Width + 1), rand.Next(0, Height + 1));
        } 
        #endregion
    }
}