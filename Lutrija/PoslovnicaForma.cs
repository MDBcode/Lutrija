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
        XDocument doc;
        XDocument docLoto;
        public PoslovnicaForma()
        {
            InitializeComponent();

            labelRedniIzvuceni.Text = "0";
            listBoxDobitniListici.Visible = false;
            labelDobitniListici.Visible = false;
            buttonIzvlacenjeBinga.Enabled = false;

            var environment = System.Environment.CurrentDirectory;
            path = Directory.GetParent(environment).Parent.FullName;
            path += @"\Database\BingoListic.xml";
            doc = XDocument.Load(path);

            pathLoto = Directory.GetParent(environment).Parent.FullName;
            pathLoto += @"\Database\LotoListic.xml";
            docLoto = XDocument.Load(pathLoto);

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
    }
}
