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
    }
}