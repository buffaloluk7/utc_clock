using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTCClock.Presentation.Console
{
    class Program
    {
        static TimerViewModel model = new TimerViewModel();

        static void Main(string[] args)
        {
            model.Start();
            System.Console.WriteLine("Hallo Freund");
            System.Console.ReadLine();
        }
    }
}
