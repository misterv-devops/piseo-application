using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testDLLAlim;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            DLLAlim test = new DLLAlim();
            bool connection = test.Connect("192.168.1.200", 5025);
            test.fileRead((@"C:\Users\MASTER\Documents\deconnexion.txt"));
            test.Send();
            test.Recevoir();

            //test.Disconnect(@"C:\Users\Margaux\Documents\deconnexion.txt");
        }
    }
}
