using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using testDLLAlim; // DLL Alim
using DLLSpectrometer; // DLL Spectromètre
using System.Threading;

namespace IHM_final
{

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {

        // Variables Spectro
        Spectrometer spectroVirtuel;
        ConfigurationParameter parametresSpectro;
        ResultSpectro resultatsSpectro;
        bool isIntegrationTimeCorrect = false;
        bool integrationTimeModeOn;
        string interfaceSpectro;

        // Variables Alimentation
        int octetsLu;

        // Création  de l'alim 
        DLLAlim alim = new DLLAlim();

        // Variables dossier
        string nom_fichier;

        // Variables connexion
        int port;
        string ip_adresse;

        // Variables données
        string tension; // Tenstion
		string range; // Valeur max. de la tension
        string intensite; // Intensité
        string delay; // Délai

		public MainWindow()
		{
			// Initialisation des composants
			InitializeComponent();

            cbInterface.Items.Add("InterfaceISA");
            cbInterface.Items.Add("InterfacePCI");
            cbInterface.Items.Add("InterfaceTest");
            cbInterface.Items.Add("InterfaceUSB");
            cbInterface.Items.Add("InterfacePCIe");
            cbInterface.Items.Add("InterfaceEthernet");
        }
        public void BtTransmi_Click(object sander, RoutedEventArgs e) // Évènement en cas de click pour upload le fichier de transmission
        {
            Microsoft.Win32.OpenFileDialog fichierTransmission = new Microsoft.Win32.OpenFileDialog();
            string fileName;
            fichierTransmission.FileName = string.Empty;

            // Filtres pour les types de fichiers : "Nom|*.extension|autreNom|*.autreExtension"
            fichierTransmission.Filter = "Fichier de transmission|*.isa|Tous les fichiers|*.*";

            // Affichage du dernier dossier ouvert.
            fichierTransmission.RestoreDirectory = true;

            // Si l'utilisateur clique sur "Ouvrir"...
            if (fichierTransmission.ShowDialog() == true)
            {
                try
                {
                    // Stockage du nom du fichier dans la TextBox
                    fileName = fichierTransmission.FileName;
                    tbFTransmission.Text = fileName;
                }
                catch (Exception ex)
                {
                    // Log de l'erreur
                    Log.LogWriter(ex.Message, "Fichier de transmission");
                    // Gestion de l'erreur
                    System.Windows.MessageBox.Show("Erreur : Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nUne erreur est survenue lors de l'ouverture du fichier de transmission : {0}." + ex.Message;
                }
            }
        }
		public void BtConfig_Click(object sander, RoutedEventArgs e)
		{ 
			Microsoft.Win32.OpenFileDialog fichierConfig = new Microsoft.Win32.OpenFileDialog();
			string fileName;
			fichierConfig.FileName = string.Empty;
			fichierConfig.Filter = "Fichier de configuration|*.ini|Tous les fichiers|*.*";
			fichierConfig.RestoreDirectory = true;
			if (fichierConfig.ShowDialog() == true)
			{
				try
				{
					fileName = fichierConfig.FileName;
					tbFConfig.Text = fileName;
				}
				catch (Exception ex)
				{
                    // Log de l'erreur
                    Log.LogWriter(ex.Message, "Fichier de configuration");
                    // Gestion de l'erreur
                    System.Windows.MessageBox.Show("Erreur : Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nUne erreur est survenue lors de l'ouverture du fichier de configuration : {0}." + ex.Message;
                }
			}
		}
		private void BtCalibr_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog fichierCalibration = new Microsoft.Win32.OpenFileDialog();
			string fileName;
			fichierCalibration.FileName = string.Empty;
			fichierCalibration.Filter = "Fichier de calibration|*.isc|Tous les fichiers|*.*";
			fichierCalibration.RestoreDirectory = true;
			if (fichierCalibration.ShowDialog() == true)
			{
				try
				{
					fileName = fichierCalibration.FileName;
					tbFCalibration.Text = fileName;
				}
				catch (Exception ex)
				{
                    // Log de l'erreur
                    Log.LogWriter(ex.Message, "Fichier de calibration");
                    // Gestion de l'erreur
                    System.Windows.MessageBox.Show("Erreur : Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nUne erreur est survenue lors de l'ouverture du fichier de calibration : {0}." + ex.Message;
                }
			}
		}
        private void connexion(object sender, RoutedEventArgs e)
        {
            try
            {
                port = int.Parse(T_port.Text);
                ip_adresse = T_IP.Text;
                if (alim.Connect(ip_adresse, port) == true)
                {
                    textbox_retour_alim.Text = textbox_retour_alim.Text + "\r\n Connexion effectuée !";
                    R_etat_alim.Fill = Brushes.Green;
                    R_Connexion.Fill = Brushes.Green;
                } 
                else
                {
                    throw new Exception(); 
                }
            }
            catch (Exception ex)
            {
                // Log de l'erreur
                Log.LogWriter(ex.Message, "Connexion alimentation");
                // Gestion de l'erreur
                System.Windows.MessageBox.Show("Erreur : Regardez la boîte d'information Alimentation pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_retour_alim.Text = textbox_retour_alim.Text + "\r\n Erreur de connexion à l'alimentation: {0}." + ex.Message;
            }
        }

        // Méthode pour rentrer un fichier 
        private void bScript_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fichier = new Microsoft.Win32.OpenFileDialog();
            if (fichier.ShowDialog() == true)
                T_fichier.Text = fichier.FileName;
        }
        private void Envoie(object sender, RoutedEventArgs e)
        {
            // SPECTROMETRE CONFIGURATION DU TEMPS D'INTÉGRATION
            try
            {
                do
                {
                    if ((bool)(rbIntegrationTimeFixed.IsChecked == true))
                    {
                        // Chargement des valeurs du temps d'intégration
                        int tempsFixe = int.Parse(tbTempsInt.Text);
                        parametresSpectro.IntegrationTimeFixed = Convert.ToDouble(tempsFixe);
                        integrationTimeModeOn = true;
                        isIntegrationTimeCorrect = true;
                    }
                    else if ((bool)(rbIntegrationTimeAuto.IsChecked == true))
                    {
                        // Chargement des valeurs du temps d'intégration
                        parametresSpectro.ARMinLevel = Convert.ToDouble(tbNivSignalMin.Text);
                        parametresSpectro.ARMaxLevel = Convert.ToDouble(tbNivSignalMax.Text);
                        parametresSpectro.ARMaxIntegrationTime = Convert.ToDouble(tbIntTimeMax.Text);
                        parametresSpectro.ARAverages = Convert.ToDouble(tbIntTimeMoyenne.Text);
                        isIntegrationTimeCorrect = true;
                    }
                    else
                    {
                        textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nTemps d'intégration invalide";
                        integrationTimeModeOn = false;
                        isIntegrationTimeCorrect = false;
                    }
                }
                while (isIntegrationTimeCorrect != true);
                spectroVirtuel.setIntegrationTime(parametresSpectro);
            }
            catch (Exception ex)
            {
                // Log de l'erreur
                Log.LogWriter(ex.Message, "Lancement de la mesure");
                // Gestion de l'erreur
                System.Windows.MessageBox.Show("Erreur : Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\n Erreur lors du lancement de la mesure : {0}." + ex.Message;
            }
            // FIN CONFIGURATION TEMPS D'INTEGRATION SPECTROMETRE
            // ALIMENTATION
            double i;
            if (!double.TryParse(T_intensite.Text, out i) || !double.TryParse(T_tension.Text, out i) || !double.TryParse(T_range.Text, out i))
            {
                textbox_retour_bancdetest.Text = textbox_retour_bancdetest.Text + "\r\n ATTENTION ERREUR DE FORMAT ( les valeurs doivent être superieurs à 0 et les points ne sont pas acceptées";
                return;
            }
            else
            {
                delay = string.Format(T_delay.Text);
                tension = string.Format(T_tension.Text);
                range = string.Format(T_range.Text);
                intensite = string.Format(T_intensite.Text);
                nom_fichier = string.Format(T_fichier.Text);
               //// bool lecture_fichier = alim.fileRead(nom_fichier);
               // try
               // {
               //     if (lecture_fichier == false)
               //     {
               //         throw new Exception();
               //     }
               //     else
               //     {
               //         textbox_retour_bancdetest.Text = textbox_retour_bancdetest.Text + "\r\n lecture effectué";
               //     }
               // }
               // catch (Exception ex)
               // {

               //     System.Windows.Forms.MessageBox.Show("Erreur de lecture du fichier" + ex);
               // }

                try
                {
                    if (alim.fileReadModifierPiseo(nom_fichier, range, tension, intensite, delay) == true)
                    {
                        //System.Windows.MessageBox.Show("modification réussie  ");
                        textbox_retour_bancdetest.Text = textbox_retour_bancdetest.Text + "\r\n modication effectuée";
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    //System.Windows.MessageBox.Show("erreur de modification.");
                    textbox_retour_bancdetest.Text = textbox_retour_bancdetest.Text + "\r\n erreur de modification";
                }

                // DEBUT Thread Spectro
                //System.Threading.Thread myThreadHandle;
                //myThreadHandle = new System.Threading.Thread(() => spectroVirtuel.MeasureWithTrigger(10000));
                //myThreadHandle.Start();

                try
                {
                    if (alim.Send() == false)
                    {

                        throw new Exception();
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        //System.Windows.MessageBox.Show("Lancement des mesures");
                        textbox_retour_bancdetest.Text = textbox_retour_bancdetest.Text + "\r\n lancement des mesures";
                    }
                }
                catch (Exception ex)
                {
                    // System.Windows.MessageBox.Show("erreur d'envoie du script" + ex);
                    textbox_retour_bancdetest.Text = textbox_retour_bancdetest.Text + "\r\n erreur de lancement des mesures";
                }
                // FIN Thread Spectro
                //myThreadHandle.Abort();

                // RÉCUPÉRATION DES RÉSULTATS ET AFFICHAGE
                
                // Résultats Spectromètre
                resultatsSpectro = spectroVirtuel.GetResultSpectro();

                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nRésultats :"
                    + "Unité de calibration :" + resultatsSpectro.CalibrationUnit + ". Valeur :" + resultatsSpectro.MaxADCValue
                    + "\rUnité de radiométrie :" + resultatsSpectro.RadUnit + ". Valeur :" + resultatsSpectro.RadValue
                    + "\rUnité de photométrie :" + resultatsSpectro.PhotUnit + ". Valeur :" + resultatsSpectro.PhotValue;

                // Résultats Alimentation
                alim.Recevoir();
                string msg = alim.Returndata;

                textbox_retour_alim.Text = textbox_retour_alim.Text + "\r\nRésultats :" + msg;

                try
                {
                    string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "'");
                    string nom = T_F_exportation.Text + "\\MesureLED-Alimentation_" + date + ".csv";
                    alim.Exporter(@nom);
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void actu(object sender, RoutedEventArgs e)
        {
            T_IP.Text = "";
            T_port.Text = "";

        }
        private void valider(object sender, RoutedEventArgs e)
        {
            // Transformation d'une textbox en string 
            nom_fichier = string.Format(T_fichier.Text);
            /*
            // Appel de la fonction fileRead de la DLL
            bool lecture_fichier = alim.fileRead(nom_fichier);

            try
            {
                // Utilisation de la fonction
                if (lecture_fichier == false)
                {
                    throw new Exception();
                }
                else
                {
                    // Si le fichier est bien lu 
                    textbox_retour_bancdetest.Text = textbox_retour_bancdetest.Text + "\r\n Lecture effectuée";
                }
            }
            // L'erreur est signalée si il y en a une 
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erreur de lecture du fichier" + ex);
            }

            delay = string.Format(T_delay.Text);
            tension = string.Format(T_tension.Text);
            range = string.Format(T_range.Text);
            intensite = string.Format(T_intensite.Text);

            tension = alim.GetLimitV();
            delay = alim.GetDelay();
            intensite = alim.GetLevelI();
            range = alim.GetLimitI();

            try
            {
                if (alim.SetDelay(delay) == true &&
                 alim.SetLevelI(intensite) == true &&
                 alim.SetLimitI(range) == true &&
                 alim.SetLimitV(tension) == true)
                {
                    textbox_retour_alim.Text = textbox_retour_alim.Text + "\r\n paramètres: " + "\r\n tension: " + T_tension.Text + "\r\n intensité: " + T_intensite.Text + "\r\n range: " + T_range.Text + "\r\n delay: " + T_delay.Text;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                textbox_retour_alim.Text = textbox_retour_alim.Text + "\r\n Erreur de validation des paramètres";
            }
            */
        }

            private void actu_données(object sender, RoutedEventArgs e)
        {
            T_tension.Text = "";
            T_range.Text = "";
            T_intensite.Text = "";
        }

        /*
        *      CONFIGURATION DU SPECTROMÈTRE
        */
        private void bCréation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                interfaceSpectro = cbInterface.SelectedItem.ToString();
                
                spectroVirtuel = new Spectrometer(interfaceSpectro, tbFConfig.Text, tbFCalibration.Text, tbFTransmission.Text, true);
                
                parametresSpectro = new ConfigurationParameter();
                resultatsSpectro = new ResultSpectro();

                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nSpectromètre : Init ok !";
            }
            catch(NullReferenceException exReference)
            {
                // Log de l'erreur
                Log.LogWriter(exReference.Message, "Interface spectromètre");
                // Gestion de l'erreur
                System.Windows.MessageBox.Show("Erreur: Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\n Erreur : L'interface sélectionnée n'est pas valide : {0}." + exReference.Message;
            }
            catch (ArgumentException exArgument)
            {
                // Log de l'erreur
                Log.LogWriter(exArgument.Message, "Interface spectromètre");
                // Gestion de l'erreur
                System.Windows.MessageBox.Show("Erreur: Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\n Erreur : L'interface sélectionnée n'est pas valide : {0}." + exArgument.Message;
            }
            catch (BadImageFormatException exImage)
            {
                // Log de l'erreur
                Log.LogWriter(exImage.Message, "DLL Spectromètre");
                // Gestion de l'erreur
                System.Windows.MessageBox.Show("Erreur: Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\n Erreur : Problème avec la DLL : {0}." + exImage.Message;
            }
            catch (Exception ex)
            {
                // Log de l'erreur
                Log.LogWriter(ex.Message, "Création spectromètre");
                // Gestion de l'erreur
                System.Windows.MessageBox.Show("Erreur: Regardez la boîte d'information Spectromètre pour plus d'informations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\n Erreur lors de la création du spectromètre : {0}." + ex.Message;
            }
        }

        /*
         *      ACQUISITION DES DONNÉES LIÉES AU SPECTROMÈTRE
         */
        private void B_lancement_Spectro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                do
                {
                    if ((bool)(rbIntegrationTimeFixed.IsChecked = true))
                    {
                        // Chargement des valeurs du temps d'intégration
                        int tempsFixe = int.Parse(tbTempsInt.Text);
                        parametresSpectro.IntegrationTimeFixed = Convert.ToDouble(tempsFixe);
                        integrationTimeModeOn = true;
                        isIntegrationTimeCorrect = true;
                    }
                    else if ((bool)(rbIntegrationTimeAuto.IsChecked = true))
                    {
                        // Chargement des valeurs du temps d'intégration
                        parametresSpectro.ARMinLevel = Convert.ToDouble(tbNivSignalMin.Text);
                        parametresSpectro.ARMaxLevel = Convert.ToDouble(tbNivSignalMax.Text);
                        parametresSpectro.ARMaxIntegrationTime = Convert.ToDouble(tbIntTimeMax.Text);
                        parametresSpectro.ARAverages = Convert.ToDouble(tbIntTimeMoyenne.Text);
                        isIntegrationTimeCorrect = true;
                    }
                    else
                    {
                        textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nTemps d'intégration invalide";
                        integrationTimeModeOn = false;
                        isIntegrationTimeCorrect = false;
                    }
                }
                while (isIntegrationTimeCorrect != true);

                spectroVirtuel.setIntegrationTime(parametresSpectro);
                spectroVirtuel.MeasureWithTrigger(10000);
                resultatsSpectro = spectroVirtuel.GetResultSpectro();

                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\nRésultats :"
                    + "Unité de calibration :" + resultatsSpectro.CalibrationUnit + ". Valeur :" + resultatsSpectro.MaxADCValue
                    + "\rUnité de radiométrie :" + resultatsSpectro.RadUnit + ". Valeur :" + resultatsSpectro.RadValue
                    + "\rUnité de photométrie :" + resultatsSpectro.PhotUnit + ". Valeur :" + resultatsSpectro.PhotValue;
            }
            catch (Exception ex)
            {
                // Log de l'erreurs
                Log.LogWriter(ex.Message, "Lancement de la mesure");
                // Gestion de l'erreur
                textbox_retour_spectro.Text = textbox_retour_spectro.Text + "\r\n Erreur lors du lancement de la mesure : {0}." + ex.Message;
            }
        }

        // Téléchargement des fichiers CSV
        private void B_Exporter_Click(object sender, RoutedEventArgs e)
        {
            alim.Exporter(T_F_exportation.Text);
        }

        // Choix du dossier de destination des résultats
        private void bDestination_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                string path = dialog.SelectedPath;
                T_F_exportation.Text = path;
            }
        }
    }
}