using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLLSpectrometer;
using System.Diagnostics;
using System.IO;

namespace IHM_final
{
    public class Log
    {
        public static void LogWriter(string messageErreur, string causeErreur)
        {
            DateTime date = DateTime.Now;
            // Choix du format de la date
            string shortDate = String.Format("{0:dd/MM/yyyy}", date);
            // Choix du format du nom de fichier (date.log)
            string filename = string.Format("{0}.log", shortDate);
            // Création du nom de fichier .log   
            filename = filename.Replace("/", "-");
            // Récupérer le path complet
            string rootPath = Path.GetFullPath("./Data/Log/");
            // Création du chemin complet du fichier
            string fullFilename = string.Format(@"{0}{1}", rootPath, filename);
            // Vérifier l'existence des dossiers Data et Log
            if (!Directory.Exists(rootPath))
            {
                // Création du dossier
                Directory.CreateDirectory(rootPath);
            }
            // Vérification de l'existence du fichier
            if (!File.Exists(fullFilename))
            {
                // Création du fichier log du jour
                FileStream logFile = File.Create(fullFilename);
                logFile.Close();
            }
            using (StreamWriter writer = new StreamWriter(fullFilename, true))
            {
                // Écriture dans le fichier log du jour
                writer.WriteLine(string.Format(
                                       "[{0} ON {1}] : {2}",
                                       DateTime.Now,
                                       causeErreur,
                                       messageErreur));
            }
        }
    }
}
