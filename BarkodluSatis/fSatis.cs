﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatis
{
    public partial class fSatis : Form
    {
        public fSatis()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fSatis_Load(object sender, EventArgs e)
        {
            HizliButonDoldur();
            b5.Text = 5.ToString("C2");
            b10.Text = 10.ToString("C2");
            b20.Text = 20.ToString("C2");
            b50.Text = 50.ToString("C2");
            b100.Text = 100.ToString("C2");
            b200.Text = 200.ToString("C2");
        }

        private void HizliButonDoldur()
        {

            var hizliurun = db.HizliUrun.ToList();

            foreach (var h in hizliurun)
            {

                Button bh = this.Controls.Find("Bh" + h.Id, true).FirstOrDefault() as Button;

                if (bh != null)
                {


                    double fiyat = Islemler.DoubleYap(h.Fiyat.ToString());
                    bh.Text = h.UrunAd + "\n" + fiyat.ToString("C2");
                }

            }

        }
        private void HizliButonClick(object sender, EventArgs e)
        {

            Button b = (Button)sender;
            int butonid = Convert.ToInt32(b.Name.ToString().Substring(2, b.Name.Length - 2));

            if (b.Text.ToString().StartsWith("-"))
            {

                fHizliButonUrunEkle f = new fHizliButonUrunEkle();

                f.lButonId.Text = butonid.ToString();
                f.ShowDialog();

            }

            else
            {

                double xmik = 1;
                double.TryParse(tMiktar.Text.Trim(), out xmik);
                xmik = xmik == 0 ? 1 : xmik;
                var urunbarkod = db.HizliUrun.Where(a => a.Id == butonid).Select(a => a.Barkod).FirstOrDefault();
                var urun = db.Urun.Where(a => a.Barkod == urunbarkod).FirstOrDefault();
                UrunGetirListeye(urun, urunbarkod, xmik);
                GenelToplam();
            }

        }
        BarkodDBEntities db = new BarkodDBEntities();

        private void tBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barkod = tBarkod.Text.Trim();

                if (barkod.Length <= 2)
                {
                    tMiktar.Text = barkod;
                    tBarkod.Clear();
                    tBarkod.Focus();

                }
                else
                {
                    if (db.Urun.Any(a => a.Barkod == barkod))
                    {
                        var urun = db.Urun.Where(a => a.Barkod == barkod).FirstOrDefault();
                        UrunGetirListeye(urun, barkod, Convert.ToDouble(tMiktar.Text));
                    }
                    else
                    {
                        int onek = Convert.ToInt32(barkod.Substring(0, 2));
                        if (db.Terazi.Any(a => a.TeraziOnEk == onek))
                        {

                            string teraziurunno = barkod.Substring(2, 5);

                            if (db.Urun.Any(a => a.Barkod == teraziurunno))
                            {
                                var urunterazi = db.Urun.Where(a => a.Barkod == teraziurunno).FirstOrDefault();
                                double miktarkg = Convert.ToDouble(barkod.Substring(7, 5)) / 100;
                                UrunGetirListeye(urunterazi, teraziurunno, miktarkg);
                               

                            }
                            else
                            {
                                Console.Beep(400,1000);


                            }
                        }
                        else
                        {
                            Console.Beep(400, 1000);
                            fUrunGiris f = new fUrunGiris();
                            f.tBarkod.Text = barkod;
                            f.ShowDialog();
                        }


                    }
                    gridSatisListesi.ClearSelection();
                    GenelToplam();
                }

            }


        }

        private void GenelToplam()
        {

            
                double toplam = 0;

                for (int i = 0; i < gridSatisListesi.Rows.Count; i++)
                {

                    toplam += Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Toplam"].Value);
                }

                tGenelToplam.Text = toplam.ToString("C2");
                tMiktar.Text = "1";
                tBarkod.Focus();
                tBarkod.Clear();

            
        }
        private void UrunGetirListeye(Urun urun, string barkod, double miktar)
        {


            int satirsayisi = gridSatisListesi.Rows.Count;
            //double miktar = Convert.ToDouble(tMiktar.Text);
            bool eklenmismi = false;

            if (satirsayisi > 0)
            {
                for (int i = 0; i < satirsayisi; i++)
                {

                    if (gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString() == barkod)
                    {
                        gridSatisListesi.Rows[i].Cells["Miktar"].Value = miktar + Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value);
                        gridSatisListesi.Rows[i].Cells["Toplam"].Value = Math.Round(Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value) * Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Fiyat"].Value), 2);
                        eklenmismi = true;



                    }
                }
            }

            if (!eklenmismi)
            {
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirsayisi].Cells["Barkod"].Value = barkod;
                gridSatisListesi.Rows[satirsayisi].Cells["UrunAdi"].Value = urun.UrunAd;
                gridSatisListesi.Rows[satirsayisi].Cells["UrunGrup"].Value = urun.UrunGrup;
                gridSatisListesi.Rows[satirsayisi].Cells["Birim"].Value = urun.Birim;
                gridSatisListesi.Rows[satirsayisi].Cells["Fiyat"].Value = urun.SatisFiyat;
                gridSatisListesi.Rows[satirsayisi].Cells["Miktar"].Value = miktar;
                gridSatisListesi.Rows[satirsayisi].Cells["Toplam"].Value = Math.Round(miktar * (double)urun.SatisFiyat, 2);
                gridSatisListesi.Rows[satirsayisi].Cells["AlisFiyat"].Value = urun.AlisFiyat;
                gridSatisListesi.Rows[satirsayisi].Cells["KdvTutari"].Value = urun.KdvTutari;
            }


        }

        private void gridSatisListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 9)
            {
                gridSatisListesi.Rows.Remove(gridSatisListesi.CurrentRow);

                gridSatisListesi.ClearSelection();
                GenelToplam();
                tBarkod.Focus();
            }
        }
        private void bh_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                Button b = (Button)sender;

                if (!b.Text.StartsWith("-"))
                {
                    int butonid = Convert.ToInt16(b.Name.ToString().Substring(2, b.Name.Length - 2));
                    ContextMenuStrip s = new ContextMenuStrip();
                    ToolStripMenuItem sil = new ToolStripMenuItem();
                    sil.Text = "Temizle - Buton No:" + butonid.ToString();
                    sil.Click += Sil_Click;
                    s.Items.Add(sil);
                    this.ContextMenuStrip = s;

                }

            }


        }

        private void Sil_Click(object sender, EventArgs e)
        {
            int butonid = Convert.ToInt16(sender.ToString().Substring(19, sender.ToString().Length - 19));
            var guncelle = db.HizliUrun.Find(butonid);
            guncelle.Barkod = "-";
            guncelle.UrunAd = "-";
            guncelle.Fiyat = 0;
            db.SaveChanges();
            double fiyat = 0;
            Button b = this.Controls.Find("bH" + butonid, true).FirstOrDefault() as Button;
            b.Text = "-" + "\n" + fiyat.ToString("C2");
        }

        private void Bnx_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == ",")
            {

                int virgul = tNumarator.Text.Count(x => x == ',');

                if (virgul < 1)
                {
                    tNumarator.Text += b.Text;

                }
            }
            else if (b.Text == "<")
            {
                if (tNumarator.Text.Length > 0)
                {

                    tNumarator.Text = tNumarator.Text.Substring(0, tNumarator.Text.Length - 1);

                }

            }
            else
            {

                tNumarator.Text += b.Text;
            }


        }

        private void bOdenen_Click(object sender, EventArgs e)
        {

            
            if (tNumarator.Text != "")
            {
  
                double sonuc = Islemler.DoubleYap(tNumarator.Text)- Islemler.DoubleYap(tGenelToplam.Text);
                tParaUstu.Text = sonuc.ToString("C2");
                tOdenen.Text = Islemler.DoubleYap(tNumarator.Text).ToString("C2");
                tNumarator.Clear();
                tBarkod.Focus();
                
            }
        }
        private void bAdet_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {
                tMiktar.Text = tNumarator.Text;
                tNumarator.Clear();
                tBarkod.Clear();
                tBarkod.Focus();

            }
        }

        private void bBarkod_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text != "")
            {

                if (db.Urun.Any(a => a.Barkod == tNumarator.Text))
                {
                    double xmik = 1;
                    double.TryParse(tMiktar.Text.Trim(), out xmik);
                    xmik = xmik == 0 ? 1 : xmik;
                    var urun = db.Urun.Where(a => a.Barkod == tNumarator.Text).FirstOrDefault();
                    UrunGetirListeye(urun, tNumarator.Text, xmik);
                    tNumarator.Clear();
                    tBarkod.Focus();
                    GenelToplam();
                }




            }
        }
        private void tParaUstu_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            
            tNumarator.Text = b.Text;
             double sonuc  = Islemler.DoubleYap(tNumarator.Text) - Islemler.DoubleYap(tGenelToplam.Text);
            tOdenen.Text=Islemler.DoubleYap(b.Text).ToString("C2");
            tParaUstu.Text= sonuc.ToString("C2"); 
        }

        private void bDigerUrun_Click(object sender, EventArgs e)
        {
            if (tNumarator.Text !="")
            {
                int satirsayisi=gridSatisListesi.Rows.Count; 
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirsayisi].Cells["Barkod"].Value = "1111111111116";
                gridSatisListesi.Rows[satirsayisi].Cells["UrunAdi"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirsayisi].Cells["UrunGrup"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirsayisi].Cells["Birim"].Value = "Adet";
                gridSatisListesi.Rows[satirsayisi].Cells["Miktar"].Value = 1;
                gridSatisListesi.Rows[satirsayisi].Cells["AlisFiyat"].Value = 0;
                gridSatisListesi.Rows[satirsayisi].Cells["Fiyat"].Value = Convert.ToDouble(tNumarator.Text);
                gridSatisListesi.Rows[satirsayisi].Cells["KdvTutari"].Value = 0;
                gridSatisListesi.Rows[satirsayisi].Cells["Toplam"].Value = Convert.ToDouble(tNumarator.Text);
                tNumarator.Text = "";
                GenelToplam();
                tBarkod.Focus();
                
            }
        }

        private void bIade_Click(object sender, EventArgs e)
        {
            if (chSatisIadeislemi.Checked)
            {
                chSatisIadeislemi.Checked = false;
                chSatisIadeislemi.Text = "Satış Yapılıyor";

            }
            else
            {
                chSatisIadeislemi.Checked = true;
                chSatisIadeislemi.Text = "İade İşlemi";
            }
        }

        private void bTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Temizle()
        {
            tMiktar.Text="1";
            tBarkod.Clear();
            tOdenen.Clear();
            tParaUstu.Clear();
            tGenelToplam.Text = 0.ToString("C2");
            chSatisIadeislemi.Checked = false;
            tNumarator.Clear();
            gridSatisListesi.Rows.Clear();
            tBarkod.Clear();
            tBarkod.Focus();

        }


        public void SatisYap(string odemesekli)
        {

            int satirsayisi=gridSatisListesi.Rows.Count;
            bool satisiade = chSatisIadeislemi.Checked;
            double alisfiyattoplam=0;

            if (satirsayisi>0)
            {
                int? islemno = db.Islem.First().IslemNo;
                Satis satis = new Satis();
                for (int i = 0; i<satirsayisi; i++)
                {
                    satis.IslemNo = islemno;
                    satis.UrunAd = gridSatisListesi.Rows[i].Cells["UrunAdi"].Value.ToString();
                    satis.UrunGrup = gridSatisListesi.Rows[i].Cells["UrunGrup"].Value.ToString();
                    satis.Barkod = gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString();
                    satis.Birim = gridSatisListesi.Rows[i].Cells["Birim"].Value.ToString();
                    satis.AlisFiyat = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString());
                    satis.SatisFiyat = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Fiyat"].Value.ToString());
                    satis.Miktar = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.Toplam = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Toplam"].Value.ToString());
                    satis.KdvTutari = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["KdvTutari"].Value.ToString()) * Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.OdemeSekli = odemesekli;
                    satis.Iade = satisiade;
                    satis.Tarih=DateTime.Now;
                    satis.Kullanici = lKullanici.Text;
                    db.Satis.Add(satis);
                    db.SaveChanges();
                    


                    if (!satisiade)
                    {
                        Islemler.StokAzalt(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString(), Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                    }
                    else
                    {
                        Islemler.StokArtir(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString(), Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                    }

                    alisfiyattoplam += Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyat"].Value.ToString()) * Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());

                }    

                IslemOzet io=new IslemOzet();
                io.IslemNo = islemno;
                io.Iade = satisiade;
                io.AlisFiyatToplam = alisfiyattoplam;
                
                io.Gelir = false;
                io.Gider = false;

                if (!satisiade)
                {
                    io.Aciklama = odemesekli + "Satış";
                   
                }
                else
                {
                    io.Aciklama = "İade İşlemi(" + odemesekli + ")";
                }
                io.OdemeSekli = odemesekli;
                io.Kullanici = lKullanici.Text;
                io.Tarih = DateTime.Now;
                switch(odemesekli)
                {
                    case "Nakit":

                        double xsonuc=0;
                        io.Kart = 0;
                        Double.TryParse(tGenelToplam.Text.Trim(), out xsonuc);
                        io.Nakit = xsonuc;
                        break;
                    case "Kart":
                        io.Nakit = 0;
                        io.Kart = Islemler.DoubleYap(tGenelToplam.Text.ToString());
                        break;
                    case "Kart-Nakit":
                        io.Nakit = Islemler.DoubleYap(lNakit.Text);
                        io.Kart = Islemler.DoubleYap(lKart.Text);
                        break;
                }
                db.IslemOzet.Add(io);
                db.SaveChanges();

                var islemnoartir=db.Islem.First();
                islemnoartir.IslemNo += 1;
                db.SaveChanges();
               
                MessageBox.Show("Yazdırma İşlemi Yap");
                Temizle();
            }
        }

        private void bNakit_Click(object sender, EventArgs e)
        {
            SatisYap("Nakit");
        }

        private void bKart_Click(object sender, EventArgs e)
        {
            SatisYap("Kart");
        }

        private void bNakitKart_Click(object sender, EventArgs e)
        {
            fNakitKart f=new fNakitKart();
            f.ShowDialog();
        }

        private void tBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void tMiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void tNumarator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void fSatis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)

                SatisYap("Nakit");

            if (e.KeyCode == Keys.F2)

                SatisYap("Kart");

            if (e.KeyCode == Keys.F3)
            {

                fNakitKart f=new fNakitKart();
                f.ShowDialog();
            }

        

        }

        private void bIslemBeklet_Click(object sender, EventArgs e)
        {

            if(bIslemBeklet.Text=="İşlem Bekletme")
            {
               Bekle();
               bIslemBeklet.BackColor = Color.OrangeRed;
                bIslemBeklet.Text = "İşlem Bekletiliyor";
                gridSatisListesi.Rows.Clear();
            }
            else
            {
                BeklemedenCik();
                bIslemBeklet.BackColor= Color.DarkCyan;
                bIslemBeklet.Text = "İşlem Beklet";
                gridBekle.Rows.Clear();
            }
            
        }

        private void Bekle()
        {
            int satir = gridSatisListesi.Rows.Count;
            int sutun = gridSatisListesi.Columns.Count;
            if (satir>0)
            {
                for(int i = 0; i < satir; i++)
                {
                    gridBekle.Rows.Add();
                    for (int j = 0; j < sutun-1; j++)
                    {
                        gridBekle.Rows[i].Cells[j].Value = gridSatisListesi.Rows[i].Cells[j].Value; 
                      

                    }
                }

            }
        }

        private void BeklemedenCik()
        {
            int satir = gridBekle.Rows.Count;
            int sutun = gridBekle.Columns.Count;
            if (satir > 0)
            {
                for (int i = 0; i < satir; i++)
                {
                    gridSatisListesi.Rows.Add();
                    for (int j = 0; j < sutun - 1; j++)
                    {
                        gridSatisListesi.Rows[i].Cells[j].Value = gridBekle.Rows[i].Cells[j].Value;

                        
                    }
                }

            }

        }

        private void chSatisIadeislemi_CheckedChanged(object sender, EventArgs e)
        {
            if(chSatisIadeislemi.Checked)
            {
                chSatisIadeislemi.Text = "İade Yapılıyor";
            }
            else
            {
                chSatisIadeislemi.Text = "Satış Yapılıyor";
            }
        }
    }
}


