﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatis
{
    public partial class fBaslangic : Form
    {
        public fBaslangic()
        {
            InitializeComponent();
        }

        private void bSatisIslemi_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fSatis f =new fSatis();
            f.lKullanici.Text =lKullanici.Text;
            f.ShowDialog();
            Cursor.Current = Cursors.Default;

        }

        private void bGenelRapor_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fRapor f =new fRapor(); 
           // f.lKullanici.Text=lKullanici.Text;
           f.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bStokTakip_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fStok f =new fStok();
            f.lKullanici.Text=lKullanici.Text;
            f.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bUrunGiris_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fUrunGiris f =new fUrunGiris();
            f.lKullaniciG.Text = lKullanici.Text;
            f.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void bCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bFiyatGuncelle_Click(object sender, EventArgs e)
        {
            fFiyatGuncelle f=new fFiyatGuncelle();
            f.ShowDialog();
        }
    }
}
