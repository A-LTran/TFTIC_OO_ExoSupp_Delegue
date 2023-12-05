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

        public void DisplayMessage(Robot robot, RobotEventArgs rea)
        {
            SetStyle(rea.MessageType);
            DisplayMessageObjectAction?.Invoke(robot, rea);
            RemoveStyle();
        }

        public void RefreshGrid(Robot robot, RobotEventArgs rea, int timer = 1000)
        {
            ClearScreenAction?.Invoke();
            
            DrawGrid(robot.MyGrid);
            DisplayMessage(robot, rea);
            Thread.Sleep(timer);
        }

        public List<RobotOrder> MenuRobot(Robot robot)
        { 
            RobotOrder robotOrder = new RobotOrder();
            List<RobotOrder> robotOrderList = new List<RobotOrder>();
            string userInput = "";

            do
            {
                DisplayPositionInfo(robot.MyGrid);

                DisplayMessageAction?.Invoke("(The following options are case sensitive.)");
                DisplayMessageAction?.Invoke($">> Robot orders : \n['0' or 'Forward' = move forward] \n['1' or 'Left' = turn left] \n['2' or 'Right' = turn right] \n['3' or 'Execute' = Execute]");
                DisplayMessageAction?.Invoke($">> Other options : \n['4' or 'Quit' = Quit]");

                DisplayILMessageAction?.Invoke($"\n>> Please enter your choice : ");
                userInput = ReceiveMessage?.Invoke();
                DisplayMessageAction?.Invoke("");
                CheckUserValue(userInput);
            } while (robotOrder != RobotOrder.Execute);

            return robotOrderList;

            void CheckUserValue(string str) 
            {
                foreach (char character in str)
                {
                    if (Enum.TryParse(character.ToString(), out robotOrder)) robotOrderList.Add(robotOrder);
                    if (robotOrder == RobotOrder.Execute) return;
                }         
            }
        }

        private void SetStyle(MessageType type)
        { 
            if(type == MessageType.Erreur) Console.ForegroundColor = ConsoleColor.Red;
            else if(type == MessageType.Info) Console.ForegroundColor = ConsoleColor.Yellow;
            else Console.ForegroundColor = ConsoleColor.Green;
        }

        private void RemoveStyle()
        {
            Console.ResetColor();
        }

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
                    else if (i == grid.robot.PositionX && j == grid.robot.PositionY) DisplayILMessageAction?.Invoke(" R |");
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