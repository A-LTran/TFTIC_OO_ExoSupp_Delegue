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

        public RobotOrder MenuRobot(Robot robot)
        { 
            RobotOrder robotOrder = new RobotOrder();
            string s = "";

            do
            {
                DisplayPositionInfo(robot.MyGrid);

                DisplayMessageAction?.Invoke("(The following options are case sensitive.)");
                DisplayMessageAction?.Invoke($">> Robot orders : \n['0' or 'Forward' = move forward] \n['1' or 'Left' = turn left] \n['2' or 'Right' = turn right] \n['3' or 'Execute' = Execute]");
                DisplayMessageAction?.Invoke($">> Other options : \n['4' or 'Quit' = Quit]");

                DisplayILMessageAction?.Invoke($"\n>> Please enter your choice : ");
                s = ReceiveMessage?.Invoke();
                DisplayMessageAction?.Invoke("");

            } while (!Enum.TryParse(s, out robotOrder));

            return robotOrder;
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

        private void DrawGrid(Grid g)
        {
            DisplayMessageAction?.Invoke(new string('-', (g.Width + 1) * 4 + 4));

            for (int j = g.Height; j >= 0; j--)
            {
                DisplayILMessageAction?.Invoke((j<10)?$" {j} |":$"{j} |");
                for (int i = 0; i <= g.Width; i++)
                {
                    if (g.robot.CheckVictory() && i == g.robot.PositionX && j == g.robot.PositionY) DisplayILMessageAction?.Invoke(" V |");
                    else if (i == g.FinalX && j == g.FinalY) DisplayILMessageAction?.Invoke(" X |");
                    else if (i == g.robot.PositionX && j == g.robot.PositionY) DisplayILMessageAction?.Invoke(" R |");
                    else DisplayILMessageAction?.Invoke("   |");
                }
                DisplayMessageAction?.Invoke(" ");
                DisplayMessageAction?.Invoke(new string('-', (g.Width + 1) * 4 + 4));
            }
            DisplayILMessageAction?.Invoke("   |");
            for (int i = 0; i <= g.Width; i++)
            {
                DisplayILMessageAction?.Invoke((i<10)?$" {i} |":$"{i} |");
            }
            DisplayMessageAction?.Invoke("\n");
        }

        private void DisplayPositionInfo(Grid g)
        {
            if (g.robot.CheckVictory())
            {
                g.ResetGrid();
                g.UI.RefreshGrid(g.robot, new RobotEventArgs("New grid initiated.", MessageType.Info));
                //DisplayMessageAction?.Invoke("Initiating new grid...");
            }
            DisplayMessageAction?.Invoke($"Robot's position => X : {g.robot.PositionX} - Y : {g.robot.PositionY}.");
            DisplayMessageAction?.Invoke($"Direction : {g.robot.Direction}.");  
            DisplayMessageAction?.Invoke("");  
            
        }
    }
}