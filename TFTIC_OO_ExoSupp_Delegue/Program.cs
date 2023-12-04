using System.Threading.Channels;

namespace TFTIC_OO_ExoSupp_Delegue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grid theGrid = new Grid
                (
                    (Robot, RobotEventArgs) => { Console.WriteLine($"[{RobotEventArgs.MessageType}] - {RobotEventArgs.Message}"); },
                    (String) => { Console.WriteLine(String); },
                    (String) => { Console.Write(String); },
                    () => { Console.Clear(); },
                    () => { return Console.ReadLine(); }
                );
            theGrid.InitGame();        
        }

        #region Methods
        //static void DrawGrid(Grid g)
        //{
        //    Console.WriteLine(new string('-', (g.Width + 1) * 4 + 1));

        //    for (int j = g.Height; j >= 0; j--)
        //    {
        //        Console.Write("|");
        //        for (int i = 0; i <= g.Width; i++)
        //        {
        //            if (g.robot.CheckVictory() && i == g.robot.PositionX && j == g.robot.PositionY) Console.Write(" V |");
        //            else if (i == g.FinalX && j == g.FinalY) Console.Write(" X |");
        //            else if (i == g.robot.PositionX && j == g.robot.PositionY) Console.Write(" R |");
        //            else Console.Write("   |");
        //        }
        //        Console.WriteLine();
        //        Console.WriteLine(new string('-', (g.Width + 1) * 4 + 1));
        //    }
        //    Console.WriteLine();
        //}

        //static void UserMenu(Grid g)
        //{
        //    string s = "";
            
        //    Console.WriteLine("Please help our robot in reaching its destination!\n");
        //    do
        //    {
        //        DisplayPositionInfo(g);

        //        Console.WriteLine("(The following options are case sensitive.)");
        //        Console.WriteLine($"Robot orders : \n['0' or 'Forward' = move forward] \n['1' or 'Left' = turn left] \n['2' or 'Right' = turn right] \n['3' or 'Execute' = Execute]");
        //        Console.WriteLine($"Other options : \n['4' = Quit]");

        //        Console.Write($"Please enter your choice : ");
        //        s = Console.ReadLine();
        //        Console.WriteLine();

        //        g.robot.RegisterOrder(s);
        //        Console.WriteLine();

        //        DisplayPositionInfo(g);
        //        if (!g.robot.CheckVictory()) g.robot.RestorePosition();

        //        Console.WriteLine("Press Enter to continue...");
        //        Console.ReadLine();
                
        //        Console.Clear();
        //    } while (s != "4");

        //}

        //static void DisplayPositionInfo(Grid g)
        //{
        //    if (g.robot.CheckVictory())             
        //    {                  
        //        g.ResetGrid();
        //        Console.WriteLine("Initiating new grid...");
        //    }
        //    Console.WriteLine($"Robot's position => X : {g.robot.PositionX} - Y : {g.robot.PositionY}.");
        //    Console.WriteLine($"Direction : {g.robot.Direction}.");
        //    DrawGrid(g);
        //} 
        #endregion
    }
}