namespace TFTIC_OO_ExoSupp_Delegue
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Grid theGrid = new Grid();
            theGrid.InitGame();

            /*//theGrid.robot.RegisterOrder("Forward"); //Y+1
            //theGrid.robot.RegisterOrder("Forward"); //Y+1

            //theGrid.robot.RegisterOrder("Left"); // West
            //theGrid.robot.RegisterOrder("Forward"); //X-1
            //theGrid.robot.RegisterOrder("Forward"); //X-1

            //theGrid.robot.RegisterOrder("Right"); // North
            //theGrid.robot.RegisterOrder("Forward"); //Y+1
            //theGrid.robot.RegisterOrder("Forward"); //Y+1

            //theGrid.robot.RegisterOrder("Right"); // East
            //theGrid.robot.RegisterOrder("Right"); // South
            //theGrid.robot.RegisterOrder("Forward"); //X+1
            //theGrid.robot.RegisterOrder("Forward"); //X+1

            //theGrid.robot.Execute();*/

            UserMenu(theGrid);          
        }

        static void DrawGrid(Grid g)
        {
            Console.WriteLine(new string('-', (g.Width + 1) * 4 +1));       

            for (int j = g.Height; j >= 0; j--)
            {
                Console.Write("|");
                for (int i = 0; i <= g.Width; i++)
                {
                    if(g.robot.FinalPositionReached() && i == g.robot.PositionX && j == g.robot.PositionY) Console.Write(" V |");
                    if (i == g.FinalX && j == g.FinalY && !g.robot.FinalPositionReached()) Console.Write(" X |");
                    else if (i == g.robot.PositionX && j == g.robot.PositionY && !g.robot.FinalPositionReached()) Console.Write(" R |");
                    else Console.Write("   |");
                }
                Console.WriteLine();
                Console.WriteLine(new string('-', (g.Width + 1) * 4 + 1));

            }
            Console.WriteLine();
        }

        static void UserMenu(Grid g)
        {
            string s = "";           
           
            RobotOrder order;
            do
            {
                DisplayPositionInfo(g);

                Console.WriteLine("(The following options are case sensitive.)");
                Console.WriteLine($"Robot orders : \n['0' or 'Forward' = move forward] \n['1' or 'Left' = turn left] \n['2' or 'Right' = turn right]");
                Console.WriteLine($"Other options : \n['3' = Execute] \n['4' = Quit]");

                Console.Write($"Please enter your choice : ");
                s = Console.ReadLine();
                if (s == "3")
                {
                    g.robot.Execute();
                    DisplayPositionInfo(g);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
                else g.robot.RegisterOrder(s);
                Console.Clear();
            } while (s != "4" && !g.robot.FinalPositionReached());

            Console.WriteLine("Well done!");
        }

        static void DisplayPositionInfo(Grid g)
        {
            Console.WriteLine();
            Console.WriteLine($"Robot's position => X : {g.robot.PositionX} - Y : {g.robot.PositionY}.");
            Console.WriteLine($"Direction : {g.robot.direction}.");
            DrawGrid(g);
        }
    }
}