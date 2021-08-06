/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Seppel
 * Datum: 08.06.2010
 * Zeit: 17:08
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace test
{
    partial class application
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(application));
            this.interfaceType = new System.Windows.Forms.ComboBox();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.cbInterfaceOption = new System.Windows.Forms.ComboBox();
            this.creerSpectro = new System.Windows.Forms.Button();
            this.mesure = new System.Windows.Forms.Button();
            this.callab = new System.Windows.Forms.Label();
            this.conflab = new System.Windows.Forms.Label();
            this.calibbox = new System.Windows.Forms.TextBox();
            this.configbox = new System.Windows.Forms.TextBox();
            this.btnBrowseForConfig = new System.Windows.Forms.Button();
            this.btnBrowseForCalib = new System.Windows.Forms.Button();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.confparametre = new System.Windows.Forms.Button();
            this.resultat = new System.Windows.Forms.Button();
            this.detrireSpectro = new System.Windows.Forms.Button();
            this.infospectro = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // interfaceType
            // 
            this.interfaceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.interfaceType.FormattingEnabled = true;
            this.interfaceType.Location = new System.Drawing.Point(1098, 50);
            this.interfaceType.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.interfaceType.Name = "interfaceType";
            this.interfaceType.Size = new System.Drawing.Size(361, 31);
            this.interfaceType.TabIndex = 3;
            // 
            // OFD
            // 
            this.OFD.FileName = "openFileDialog1";
            this.OFD.Filter = "INI files (*.ini)|*.ini|ISC files (*.isc)|*.isc|All files (*.*)|*.*";
            // 
            // cbInterfaceOption
            // 
            this.cbInterfaceOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterfaceOption.FormattingEnabled = true;
            this.cbInterfaceOption.Location = new System.Drawing.Point(1167, 9);
            this.cbInterfaceOption.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.cbInterfaceOption.Name = "cbInterfaceOption";
            this.cbInterfaceOption.Size = new System.Drawing.Size(292, 31);
            this.cbInterfaceOption.TabIndex = 4;
            // 
            // creerSpectro
            // 
            this.creerSpectro.Location = new System.Drawing.Point(199, 105);
            this.creerSpectro.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.creerSpectro.Name = "creerSpectro";
            this.creerSpectro.Size = new System.Drawing.Size(184, 88);
            this.creerSpectro.TabIndex = 8;
            this.creerSpectro.Text = "creer spectrometre";
            this.creerSpectro.UseVisualStyleBackColor = true;
            this.creerSpectro.Click += new System.EventHandler(this.click_creerspetro);
            // 
            // mesure
            // 
            this.mesure.Location = new System.Drawing.Point(397, 105);
            this.mesure.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.mesure.Name = "mesure";
            this.mesure.Size = new System.Drawing.Size(184, 88);
            this.mesure.TabIndex = 9;
            this.mesure.Text = "lancer une mesure";
            this.mesure.UseVisualStyleBackColor = true;
            this.mesure.Click += new System.EventHandler(this.LancerMesure);
            // 
            // callab
            // 
            this.callab.AutoSize = true;
            this.callab.Location = new System.Drawing.Point(87, 63);
            this.callab.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.callab.Name = "callab";
            this.callab.Size = new System.Drawing.Size(220, 23);
            this.callab.TabIndex = 11;
            this.callab.Text = "fichier de calibration";
            // 
            // conflab
            // 
            this.conflab.AutoSize = true;
            this.conflab.Location = new System.Drawing.Point(76, 9);
            this.conflab.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.conflab.Name = "conflab";
            this.conflab.Size = new System.Drawing.Size(249, 23);
            this.conflab.TabIndex = 12;
            this.conflab.Text = "fichier de configuration";
            // 
            // calibbox
            // 
            this.calibbox.Location = new System.Drawing.Point(339, 54);
            this.calibbox.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.calibbox.Name = "calibbox";
            this.calibbox.Size = new System.Drawing.Size(721, 27);
            this.calibbox.TabIndex = 13;
            // 
            // configbox
            // 
            this.configbox.Location = new System.Drawing.Point(339, 5);
            this.configbox.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.configbox.Name = "configbox";
            this.configbox.Size = new System.Drawing.Size(752, 27);
            this.configbox.TabIndex = 14;
            // 
            // btnBrowseForConfig
            // 
            this.btnBrowseForConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseForConfig.Location = new System.Drawing.Point(1, 3);
            this.btnBrowseForConfig.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.btnBrowseForConfig.Name = "btnBrowseForConfig";
            this.btnBrowseForConfig.Size = new System.Drawing.Size(61, 41);
            this.btnBrowseForConfig.TabIndex = 15;
            this.btnBrowseForConfig.Text = "...";
            this.btnBrowseForConfig.UseVisualStyleBackColor = true;
            this.btnBrowseForConfig.Click += new System.EventHandler(this.btnBrowseForConfig_Click);
            // 
            // btnBrowseForCalib
            // 
            this.btnBrowseForCalib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseForCalib.Location = new System.Drawing.Point(1, 54);
            this.btnBrowseForCalib.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.btnBrowseForCalib.Name = "btnBrowseForCalib";
            this.btnBrowseForCalib.Size = new System.Drawing.Size(61, 41);
            this.btnBrowseForCalib.TabIndex = 16;
            this.btnBrowseForCalib.Text = "...";
            this.btnBrowseForCalib.UseVisualStyleBackColor = true;
            this.btnBrowseForCalib.Click += new System.EventHandler(this.btnBrowseForCalib_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(595, 176);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(173, 17);
            this.hScrollBar1.TabIndex = 17;
            this.hScrollBar1.Value = 50;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(652, 193);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 23);
            this.label5.TabIndex = 18;
            this.label5.Text = "temp";
            // 
            // confparametre
            // 
            this.confparametre.Location = new System.Drawing.Point(595, 105);
            this.confparametre.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.confparametre.Name = "confparametre";
            this.confparametre.Size = new System.Drawing.Size(184, 48);
            this.confparametre.TabIndex = 19;
            this.confparametre.Text = "setParametre";
            this.confparametre.UseVisualStyleBackColor = true;
            this.confparametre.Click += new System.EventHandler(this.definirParametre);
            // 
            // resultat
            // 
            this.resultat.Location = new System.Drawing.Point(991, 105);
            this.resultat.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.resultat.Name = "resultat";
            this.resultat.Size = new System.Drawing.Size(184, 88);
            this.resultat.TabIndex = 20;
            this.resultat.Text = "resultat";
            this.resultat.UseVisualStyleBackColor = true;
            this.resultat.Click += new System.EventHandler(this.Resultat);
            // 
            // detrireSpectro
            // 
            this.detrireSpectro.Location = new System.Drawing.Point(793, 105);
            this.detrireSpectro.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.detrireSpectro.Name = "detrireSpectro";
            this.detrireSpectro.Size = new System.Drawing.Size(184, 88);
            this.detrireSpectro.TabIndex = 22;
            this.detrireSpectro.Text = "detruire spectrometre";
            this.detrireSpectro.UseVisualStyleBackColor = true;
            this.detrireSpectro.Click += new System.EventHandler(this.detrireSpectro_Click);
            // 
            // infospectro
            // 
            this.infospectro.AutoSize = true;
            this.infospectro.Location = new System.Drawing.Point(16, 464);
            this.infospectro.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.infospectro.Name = "infospectro";
            this.infospectro.Size = new System.Drawing.Size(71, 23);
            this.infospectro.TabIndex = 23;
            this.infospectro.Text = "label6";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1, 105);
            this.button1.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 88);
            this.button1.TabIndex = 24;
            this.button1.Text = "boucle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1189, 105);
            this.button2.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 88);
            this.button2.TabIndex = 25;
            this.button2.Text = "multitrack";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(793, 317);
            this.button3.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(184, 88);
            this.button3.TabIndex = 26;
            this.button3.Text = "copy data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(991, 317);
            this.button5.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(184, 88);
            this.button5.TabIndex = 28;
            this.button5.Text = "trigger";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(397, 301);
            this.button7.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(184, 88);
            this.button7.TabIndex = 30;
            this.button7.Text = "faire action";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(595, 317);
            this.button8.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(184, 88);
            this.button8.TabIndex = 31;
            this.button8.Text = "save data";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(199, 301);
            this.button9.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(184, 88);
            this.button9.TabIndex = 32;
            this.button9.Text = "initialiser buffer";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1, 301);
            this.button10.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(184, 88);
            this.button10.TabIndex = 33;
            this.button10.Text = "obtenir nom interface";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(1189, 219);
            this.button11.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(184, 88);
            this.button11.TabIndex = 34;
            this.button11.Text = "etat spectro";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(991, 219);
            this.button12.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(184, 88);
            this.button12.TabIndex = 35;
            this.button12.Text = "données acquis";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(793, 219);
            this.button13.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(184, 88);
            this.button13.TabIndex = 36;
            this.button13.Text = "assignier propriete spectro";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(595, 221);
            this.button14.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(184, 88);
            this.button14.TabIndex = 37;
            this.button14.Text = "set numerique parametre";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(397, 203);
            this.button15.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(184, 88);
            this.button15.TabIndex = 38;
            this.button15.Text = "obtenir numeric parametre";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(199, 203);
            this.button16.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(184, 88);
            this.button16.TabIndex = 39;
            this.button16.Text = "obtenir parametre spectro";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(1, 203);
            this.button17.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(184, 88);
            this.button17.TabIndex = 40;
            this.button17.Text = "chnger spectro";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1610, 722);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.infospectro);
            this.Controls.Add(this.detrireSpectro);
            this.Controls.Add(this.resultat);
            this.Controls.Add(this.confparametre);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.btnBrowseForCalib);
            this.Controls.Add(this.btnBrowseForConfig);
            this.Controls.Add(this.configbox);
            this.Controls.Add(this.calibbox);
            this.Controls.Add(this.conflab);
            this.Controls.Add(this.callab);
            this.Controls.Add(this.mesure);
            this.Controls.Add(this.creerSpectro);
            this.Controls.Add(this.interfaceType);
            this.Controls.Add(this.cbInterfaceOption);
            this.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.MinimumSize = new System.Drawing.Size(926, 437);
            this.Name = "application";
            this.Text = "CAS4 DLL C# Demo Application";
            this.Shown += new System.EventHandler(this.MainFormShown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.ComboBox interfaceType;
        private System.Windows.Forms.ComboBox cbInterfaceOption;
        private System.Windows.Forms.Button creerSpectro;
        private System.Windows.Forms.Button mesure;
        private System.Windows.Forms.Label callab;
        private System.Windows.Forms.Label conflab;
        private System.Windows.Forms.TextBox calibbox;
        private System.Windows.Forms.TextBox configbox;
        private System.Windows.Forms.Button btnBrowseForConfig;
        private System.Windows.Forms.Button btnBrowseForCalib;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button confparametre;
        private System.Windows.Forms.Button resultat;
        private System.Windows.Forms.Button detrireSpectro;
        private System.Windows.Forms.Label infospectro;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
    }
}
