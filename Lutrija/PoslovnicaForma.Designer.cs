
namespace Lutrija
{
    partial class PoslovnicaForma
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPageKladionica = new System.Windows.Forms.TabPage();
            this.tabPageBingo = new System.Windows.Forms.TabPage();
            this.buttonDobitniBingoListici = new System.Windows.Forms.Button();
            this.buttonIzvlacenjeBinga = new System.Windows.Forms.Button();
            this.labelBingo = new System.Windows.Forms.Label();
            this.tabControlPoslovnica = new System.Windows.Forms.TabControl();
            this.tabPageLoto = new System.Windows.Forms.TabPage();
            this.labelIzvuceniBrojevi = new System.Windows.Forms.Label();
            this.textBoxIzvuceniBrojevi = new System.Windows.Forms.TextBox();
            this.labelBrojIzvucenih = new System.Windows.Forms.Label();
            this.labelRedniIzvuceni = new System.Windows.Forms.Label();
            this.listViewDobitniListici = new System.Windows.Forms.ListView();
            this.labelDobitniListici = new System.Windows.Forms.Label();
            this.tabPageBingo.SuspendLayout();
            this.tabControlPoslovnica.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageKladionica
            // 
            this.tabPageKladionica.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tabPageKladionica.Location = new System.Drawing.Point(4, 29);
            this.tabPageKladionica.Name = "tabPageKladionica";
            this.tabPageKladionica.Size = new System.Drawing.Size(792, 417);
            this.tabPageKladionica.TabIndex = 0;
            this.tabPageKladionica.Text = "Kladionica";
            // 
            // tabPageBingo
            // 
            this.tabPageBingo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tabPageBingo.Controls.Add(this.labelDobitniListici);
            this.tabPageBingo.Controls.Add(this.listViewDobitniListici);
            this.tabPageBingo.Controls.Add(this.labelRedniIzvuceni);
            this.tabPageBingo.Controls.Add(this.labelBrojIzvucenih);
            this.tabPageBingo.Controls.Add(this.textBoxIzvuceniBrojevi);
            this.tabPageBingo.Controls.Add(this.labelIzvuceniBrojevi);
            this.tabPageBingo.Controls.Add(this.buttonDobitniBingoListici);
            this.tabPageBingo.Controls.Add(this.buttonIzvlacenjeBinga);
            this.tabPageBingo.Controls.Add(this.labelBingo);
            this.tabPageBingo.Location = new System.Drawing.Point(4, 29);
            this.tabPageBingo.Name = "tabPageBingo";
            this.tabPageBingo.Size = new System.Drawing.Size(804, 605);
            this.tabPageBingo.TabIndex = 0;
            this.tabPageBingo.Text = "Bingo";
            // 
            // buttonDobitniBingoListici
            // 
            this.buttonDobitniBingoListici.Location = new System.Drawing.Point(423, 107);
            this.buttonDobitniBingoListici.Name = "buttonDobitniBingoListici";
            this.buttonDobitniBingoListici.Size = new System.Drawing.Size(190, 45);
            this.buttonDobitniBingoListici.TabIndex = 2;
            this.buttonDobitniBingoListici.Text = "Prikaži dobitne listiće";
            this.buttonDobitniBingoListici.UseVisualStyleBackColor = true;
            this.buttonDobitniBingoListici.Click += new System.EventHandler(this.buttonDobitniBingoListici_Click);
            // 
            // buttonIzvlacenjeBinga
            // 
            this.buttonIzvlacenjeBinga.Location = new System.Drawing.Point(180, 107);
            this.buttonIzvlacenjeBinga.Name = "buttonIzvlacenjeBinga";
            this.buttonIzvlacenjeBinga.Size = new System.Drawing.Size(190, 45);
            this.buttonIzvlacenjeBinga.TabIndex = 1;
            this.buttonIzvlacenjeBinga.Text = "Pokreni izvlačenje";
            this.buttonIzvlacenjeBinga.UseVisualStyleBackColor = true;
            this.buttonIzvlacenjeBinga.Click += new System.EventHandler(this.buttonIzvlacenjeBinga_Click);
            // 
            // labelBingo
            // 
            this.labelBingo.AutoSize = true;
            this.labelBingo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBingo.Location = new System.Drawing.Point(308, 40);
            this.labelBingo.Name = "labelBingo";
            this.labelBingo.Size = new System.Drawing.Size(163, 29);
            this.labelBingo.TabIndex = 0;
            this.labelBingo.Text = "BINGO 24 od 75";
            this.labelBingo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelBingo.UseCompatibleTextRendering = true;
            // 
            // tabControlPoslovnica
            // 
            this.tabControlPoslovnica.Controls.Add(this.tabPageLoto);
            this.tabControlPoslovnica.Controls.Add(this.tabPageBingo);
            this.tabControlPoslovnica.Controls.Add(this.tabPageKladionica);
            this.tabControlPoslovnica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPoslovnica.Location = new System.Drawing.Point(0, 0);
            this.tabControlPoslovnica.Name = "tabControlPoslovnica";
            this.tabControlPoslovnica.SelectedIndex = 0;
            this.tabControlPoslovnica.Size = new System.Drawing.Size(812, 638);
            this.tabControlPoslovnica.TabIndex = 0;
            // 
            // tabPageLoto
            // 
            this.tabPageLoto.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tabPageLoto.Location = new System.Drawing.Point(4, 29);
            this.tabPageLoto.Name = "tabPageLoto";
            this.tabPageLoto.Size = new System.Drawing.Size(792, 417);
            this.tabPageLoto.TabIndex = 0;
            this.tabPageLoto.Text = "Loto";
            // 
            // labelIzvuceniBrojevi
            // 
            this.labelIzvuceniBrojevi.AutoSize = true;
            this.labelIzvuceniBrojevi.Location = new System.Drawing.Point(56, 193);
            this.labelIzvuceniBrojevi.Name = "labelIzvuceniBrojevi";
            this.labelIzvuceniBrojevi.Size = new System.Drawing.Size(120, 20);
            this.labelIzvuceniBrojevi.TabIndex = 3;
            this.labelIzvuceniBrojevi.Text = "Izvučeni brojevi:";
            // 
            // textBoxIzvuceniBrojevi
            // 
            this.textBoxIzvuceniBrojevi.Location = new System.Drawing.Point(60, 216);
            this.textBoxIzvuceniBrojevi.Multiline = true;
            this.textBoxIzvuceniBrojevi.Name = "textBoxIzvuceniBrojevi";
            this.textBoxIzvuceniBrojevi.Size = new System.Drawing.Size(681, 89);
            this.textBoxIzvuceniBrojevi.TabIndex = 4;
            // 
            // labelBrojIzvucenih
            // 
            this.labelBrojIzvucenih.AutoSize = true;
            this.labelBrojIzvucenih.Location = new System.Drawing.Point(606, 193);
            this.labelBrojIzvucenih.Name = "labelBrojIzvucenih";
            this.labelBrojIzvucenih.Size = new System.Drawing.Size(114, 20);
            this.labelBrojIzvucenih.TabIndex = 5;
            this.labelBrojIzvucenih.Text = "Broj izvučenih: ";
            // 
            // labelRedniIzvuceni
            // 
            this.labelRedniIzvuceni.AutoSize = true;
            this.labelRedniIzvuceni.Location = new System.Drawing.Point(714, 193);
            this.labelRedniIzvuceni.Name = "labelRedniIzvuceni";
            this.labelRedniIzvuceni.Size = new System.Drawing.Size(51, 20);
            this.labelRedniIzvuceni.TabIndex = 6;
            this.labelRedniIzvuceni.Text = "label1";
            // 
            // listViewDobitniListici
            // 
            this.listViewDobitniListici.HideSelection = false;
            this.listViewDobitniListici.Location = new System.Drawing.Point(60, 363);
            this.listViewDobitniListici.Name = "listViewDobitniListici";
            this.listViewDobitniListici.Size = new System.Drawing.Size(681, 195);
            this.listViewDobitniListici.TabIndex = 7;
            this.listViewDobitniListici.UseCompatibleStateImageBehavior = false;
            // 
            // labelDobitniListici
            // 
            this.labelDobitniListici.AutoSize = true;
            this.labelDobitniListici.Location = new System.Drawing.Point(56, 340);
            this.labelDobitniListici.Name = "labelDobitniListici";
            this.labelDobitniListici.Size = new System.Drawing.Size(100, 20);
            this.labelDobitniListici.TabIndex = 8;
            this.labelDobitniListici.Text = "Dobitni listići:";
            // 
            // PoslovnicaForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 638);
            this.Controls.Add(this.tabControlPoslovnica);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PoslovnicaForma";
            this.Text = "Poslovnica";
            this.tabPageBingo.ResumeLayout(false);
            this.tabPageBingo.PerformLayout();
            this.tabControlPoslovnica.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageKladionica;
        private System.Windows.Forms.TabPage tabPageBingo;
        private System.Windows.Forms.TabControl tabControlPoslovnica;
        private System.Windows.Forms.TabPage tabPageLoto;
        private System.Windows.Forms.Label labelBingo;
        private System.Windows.Forms.Button buttonDobitniBingoListici;
        private System.Windows.Forms.Label labelIzvuceniBrojevi;
        public System.Windows.Forms.TextBox textBoxIzvuceniBrojevi;
        private System.Windows.Forms.Label labelBrojIzvucenih;
        public System.Windows.Forms.Label labelRedniIzvuceni;
        public System.Windows.Forms.Button buttonIzvlacenjeBinga;
        private System.Windows.Forms.Label labelDobitniListici;
        public System.Windows.Forms.ListView listViewDobitniListici;
    }
}

