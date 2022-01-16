
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
            this.buttonIzvlacenjeBinga = new System.Windows.Forms.Button();
            this.labelBingo = new System.Windows.Forms.Label();
            this.tabControlPoslovnica = new System.Windows.Forms.TabControl();
            this.tabPageLoto = new System.Windows.Forms.TabPage();
            this.buttonDobitniBingoListici = new System.Windows.Forms.Button();
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
            this.tabPageBingo.Controls.Add(this.buttonDobitniBingoListici);
            this.tabPageBingo.Controls.Add(this.buttonIzvlacenjeBinga);
            this.tabPageBingo.Controls.Add(this.labelBingo);
            this.tabPageBingo.Location = new System.Drawing.Point(4, 29);
            this.tabPageBingo.Name = "tabPageBingo";
            this.tabPageBingo.Size = new System.Drawing.Size(792, 417);
            this.tabPageBingo.TabIndex = 0;
            this.tabPageBingo.Text = "Bingo";
            // 
            // buttonIzvlacenjeBinga
            // 
            this.buttonIzvlacenjeBinga.Location = new System.Drawing.Point(188, 180);
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
            this.tabControlPoslovnica.Size = new System.Drawing.Size(800, 450);
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
            // buttonDobitniBingoListici
            // 
            this.buttonDobitniBingoListici.Location = new System.Drawing.Point(421, 180);
            this.buttonDobitniBingoListici.Name = "buttonDobitniBingoListici";
            this.buttonDobitniBingoListici.Size = new System.Drawing.Size(190, 45);
            this.buttonDobitniBingoListici.TabIndex = 2;
            this.buttonDobitniBingoListici.Text = "Prikaži dobitne listiće";
            this.buttonDobitniBingoListici.UseVisualStyleBackColor = true;
            // 
            // PoslovnicaForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlPoslovnica);
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
        private System.Windows.Forms.Button buttonIzvlacenjeBinga;
        private System.Windows.Forms.Button buttonDobitniBingoListici;
    }
}

