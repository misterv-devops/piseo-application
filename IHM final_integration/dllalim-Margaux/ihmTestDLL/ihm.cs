using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// using en +
using testDLLAlim;

namespace ihmTestDLL
{
    public partial class ihm : Form
    {
        public ihm()
        {
            InitializeComponent();
            DLLAlim dllAlim = new DLLAlim();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool connection = DLLAlim.Connect("192.168.1.2", 5025);
            if (connection == true)
            {
                MessageBox.Show("Connected to the device !");
            }
            else
            {
                MessageBox.Show("Connection error...");
            }
        }
    }
}
