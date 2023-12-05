namespace TFTIC_OO_ExoSupp_Delegue
{
    public enum Style { Red, Yellow, Green }
    internal class RobotUI
    {
        public Action<Object, RobotEventArgs> DisplayMessageObjectAction;
        public Action<string> DisplayMessageAction;
        public Action<string> DisplayILMessageAction;
        public Action ClearScreenAction;
        public Func<string> ReceiveMessage;

        /// <summary>
        /// Display message with set style (depending on the messageType)
        /// </summary>
        /// <param name="robot"> robot instance </param>
        /// <param name="rea"> RobotEventArgs instance </param>
        public void DisplayMessage(Robot robot, RobotEventArgs rea)
        {
            SetStyle(rea.MessageType);
            DisplayMessageObjectAction?.Invoke(robot, rea);
            RemoveStyle();
        }

        /// <summary>
        /// Refreshing the grid
        /// </summary>
        /// <param name="robot"> Robot </param>
        /// <param name="rea"> RobotEventArgs </param>
        /// <param name="timer"> int </param>
        public void RefreshGrid(Robot robot, RobotEventArgs rea, int timer = 1000)
        {
            ClearScreenAction?.Invoke();
            
            DrawGrid(robot.MyGrid);
            DisplayMessage(robot, rea);
            Thread.Sleep(timer);
        }

        /// <summary>
        /// User Menu to retrieve orders
        /// </summary>
        /// <param name="robot"></param>
        /// <returns> Returns a list of orders </returns>
        public List<RobotOrder> MenuRobot(Robot robot)
        { 
            RobotOrder robotOrder = new RobotOrder();
            List<RobotOrder> robotOrderList = new List<RobotOrder>();
            string userInput = "";
            bool quit = false;
            do
            {
                DisplayPositionInfo(robot.MyGrid);

                DisplayMessageAction?.Invoke($">> Robot orders : \n['0' = move forward] \n['1' = turn left] \n['2' = turn right] \n['3' = Execute]");
                DisplayMessageAction?.Invoke($">> Other options : \n['4' = Quit]");

                DisplayILMessageAction?.Invoke($"\n>> Please enter your choice : ");
                userInput = ReceiveMessage?.Invoke();
                DisplayMessageAction?.Invoke("");
                quit = CheckUserValue(userInput);

            } while (!quit);

            return robotOrderList;

            bool CheckUserValue(string str) 
            {
                foreach (char character in str)
                {
                    if (Enum.TryParse(character.ToString(), out robotOrder)) robotOrderList.Add(robotOrder);
                    if (robotOrder == RobotOrder.Execute || robotOrder == RobotOrder.Quit) return true;
                }         
                return false;
            }
        }

        /// <summary>
        /// Set coloring style
        /// </summary>
        /// <param name="type"> MessageType </param>
        private static void SetStyle(MessageType type)
        {
            if(type == MessageType.Erreur) Console.ForegroundColor = ConsoleColor.Red;
            else if(type == MessageType.Info) Console.ForegroundColor = ConsoleColor.Yellow;
            else Console.ForegroundColor = ConsoleColor.Green;
        }

        /// <summary>
        /// Resetting style
        /// </summary>
        private static void RemoveStyle()
        {
            Console.ResetColor();
        }

        /// <summary>
        /// Drawing the grid
        /// </summary>
        /// <param name="grid"></param>
        private void DrawGrid(Grid grid)
        {
            DisplayMessageAction?.Invoke(new string('-', (grid.Width + 1) * 4 + 4));

            for (int j = grid.Height; j >= 0; j--)
            {
                DisplayILMessageAction?.Invoke((j<10)?$" {j} |":$"{j} |");
                for (int i = 0; i <= grid.Width; i++)
                {
                    if (grid.robot.MyGrid.CheckVictory() && i == grid.robot.PositionX && j == grid.robot.PositionY) DisplayILMessageAction?.Invoke(" V |");
                    else if (i == grid.FinalX && j == grid.FinalY) DisplayILMessageAction?.Invoke(" X |");
                    else if (i == grid.robot.PositionX && j == grid.robot.PositionY) DisplayILMessageAction?.Invoke($" R |");
                    else DisplayILMessageAction?.Invoke("   |");
                }
                DisplayMessageAction?.Invoke(" ");
                DisplayMessageAction?.Invoke(new string('-', (grid.Width + 1) * 4 + 4));
            }
            DisplayILMessageAction?.Invoke("   |");
            for (int i = 0; i <= grid.Width; i++)
            {
                DisplayILMessageAction?.Invoke((i<10)?$" {i} |":$"{i} |");
            }
            DisplayMessageAction?.Invoke("\n");
        }

        /// <summary>
        /// Displaying position informations
        /// </summary>
        /// <param name="grid"> Grid </param>
        private void DisplayPositionInfo(Grid grid)
        {
            if (grid.robot.MyGrid.CheckVictory())
            {
                grid.ResetGrid();
                grid.UI.RefreshGrid(grid.robot, new RobotEventArgs("New grid initiated.\n", MessageType.Info));
            }
            DisplayMessageAction?.Invoke($"Robot's position => X : {grid.robot.PositionX} - Y : {grid.robot.PositionY}.");
            DisplayMessageAction?.Invoke($"Direction : {grid.robot.Direction}.");  
            DisplayMessageAction?.Invoke("");              
        }
    }
}