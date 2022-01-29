using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Lutrija
{
    public partial class PoslovnicaForma : Form
    {
        public KlijentForma klijent;
        string path;
        string pathLoto;
        string pathKladionica;
        string pathPonuda;
        XDocument doc;
        XDocument docLoto;
        XDocument docKladionica;
        XDocument docPonuda;
        public PoslovnicaForma()
        {
            InitializeComponent();

            labelRedniIzvuceni.Text = "0";
            listBoxDobitniListici.Visible = false;
            labelDobitniListici.Visible = false;
            buttonIzvlacenjeBinga.Enabled = false;
            listBoxDobitniListiciKladionica.Visible = false;
            Dobitnilisticikladionicalabel.Visible = false;

            var environment = System.Environment.CurrentDirectory;
            path = Directory.GetParent(environment).Parent.FullName;
            path += @"\Database\BingoListic.xml";
            doc = XDocument.Load(path);

            pathLoto = Directory.GetParent(environment).Parent.FullName;
            pathLoto += @"\Database\LotoListic.xml";
            docLoto = XDocument.Load(pathLoto);

            pathKladionica = Directory.GetParent(environment).Parent.FullName;
            pathKladionica += @"\Database\KladionicaListic.xml";
            docKladionica = XDocument.Load(pathKladionica);

            pathPonuda = Directory.GetParent(environment).Parent.FullName;
            pathPonuda += @"\Database\Ponuda.xml";
            docPonuda = XDocument.Load(pathPonuda);

            klijent = new KlijentForma(this);
            klijent.Show();
        }

        public void zapisiIzvuceniBroj(int izvuceniBroj, int redniIzvuceni) {
            textBoxIzvuceniBrojevi.Text += izvuceniBroj.ToString() + "  ";
            labelRedniIzvuceni.Text = redniIzvuceni.ToString();
        }

        public void spremiBingoListicUBazu(DateTime d, List<int> brojevi) {
            string stringBrojeva = "";
            foreach (int broj in brojevi) stringBrojeva += broj.ToString() + " ";

            int noviID = this.doc.Descendants("BingoListic").Count() + 1;

            XElement BingoListic = new XElement("BingoListic",
                new XElement("ID", noviID.ToString()),
                new XElement("vrijemeUplate", d.ToString()),
                new XElement("brojevi", stringBrojeva));

            this.doc.Root.Add(BingoListic);
            this.doc.Save(this.path);
        }

        public void spremiLotoListicUBazu(DateTime d, List<int> brojevi, List<int> pogodeni)
        {
            string stringBrojeva = "";
            string stringPogodenih = "";
            foreach (int broj in brojevi) stringBrojeva += broj.ToString() + " ";
            foreach (int pogodeni_broj in pogodeni) stringPogodenih += pogodeni_broj.ToString() + " ";

            int noviID = this.docLoto.Descendants("LotoListic").Count() + 1;

            XElement LotoListic = new XElement("LotoListic",
                new XElement("ID", noviID.ToString()),
                new XElement("vrijemeUplate", d.ToString()),
                new XElement("brojevi", stringBrojeva),
                new XElement("pogodeni", stringPogodenih));

            this.docLoto.Root.Add(LotoListic);
            this.docLoto.Save(this.pathLoto);
        }

        public void spremiKladionicaListicUBazu(DateTime d, List<Tuple<string,string>> parovi, int dobitak)
        {
            string stringParova = "";
            foreach (Tuple<string, string> par in parovi) stringParova += par.Item1 + " " + par.Item2;

            int noviID = this.docKladionica.Descendants("KladionicaListic").Count() + 1;

            XElement KladionicaListic = new XElement("KladionicaListic",
                new XElement("ID", noviID.ToString()),
                new XElement("vrijemeUplate", d.ToString()),
                new XElement("parovi", stringParova),
                new XElement("dobitak", dobitak));

            this.docKladionica.Root.Add(KladionicaListic);
            this.docKladionica.Save(this.pathKladionica);
        }

        public void dohvatiBingoListiceIzBaze() {
            var dobitniListici = from listic in this.doc.Elements("Root").Elements("BingoListic") select new { 
                ID = (string)listic.Element("ID"),
                vrijemeUplate = (string)listic.Element("vrijemeUplate"),
                brojevi = (string)listic.Element("brojevi")
            };

            listBoxDobitniListici.Items.Clear();
            foreach(var listic in dobitniListici) {
                string sadrzaj = listic.ID + "  |  " + listic.vrijemeUplate + "  |  " + listic.brojevi;
                listBoxDobitniListici.Items.Add(sadrzaj);
            }
        }

        public void dohvatiLotoListiceIzBaze()
        {
            var dobitniLotoListici = from listic in this.docLoto.Elements("Root").Elements("LotoListic")
                                 select new
                                 {
                                     ID = (string)listic.Element("ID"),
                                     vrijemeUplate = (string)listic.Element("vrijemeUplate"),
                                     brojevi = (string)listic.Element("brojevi"),
                                     pogodeni = (string)listic.Element("pogodeni")
                                 };

            listBoxDobitniListiciLoto.Items.Clear();
            foreach (var listic in dobitniLotoListici)
            {
                string sadrzaj = listic.ID + "  |  " + listic.vrijemeUplate + "  |  " + listic.brojevi + "  |  " + listic.pogodeni;
                listBoxDobitniListiciLoto.Items.Add(sadrzaj);
            }
        }

        public void dohvatiKladionicaListiceIzBaze()
        {
            var dobitniListici = from listic in this.doc.Elements("Root").Elements("KladionicaListic")
                                 select new
                                 {
                                     ID = (string)listic.Element("ID"),
                                     vrijemeUplate = (string)listic.Element("vrijemeUplate"),
                                     parovi = (string)listic.Element("parovi"),
                                     dobitak = (string)listic.Element("dobitak")
                                 };

            listBoxDobitniListiciKladionica.Items.Clear();
            foreach (var listic in dobitniListici)
            {
                string sadrzaj = listic.ID + "  |  " + listic.vrijemeUplate + "  |  " + listic.parovi + "  |  " + listic.dobitak;
                listBoxDobitniListiciKladionica.Items.Add(sadrzaj);
            }
        }

        private void buttonIzvlacenjeBinga_Click(object sender, EventArgs e) => klijent.izvlacenjeBinga();

        private void buttonDobitniBingoListici_Click(object sender, EventArgs e)
        {
            labelDobitniListici.Visible = !labelDobitniListici.Visible;
            listBoxDobitniListici.Visible = !listBoxDobitniListici.Visible;
            dohvatiBingoListiceIzBaze();
        }

        private void buttonIzvlacenjeLota_Click(object sender, EventArgs e) => klijent.izvlacenjeLota();

        public void zapisiIzvuceniLotoBroj(int izvuceniBroj, int redniIzvuceni)
        {
            textBoxIzvuceniBrojeviLoto.Text += izvuceniBroj.ToString() + "  ";
            labelRedniIzvuceni.Text = redniIzvuceni.ToString();
        }

        public void zapisiIzvuceniLotoBrojSortirano(int izvuceniBroj, int redniIzvuceni)
        {
            textBoxIzvuceniBrojeviLoto.Text += izvuceniBroj.ToString() + "  ";
            textBoxIzvuceniBrojeviLoto.BackColor = Color.SandyBrown;
            labelRedniIzvuceni.Text = redniIzvuceni.ToString();
        }

        private void buttonDobitniLotoListici_Click(object sender, EventArgs e)
        {
            label2.Visible = !label2.Visible;
            listBoxDobitniListiciLoto.Visible = !listBoxDobitniListiciLoto.Visible;
            dohvatiLotoListiceIzBaze();
        }

        private void buttonDobitniListiciKladionica_Click(object sender, EventArgs e)
        {
            Dobitnilisticikladionicalabel.Visible = !Dobitnilisticikladionicalabel.Visible;
            listBoxDobitniListiciKladionica.Visible = !listBoxDobitniListiciKladionica.Visible;
            dohvatiKladionicaListiceIzBaze();
        }

        private void buttonDodajpar_Click(object sender, EventArgs e)
        {
            if (textBoxtim1.TextLength > 13 || textBoxtim2.TextLength > 13)
                MessageBox.Show(this, "Predugačko ime kluba,mora biti manje od 13 znakova");
            else
            {
                string par = textBoxtim1.Text + "-" + textBoxtim2.Text;
                string tečaj_1 = textBoxTečajZaTip1.Text;
                string tečaj_X = textBoxTečajZaTipX.Text;
                string tečaj_2 = textBoxTečajZaTip2.Text;

                int noviID = this.docPonuda.Descendants("Par").Count() + 1;

                XElement Par = new XElement("Par",
                    new XElement("ID", noviID.ToString()),
                    new XElement("par", par),
                    new XElement("tečaj_1", tečaj_1),
                    new XElement("tečaj_X", tečaj_X),
                    new XElement("tečaj_2", tečaj_2));

                this.docPonuda.Root.Add(Par);
                this.docPonuda.Save(this.pathPonuda);

                textBoxtim1.Clear();
                textBoxtim2.Clear();
                textBoxTečajZaTip1.Clear();
                textBoxTečajZaTipX.Clear();
                textBoxTečajZaTip2.Clear();
                klijent.prikaziponudu();
                MessageBox.Show(this, "Par dodan na ponudu.");
            }
        }

        private void textBoxTečajZaTip1_TextChanged(object sender, EventArgs e)
        {
            Decimal s;
            if (!Decimal.TryParse(textBoxTečajZaTip1.Text, out s) || textBoxTečajZaTip1.Text == "")
            {
                MessageBox.Show(this, "tečaj mora biti decimalni broj veći od 1.0");
                textBoxTečajZaTip1.Text = "1,0";
            }
        }

        private void textBoxTečajZaTipX_TextChanged(object sender, EventArgs e)
        {
            Decimal s;
            if (!Decimal.TryParse(textBoxTečajZaTipX.Text, out s) || textBoxTečajZaTipX.Text == "")
            {
                MessageBox.Show(this, "tečaj mora biti decimalni broj  veći od 1.0");
                textBoxTečajZaTipX.Text = "1,0";
            }
        }

        private void textBoxTečajZaTip2_TextChanged(object sender, EventArgs e)
        {
            Decimal s;
            if (!Decimal.TryParse(textBoxTečajZaTip2.Text, out s) || textBoxTečajZaTip2.Text == "")
            {
                MessageBox.Show(this, "tečaj mora biti decimalni broj veći od 1.0");
                textBoxTečajZaTip2.Text = "1,0";
            }
        }
    }
}
