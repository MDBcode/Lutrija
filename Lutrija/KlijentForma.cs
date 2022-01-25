using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lutrija
{
    public partial class KlijentForma : Form
    {
        public PoslovnicaForma poslovnica;
        List<int> izvuceni;
        List<int> brojeviNaListicu;
        DateTime vrijemeUplateBingoListica;
        Boolean bingoAlert;
        public KlijentForma(PoslovnicaForma pf)
        {
            InitializeComponent();
            SuspendLayout();

            this.poslovnica = pf;
            izvuceni = new List<int>();
            brojeviNaListicu = new List<int>();
            bingoAlert = false;
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
            this.brojeviNaListicu.Clear();
            this.izvuceni.Clear();
            this.bingoAlert = false;
            this.vrijemeUplateBingoListica = DateTime.Now;
            this.poslovnica.textBoxIzvuceniBrojevi.Text = "";
            this.poslovnica.labelRedniIzvuceni.Text = "0";
            this.poslovnica.buttonIzvlacenjeBinga.Enabled = true;
            this.tableListićBingo.Visible = false;
            Random r = new Random();
            for (int i = 1; i <= 25; i++) {
                var labela = tableListićBingo.Controls["label" + i.ToString()];
                if (i == 13) labela.Text = ""; // središnja ćelija, ona je prazna
                else {
                    int randBroj = r.Next(1, 75 + 1);
                    while (this.brojeviNaListicu.Contains(randBroj)) randBroj = r.Next(1, 75 + 1);
                    this.brojeviNaListicu.Add(randBroj);
                    labela.Text = randBroj.ToString();
                    labela.ForeColor = Color.Black;    
                }
            }
            this.tableListićBingo.Visible = true;
        }

        public Boolean provjeriRetke(ref Boolean bingoAlert)
        {
            int pogodenihRedova = 4;
            for (int i = 0; i < 5; i++)
            {
                if (i != 2)
                {
                    Boolean pogoden = true;
                    for (int j = 5 * i + 1; j <= 5 * i + 5; j++)
                    {
                        var label = tableListićBingo.Controls["label" + j.ToString()];
                        if (j == 13) continue;
                        if (label.ForeColor != Color.Green)
                        {
                            pogoden = false;
                            pogodenihRedova--;
                            break;
                        }
                    }
                    if (pogoden)
                    {
                        if (!bingoAlert)
                        {
                            MessageBox.Show("Listić je dobitan! Pogođen redak.");
                            //TODO spremi u bazu
                            bingoAlert = true;
                            //break;

                        }
                    }
                }
            }
            if (pogodenihRedova == 4) return true;
            return false;
        }

        public Boolean provjeriStupce(ref Boolean bingoAlert)
        {
            int pogodenihStupaca = 4;
            for (int i = 0; i < 5; i++)
            {
                if (i != 2)
                {
                    Boolean pogoden = true;
                    for (int j = i + 1; j <= 20 + i + 1; j += 5)
                    {
                        var label = tableListićBingo.Controls["label" + j.ToString()];
                        if (j == 13) continue;
                        if (label.ForeColor != Color.Green)
                        {
                            pogoden = false;
                            pogodenihStupaca--;
                            break;
                        }
                    }
                    if (pogoden)
                    {
                        if (!bingoAlert)
                        {
                            MessageBox.Show("Listić je dobitan! Pogođen stupac.");
                            //TODO spremi ga u bazu
                            bingoAlert = true;
                            //break;
                        }
                    }
                }
            }
            if (pogodenihStupaca == 4) return true;
            return false;
        }
        public Boolean provjeriPlus(ref Boolean bingoAlert)
        {
            Boolean pogoden = true;
            for (int j = 11; j <= 15; j++)
            {
                var label = tableListićBingo.Controls["label" + j.ToString()];
                if (j == 13) continue;
                if (label.ForeColor != Color.Green)
                {
                    pogoden = false;
                    break;
                }
            }
            if (pogoden)
            {
                for (int j = 3; j <= 23; j += 5)
                {
                    var label = tableListićBingo.Controls["label" + j.ToString()];
                    if (j == 13) continue;
                    if (label.ForeColor != Color.Green)
                    {
                        pogoden = false;
                        break;
                    }
                }
                if (pogoden)
                {
                    if (!bingoAlert)
                    {
                        MessageBox.Show("Listić je dobitan! Dobitak druge vrste - PLUS.");
                        bingoAlert = true;

                    }
                }
            }
            if (pogoden) return true;
            return false;
        }
        public Boolean provjeriKuteve(ref Boolean bingoAlert)
        {
            Boolean pogoden = false;
            if (tableListićBingo.Controls["label1"].ForeColor == Color.Green && tableListićBingo.Controls["label5"].ForeColor == Color.Green
                && tableListićBingo.Controls["label21"].ForeColor == Color.Green && tableListićBingo.Controls["label25"].ForeColor == Color.Green)
            {
                pogoden = true;
                if (!bingoAlert)
                {
                    MessageBox.Show("Listić je dobitan! Dobitak treće vrste - KUTOVI.");
                    bingoAlert = true;
                }
            }
            if (pogoden) return true;
            return false;
        }

        public void izvlacenjeBinga() {
            var r = new Random();
            for (int ukupnoIzvucenih=0; ukupnoIzvucenih<10; ukupnoIzvucenih++) { 
                int randBroj = r.Next(1,75+1);
                while (this.izvuceni.Contains(randBroj) && this.izvuceni.Count < 75) randBroj = r.Next(1, 75 + 1);
                this.izvuceni.Add(randBroj);
                Thread.Sleep(200);
                poslovnica.zapisiIzvuceniBroj(randBroj, this.izvuceni.Count);
                poslovnica.Refresh();

                for (int i=1; i<=25; i++) {
                    if (i != 13)
                    {
                        var label = tableListićBingo.Controls["label" + i.ToString()];
                        if (label.Text.Equals(randBroj.ToString()))
                        {
                            label.ForeColor = Color.Green;
                            this.Refresh();
                        }
                    }
                }
                // pronađi postoji li dobitan stupac
                Boolean sviStupciPogodeni = provjeriStupce(ref bingoAlert);

                // je li bingo u znaku "plus"...srednji redak i srednji stupac
                Boolean pogodenPlus = provjeriPlus(ref bingoAlert);
                if (sviStupciPogodeni && pogodenPlus)
                {
                    MessageBox.Show("BINGO!");
                    this.poslovnica.buttonIzvlacenjeBinga.Enabled = false;
                    this.poslovnica.spremiBingoListicUBazu(this.vrijemeUplateBingoListica, this.brojeviNaListicu);
                    break;
                }
                else {
                    // pronađi postoji li dobitan redak
                    Boolean sviRedoviPogodeni = provjeriRetke(ref bingoAlert);
                    // jesu li pogođeni kutovi
                    Boolean pogodeniKutevi = provjeriKuteve(ref bingoAlert);
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

        private void KlijentForma_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
