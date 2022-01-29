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
using System.IO;
using System.Xml.Linq;

namespace Lutrija
{
    public partial class KlijentForma : Form
    {
        public PoslovnicaForma poslovnica;

        /*----------------------------------------------------------------------------------------------
                                                BINGO
         -----------------------------------------------------------------------------------------------*/
        List<int> izvuceni;
        List<int> brojeviNaListicu;
        Boolean bingoAlert;

        /*----------------------------------------------------------------------------------------------
                                               LOTO
        -----------------------------------------------------------------------------------------------*/
        List<int> izvuceniLoto;
        List<int> brojeviNaLotoListicu;
        List<int> pogodeniLotoBrojevi;
        List<int> odabraniJoker;
        List<int> izvuceniJoker;
        DateTime vrijemeUplateLotoListica;
        Boolean dobitan_loto;
        public Boolean joker_loto;
        Dictionary<int, int> statistika;

        /*----------------------------------------------------------------------------------------------
                                            KLADIONICA
        -----------------------------------------------------------------------------------------------*/
        List<string> ponuda;
        List<string> koef1;
        List<string> koefx;
        List<string> koef2;
        List<(string, string)> listic;
        DateTime vrijemeUplateKladionicaListica;
        string pathPonuda;
        XDocument docPonuda;
        /*----------------------------------------------------------------------------------------------
                                            EUROJACKPOT
        -----------------------------------------------------------------------------------------------*/
        List<int> brojeviNaListicuEJ;
        List<int> pogodeniEJBrojevi;
        List<int> izvuceniEJ;
        DateTime vrijemeUplateBingoListica;
        DateTime vrijemeUplateEJListica;
        Boolean dobitan_joker;
        Boolean dobitan_ej;

        public KlijentForma(PoslovnicaForma pf)
        {
            InitializeComponent();
            SuspendLayout();

            this.poslovnica = pf;

         /*----------------------------------------------------------------------------------------------
                                                BINGO
         -----------------------------------------------------------------------------------------------*/
            izvuceni = new List<int>();
            brojeviNaListicu = new List<int>();
            bingoAlert = false;
            stilizirajBingoTablicu();
         /*----------------------------------------------------------------------------------------------
                                                LOTO
         -----------------------------------------------------------------------------------------------*/
            izvuceniLoto = new List<int>();
            brojeviNaLotoListicu = new List<int>();
            pogodeniLotoBrojevi = new List<int>();
            odabraniJoker = new List<int>();
            izvuceniJoker = new List<int>();
            statistika = new Dictionary<int, int>();
            joker_loto = false;
            stilizirajLotoTablicu();

            /*----------------------------------------------------------------------------------------------
                                                KLADIONICA
            -----------------------------------------------------------------------------------------------*/
            ponuda = new List<string>();
            koef1 = new List<string>();
            koefx = new List<string>();
            koef2 = new List<string>();
            listic = new List<(string, string)>();
            prikaziponudu();
            /*----------------------------------------------------------------------------------------------
                                                EUROJACKPOT
            -----------------------------------------------------------------------------------------------*/
            brojeviNaListicuEJ = new List<int>();
            pogodeniEJBrojevi = new List<int>();
            izvuceniEJ = new List<int>(); 
            stilizirajEJ();

            ResumeLayout();
        }

        /*----------------------------------------------------------------------------------------------
                                                BINGO
        -----------------------------------------------------------------------------------------------*/
        public void stilizirajBingoTablicu()
        {
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
            for (int i = 1; i <= 25; i++)
            {
                var labela = tableListićBingo.Controls["label" + i.ToString()];
                if (i == 13) labela.Text = ""; // središnja ćelija, ona je prazna
                else
                {
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

        public void izvlacenjeBinga()
        {
            var r = new Random();
            for (int ukupnoIzvucenih = 0; ukupnoIzvucenih < 10; ukupnoIzvucenih++)
            {
                int randBroj = r.Next(1, 75 + 1);
                while (this.izvuceni.Contains(randBroj) && this.izvuceni.Count < 75) randBroj = r.Next(1, 75 + 1);
                this.izvuceni.Add(randBroj);
                Thread.Sleep(200);
                poslovnica.zapisiIzvuceniBroj(randBroj, this.izvuceni.Count);
                poslovnica.Refresh();

                for (int i = 1; i <= 25; i++)
                {
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
                else
                {
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

        /*----------------------------------------------------------------------------------------------
                                                LOTO
         -----------------------------------------------------------------------------------------------*/
        public void stilizirajLotoTablicu()
        {
            for (int j = 26; j <= 31; j++)
            {
                var label = table_loto.Controls["label" + j.ToString()];
                label.Text = "";
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
            }
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
            buttonIGRAJ.Visible = true;
            Random rnd = new Random();
            int[] brojevi = new int[6];
            for (int i = 1; i <= 6; i++)
            {
                int broj = rnd.Next(1, 46);
                if (brojevi.Contains(broj)) broj = rnd.Next(1, 45 + 1);
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
                label.ForeColor = Color.Black;
                brojac++;
            }
        }

        private void button_odaberi_Click(object sender, EventArgs e)
        {
            var forma = new Form_unos_loto();
            forma.ShowDialog();
            DialogResult result = forma.DialogResult;
            if (result == DialogResult.OK)
            {
                buttonIGRAJ.Visible = true;
                for (int i = 0; i < 6; i++)
                {
                    table_loto.Controls["label" + (i + 26).ToString()].Text = Form_unos_loto.brojevi[i].ToString();
                }
            }
        }

        private void KlijentForma_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void izvlacenjeLota()
        {
            var r = new Random();
            int broji_dobitne = 0;
            for (int ukupnoIzvucenih = 0; ukupnoIzvucenih < 6; ukupnoIzvucenih++)
            {
                int randBroj = r.Next(1, 45 + 1);
                if (this.izvuceniLoto.Contains(randBroj)) randBroj = r.Next(1, 45 + 1);
                this.izvuceniLoto.Add(randBroj);
                Thread.Sleep(500);
                poslovnica.zapisiIzvuceniLotoBroj(randBroj, this.izvuceniLoto.Count);
                poslovnica.Refresh();
                poslovnica.buttonIzvlacenjeLota.Enabled = false;

                for (int i = 26; i <= 31; i++)
                {
                    var label = table_loto.Controls["label" + i.ToString()];
                    if (label.Text.Equals(randBroj.ToString()))
                    {
                        label.ForeColor = Color.Green;
                        this.Refresh();
                        broji_dobitne++;
                        pogodeniLotoBrojevi.Add(randBroj);
                    }
                }
            }
            Thread.Sleep(3000);
            poslovnica.textBoxIzvuceniBrojeviLoto.Clear();
            izvuceniLoto.Sort();
            
            for(int i =0; i <izvuceniLoto.Count; i++)
            {
                poslovnica.zapisiIzvuceniLotoBrojSortirano(izvuceniLoto[i], i);
            }

            izvlacenjeJokera();
            if (broji_dobitne >= 3)
            {
                MessageBox.Show("Dobitan!");
                dobitan_loto = true;
            }

            provjeriJoker();

            if(dobitan_joker == false)
            {
                odabraniJoker.Clear();
                izvuceniJoker.Clear();
            }

            if(dobitan_loto == false)
            {
                brojeviNaLotoListicu.Clear();
                pogodeniLotoBrojevi.Clear();
            }

            if(dobitan_joker == true ||dobitan_loto == true)
                this.poslovnica.spremiLotoListicUBazu(this.vrijemeUplateLotoListica, this.brojeviNaLotoListicu, this.pogodeniLotoBrojevi, this.odabraniJoker, this.izvuceniJoker);

            Button novi_listic = new Button();
            novi_listic.Text = "NOVA IGRA!";
            novi_listic.Font = new Font("MicrosoftSansSerif", 10);
            novi_listic.Size = new System.Drawing.Size(106, 48);
            novi_listic.BackColor = Color.PaleGreen;
            tabPageLoto.Controls.Add(novi_listic);

            novi_listic.Click += nova_igra;
        }

        private void izvlacenjeJokera()
        {
            var r = new Random();
            for (int ukupnoIzvucenih = 0; ukupnoIzvucenih < 6; ukupnoIzvucenih++)
            {
                int jokerBroj = r.Next(0, 9 + 1);
                izvuceniJoker.Add(jokerBroj);
                poslovnica.zapisiIzvuceniJoker(jokerBroj);
            }
        }

        private void provjeriJoker()
        {
            if(joker_loto == true)
            {
                int dobitni = 0;
                for(int i = 5; i > 0; i--)
                {
                    if (izvuceniJoker[i] == odabraniJoker[i])
                    {
                        var label = table_joker.Controls["label" + (i + 32).ToString()];
                        label.ForeColor = Color.Green;
                        this.Refresh();
                        dobitni++;
                    }
                    else break;
                }
                if (dobitni > 0) { MessageBox.Show("Dobitan!"); dobitan_joker = true; }
            }
        }

        void nova_igra(object sender, EventArgs e)
        {
            buttonIGRAJ.Visible = false;
            this.brojeviNaLotoListicu.Clear();
            this.pogodeniLotoBrojevi.Clear();
            this.izvuceniJoker.Clear();
            this.odabraniJoker.Clear();
            checkBoxLotoJoker.Checked = false;
            tabPageLoto.Controls.Remove((Control)sender);
            izvuceniLoto.Clear();
            foreach (var gumb in tabPageLoto.Controls.OfType<Button>())
                gumb.Enabled = true;
            foreach (var gumb in panel1.Controls.OfType<Button>())
                gumb.Enabled = true;
            checkBoxLotoJoker.Enabled = true;
            poslovnica.buttonIzvlacenjeLota.Enabled = false;
            for (int i = 26; i <= 31; i++)
            {
                var label = table_loto.Controls["label" + i.ToString()];
                label.Text = "";
                label.ForeColor = Color.Black;
            }
            poslovnica.textBoxIzvuceniBrojeviLoto.BackColor = Color.White;
            poslovnica.textBoxIzvuceniBrojeviLoto.Clear();
            poslovnica.obrisiJoker();
            izvuceniJoker.Clear();
            generiraj_joker_brojeve();
        }

        private void checkBoxLotoJoker_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLotoJoker.Checked) joker_loto = true;
            else joker_loto = false;

            if (joker_loto == true)
                for (int i = 32; i <= 37; i++)
                {
                    var label = table_joker.Controls["label" + i.ToString()];
                    odabraniJoker.Add(Int16.Parse(label.Text));
                }
            else odabraniJoker.Clear();
        }

        private void buttonIGRAJ_Click(object sender, EventArgs e)
        {
            var forma = new CijenaLoto(joker_loto);
            forma.ShowDialog();
            DialogResult result = forma.DialogResult;
            if (result == DialogResult.OK)
            {
                foreach (var gumb in tabPageLoto.Controls.OfType<Button>())
                    gumb.Enabled = false;
                foreach (var gumb in panel1.Controls.OfType<Button>())
                    gumb.Enabled = false;
                checkBoxLotoJoker.Enabled = false;
                poslovnica.buttonIzvlacenjeLota.Enabled = true;
                for (int i = 26; i <= 31; i++)
                {
                    var label = table_loto.Controls["label" + i.ToString()];
                    brojeviNaLotoListicu.Add(Int16.Parse(label.Text));
                }
                this.vrijemeUplateLotoListica = DateTime.Now;
                this.poslovnica.spremiUBazuSvihLotoListica(this.brojeviNaLotoListicu);
            }
        }

        private void button_proslost_Click(object sender, EventArgs e)
        {
            statistika = this.poslovnica.povuciIzBazeLota();
            int brojac = 0;
            int[] brojevi = new int[6];
            foreach (KeyValuePair<int, int> broj in statistika.OrderByDescending(key => key.Value))
            {
                brojevi[brojac] = broj.Key;
                brojac++;
                if (brojac == 6) break;
            }

            Array.Sort(brojevi);
            for (int i = 0; i < 6; i++)
            {
                table_loto.Controls["label" + (i + 26).ToString()].Text = brojevi[i].ToString();
            }

            buttonIGRAJ.Visible = true;
        }

        /*----------------------------------------------------------------------------------------------
                                                KLADIONICA
         -----------------------------------------------------------------------------------------------*/
        public void prikaziponudu()
        {
            var environment = System.Environment.CurrentDirectory;
            pathPonuda = Directory.GetParent(environment).Parent.FullName;
            pathPonuda += @"\Database\Ponuda.xml";
            docPonuda = XDocument.Load(pathPonuda);
            var parovi = from par in this.docPonuda.Elements("Root").Elements("Par")
                         select new
                         {
                             dogadaj = (string)par.Element("par"),
                             tecaj1 = (string)par.Element("tečaj_1"),
                             tecajX = (string)par.Element("tečaj_X"),
                             tecaj2 = (string)par.Element("tečaj_2"),
                         };
            panelPonuda.Controls.Clear();

            int i = 0;
            foreach (var par in parovi)
            {
                Label labelpar = new Label();
                Label label1 = new Label();
                Label labelx = new Label();
                Label label2 = new Label();
                labelpar.Text = par.dogadaj;
                label1.Text = par.tecaj1;
                labelx.Text = par.tecajX;
                label2.Text = par.tecaj2;
                labelpar.Width = 210;
                label1.Width = 40;
                labelx.Width = 40;
                label2.Width = 40;
                labelpar.TextAlign = ContentAlignment.MiddleCenter;
                label1.TextAlign = ContentAlignment.MiddleCenter;
                labelx.TextAlign = ContentAlignment.MiddleCenter;
                label2.TextAlign = ContentAlignment.MiddleCenter;
                labelpar.BorderStyle = BorderStyle.FixedSingle;
                label1.BorderStyle = BorderStyle.FixedSingle;
                labelx.BorderStyle = BorderStyle.FixedSingle;
                label2.BorderStyle = BorderStyle.FixedSingle;

                label1.Click += (sender, e) =>
                {
                    if (listBoxOdigraniparovi.FindString(labelpar.Text) == ListBox.NoMatches)
                    {
                        label1.BackColor = Color.Red;
                        listBoxOdigraniparovi.Items.Add(labelpar.Text + ":1, Tečaj:" + label1.Text);
                        Tečaj.Text = (Decimal.Parse(Tečaj.Text) * Decimal.Parse(label1.Text)).ToString();
                    }
                    else if (!(listBoxOdigraniparovi.FindString(labelpar.Text) == ListBox.NoMatches) && label1.BackColor == Color.Red)
                    {
                        label1.BackColor = Color.Lavender;
                        listBoxOdigraniparovi.Items.Remove(labelpar.Text + ":1, Tečaj:" + label1.Text);
                        Tečaj.Text = (Decimal.Parse(Tečaj.Text) / Decimal.Parse(label1.Text)).ToString();
                    }
                };
                labelx.Click += (sender, e) =>
                {
                    if (listBoxOdigraniparovi.FindString(labelpar.Text) == ListBox.NoMatches)
                    {
                        labelx.BackColor = Color.Red;
                        listBoxOdigraniparovi.Items.Add(labelpar.Text + ":X, Tečaj:" + labelx.Text);
                        Tečaj.Text = (Decimal.Parse(Tečaj.Text) * Decimal.Parse(labelx.Text)).ToString();
                    }
                    else if (!(listBoxOdigraniparovi.FindString(labelpar.Text) == ListBox.NoMatches) && labelx.BackColor == Color.Red)
                    {
                        labelx.BackColor = Color.Lavender;
                        listBoxOdigraniparovi.Items.Remove(labelpar.Text + ":X, Tečaj:" + labelx.Text);
                        Tečaj.Text = (Decimal.Parse(Tečaj.Text) / Decimal.Parse(labelx.Text)).ToString();
                    }
                };
                label2.Click += (sender, e) =>
                {
                    if (listBoxOdigraniparovi.FindString(labelpar.Text) == ListBox.NoMatches)
                    {
                        label2.BackColor = Color.Red;
                        listBoxOdigraniparovi.Items.Add(labelpar.Text + ":2, Tečaj:" + label2.Text);
                        Tečaj.Text = (Decimal.Parse(Tečaj.Text) * Decimal.Parse(label2.Text)).ToString();
                    }
                    else if (!(listBoxOdigraniparovi.FindString(labelpar.Text) == ListBox.NoMatches) && label2.BackColor == Color.Red)
                    {
                        label2.BackColor = Color.Lavender;
                        listBoxOdigraniparovi.Items.Remove(labelpar.Text + ":2, Tečaj:" + label2.Text);
                        Tečaj.Text = (Decimal.Parse(Tečaj.Text) / Decimal.Parse(label2.Text)).ToString();
                    }
                };
                panelPonuda.Controls.Add(labelpar);
                panelPonuda.Controls.Add(label1);
                panelPonuda.Controls.Add(labelx);
                panelPonuda.Controls.Add(label2);
                ponuda.Add(labelpar.Text);
                koef1.Add(label1.Text);
                koefx.Add(labelx.Text);
                koef2.Add(label2.Text);
                labelpar.Location = new System.Drawing.Point(0, i * 22);
                label1.Location = new System.Drawing.Point(210, i * 22);
                labelx.Location = new System.Drawing.Point(260, i * 22);
                label2.Location = new System.Drawing.Point(310, i * 22);
                i++;
                //ponuda.Add(ponuda)
            }
        }

        private void buttonPomoć_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Odaberite tip klikom na tečaj");
        }

        private void Tečaj_TextChanged(object sender, EventArgs e)
        {
            Decimal s;
            if (Decimal.TryParse(Uplata_TextBox.Text, out s) || Uplata_TextBox.Text == "")
                Dobitak.Text = (Decimal.Parse(Tečaj.Text) * s).ToString();
        }

        private void Uplata_TextBox_TextChanged(object sender, EventArgs e)
        {
            Decimal s;
            if (Decimal.TryParse(Uplata_TextBox.Text, out s) || Uplata_TextBox.Text == "")
                Dobitak.Text = (Decimal.Parse(Tečaj.Text) * s).ToString();
            else
            {
                MessageBox.Show(this, "Molimo upisujte brojeve");
                string s1 = Uplata_TextBox.Text.Remove(Uplata_TextBox.Text.Length - 1, 1);
                Uplata_TextBox.Text = s1;
                Uplata_TextBox.Select(Uplata_TextBox.Text.Length, 0);
            }
        }

        private void UplatiListicButton_Click(object sender, EventArgs e)
        {
            if (Tečaj.Text == "1")
                MessageBox.Show(this, "Izaberite barem jedan tip iz ponude.");
            else if (Dobitak.Text == "0")
                MessageBox.Show(this, "Ulog mora biti veći od 0.");
            else
            {
                this.vrijemeUplateKladionicaListica = DateTime.Now;
                for (int i = 0; i < listBoxOdigraniparovi.Items.Count; i++)
                {
                    string par = listBoxOdigraniparovi.Items[i].ToString();
                    int index = par.IndexOf(':');
                    string dvoboj = par.Substring(0, index);
                    string tip = par.Substring(index + 1, 1);
                    listic.Add((dvoboj, tip));
                }
                MessageBox.Show(this, "Listić je uplaćen.");
            }
        }

        public void odigrajKolo()
        {
            for (int i = 0; i < ponuda.Count; i++)
            {
                List<string> kolo = new List<string>(new string[1000]);
                int vjer1 = (int)Math.Floor(1000 / Decimal.Parse(koef1[i]));
                int vjerx = (int)Math.Floor(1000 / Decimal.Parse(koefx[i]));
                int vjer2 = 1000 - (vjerx + vjer1);
                var r = new Random();
                for (int j = 0; j < vjer1; j++)
                {
                    kolo[j] = "1";
                }
                for (int j = vjer1; j < vjer2; j++)
                {
                    kolo[j] = "X";
                }
                for (int j = vjer2; j < kolo.Count; j++)
                {
                    kolo[j] = "2";
                }
                int odabrani = r.Next(0, 999);

                this.poslovnica.listBoxRezultati.Items.Add(ponuda[i] + ":" + kolo[odabrani].ToString());
            }
        }

        /*----------------------------------------------------------------------------------------------
                                             EUROJACKPOT
        -----------------------------------------------------------------------------------------------*/
        private void ej_igraj_Click(object sender, EventArgs e)
        {
            foreach (var gumb in tabPageEurojackpot.Controls.OfType<Button>())
                gumb.Enabled = false;
            
            
            poslovnica.buttonIzvlacenjeEJ.Enabled = true;
            for (int i = 1; i <= 7; i++)
            {
                var label = new Control();
                if (i <= 5)
                    label = table_ej.Controls["ejlabel" + i.ToString()];
                else
                    label = table_ej2.Controls["ejlabel" + i.ToString()];
                
                brojeviNaListicuEJ.Add(Int16.Parse(label.Text));
            }
            this.vrijemeUplateEJListica = DateTime.Now;
        }

        public void stilizirajEJ()
        {
            for (int j = 1; j <= 7; j++)
            {
                var label = new Control();
                if (j<=5)
                   label = table_ej.Controls["ejlabel" + j.ToString()];
                else
                   label = table_ej2.Controls["ejlabel" + j.ToString()];
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
           
        }

        private void generiraj_ej_Click(object sender, EventArgs e)
        {
            ej_igraj.Visible = true;
            Random rnd = new Random();
            int[] brojevi = new int[7];
            for (int i = 1; i <= 7; i++)
            {
                if (i <= 5)
                {
                    int broj = rnd.Next(1, 51);
                    if (brojevi.Contains(broj)) broj = rnd.Next(1, 51);
                    brojevi[i - 1] = broj;
                }
                else
                {
                    int broj = rnd.Next(1, 11);
                    if (brojevi[6]==brojevi[5]) broj = rnd.Next(1, 11);
                    brojevi[i - 1] = broj;
                }
            }

            //Array.Sort(brojevi);
            //pogledam da nije izgenerirao više istih
           /* for (int x = 0; x < 5; x++)
            {
                if (brojevi[x] == brojevi[x + 1])
                    brojevi[x + 1]++;
                if (brojevi[x] > brojevi[x + 1])
                    brojevi[x + 1] = brojevi[x] + 1;
            }*/

            int brojac = 1;
            foreach (int value in brojevi)
            {
                var label = new Control();
                if (brojac <= 5)
                    label = table_ej.Controls["ejlabel" + brojac.ToString()];
                else
                    label = table_ej2.Controls["ejlabel" + brojac.ToString()];
                label.Text = value.ToString();
                label.ForeColor = Color.Black;
                brojac++;
            }
        }


        public void izvlacenjeEJ()
        {
            var r = new Random();
            int broji_dobitne = 0;
            for (int ukupnoIzvucenih = 0; ukupnoIzvucenih < 7; ukupnoIzvucenih++)
            {
                int randBroj = r.Next(1, 51);
                if (ukupnoIzvucenih < 5)
                {
                    randBroj = r.Next(1, 51);
                    if (this.izvuceniEJ.Contains(randBroj)) randBroj = r.Next(1, 51);
                    this.izvuceniEJ.Add(randBroj);
                }
                else
                {
                    randBroj = r.Next(1, 11);
                    if (this.izvuceniEJ.Contains(randBroj)) randBroj = r.Next(1, 11);
                    this.izvuceniEJ.Add(randBroj);
                }

                Thread.Sleep(500);
                
                poslovnica.zapisiIzvuceniEJBroj(randBroj, this.izvuceniEJ.Count);
                poslovnica.Refresh();
                poslovnica.buttonIzvlacenjeEJ.Enabled = false;

                for (int i = 1; i <= 7; i++)
                {
                    var label = new Control();
                    if (i <= 5)
                        label = table_ej.Controls["ejlabel" + i.ToString()];
                    else
                        label = table_ej2.Controls["ejlabel" + i.ToString()];
                    
                    if (label.Text.Equals(randBroj.ToString()))
                    {
                        label.ForeColor = Color.Green;
                        this.Refresh();
                        broji_dobitne++;
                        pogodeniEJBrojevi.Add(randBroj);
                    }
                }
            }
            Thread.Sleep(3000);
            poslovnica.textBoxEJglavni.Clear();
            poslovnica.textBoxEJekstra.Clear();
            //izvuceniLoto.Sort();
            /*
            for (int i = 0; i < izvuceniLoto.Count; i++)
            {
                poslovnica.zapisiIzvuceniLotoBrojSortirano(izvuceniLoto[i], i);
            }
            */
           
            if (broji_dobitne >= 3)
            {
                MessageBox.Show("Dobitan!");
                dobitan_ej = true;
            }


            if (dobitan_ej == false)
            {
                brojeviNaListicuEJ.Clear();
                pogodeniEJBrojevi.Clear();
            }

            if (dobitan_ej == true)
                this.poslovnica.spremiEJListicUBazu(this.vrijemeUplateEJListica, this.brojeviNaListicuEJ, 
                    this.pogodeniEJBrojevi);

            Button novi_listic = new Button();
            novi_listic.Text = "NOVA IGRA!";
            novi_listic.Font = new Font("MicrosoftSansSerif", 10);
            novi_listic.Size = new System.Drawing.Size(106, 48);
            novi_listic.BackColor = Color.PaleGreen;
            tabPageEurojackpot.Controls.Add(novi_listic);

            novi_listic.Click += nova_igra_ej;
        }

        void nova_igra_ej(object sender, EventArgs e)
        {
            ej_igraj.Visible = false;
            this.brojeviNaListicuEJ.Clear();
            this.pogodeniEJBrojevi.Clear();
      
            tabPageEurojackpot.Controls.Remove((Control)sender);
            izvuceniEJ.Clear();
            foreach (var gumb in tabPageEurojackpot.Controls.OfType<Button>())
                gumb.Enabled = true;
            poslovnica.buttonIzvlacenjeLota.Enabled = false;
            for (int j = 1; j < 8; j++)
            {
                var label = new Control();
                if (j <= 5)
                    label = table_ej.Controls["ejlabel" + j.ToString()];
                else
                    label = table_ej2.Controls["ejlabel" + j.ToString()];
                label.Text = "";
                label.ForeColor = Color.Black;
            }

            
            poslovnica.textBoxEJglavni.BackColor = Color.White;
            poslovnica.textBoxEJglavni.Clear();
            poslovnica.textBoxEJekstra.BackColor = Color.White;
            poslovnica.textBoxEJekstra.Clear();

        }
    }
}
