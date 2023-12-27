using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatis
{
    static class Islemler
    {
        public static double DoubleYap(string deger)
        {

            double sonuc;
            double.TryParse(deger, NumberStyles.Currency, CultureInfo.CurrentUICulture.NumberFormat, out sonuc);

            return Math.Round(sonuc, 2);

        }

        public static void StokAzalt(string barkod, double miktar)
        {

            using (var db = new BarkodDBEntities())
            {
                if (barkod != "1111111111116")
                {
                    var urunbilgi = db.Urun.SingleOrDefault(x => x.Barkod == barkod);
                    urunbilgi.Miktar -= miktar;
                    db.SaveChanges();
                }

            }
        }
        public static void StokArtir(string barkod, double miktar)
        {
            if (barkod != "1111111111116")
            {
                using (var db = new BarkodDBEntities())
                {

                    var urunbilgi = db.Urun.SingleOrDefault(x => x.Barkod == barkod);
                    urunbilgi.Miktar += miktar;
                    db.SaveChanges();
                }
            }
        }
        public static void StokHareket(string barkod, string urunad, string birim, double miktar, string urungrup, string kullanici)
        {
            using (var db = new BarkodDBEntities())
            {
                StokHareket sh = new StokHareket();
                sh.Barkod = barkod;
                sh.Birim = birim;
                sh.Miktar = miktar;
                sh.UrunAd = urunad;
                sh.UrunGrup = urungrup;
                sh.Kullanici = kullanici;
                sh.Tarih=DateTime.Now;
                db.StokHareket.Add(sh);
                db.SaveChanges();   


            }
        }
        public static int KartKomisyonu()
        {
            int sonuc = 0;
            using (var db=new BarkodDBEntities())
            {
                if (db.Sabit.Any())
                {
                    sonuc = Convert.ToInt16(db.Sabit.First().kartKomisyon);
                }
                else
                {
                    sonuc=0;
                }
                return sonuc;
            }
        }
    }
}
