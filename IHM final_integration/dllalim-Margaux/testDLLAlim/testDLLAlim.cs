using System;
using System.IO;
using System.Linq;
//using en +
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace testDLLAlim
{
    public class DLLAlim 
    {
        private TcpClient client;
        public TcpClient Client
        {
            get { return client; }
            set { client = value; }
        }

        private NetworkStream stream;
        public NetworkStream Stream
        {
            get { return stream; }
            set { stream = value; }
        }

        // A modifier
        private string[] textLines;
        public string[] TextLines
        {
            get { return textLines; }
            set { textLines = value; }
        }

        private int nbLines;
        public int NbLines
        {
            get { return nbLines; }
            set { nbLines = value; }
        }

        private string returndata;
        public string Returndata
        {
            get { return returndata; }
            set { returndata = value; }
        }

        public bool Disconnect(string disc)
        {
            try
            {
                this.fileRead(disc);
                this.Send();
                this.Recevoir();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture de fichier :" + ex);
                return false;
            }
        }

        public bool Connect(string address, int port)
        {
            try
            {
                this.client = new TcpClient();
                this.client.Connect(address, port);
                Console.WriteLine(this.client.Connected);
                this.stream = this.client.GetStream();
                return true;
            }
            catch (Exception ex)
            {
                this.stream.Close();
                this.client.Close();
                Console.WriteLine("Erreur de connexion :" + ex);
                return false;
            }
        }

        public bool fileRead(string path)
        {
            try
            {
                this.textLines = System.IO.File.ReadAllLines(path);
                this.nbLines = System.IO.File.ReadAllLines(path).Count();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture de fichier :" + ex);
                return false;
            }
        }

        public bool fileReadModifierPiseo(string path, string limiti, string limitv, string leveli, string delay)
        {
            try
            {
                this.textLines = System.IO.File.ReadAllLines(path);
                this.nbLines = System.IO.File.ReadAllLines(path).Count();
                int i = 0;
                foreach (string line in textLines)
                {
                    if (limiti != string.Empty && line.Contains("smu1.source.limiti = 2.5   ---------------------------------------------------------- SRo  - set SourceA un Courant limitée à 2500mA"))
                    {
                        this.textLines[i] = "smu1.source.limiti = " + limiti;
                        // Console.WriteLine("smua.source.limiti = " + limiti);
                    }
                    else if (limitv != string.Empty && line.Contains("smu1.source.limitv = 40     ---------------------------------------------------------- SRo  - set SourceA une Tension limitée à 40V"))
                    {
                        this.textLines[i] = "smu1.source.limitv = " + limitv;
                        // Console.WriteLine("smua.source.limitv = " + limitv);
                    }
                    else if (leveli != string.Empty && line.Contains("smu1.source.leveli = 0.35   --------------------------------------------------------- SRo  - Source Courant A applique à 700mA"))
                    {
                        this.textLines[i] = "smu1.source.leveli = " + leveli;
                        // Console.WriteLine("smua.source.leveli = " + leveli);
                    }
                    else if (delay != string.Empty && line.Contains("delay(18E-3)           ------------------    ---------------------------------------- SRo  - Applique un delai de 25ms pour le Pulse (ne tient pas compte du temps de la fonction measure)"))
                    {
                        this.textLines[i] = "delay(" + delay + ")";
                        // Console.WriteLine("delay(" + delay + ")");
                    }
                    else
                    {
                        this.textLines[i] = line;
                    }
                    // Console.WriteLine(this.textLines[i]);
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture du fichier ou de modification :" + ex);
                return false;
            }
        }


        public bool fileReadModifier(string path, string range, string intensite)
        {
            try
            {
                this.textLines = System.IO.File.ReadAllLines(path);
                this.nbLines = System.IO.File.ReadAllLines(path).Count();
                int i = 0;
                foreach (string line in textLines)
                {
                    if (line.Contains("smua.source.rangev"))
                    {
                        this.textLines[i] = "smua.source.rangev = " + range;
                        // Console.WriteLine("smua.source.ragev = " + range);
                    }
                    else if (line.Contains("smua.source.levelv"))
                    {
                        this.textLines[i] = "smua.source.levelv = " + intensite;
                        // Console.WriteLine("smua.source.levelv = " + intensite);
                    }
                    else
                    {
                        this.textLines[i] = line;
                    }
                    Console.WriteLine(this.textLines[i]);
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture du fichier ou de modification :" + ex);
                return false;
            }
        }

        public bool ChannelA(string chanA)
        {
            try
            {
                this.textLines = System.IO.File.ReadAllLines(chanA);
                this.nbLines = System.IO.File.ReadAllLines(chanA).Count();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture de fichier :" + ex);
                return false;
            }
        }

        public bool ChannelB(string chanB)
        {
            try
            {
                this.textLines = System.IO.File.ReadAllLines(chanB);
                this.nbLines = System.IO.File.ReadAllLines(chanB).Count();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture de fichier :" + ex);
                return false;
            }
        }

      

        public bool Send()
        {
                if (this.stream.CanWrite)
                {
                    for (int i = 0; i < this.nbLines; i++)
                    {
                        byte[] data = System.Text.Encoding.ASCII.GetBytes(this.textLines[i] + "\n");
                        int count = data.Count();
                        this.stream.Write(data, 0, count);
                    }
                }
                return true;
        }

        public int Recevoir()
        {
            int count = 0; // Nombre de d'octets réellement lus
            try
            {
                System.Threading.Thread.Sleep(100);
                if (this.stream.DataAvailable)
                {
                    byte[] buffer = new byte[this.client.ReceiveBufferSize];
                    // int test = stream.Read();
                    count = stream.Read(buffer, 0, (int)client.ReceiveBufferSize);
                    // buffer[count] = 10; // 10 = "\n"
                    // buffer[count + 1] = 0;
                    this.returndata = System.Text.Encoding.UTF8.GetString(buffer, 0, count);
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de reception :" + ex);
                //this.stream.Close();
                //this.client.Close();
                return -1;
            }
        }
        
        //public bool Charger(string path) // Fonction à coder (lire un fichier tant qu'il y a des lignes)
        //{
        //    try
        //    {
        //        this.textLines = System.IO.File.ReadAllLines(path);
        //        this.nbLines = System.IO.File.ReadAllLines(path).Count();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erreur de lecture de fichier :" + ex);
        //        return false;
        //    }
        //}

       public bool Exporter(string strFilePath)
        {
            try
            {
                string txt = Returndata;

                string strSeperator = ",";
                StringBuilder sbOutput = new StringBuilder();

                string[][] inaOutput = new string[][]{
            new string[]{txt}
        };

                int ilength = inaOutput.GetLength(0);
                for (int i = 0; i < inaOutput.Length; i++)
                    sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));

                // Create and write the csv file
                File.WriteAllText(strFilePath, sbOutput.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de lecture de fichier :" + ex);
                return false;
            }
        }

        public double GetLimitI()
        {
            return 2.5;
        }

        public int GetLimitV()
        {
            return 40;
        }

        public double GetLevelI()
        {
            return 0.35;
        }

        public string GetDelay()
        {
            return "18E-3";
        }
        public bool SetLimitI(string limiti)
        {
            try
            {
                int i = 0;
                foreach (string line in textLines)
                {
                    if (line.Contains("smu1.source.limiti = 2.5   ---------------------------------------------------------- SRo  - set SourceA un Courant limitée à 2500mA"))
                    {
                        this.textLines[i] = "smu1.source.limiti = " + limiti;
                        Console.WriteLine(this.textLines[i]);
                    }
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de Set LimitI :" + ex);
                return false;
            }
        }

        public bool SetLimitV(string limitv)
        {
            try
            {
                int i = 0;
                foreach (string line in textLines)
                {
                    if (line.Contains("smu1.source.limitv = 40     ---------------------------------------------------------- SRo  - set SourceA une Tension limitée à 40V"))
                    {
                        this.textLines[i] = "smu1.source.limitv = " + limitv;
                        Console.WriteLine(this.textLines[i]);
                    }
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de Set LimitV :" + ex);
                return false;
            }
        }

        public bool SetLevelI(string leveli)
        {
            try
            {
                int i = 0;
                foreach (string line in textLines)
                {
                    if (line.Contains("smu1.source.leveli = 0.35   --------------------------------------------------------- SRo  - Source Courant A applique à 700mA"))
                    {
                        this.textLines[i] = "smua1.source.leveli = " + leveli;
                        Console.WriteLine(this.textLines[i]);
                    }
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de Set LevelI :" + ex);
                return false;
            }
        }

        public bool SetDelay(string delay)
        {
            try
            {
                int i = 0;
                foreach (string line in textLines)
                {
                    if (line.Contains("delay(18E-3)           ------------------    ---------------------------------------- SRo  - Applique un delai de 25ms pour le Pulse (ne tient pas compte du temps de la fonction measure)"))
                    {
                        this.textLines[i] = "delay(" + delay + ")";
                        Console.WriteLine(this.textLines[i]);
                    }
                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de Set Delay :" + ex);
                return false;
            }
        }


        public DLLAlim ()
        {

        }

        ~DLLAlim()
        {
            this.Disconnect(@"C:\Users\MASTER\Documents\deconnexion.txt");
            Console.WriteLine("Déconnexion...");
            Thread.Sleep(1500);
        }
    }
}
