﻿namespace BarkodluSatis
{
    partial class fUrunGrubuEkle
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
            this.listUrunGrup = new System.Windows.Forms.ListBox();
            this.tUrunGrupAd = new BarkodluSatis.tStandart();
            this.lStandart1 = new BarkodluSatis.lStandart();
            this.bSil = new BarkodluSatis.bStandart();
            this.bEkle = new BarkodluSatis.bStandart();
            this.SuspendLayout();
            // 
            // listUrunGrup
            // 
            this.listUrunGrup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listUrunGrup.FormattingEnabled = true;
            this.listUrunGrup.ItemHeight = 20;
            this.listUrunGrup.Location = new System.Drawing.Point(12, 91);
            this.listUrunGrup.Name = "listUrunGrup";
            this.listUrunGrup.Size = new System.Drawing.Size(250, 164);
            this.listUrunGrup.TabIndex = 2;
            // 
            // tUrunGrupAd
            // 
            this.tUrunGrupAd.BackColor = System.Drawing.Color.White;
            this.tUrunGrupAd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tUrunGrupAd.Location = new System.Drawing.Point(12, 59);
            this.tUrunGrupAd.Name = "tUrunGrupAd";
            this.tUrunGrupAd.Size = new System.Drawing.Size(250, 26);
            this.tUrunGrupAd.TabIndex = 1;
            // 
            // lStandart1
            // 
            this.lStandart1.AutoSize = true;
            this.lStandart1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lStandart1.ForeColor = System.Drawing.Color.Blue;
            this.lStandart1.Location = new System.Drawing.Point(12, 27);
            this.lStandart1.Name = "lStandart1";
            this.lStandart1.Size = new System.Drawing.Size(120, 20);
            this.lStandart1.TabIndex = 0;
            this.lStandart1.Text = "Ürün Grubu Adı";
            // 
            // bSil
            // 
            this.bSil.BackColor = System.Drawing.Color.Firebrick;
            this.bSil.FlatAppearance.BorderColor = System.Drawing.Color.Firebrick;
            this.bSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSil.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bSil.Image = global::BarkodluSatis.Properties.Resources.trash1;
            this.bSil.Location = new System.Drawing.Point(12, 259);
            this.bSil.Margin = new System.Windows.Forms.Padding(1);
            this.bSil.Name = "bSil";
            this.bSil.Size = new System.Drawing.Size(116, 79);
            this.bSil.TabIndex = 0;
            this.bSil.Text = "Sil";
            this.bSil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bSil.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.bSil.UseVisualStyleBackColor = false;
            this.bSil.Click += new System.EventHandler(this.bSil_Click);
            // 
            // bEkle
            // 
            this.bEkle.BackColor = System.Drawing.Color.Tomato;
            this.bEkle.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.bEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEkle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bEkle.Image = global::BarkodluSatis.Properties.Resources.plus;
            this.bEkle.Location = new System.Drawing.Point(147, 259);
            this.bEkle.Margin = new System.Windows.Forms.Padding(1);
            this.bEkle.Name = "bEkle";
            this.bEkle.Size = new System.Drawing.Size(115, 79);
            this.bEkle.TabIndex = 0;
            this.bEkle.Text = "Ekle";
            this.bEkle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bEkle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.bEkle.UseVisualStyleBackColor = false;
            this.bEkle.Click += new System.EventHandler(this.bEkle_Click);
            // 
            // fUrunGrubuEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(279, 366);
            this.Controls.Add(this.bSil);
            this.Controls.Add(this.bEkle);
            this.Controls.Add(this.listUrunGrup);
            this.Controls.Add(this.tUrunGrupAd);
            this.Controls.Add(this.lStandart1);
            this.Name = "fUrunGrubuEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ürün Grubu İşlemleri";
            this.Load += new System.EventHandler(this.fUrunGrubuEkle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private lStandart lStandart1;
        private tStandart tUrunGrupAd;
        private System.Windows.Forms.ListBox listUrunGrup;
        private bStandart bEkle;
        private bStandart bSil;
    }
}