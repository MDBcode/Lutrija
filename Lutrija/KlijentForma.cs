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
    public partial class KlijentForma : Form
    {
        public KlijentForma()
        {
            InitializeComponent();
            SuspendLayout();

            stilizirajBingoTablicu();
            stilizirajLotoTablicu();

            ResumeLayout();
        }

        public void stilizirajLotoTablicu()
        {
            for (int j = 26; j <= 31; j++)
            {
                var label = table_loto.Controls["label" + j.ToString()];
                label.Text = "";
                Padding pad = new Padding();
                pad.Left = label.Parent.Size.Width / 25;
                pad.Top = label.Parent.Size.Height / 3;
                label.Padding = pad;
                Font font = new Font(label.Font, FontStyle.Bold);
                label.Font = font;
                Size velicina = new Size();
                velicina.Width = label.Parent.Width;
                velicina.Height = label.Parent.Height;
                label.Size = velicina;
            }
            generiraj_joker_brojeve();
        }

        public void generiraj_joker_brojeve()
        {
            Random rnd = new Random();
            for (int k = 32; k <= 37; k++)
            {
                var label = table_joker.Controls["label" + k.ToString()];

                int broj = rnd.Next(0, 10);
                label.Text = broj.ToString();

                Padding pad = new Padding();
                pad.Left = label.Parent.Size.Width / 25;
                pad.Top = label.Parent.Size.Height / 3;
                label.Padding = pad;
                Font font = new Font(label.Font, FontStyle.Bold);
                label.Font = font;
                Size velicina = new Size();
                velicina.Width = label.Parent.Width;
                velicina.Height = label.Parent.Height;
                label.Size = velicina;
            }
        }

        public void stilizirajBingoTablicu() {
            for (int i = 1; i <= 25; i++)
            {
                var label = tableListićBingo.Controls["label" + i.ToString()];
                label.Text = ""; //inicijaliziraj prazan bingo listić
                Padding pad = new Padding();
                pad.Left = label.Parent.Size.Width / 5 / 4; //5 ćelija, /4 je namješteno
                pad.Top = label.Parent.Size.Height / 5 / 4;
                label.Padding = pad;
                Font font = new Font(label.Font, FontStyle.Bold);
                label.Font = font;
                Size velicina = new Size();
                velicina.Width = label.Parent.Width / 5;
                velicina.Height = label.Parent.Height / 5;
                label.Size = velicina;
            }
        }

        //public event EventHandler kreirajBingo; // trebalo bi sa eventima

        public void kreirajBingoListic()
        {
            Random r = new Random();
            for (int i = 1; i <= 25; i++) {
                var labela = tableListićBingo.Controls["label" + i.ToString()];
                if(i==13) labela.Text = ""; // središnja ćelija, ona je prazna
                else labela.Text = r.Next(1,75+1).ToString();
                labela.ForeColor = Color.Black;
            }
        }

        public void izvlacenjeBinga() {
            int ukupno = 0;
            List<int> brojevi = new List<int>();
            var r = new Random();
            Boolean bingo = true;
            Boolean bingoAlert = false;
            while (ukupno < 40) // zašto 40?
            {
                int rand = r.Next(1,75+1);
                if (!brojevi.Contains(rand)) brojevi.Add(rand);
                else
                {
                    while (!brojevi.Contains(rand)) rand = r.Next(1, 75 + 1);
                }
                ukupno++;
                for (int i=1; i<=25; i++) {
                    if (i != 13) {
                        var label = tableListićBingo.Controls["label" + i.ToString()];
                        if (label.Text.Equals(rand.ToString())) label.ForeColor = Color.Green;
                    }
                }
            }
            // pronađi postoji li dobitan redak
            for (int i=0; i<5; i++) {
                for (int j = 5*i+1; j <= 5*i+5; j++) {
                    var label = tableListićBingo.Controls["label" + j.ToString()];
                    if (j == 13) continue;
                    if (label.ForeColor != Color.Green) {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                {
                    if(!bingoAlert) MessageBox.Show("Bingo! Listić je dobitan.");
                    bingoAlert = true; 
                    break;
                }
            }
            // pronađi postoji li dobitan stupac
            for (int i=0; i<5; i++)
            {
                for (int j=i+1; j<=20+i+1; j+=5)
                {
                    var label = tableListićBingo.Controls["label" + j.ToString()];
                    if (j == 13) continue;
                    if (label.ForeColor != Color.Green)
                    {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                {
                    if (!bingoAlert) MessageBox.Show("Bingo! Listić je dobitan.");
                    bingoAlert = true;
                    break;
                }
            }
            if (tableListićBingo.Controls["label1"].ForeColor == Color.Green && tableListićBingo.Controls["label5"].ForeColor == Color.Green
                && tableListićBingo.Controls["label21"].ForeColor == Color.Green && tableListićBingo.Controls["label25"].ForeColor == Color.Green) {
                if (!bingo)
                {
                    bingo = true;
                    if (!bingoAlert) MessageBox.Show("Bingo! Listić je dobitan.");
                    bingoAlert = true;
                }
            }

        }

        private void buttonKreirajBingoListic_Click(object sender, EventArgs e)
        {
            /*decimal iznos; //ovako ne treba jer je iznos bingo listića fiksne cijene
            if (textBoxIznosUplateBingo.Text.Equals("")) MessageBox.Show("Unesite iznos uplate.");
            else {
                if (Decimal.TryParse(textBoxIznosUplateBingo.Text, out iznos)) kreirajBingoListic();
                else MessageBox.Show("Krivi format unosa.");
            } */
            kreirajBingoListic();
        }

        private void generiraj_joker_Click(object sender, EventArgs e)
        {
            generiraj_joker_brojeve();
        }

        private void povecaj_Click(object sender, EventArgs e)
        {
            var label = table_joker.Controls["label" + (sender as Button).Tag.ToString()];
            int broj = Int16.Parse(label.Text);
            broj++;
            if (broj == 10) broj = 0;
            label.Text = broj.ToString();
        }

        private void smanji_Click(object sender, EventArgs e)
        {
            var label = table_joker.Controls["label" + (sender as Button).Tag.ToString()];
            int broj = Int16.Parse(label.Text);
            broj--;
            if (broj == -1) broj = 9;
            label.Text = broj.ToString();
        }

        private void generiraj_loto_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int[] brojevi = new int[6];
            for (int i = 1; i <= 6; i++)
            {
                int broj = rnd.Next(1, 46);
                brojevi[i-1] = broj;
            }

            Array.Sort(brojevi);
            //pogledam da nije izgenerirao više istih
            for (int x = 0; x < 5; x++)
            {
                if (brojevi[x] == brojevi[x + 1])
                    brojevi[x + 1]++;
                if (brojevi[x] > brojevi[x + 1])
                    brojevi[x + 1] = brojevi[x] + 1;
            }

            int brojac = 26;
            foreach(int value in brojevi)
            {
                var label = table_loto.Controls["label" + brojac.ToString()];
                label.Text = value.ToString();
                brojac++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var forma = new Form_unos_loto();
            forma.ShowDialog();

            for(int i=0; i<6; i++)
            {
                table_loto.Controls["label" + (i+26).ToString()].Text = Form_unos_loto.brojevi[i].ToString();
            }
        }

    }
}
