﻿using System;

namespace TFTIC_OO_ExoSupp_Delegue
{
    public enum Direction { North, East, South, West }
    public enum RobotOrder { Forward, Left, Right, Execute, Quit }
    internal class Robot 
    {
        #region Constructors
        public Robot(Grid grid)
        {
            MyGrid = grid;
            PositionX = 0;
            PositionY = 0;
            robotEvent += MyGrid.UI.DisplayMessage;
        }
        #endregion

        #region Properties
        private Action orders;

        internal Grid MyGrid { get; private set; }
        internal Direction Direction { get; private set; } = Direction.North;
        private int _positionX;
        public int PositionX
        {
            get { return _positionX; }
            set
            {
                if (!(value >= 0 && value <= this.MyGrid.Width))
                {
                    robotEvent?.Invoke(this, new RobotEventArgs("Aoutch, a wall!", MessageType.Erreur));
                    return;
                }
                _positionX = value;
            }
        }
        private int _oldPositionX;
        private int _positionY;
        public int PositionY
        {
            get { return _positionY; }
            set
            {
                if (!(value >= 0 && value <= MyGrid.Height)) 
                {
                    robotEvent?.Invoke(this, new RobotEventArgs("Aoutch, a wall!", MessageType.Erreur));
                    return; 
                }
                _positionY = value;
            }
        }
        private int _oldPositionY;

        private Action<Robot, RobotEventArgs> robotEvent;

        #endregion

        #region Methods
        private void MoveForward()
        {
            switch (Direction)
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
            MyGrid.UI.RefreshGrid(this,new RobotEventArgs($"Moving {Direction}.", MessageType.Info));
            MyGrid.CheckVictory();
        }
        private void TurnLeft()
        {
            this.Direction = Direction - 1;
            if ((int)this.Direction < 0) this.Direction = Direction.West;
            MyGrid.UI.RefreshGrid(this, new RobotEventArgs($"Turning left towards the {Direction}.", MessageType.Info));
        }
        private void TurnRight()
        {
            this.Direction = Direction + 1;
            int DirectionLength = Enum.GetNames(typeof(Direction)).Length;
            if ((int)this.Direction > DirectionLength-1) this.Direction = Direction.North;
            MyGrid.UI.RefreshGrid(this, new RobotEventArgs($"Turning right towards the {Direction}.", MessageType.Info));                       
        }

        public void RegisterOrder(List<RobotOrder> orderList)
        {
            foreach (RobotOrder order in orderList)
            {
                RegisterOrder(order);
            }
        }
        public void RegisterOrder(RobotOrder order)
        {           
            robotEvent?.Invoke(this, new RobotEventArgs($"[{order}] - Order registered!", MessageType.Info));
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
                case RobotOrder.Execute:
                    Execute();
                    break;
                case RobotOrder.Quit:
                    MyGrid.quit = true;
                    break;
            }
        }

        public void CancelOrder()
        {
            orders = null;
        }

        private void Execute()
        {
            robotEvent?.Invoke(this, new RobotEventArgs("Process initiated...", MessageType.Info));
            SavePosition();
            MyGrid.Attempts++;
            if (orders is null)
            {
                MyGrid.UI.DisplayMessageAction("No previous orders have been registered...");
                return;
            }
            orders();
            orders = null;
            if (!MyGrid.CheckVictory())
            {
                robotEvent?.Invoke(this, new RobotEventArgs($"[Attempts : {MyGrid.Attempts}] - You have not reached the final destination! \n\tResetting moves...", MessageType.Info));
                robotEvent?.Invoke(this, new RobotEventArgs($"Resetting previous positions...", MessageType.Info));
                
                RestorePosition();
            }
            else robotEvent?.Invoke(this, new RobotEventArgs($"[Attempts : {MyGrid.Attempts}] - You have reached your destination! Well done!", MessageType.Victory));
            Thread.Sleep(3000);
        }

        private void SavePosition()
        {
            _oldPositionX = PositionX;
            _oldPositionY = PositionY;
        }
        private void RestorePosition()
        {
            PositionX = _oldPositionX;
            PositionY = _oldPositionY;
            Direction = Direction.North;
        } 
        #endregion
    }
}