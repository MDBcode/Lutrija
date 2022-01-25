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
        XDocument doc;
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

        private void buttonIzvlacenjeBinga_Click(object sender, EventArgs e) => klijent.izvlacenjeBinga();

        private void buttonDobitniBingoListici_Click(object sender, EventArgs e)
        {
            labelDobitniListici.Visible = !labelDobitniListici.Visible;
            listBoxDobitniListici.Visible = !listBoxDobitniListici.Visible;
            dohvatiBingoListiceIzBaze();
        }
    }
}
