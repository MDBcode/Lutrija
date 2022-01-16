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
    public partial class PoslovnicaForma : Form
    {
        public KlijentForma klijent = null;
        public PoslovnicaForma()
        {
            InitializeComponent();
            klijent = new KlijentForma(this);
            klijent.Show();
        }

        private void buttonIzvlacenjeBinga_Click(object sender, EventArgs e) => klijent.izvlacenjeBinga();
    }
}
