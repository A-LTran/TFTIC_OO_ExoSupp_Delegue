namespace TFTIC_OO_ExoSupp_Delegue
{
    internal class Grid 
    {
        #region Constructors
        public Grid()
        {
            SetGrid();
        }
        public Grid(Action<Object, RobotEventArgs> displayMessageObject, Action<string> displayMessage, Action<string> displayMessageIL, Action clearScreen, Func<string> receiveMessage)
        {
            SetGrid();
            UI = new RobotUI();
            UI.DisplayMessageObjectAction += displayMessageObject;
            UI.DisplayMessageAction += displayMessage;
            UI.DisplayILMessageAction += displayMessageIL;
            UI.ClearScreenAction += clearScreen;
            UI.ReceiveMessage += receiveMessage;
        }      
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

        public int Attempts { get; set; } = 0;
        internal bool quit = false;

        private Random rand = new Random();
        public RobotUI UI { get; private set; }
        public Robot robot { get; private set; }

        #endregion

        #region Methods
        public void InitGame()
        {
            robot = new Robot(this);
            GameProcess();
        }

        private void GameProcess()
        {
            UI.DisplayMessageAction?.Invoke("Please help our robot in reaching its destination!\n");
            do
            {
                UI.RefreshGrid(robot, new RobotEventArgs("Grid has been refreshed.\n", MessageType.Info), 0);
                robot.RegisterOrder(UI.MenuRobot(robot));
            } while (!quit);    
        }

        private void SetGrid()
        {
            Width = rand.Next(3, 11);
            Height = rand.Next(3, 11);
            do
            {
                FinalX = rand.Next(0, Width + 1);
                FinalY = rand.Next(0, Height + 1); 
            } while (FinalX == 0 && FinalY == 0);
        }

        public void ResetGrid()
        {
            Attempts = 0;
            SetGrid();
            robot.PositionX = 0; 
            robot.PositionY=0;
        }

        public bool CheckVictory()
        {
            if (robot.PositionX == FinalX && robot.PositionY == FinalY)
            {
                robot.CancelOrder();
                return true;
            }
            return false;
        }
        #endregion
    }
}