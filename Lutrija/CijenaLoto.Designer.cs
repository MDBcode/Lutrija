namespace Lutrija
{
    partial class CijenaLoto
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelJoker = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelJokerCijena = new System.Windows.Forms.Label();
            this.button_potvrdaCijene = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_cijena = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(33, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loto 6:";
            // 
            // labelJoker
            // 
            this.labelJoker.AutoSize = true;
            this.labelJoker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelJoker.Location = new System.Drawing.Point(33, 110);
            this.labelJoker.Name = "labelJoker";
            this.labelJoker.Size = new System.Drawing.Size(61, 20);
            this.labelJoker.TabIndex = 1;
            this.labelJoker.Text = "Joker:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(251, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "2,00 kn";
            // 
            // labelJokerCijena
            // 
            this.labelJokerCijena.AutoSize = true;
            this.labelJokerCijena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelJokerCijena.Location = new System.Drawing.Point(251, 110);
            this.labelJokerCijena.Name = "labelJokerCijena";
            this.labelJokerCijena.Size = new System.Drawing.Size(62, 20);
            this.labelJokerCijena.TabIndex = 3;
            this.labelJokerCijena.Text = "5,00 kn";
            // 
            // button_potvrdaCijene
            // 
            this.button_potvrdaCijene.BackColor = System.Drawing.Color.LightGreen;
            this.button_potvrdaCijene.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_potvrdaCijene.Location = new System.Drawing.Point(0, 213);
            this.button_potvrdaCijene.Name = "button_potvrdaCijene";
            this.button_potvrdaCijene.Size = new System.Drawing.Size(338, 36);
            this.button_potvrdaCijene.TabIndex = 4;
            this.button_potvrdaCijene.Text = "U redu";
            this.button_potvrdaCijene.UseVisualStyleBackColor = false;
            this.button_potvrdaCijene.Click += new System.EventHandler(this.button_potvrdaCijene_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(294, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "_________________________________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(33, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Iznos uplate:";
            // 
            // label_cijena
            // 
            this.label_cijena.AutoSize = true;
            this.label_cijena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_cijena.Location = new System.Drawing.Point(251, 146);
            this.label_cijena.Name = "label_cijena";
            this.label_cijena.Size = new System.Drawing.Size(0, 20);
            this.label_cijena.TabIndex = 7;
            // 
            // CijenaLoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 249);
            this.Controls.Add(this.label_cijena);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_potvrdaCijene);
            this.Controls.Add(this.labelJokerCijena);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelJoker);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CijenaLoto";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cijena";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelJoker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelJokerCijena;
        private System.Windows.Forms.Button button_potvrdaCijene;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_cijena;
    }
}