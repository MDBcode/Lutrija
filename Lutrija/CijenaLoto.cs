using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lutrija
{
    public partial class CijenaLoto : Form
    {
        public CijenaLoto( bool joker_loto)
        {
            InitializeComponent();
            if (joker_loto == false)
            {
                labelJoker.Visible = false;
                labelJokerCijena.Visible = false;
                label_cijena.Text = "2,00 kn";
            }
            else label_cijena.Text = "7,00 kn";
        }

        private void button_potvrdaCijene_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
