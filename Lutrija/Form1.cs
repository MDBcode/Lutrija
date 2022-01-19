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
    public partial class Form_unos_loto : Form
    {
        public static int[] brojevi = new int[6];
        public Form_unos_loto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j = 0;
            foreach (NumericUpDown n in this.Controls.OfType<NumericUpDown>())
            {
                brojevi[j] = (int)n.Value;
                j++;
            }

            Array.Sort(brojevi);

            int brojac_ispravnih = 0;
            for (int x = 0; x < 5; x++)
            {
                if (brojevi[x] == brojevi[x + 1])
                {
                    MessageBox.Show("Ne možete unijeti više istih brojeva!");
                    break;
                }
                else brojac_ispravnih++;
            }
            if (brojac_ispravnih == 5) 
                this.Close();
            else
            {
                int k = 6;
                foreach (NumericUpDown n in this.Controls.OfType<NumericUpDown>())
                {
                    n.Value = k;
                    k--;
                    brojevi[k] = k+1;
                }
            }
        }
    }
}
