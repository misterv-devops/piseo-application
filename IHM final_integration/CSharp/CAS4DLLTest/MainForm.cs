using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Monspectro;
using System.Data;
using System.Web;
using System.Threading;

namespace test
{

    public partial class application : Form
    {
        public application()
        {
            InitializeComponent();  //toujours initialiser en 1er

            //creation des variables qui sont utiles pour le demarrage de l'application
            monspectro = -1;
            //chemin de la dll et de la version
            
            //int error = Class1.casGetError(monspectro);
        }
        //int  monspectro = -1;
        //creation variables qui seront utilisées par le programmmes tout au long du scénarios
        private StringBuilder sb = new StringBuilder(256);
        private int monspectro;
        public int monspec;
        public Double test = 0;
        public string CalibrationUnit;
        public int MaxADCValue;
        public double DarkCurrentAge;
        public double RadInt;
        public string RadIntUnit;
        public double PhotInt;
        public string PhotIntUnit;
        public double Centroid;
        public int boucle;
        public IntPtr aa;


        void MainFormShown(object sender, EventArgs e) // Ce qui se passe au démarage de l'app remplicage des interface et chemin de la dll /version
        {
            cbInterfaceOption.Enabled = false;
            RemplireIinterfaces();
            infospec();
        }
        private class MyComboBoxItem    // Simplification des interfaces pour les manipuler dans la combotext
        {
            public string Name { get; set; }
            public int ID { get; set; }

            public MyComboBoxItem(string AName, int AID)
            {
                Name = AName;
                ID = AID;
            }
        }
        public int SelectedInterface              // Simplification de l'interface avec un get
        {
            get
            {
                return (interfaceType.Items[interfaceType.SelectedIndex] as MyComboBoxItem).ID;
            }
        }
        public int SelectedInterfaceOption    // Pareil pour les options
        {
            get
            {
                if (cbInterfaceOption.SelectedIndex >= 0)
                {
                    return (cbInterfaceOption.Items[cbInterfaceOption.SelectedIndex] as MyComboBoxItem).ID;
                }
                else
                {
                    return 0;
                }
            }
        }
        private void RemplireIinterfaces()      
        {
            int theIdx;
            int selectIdx;
            selectIdx = 0;
            interfaceType.BeginUpdate();
            try
            {
                interfaceType.Items.Clear();
                interfaceType.DisplayMember = "Name";
                theIdx = 0;
                for (int i = 0; i < MonSpectro.interfaceType(); i++)  // On parcour notre methode qui permet d'obtenir les interfaces disponible.
                {
                    MonSpectro.nameInterface(i, sb, sb.Capacity);     // Ceci permet de mettre les nom sur les interface (pci usb etc)
                                                                      // Saut si il ya des interfaces vides
                    if (sb.Length > 0)
                    {
                        theIdx = interfaceType.Items.Add(new MyComboBoxItem(sb.ToString(), i));
                            // L'index peut etre selectionné plus tard
                        if (i == MonSpectro.demo) selectIdx = theIdx;
                    }
                }
            }
            finally
            {
                interfaceType.EndUpdate();
            }
            interfaceType.SelectedIndex = selectIdx;
        }
        void CbInterfaceSelectedIndexChanged(object sender, EventArgs e) //remplir les options
        {
            int iface;
            int option;
            iface = SelectedInterface;
            cbInterfaceOption.BeginUpdate();
            try
            {
                cbInterfaceOption.Items.Clear();
                cbInterfaceOption.DisplayMember = "Name";
                for (int i = 0; i < MonSpectro.optionSType(iface); i++)
                {
                    option = MonSpectro.optionType(iface, i);
                    MonSpectro.nameOption(iface, i, sb, sb.Capacity);
                    cbInterfaceOption.Items.Add(new MyComboBoxItem(sb.ToString(), option));
                }
            }
            finally
            {
                cbInterfaceOption.EndUpdate();
            }
            cbInterfaceOption.Enabled = (cbInterfaceOption.Items.Count > 1);
            if (cbInterfaceOption.Items.Count > 0) cbInterfaceOption.SelectedIndex = 0;
        }
        private void btnBrowseForConfig_Click(object sender, EventArgs e)
        {
            OFD.FileName = configbox.Text;
            OFD.FilterIndex = 1;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                configbox.Text = OFD.FileName;
            }
        }
        private void btnBrowseForCalib_Click(object sender, EventArgs e)
        {
            OFD.FileName = calibbox.Text;
            OFD.FilterIndex = 2;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                calibbox.Text = OFD.FileName;
            }
        }
        private void infospec()
        {
            if (monspectro == -1)
            {
                infospectro.Text = "spectro non cree";
            }
            else
            {
                infospectro.Text = "spectro  cree";
            }
        }
        private void click_creerspetro(object sender, EventArgs e)
        {
            monspectro = MonSpectro.creationSpectrometre(SelectedInterface, SelectedInterfaceOption);
            MonSpectro.creerParametreConfig(monspectro, MonSpectro.chargeConf, configbox.Text);
            MonSpectro.creerParametreConfig(monspectro, MonSpectro.chargeCalib, calibbox.Text);
            MonSpectro.creerParametreConfig(monspectro, MonSpectro.dpidTransmission, "C:\\Users\aa\\Desktop\\LE DERANGÉ\\aa.isa");
            MonSpectro.casSetOptionsOnOff(monspectro, MonSpectro.UseTransmission, 1);
            MonSpectro.initializerSpectrometre(monspectro, MonSpectro.initializeSpectrometre);
            int errore = MonSpectro.codeError(monspectro);
            IntPtr messageerroe = MonSpectro.MessageDerreur(errore, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", errore, sb.ToString()));
            if (errore < 0)
            {
                MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", errore, sb.ToString()));
                //monspectro = -1;
            }
            else
            {
                MessageBox.Show("interface valide");
            }
            hScrollBar1.Minimum = (int)Math.Round(MonSpectro.recupererParametreConfig(monspectro, MonSpectro.minEntrechaqueMesure));
            hScrollBar1.Maximum = (int)Math.Round(MonSpectro.recupererParametreConfig(monspectro, MonSpectro.maxEntrechaqueMesure));
            if (errore < 0)
            {
                monspectro = -1;
            }
            infospec();
        }
        private void detrireSpectro_Click(object sender, EventArgs e)
        {
            MonSpectro.detruireSpectro(monspectro);
            monspectro = -1;
            infospec();
        }
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = hScrollBar1.Value.ToString();
        }
        private void definirParametre(object sender, EventArgs e)
        {
            int i = -50;
            i= MonSpectro.definirParametreMesure(monspectro, MonSpectro.parametreProchaineMesure, hScrollBar1.Value);
            int errore = MonSpectro.codeError(monspectro);
            if (i != 0)
            {
                MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", errore, sb.ToString()));
            }
            else
            {
                MessageBox.Show("parametre definit" + ":" + "spectrophotometre :" + monspectro.ToString() + ":" + 
                    "constante :" + MonSpectro.parametreProchaineMesure.ToString() + ":" + "intervalle :" + hScrollBar1.Value.ToString());
            }
                // MessageBox.Show("parametre definit" + ":" + monspectro.ToString() + ":" + 
                // Class1.parametreProchaineMesure.ToString() + ":" + hScrollBar1.Value.ToString());
        }
        private void LancerMesure(object sender, EventArgs e) 
        {
            if ((int)Math.Round(MonSpectro.recupererParametreConfig(monspectro, MonSpectro.filterchange)) != 0)
            {
                MonSpectro.definirParametreMesure(monspectro, MonSpectro.densityfilter,
                                                       MonSpectro.recupererParametreConfig(monspectro, MonSpectro.newdensity));                
            }
            if ((int)Math.Round(MonSpectro.recupererParametreConfig(monspectro, MonSpectro.needdark)) != 0)
            {
                MonSpectro.parametrerObturateur(monspectro, 1);
                try
                {
                    MonSpectro.mesureLecourantdobscurit(monspectro);
                 //   CheckCASError();
                }
                finally
                {
                        // If the measure failed, at least make sure the shutter is opened again
                    MonSpectro.parametrerObturateur(monspectro, 0);
                }               
            }
            MonSpectro.Mesurer(monspectro);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void Resultat(object sender, EventArgs e)
        {
                // On récupere les parametres necessaires pour les résultats
            MonSpectro.RecupereParametreCaractere(monspectro, MonSpectro.uniterCalib, sb, sb.Capacity);
            CalibrationUnit = sb.ToString();
            MaxADCValue = (int)Math.Round(MonSpectro.recupererParametreMesure(monspectro, MonSpectro.maxADC));
            DarkCurrentAge = Math.Round(MonSpectro.recupererParametreMesure(monspectro, MonSpectro.retourneNbMilePasse) / 60000, 1);
            MonSpectro.calculColometric(monspectro);
            double tempFloat;
            tempFloat = 0;
            MonSpectro.ObtenirRadInt(monspectro, out tempFloat, sb, sb.Capacity);
            //  CheckCASError();
            RadInt = tempFloat;
            RadIntUnit = sb.ToString();
            MonSpectro.ObtenirPhotInt(monspectro, out tempFloat, sb, sb.Capacity);
            //  CheckCASError();
            PhotInt = tempFloat;
            PhotIntUnit = sb.ToString();
            Centroid = Math.Round(MonSpectro.obtenirCentroid(monspectro), 2);
            MessageBox.Show("MAXadvvalu:" + MaxADCValue.ToString() + "\n" + "darkcurreent:" + 
                DarkCurrentAge.ToString() + "\n" + "Radint :" + RadInt.ToString() + "\n" + "radinUNIt:" + 
                RadIntUnit + "\n" + "phootint:" + PhotInt.ToString() + "\n" + "photointnunit:" + PhotIntUnit + "\n" + 
                "centroiid!" + Centroid.ToString());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            boucle = 2;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            boucle = 1;
            while (boucle != 10)
            {
                Thread.Sleep(10);
                MonSpectro.Mesurer(monspectro);
                Thread.Sleep(10);
                    // On récupere les parametres necessaires pour les résultats
                MonSpectro.RecupereParametreCaractere(monspectro, MonSpectro.uniterCalib, sb, sb.Capacity);
                CalibrationUnit = sb.ToString();
                    // Tout les resultats sont arrondis ou subissent une autre operation
                MaxADCValue = (int)Math.Round(MonSpectro.recupererParametreMesure(monspectro, MonSpectro.maxADC));
                    // Ce sont des calculs complexes ...
                DarkCurrentAge = Math.Round(MonSpectro.recupererParametreMesure(monspectro, MonSpectro.retourneNbMilePasse) / 60000, 1);   
                Thread.Sleep(10);
                MonSpectro.calculColometric(monspectro);
                double tempFloat;
                tempFloat = 0;
                MonSpectro.ObtenirRadInt(monspectro, out tempFloat, sb, sb.Capacity);
                //  CheckCASError();
                RadInt = tempFloat;
                RadIntUnit = sb.ToString();
                MonSpectro.ObtenirPhotInt(monspectro, out tempFloat, sb, sb.Capacity);
                //  CheckCASError();
                PhotInt = tempFloat;
                PhotIntUnit = sb.ToString();
                Centroid = Math.Round(MonSpectro.obtenirCentroid(monspectro), 2);
                Thread.Sleep(10);
                MessageBox.Show("MAXadvvalu:" + MaxADCValue.ToString() + "\n" +
                    "darkcurreent:" + DarkCurrentAge.ToString() + "\n" + "Radint :" + 
                    RadInt.ToString() + "\n" + "radinUNIt:" + RadIntUnit + "\n" + "phootint:" + 
                    PhotInt.ToString() + "\n" + "photointnunit:" + PhotIntUnit + "\n" + "centroiid!" + 
                    Centroid.ToString());
                boucle++;
            }
#if TLCI
        public int CheckTLCIError(int AError)
        {
            if (AError < TLCIDLL.tiErrorNoError)
            {
                TLCIDLL.tlciGetString(AError, 0, 0, sb, sb.Capacity);
                throw new Exception(string.Format("CAS TLCI DLL error ({0}): {1}", AError, sb.ToString()));
            }
            return AError;
        }

        public double GetTLCIFloat(int AWhat, int AIndex, int AExtra)
        {
            double TempFloat = 0;
            CheckTLCIError(TLCIDLL.tlciGetFloat(AWhat, AIndex, AExtra, out TempFloat));
            return TempFloat;
        }
#endif
#if TM30
        public int CheckTM30Error(int AError)
        {
            if (AError < TM30DLL.tiErrorNoError)
            {
                TM30DLL.tm30GetString(AError, 0, 0, sb, sb.Capacity);
                throw new Exception(string.Format("CAS TM30 DLL error ({0}): {1}", AError, sb.ToString()));
            }
            return AError;
        }

        public double GetTM30Float(int AWhat, int AIndex, int AExtra)
        {
            double TempFloat = 0;
            CheckTM30Error(TM30DLL.tm30GetFloat(AWhat, AIndex, AExtra, out TempFloat));
            return TempFloat;
        }
#endif
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            MonSpectro.Mesurer(monspectro);
            MonSpectro.InitialiseBuffer(monspectro, 5);
            MonSpectro.FaireACTION(monspectro, MonSpectro.startmultitrack, 1, 2, aa);
            MonSpectro.copydata(monspectro, 1);
            MonSpectro.casMultiTrackSaveData(monspectro, "c:/AL.SWM");
        }
        private void button17_Click(object sender, EventArgs e)
        {
            MonSpectro.changerSpectro(monspectro, SelectedInterface, SelectedInterfaceOption);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button16_Click(object sender, EventArgs e)
        {       
            MonSpectro.obtenirchainedecaracSpectro(monspectro, MonSpectro.name, sb, sb.Capacity);
            MessageBox.Show(sb.ToString());
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
            MessageBox.Show(MonSpectro.obtenirchainedecaracSpectro(monspectro, MonSpectro.name, sb, sb.Capacity).ToString());
        }
        private void button15_Click(object sender, EventArgs e)
        {
            double ao;
            ao= MonSpectro.obtenirfloatSpectro(monspectro, MonSpectro.dpidparam);
            MessageBox.Show(ao.ToString());
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button14_Click(object sender, EventArgs e)
        {
            MonSpectro.setParametreNumeriqueSpectro(monspectro, MonSpectro.dpidcurrent,4);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button13_Click(object sender, EventArgs e)
        {
            MonSpectro.AssignerProprietsAutreSpectro(monspectro, monspec, 45);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button12_Click(object sender, EventArgs e)
        {
            MonSpectro.lireDonnAcquis(monspectro);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button11_Click(object sender, EventArgs e)
        {
            MonSpectro.checkSileSpectroEstEnAttente(monspectro);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button10_Click(object sender, EventArgs e)
        {
            IntPtr test= MonSpectro.DonnerNomInterface(monspectro,sb,sb.Capacity);
            MessageBox.Show(sb.ToString());
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", test, sb.ToString()));
        }
        private void button9_Click(object sender, EventArgs e)
        {
            MonSpectro.InitialiseBuffer(monspectro, 4);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MonSpectro.FaireACTION(monspectro, MonSpectro.startmultitrack,2,3,messageerro);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button8_Click(object sender, EventArgs e)
        {
            MonSpectro.casMultiTrackSaveData(monspectro, "c:/SAVE.isd");
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MonSpectro.copydata(monspectro, 4);
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int error = MonSpectro.codeError(monspectro);
            IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
            MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
            MonSpectro.definirParametreMesure(monspectro, MonSpectro.DelayTime, 5);
            MonSpectro.FaireACTION(monspectro, MonSpectro.prepar, MonSpectro.TriggerSource, MonSpectro.FlipFlop, messageerro);
            MonSpectro.definirParametreMesure(monspectro, MonSpectro.TriggerSource, MonSpectro.FlipFlop);
            MonSpectro.definirParametreMesure(monspectro, MonSpectro.TriggerOptions, 11);
            MonSpectro.Mesurer(monspectro);
            double cap;
            cap = MonSpectro.recupererParametreConfig(monspectro, MonSpectro.dpidTriggeercap);
            if (cap >= 0)
            {
                MessageBox.Show("spectro supporte ACQ");
            }
            // MonSpectro.Mesurer(monspectro);
                // On récupere les parametres necessaires pour les résultats
            MonSpectro.RecupereParametreCaractere(monspectro, MonSpectro.uniterCalib, sb, sb.Capacity);
            CalibrationUnit = sb.ToString();
                // Tout les resultats sont arrondis ou subissent une autre operation
            MaxADCValue = (int)Math.Round(MonSpectro.recupererParametreMesure(monspectro, MonSpectro.maxADC));
            DarkCurrentAge = Math.Round(MonSpectro.recupererParametreMesure(monspectro, MonSpectro.retourneNbMilePasse) / 60000, 1);
            MonSpectro.calculColometric(monspectro);
            double tempFloat;
            tempFloat = 0;
            MonSpectro.ObtenirRadInt(monspectro, out tempFloat, sb, sb.Capacity);
                //  CheckCASError();
            RadInt = tempFloat;
            RadIntUnit = sb.ToString();
            MonSpectro.ObtenirPhotInt(monspectro, out tempFloat, sb, sb.Capacity);
                //  CheckCASError();
            PhotInt = tempFloat;
            PhotIntUnit = sb.ToString();
            Centroid = Math.Round(MonSpectro.obtenirCentroid(monspectro), 2);
            MessageBox.Show("MAXadvvalu:" + MaxADCValue.ToString() + "\n" + "darkcurreent:" + 
                DarkCurrentAge.ToString() + "\n" + "Radint :" + RadInt.ToString() + "\n" + "radinUNIt:" + 
                RadIntUnit + "\n" + "phootint:" + PhotInt.ToString() + "\n" + "photointnunit:" + PhotIntUnit + 
                "\n" + "centroiid!" + Centroid.ToString());
        }
        // int error = MonSpectro.codeError(monspectro);

        // IntPtr messageerro = MonSpectro.MessageDerreur(error, sb, sb.Capacity);
        // MessageBox.Show(string.Format("CAS DLL error ({0}): {1}", error, sb.ToString()));
    }


        //obtenir parametre spectro non affiché
        //param numeric non affiché
        //set numeric nonplus 
        //assign propriete ... option
        //casgetfifoo
        //etat spect
        //
    }


