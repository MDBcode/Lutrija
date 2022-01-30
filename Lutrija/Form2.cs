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
    public partial class Form_unos_ej : Form
    {
        public Form_unos_ej()
        {
            InitializeComponent();
        }
        public static int[] brojevi1 = new int[5];
        public static int[] brojevi2 = new int[2];
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            int j = 0;
            foreach (NumericUpDown n in this.Controls.OfType<NumericUpDown>())
            {
                if(j<5)
                    brojevi1[j] = (int)n.Value;
                else
                    brojevi2[j-5] = (int)n.Value;
                j++;
            }


            int brojac_ispravnih = 0;
            int c;
            for (int x = 0; x < 4; x++)
            {
                c = 1;
                for (int i = x+1; i < 5; i++)
                    if (brojevi1[x] == brojevi1[i])
                        c = 0;
                if(c==0)
                {
                    MessageBox.Show("Ne možete unijeti više istih brojeva!");
                    break;
                }
                else
                    brojac_ispravnih++;
            }
            if (brojevi2[0] == brojevi2[1])
            {
                MessageBox.Show("Ne možete unijeti više istih brojeva!");
                brojevi2[1]++;
            }
            
            if (brojac_ispravnih == 4)
                this.Close();
            else
            {
                int k = 5;
                int i = 1;
                foreach (NumericUpDown n in this.Controls.OfType<NumericUpDown>())
                {
                    if (i < 6)
                    {
                        n.Value = k;
                        k--;
                        brojevi1[k] = k + 1;
                    }
                    i++;
                }
            }

            Array.Sort(brojevi1);
            Array.Sort(brojevi2);


        }
}
}
