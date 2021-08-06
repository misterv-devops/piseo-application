using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// using en +
using testDLLAlim;

using System.IO;
using System.Net.Sockets;

namespace consoleDLLAlim
{
   public class consoleDLLAlim
   {   static bool exitSystem = false;

        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;
        private static int amp;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {

            Console.WriteLine("Exiting system due to external CTRL-C, or process kill, or shutdown");

            //do your cleanup here
            Thread.Sleep(1000); //simulate some cleanup delay

            Console.WriteLine("Cleanup complete");

            //allow main to run off
            exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion

        // ////////////////////////////////////////////////////////////////////////////////////////////

        static void Main(string[] args)
        {
            DLLAlim Alim = new DLLAlim();
            string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "'");

            //////////////////   Connexion Alimentation réelle     /////////////////////////////////
            bool connection = Alim.Connect("192.168.1.200", 5025);

            /////////////////   Connexion Alimmentation virtuelle  ////////////////////////////////
            // bool connection = Alim.Connect("127.0.0.1", 5025);

            //Alim.ChannelA(@"C:\Users\Margaux\Documents\channelA.txt");
            //Alim.ChannelB(@"C:\Users\Margaux\Documents\channelB.txt"); 
            //Alim.fileRead(@"C:\Users\MASTER\Documents\testBasique.txt");

            Console.WriteLine("limiti : ");
            string limiti = Alim.GetLimitI().ToString();
            Console.WriteLine("limitv : ");
            string limitv = Alim.GetLimitV().ToString();
            Console.WriteLine("leveli : ");
            string leveli = Alim.GetLevelI().ToString();
            Console.WriteLine("delay  : ");
            string delay = Alim.GetDelay();
            Console.ReadLine();

            Alim.fileReadModifierPiseo(@"C:\Users\MASTER\Documents\MesurePiseo.txt", limiti, limitv, leveli, delay);
            Alim.Send();

            //string reponse = Alim.Returndata;
            //int taille = reponse.Length;
            //Console.WriteLine(taille);
            //Console.WriteLine(reponse);
            Thread.Sleep(3000);

            //Alim.Recevoir();
            //reponse = Alim.Returndata;
            //int taille = reponse.Length;
            //Console.WriteLine(taille);
            //Console.WriteLine(reponse);
            //Thread.Sleep(1000);
            Alim.Recevoir();
            string txt = Alim.Returndata;
            Console.WriteLine(txt);
            Alim.Exporter(@"C:\Users\MASTER\Documents\MesureLED-Alimentation_" + date + ".csv");
            Thread.Sleep(1000);
            Console.WriteLine("Données enregistrés ");
            Thread.Sleep(2000);

        ///////////////////////////////////////////////////////////////////

        // Some boilerplate to react to close window event, CTRL-C, kill, etc
        _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);


            Alim.Disconnect(@"C:\Users\MASTER\Documents\deconnexion.txt");
        }

    }
}