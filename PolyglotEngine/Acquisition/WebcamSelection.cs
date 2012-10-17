using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyglotFramework.Acquisition
{
    public partial class WebcamSelection : Form
    {

        private string[] options;
        public string[] Options
        {
            get { return options; }
            set
            {
                foreach (string option in value) webCamComboBox.Items.Add(option);
                options = value;
            }
        }

        public int Choice { get; private set; }

        public WebcamSelection()
        {
            InitializeComponent();
        }

        private void webCamComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (webCamComboBox.SelectedIndex != -1)
            {
                this.Choice = webCamComboBox.SelectedIndex;
                this.confirmationButton.Enabled = true;
            }
        }

        private void confirmationButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
