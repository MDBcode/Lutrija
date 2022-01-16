
namespace Lutrija
{
    partial class Poslovnica
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLoto = new System.Windows.Forms.TabPage();
            this.tabPageBingo = new System.Windows.Forms.TabPage();
            this.tabPageKladionica = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLoto);
            this.tabControl1.Controls.Add(this.tabPageBingo);
            this.tabControl1.Controls.Add(this.tabPageKladionica);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageLoto
            // 
            this.tabPageLoto.Location = new System.Drawing.Point(4, 29);
            this.tabPageLoto.Name = "tabPageLoto";
            this.tabPageLoto.Size = new System.Drawing.Size(792, 417);
            this.tabPageLoto.TabIndex = 0;
            this.tabPageLoto.Text = "Loto";
            this.tabPageLoto.UseVisualStyleBackColor = true;
            // 
            // tabPageBingo
            // 
            this.tabPageBingo.Location = new System.Drawing.Point(4, 29);
            this.tabPageBingo.Name = "tabPageBingo";
            this.tabPageBingo.Size = new System.Drawing.Size(792, 417);
            this.tabPageBingo.TabIndex = 0;
            this.tabPageBingo.Text = "Bingo";
            this.tabPageBingo.UseVisualStyleBackColor = true;
            // 
            // tabPageKladionica
            // 
            this.tabPageKladionica.Location = new System.Drawing.Point(4, 29);
            this.tabPageKladionica.Name = "tabPageKladionica";
            this.tabPageKladionica.Size = new System.Drawing.Size(792, 417);
            this.tabPageKladionica.TabIndex = 0;
            this.tabPageKladionica.Text = "Kladionica";
            this.tabPageKladionica.UseVisualStyleBackColor = true;
            // 
            // Poslovnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Poslovnica";
            this.Text = "Poslovnica";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLoto;
        private System.Windows.Forms.TabPage tabPageBingo;
        private System.Windows.Forms.TabPage tabPageKladionica;
    }
}

