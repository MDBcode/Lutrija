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
        /*----------------------------------------------------------------------------------------------
                                                BINGO
        -----------------------------------------------------------------------------------------------*/
        string path;
        XDocument doc;

        /*----------------------------------------------------------------------------------------------
                                                LOTO
        -----------------------------------------------------------------------------------------------*/
        string pathLoto;
        string pathLotoSvi;
        XDocument docLoto;
        XDocument docLotoSvi;

        /*----------------------------------------------------------------------------------------------
                                                KLADIONICA
        -----------------------------------------------------------------------------------------------*/
        string pathKladionica;
        string pathPonuda;
        XDocument docKladionica;
        XDocument docPonuda;

        /*----------------------------------------------------------------------------------------------
                                             EUROJACKPOT
        -----------------------------------------------------------------------------------------------*/

        string pathEJ;
        string pathEJSvi;
        XDocument docEJ;
        XDocument docEJSvi;

        public PoslovnicaForma()
        {
            InitializeComponent();
            /*----------------------------------------------------------------------------------------------
                                                BINGO
            -----------------------------------------------------------------------------------------------*/
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

            /*----------------------------------------------------------------------------------------------
                                                LOTO
            -----------------------------------------------------------------------------------------------*/
            pathLoto = Directory.GetParent(environment).Parent.FullName;
            pathLoto += @"\Database\LotoListic.xml";
            docLoto = XDocument.Load(pathLoto);

            pathLotoSvi = Directory.GetParent(environment).Parent.FullName;
            pathLotoSvi += @"\Database\sviLotoListici.xml";
            docLotoSvi = XDocument.Load(pathLotoSvi);

            /*----------------------------------------------------------------------------------------------
                                                KLADIONICA
            -----------------------------------------------------------------------------------------------*/
            pathKladionica = Directory.GetParent(environment).Parent.FullName;
            pathKladionica += @"\Database\KladionicaListic.xml";
            docKladionica = XDocument.Load(pathKladionica);

            pathPonuda = Directory.GetParent(environment).Parent.FullName;
            pathPonuda += @"\Database\Ponuda.xml";
            docPonuda = XDocument.Load(pathPonuda);

            /*----------------------------------------------------------------------------------------------
                                              EUROJACKPOT
            -----------------------------------------------------------------------------------------------*/
            pathEJ = Directory.GetParent(environment).Parent.FullName;
            pathEJ += @"\Database\EJListic.xml";
            docEJ = XDocument.Load(pathEJ);
            pathEJSvi = Directory.GetParent(environment).Parent.FullName;
            pathEJSvi += @"\Database\sviEJListici.xml";
            docEJSvi = XDocument.Load(pathEJSvi);
            klijent = new KlijentForma(this);
            klijent.Show();
        }

        /*----------------------------------------------------------------------------------------------
                                                BINGO
        -----------------------------------------------------------------------------------------------*/
        public void zapisiIzvuceniBroj(int izvuceniBroj, int redniIzvuceni)
        {
            textBoxIzvuceniBrojevi.Text += izvuceniBroj.ToString() + "  ";
            labelRedniIzvuceni.Text = redniIzvuceni.ToString();
        }

        public void spremiBingoListicUBazu(DateTime d, List<int> brojevi, int dobitak)
        {
            string stringBrojeva = "";
            foreach (int broj in brojevi) stringBrojeva += broj.ToString() + " ";

            int noviID = this.doc.Descendants("BingoListic").Count() + 1; // ID "autoincrement"

            XElement BingoListic = new XElement("BingoListic",
                new XElement("ID", noviID.ToString()),
                new XElement("vrijemeUplate", d.ToString()),
                new XElement("dobitak", dobitak.ToString()),
                new XElement("brojevi", stringBrojeva));

            this.doc.Root.Add(BingoListic);
            this.doc.Save(this.path);
        }

        public void dohvatiBingoListiceIzBaze()
        {
            var dobitniListici = from listic in this.doc.Elements("Root").Elements("BingoListic")
                                 select new
                                 {
                                     ID = (string)listic.Element("ID"),
                                     vrijemeUplate = (string)listic.Element("vrijemeUplate"),
                                     dobitak = (string)listic.Element("dobitak"),
                                     brojevi = (string)listic.Element("brojevi")
                                 };

            listBoxDobitniListici.Items.Clear();
            foreach (var listic in dobitniListici)
            {
                string sadrzaj = listic.ID + "  |  " + listic.vrijemeUplate + "  |  ";
                listBoxDobitniListici.Items.Add(sadrzaj);
                sadrzaj = "\t\t dobitak: " + listic.dobitak + " kn";
                listBoxDobitniListici.Items.Add(sadrzaj);
                sadrzaj = "\t\t brojevi na listiću: " + listic.brojevi;
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

        /*----------------------------------------------------------------------------------------------
                                                LOTO
        -----------------------------------------------------------------------------------------------*/
        public void spremiLotoListicUBazu(DateTime d, double nagradaLoto, double nagradaJoker, List<int> brojevi, List<int> pogodeni, List<int> uneseni_joker, List<int> izvuceni_joker)
        {
            string stringBrojeva = "";
            string stringNagradaLoto = "";
            string stringNagradaJoker = "";
            string stringPogodenih = "";
            string stringUneseniJoker = "";
            string stringIzvuceniJoker = "";
            stringNagradaLoto = nagradaLoto.ToString();
            stringNagradaJoker = nagradaJoker.ToString();
            foreach (int broj in brojevi) stringBrojeva += broj.ToString() + " ";
            foreach (int pogodeni_broj in pogodeni) stringPogodenih += pogodeni_broj.ToString() + " ";
            foreach (int unio in uneseni_joker) stringUneseniJoker += unio.ToString() + " ";
            foreach (int izv in izvuceni_joker) stringIzvuceniJoker += izv.ToString() + " ";

            int noviID = this.docLoto.Descendants("LotoListic").Count() + 1;

            XElement LotoListic = new XElement("LotoListic",
                new XElement("ID", noviID.ToString()),
                new XElement("vrijemeUplate", d.ToString()),
                new XElement("nagrada_loto", stringNagradaLoto),
                new XElement("nagrada_joker", stringNagradaJoker),
                new XElement("brojevi", stringBrojeva),
                new XElement("pogodeni", stringPogodenih),
                new XElement("uneseni_joker", stringUneseniJoker),
                new XElement("izvuceni_joker", stringIzvuceniJoker));

            this.docLoto.Root.Add(LotoListic);
            this.docLoto.Save(this.pathLoto);
        }

        public void dohvatiLotoListiceIzBaze()
        {
            var dobitniLotoListici = from listic in this.docLoto.Elements("Root").Elements("LotoListic")
                                 select new
                                 {
                                     ID = (string)listic.Element("ID"),
                                     vrijemeUplate = (string)listic.Element("vrijemeUplate"),
                                     nagradaLoto = (string)listic.Element("nagrada_loto"),
                                     nagradaJoker = (string)listic.Element("nagrada_joker"),
                                     brojevi = (string)listic.Element("brojevi"),
                                     pogodeni = (string)listic.Element("pogodeni"),
                                     uneseni_joker = (string)listic.Element("uneseni_joker"),
                                     izvuceni_joker = (string)listic.Element("izvuceni_joker")
                                 };

            listBoxDobitniListiciLoto.Items.Clear();
            foreach (var listic in dobitniLotoListici)
            {
                string sadrzaj = listic.ID + "  |  " + listic.vrijemeUplate + "  |  ";
                listBoxDobitniListiciLoto.Items.Add(sadrzaj);
                sadrzaj = "\t\t dobitak pogođenih loto brojeva: " + listic.nagradaLoto + " kn";
                listBoxDobitniListiciLoto.Items.Add(sadrzaj);
                sadrzaj = "\t\t dobitak pogođenih joker brojeva: " + listic.nagradaJoker + " kn";
                listBoxDobitniListiciLoto.Items.Add(sadrzaj);
                sadrzaj = "\t\t dobitni loto brojevi: " + listic.brojevi + "  |  od kojih pogođeni: " + listic.pogodeni;
                listBoxDobitniListiciLoto.Items.Add(sadrzaj);
                sadrzaj = "\t\t dobitni joker broj: " + listic.uneseni_joker + "  |  izvučeni joker broj: " + listic.izvuceni_joker;
                listBoxDobitniListiciLoto.Items.Add(sadrzaj);
            }
        }

        private void buttonIzvlacenjeLota_Click(object sender, EventArgs e) => klijent.izvlacenjeLota();

        public void zapisiIzvuceniLotoBroj(int izvuceniBroj, int redniIzvuceni)
        {
            textBoxIzvuceniBrojeviLoto.Text += izvuceniBroj.ToString() + "  ";
            labelRedniIzvuceni.Text = redniIzvuceni.ToString();
        }

        public void zapisiIzvuceniJoker(int jokerBroj)
        {
            izvuceniJoker.Text += jokerBroj.ToString() + "  ";
        }

        public void obrisiJoker()
        {
            izvuceniJoker.Text = "JOKER: ";
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

        internal void spremiUBazuSvihLotoListica(List<int> brojevi)
        {
            string stringBrojeva = "";
            foreach (int broj in brojevi) stringBrojeva += broj.ToString() + " ";
            stringBrojeva = stringBrojeva.Remove(stringBrojeva.Length - 1, 1);
            XElement LotoListic = new XElement("LotoListic",
                new XElement("brojevi", stringBrojeva));

            this.docLotoSvi.Root.Add(LotoListic);
            this.docLotoSvi.Save(this.pathLotoSvi);
        }

        public Dictionary<int, int> povuciIzBazeLota()
        {
            Dictionary<int, int> stat = new Dictionary<int, int>();
            for (int i = 1; i < 46; i++)
                stat.Add(i, 0);
            var LotoListici = from listic in this.docLotoSvi.Elements("Root").Elements("LotoListic")
                              select new
                              {
                                  brojevi = (string)listic.Element("brojevi")
                              };
            foreach (var listic in LotoListici)
            {
                int[] array = listic.brojevi.Split(' ').Select(int.Parse).ToArray();
                for (int i = 0; i < 6; i++)
                    stat[array[i]]++;
            }
            return stat;
        }


        /*----------------------------------------------------------------------------------------------
                                               KLADIONICA
       -----------------------------------------------------------------------------------------------*/
        public void spremiKladionicaListicUBazu(DateTime d, List<(string, string)> parovi, Decimal dobitak)
        {
            string stringParova = "";
            foreach ((string, string) par in parovi) stringParova += par.Item1 + " " + par.Item2;

            int noviID = this.docKladionica.Descendants("KladionicaListic").Count() + 1;

            XElement KladionicaListic = new XElement("KladionicaListic",
                new XElement("ID", noviID.ToString()),
                new XElement("vrijemeUplate", d.ToString()),
                new XElement("parovi", stringParova),
                new XElement("dobitak", dobitak));

            this.docKladionica.Root.Add(KladionicaListic);
            this.docKladionica.Save(this.pathKladionica);
        }
        public void dohvatiKladionicaListiceIzBaze()
        {
            var dobitniListici = from listic in this.docKladionica.Elements("Root").Elements("KladionicaListic")
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

        private void buttonDodajpar_Click(object sender, EventArgs e)
        {
            if (textBoxtim1.TextLength > 15 || textBoxtim2.TextLength > 15 || textBoxtim1.TextLength <= 0 || textBoxtim1.TextLength <= 0)
                MessageBox.Show(this, "klubovi moraju imati između 1 do 15 znakova");
            else if (textBoxTečajZaTip1.Text == "" || textBoxTečajZaTipX.Text == "" || textBoxTečajZaTip2.Text == "")
            {
                MessageBox.Show(this, "Upišite tečajeve.");
            }
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
            if (!Decimal.TryParse(textBoxTečajZaTip1.Text, out s) && !(textBoxTečajZaTip1.Text == ""))
            {
                MessageBox.Show(this, "tečaj mora biti decimalni broj veći od 1.0");
                textBoxTečajZaTip1.Text = "1,0";
            }
        }

        private void textBoxTečajZaTipX_TextChanged(object sender, EventArgs e)
        {
            Decimal s;
            if (!Decimal.TryParse(textBoxTečajZaTipX.Text, out s) && !(textBoxTečajZaTipX.Text == ""))
            {
                MessageBox.Show(this, "tečaj mora biti decimalni broj  veći od 1.0");
                textBoxTečajZaTipX.Text = "1,0";
            }
        }

        private void textBoxTečajZaTip2_TextChanged(object sender, EventArgs e)
        {
            Decimal s;
            if (!Decimal.TryParse(textBoxTečajZaTip2.Text, out s) && !(textBoxTečajZaTip2.Text == ""))
            {
                MessageBox.Show(this, "tečaj mora biti decimalni broj veći od 1.0");
                textBoxTečajZaTip2.Text = "1,0";
            }
        }

        private void buttonDobitniListiciKladionica_Click(object sender, EventArgs e)
        {
            Dobitnilisticikladionicalabel.Visible = !Dobitnilisticikladionicalabel.Visible;
            listBoxDobitniListiciKladionica.Visible = !listBoxDobitniListiciKladionica.Visible;
            dohvatiKladionicaListiceIzBaze();
        }

        private void ButtonPokreni_parove_Click(object sender, EventArgs e) => klijent.odigrajKolo();

        /*----------------------------------------------------------------------------------------------
                                             EUROJACKPOT
       -----------------------------------------------------------------------------------------------*/
        public void dohvatiEJListiceIzBaze()
        {
            var dobitniEJListici = from listic in this.docEJ.Elements("Root").Elements("EJListic")
                                     select new
                                     {
                                         ID = (string)listic.Element("ID"),
                                         vrijemeUplate = (string)listic.Element("vrijemeUplate"),
                                         brojevi = (string)listic.Element("brojevi"),
                                         pogodeni = (string)listic.Element("pogodeni"),
                                         nagrada = (string)listic.Element("nagrada")
                                     };

            listBoxDobitniListiciEJ.Items.Clear();
            foreach (var listic in dobitniEJListici)
            {
                string sadrzaj = listic.ID + "  |  " + listic.vrijemeUplate + "  |  " + listic.brojevi + "  |  " + listic.pogodeni ;
                sadrzaj += "Dobitak: " + listic.nagrada.ToString() + " kn";
                listBoxDobitniListiciEJ.Items.Add(sadrzaj);
            }
        }
        public void spremiEJListicUBazu(DateTime d, List<int> brojevi, List<int> pogodeni, double dobitak)
             {
            string stringBrojeva = "";
            string stringPogodenih = "";
            string stringNagrada = dobitak.ToString();
            foreach (int broj in brojevi) stringBrojeva += broj.ToString() + " ";
            foreach (int pogodeni_broj in pogodeni) stringPogodenih += pogodeni_broj.ToString() + " ";

            int noviID = this.docEJ.Descendants("EJListic").Count() + 1;

            XElement EJListic = new XElement("EJListic",
                new XElement("ID", noviID.ToString()),
                new XElement("vrijemeUplate", d.ToString()),
                new XElement("brojevi", stringBrojeva),
                new XElement("pogodeni", stringPogodenih),
                new XElement("nagrada", stringNagrada));
                

            this.docEJ.Root.Add(EJListic);
            this.docEJ.Save(this.pathEJ);
        }

        private void buttonPrikaziDobitneEJ_Click(object sender, EventArgs e)
        {
            label4.Visible = !label4.Visible;
            listBoxDobitniListiciEJ.Visible = !listBoxDobitniListiciEJ.Visible;
            dohvatiEJListiceIzBaze();
        }

        private void buttonIzvlacenjeEJ_Click(object sender, EventArgs e) => klijent.izvlacenjeEJ();

        public void zapisiIzvuceniEJBroj(int izvuceniBroj, int redniIzvuceni)
        {
            if (redniIzvuceni <= 5)
                textBoxEJglavni.Text += izvuceniBroj.ToString() + "  ";
            else
                textBoxEJekstra.Text += izvuceniBroj.ToString() + "  ";
        }

        public void zapisiIzvuceniEJBrojSortirano(int izvuceniBroj, int redniIzvuceni)
        {
            
            if (redniIzvuceni <= 5)
            {
                textBoxEJglavni.Text += izvuceniBroj.ToString() + "  ";
                textBoxEJglavni.BackColor = Color.SandyBrown;
            }
            else
            {
                textBoxEJekstra.Text += izvuceniBroj.ToString() + "  ";
                textBoxEJekstra.BackColor = Color.SandyBrown;
            }
        }

        internal void spremiUBazuSvihEJListica(List<int> brojevi)
        {
            string stringBrojeva = "";
            foreach (int broj in brojevi) stringBrojeva += broj.ToString() + " ";
            stringBrojeva = stringBrojeva.Remove(stringBrojeva.Length - 1, 1);
            XElement EJListic = new XElement("EJListic",
                new XElement("brojevi", stringBrojeva));

            this.docEJSvi.Root.Add(EJListic);
            this.docEJSvi.Save(this.pathEJSvi);
        }

        public List<Dictionary<int, int>> povuciIzBazeEJ()
        {
            List<Dictionary<int, int>> l = new List<Dictionary<int, int>>();
            Dictionary<int, int> stat = new Dictionary<int, int>();
            Dictionary<int, int> stat2 = new Dictionary<int, int>();
            for (int i = 1; i < 51; i++)
                stat.Add(i, 0);
            for (int i = 1; i < 11; i++)
                stat2.Add(i, 0);

            var EJListici = from listic in this.docEJSvi.Elements("Root").Elements("EJListic")
                            select new
                            {
                                brojevi = (string)listic.Element("brojevi")
                            };
            foreach (var listic in EJListici)
            {
                int[] array = listic.brojevi.Split(' ').Select(int.Parse).ToArray();
                for (int i = 0; i < 5; i++)
                    stat[array[i]]++;
                for (int i = 5; i < 7; i++)
                    stat2[array[i]]++;
            }
            l.Add(stat);
            l.Add(stat2);
            return l;
        }

    }
}
