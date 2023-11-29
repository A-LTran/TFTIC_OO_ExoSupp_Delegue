using System;

namespace TFTIC_OO_ExoSupp_Delegue
{
    public enum Direction { North, East, South, West }
    public enum RobotOrder { Forward, Left, Right }
    internal class Robot 
    {
        #region Constructors
        public Robot(Grid grid, int positionX, int positionY)
        {
            MyGrid = grid;
            PositionX = positionX;
            PositionY = positionY;
        }
        #endregion

        #region Properties
        private Action orders;

        internal Grid MyGrid { get; private set; }
        internal Direction direction { get; private set; } = Direction.North;
        private int _positionX;
        public int PositionX
        {
            get { return _positionX; }
            set
            {
                if (!(value >= 0 && value <= this.MyGrid.Width)) return;
                _positionX = value;
            }
        }
        private int _positionY;
        public int PositionY
        {
            get { return _positionY; }
            set
            {
                if (!(value >= 0 && value <= MyGrid.Height)) return;
                _positionY = value;
            }
        }
        #endregion

        #region Methods
        private void MoveForward()
        {
            switch (direction)
            {
                case Direction.North:
                    PositionY += 1;
                    break;
                case Direction.East:
                    PositionX += 1;
                    break;
                case Direction.South:
                    PositionY -= 1;
                    break;
                default:
                    PositionX -= 1;
                    break;
            }
            FinalPositionReached();
        }
        private void TurnLeft()
        {
            this.direction = direction - 1;
            if ((int)this.direction < 0) this.direction = Direction.West;
        }
        private void TurnRight()
        {
            this.direction = direction + 1;
            if ((int)this.direction > 3) this.direction = Direction.North;

        }

        public void RegisterOrder(string s)
        {
            RobotOrder order;
            if (!Enum.TryParse(s, out order)) return;
            switch (order)
            {
                case RobotOrder.Forward:
                    orders += MoveForward;
                    break;
                case RobotOrder.Left:
                    orders += TurnLeft;
                    break;
                case RobotOrder.Right:
                    orders += TurnRight;
                    break;
            }
        }

        public void Execute()
        {
            orders?.Invoke();
            orders = null;
        }

        public bool FinalPositionReached()
        {
            if (PositionX == MyGrid.FinalX && PositionY == MyGrid.FinalY)
            {
                orders = null;
                return true;
            }
            return false;
        } 
        #endregion
    }
}