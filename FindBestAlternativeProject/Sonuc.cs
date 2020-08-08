using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindBestAlternativeProject
{
    public partial class Sonuc : Form
    {
        string yontem;
        double[,] data;

        DataGridView dgv;


        double[] topsisBolum;
        double[,] topsisRMatris;
        double[,] topsisAgirlikRMatris;
        double[,] topsisPozitifMatris;
        double[,] topsisNegatifMatris;
        double[] topsisSiNegatif;
        double[] topsisSiPozitif;
        double[] cDegeri;

        

        double[] pozitifIdeal;
        double[] negatifIdeal;

        double[] agirlik;
        double ortalamaToplam;
        double[] ortalama;
        double[] standartSapma;



        double[] vikorFiPozitif;
        double[] vikorFiNegatif;
        double[] vikorSj;
        double[] vikorRj;
        double[,] vikorNormalizasyon;
        double[,] vikorIdeal;
        double[,] vikorQDegeri;
        double[] vikorQSabiti;
        double vikorRPozitif;
        double vikorRNegatif;
        double vikorSPozitif;
        double vikorSNegatif;

        double[] vikorSonuc;




        int x;
        int y;

        public Sonuc(string yontem, double[,] data, DataGridView dgv, int x, int y)
        {
            InitializeComponent();
            this.yontem = yontem;
            this.data = data;
            this.x = x;
            this.dgv = dgv;

            this.y = y;
            topsisBolum = new double[y];
            topsisRMatris = new double[x, y];
            topsisAgirlikRMatris = new double[x, y];
            topsisPozitifMatris = new double[x, y];
            topsisNegatifMatris = new double[x, y];

            ortalama = new double[y];
            negatifIdeal = new double[y];
            pozitifIdeal = new double[y];
            cDegeri = new double[x];
            standartSapma = new double[y];
            agirlik = new double[y];
            topsisSiNegatif = new double[x];
            topsisSiPozitif = new double[x];


            // Vikor


            vikorNormalizasyon = new double[x, y];
            vikorIdeal = new double[x, y];
            vikorSj = new double[x];
            vikorRj = new double[x];

            vikorFiNegatif = new double[y];
            vikorFiPozitif = new double[y];
            vikorQDegeri = new double[x, 5];
            vikorQSabiti = new double[5] { 0, 0.25, 0.50, 0.75, 1};

            vikorSonuc = new double[x];

            





        }

        private void Sonuc_Load(object sender, EventArgs e)
        {
            if (yontem == "Topsis")
            {
                Topsis();
            }
            else
            {
                Vikor();
            }
        }

        void Hesapla()
        {
            // Ortalama Bulma
            for (int i = 0; i < y; i++)
            {
                double b = 0;
                for (int j = 0; j < x; j++)
                {
                    b += data[j, i];

                }
                ortalama[i] = b / x;

                ortalamaToplam += ortalama[i];
            }
            // Standart Sapma
            for (int i = 0; i < y; i++)
            {
                double a = 0;
                for (int j = 0; j < x; j++)
                {
                    a += ((data[j, i] - ortalama[i]) * (data[j, i] - ortalama[i]));
                }
                standartSapma[i] = a / x - 1;
            }
            // Ağırlık Hesabı

            for (int i = 0; i < y; i++)
            {
                agirlik[i] = ortalama[i] / ortalamaToplam;
            }
        }

        void Vikor()
        {
            Hesapla();






            // Fi + ve - Değerleri
            double fiMin;
            double fiMax;
            for (int i = 0; i < y; i++)
            {
                fiMin = data[0, i];
                fiMax = data[0, i];
                for (int j = 0; j < x; j++)
                {
                    if (data[j, i] > fiMax)
                    {
                        fiMax = data[j, i];
                    }
                    if (data[j, i] < fiMin)
                    {
                        fiMin = data[j, i];
                    }
                }
                vikorFiPozitif[i] = fiMax;
                vikorFiNegatif[i] = fiMin;
            }


            // Normalizasyon Matrisinin Oluşturulması



            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    vikorNormalizasyon[j, i] = (vikorFiPozitif[i] - data[j, i]) / (vikorFiPozitif[i] - vikorFiNegatif[i]);


                }
            }


            // Ideal Çözüm


            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    vikorIdeal[j, i] = vikorNormalizasyon[j, i] * agirlik[i];

                }
            }


            // Sj ve Rj Bulunması 
            for (int i = 0; i < x; i++)
            {
                vikorRj[i] = vikorIdeal[i, 0];
                for (int j = 0; j < y; j++)
                {
                    vikorSj[i] += vikorIdeal[i, j];
                    if (vikorIdeal[i, j] > vikorRj[i])
                    {
                        vikorRj[i] = vikorIdeal[i, j];
                    }

                }
            }

            // R+ ve R- , S+ ve S- Bulunması



            double rArti = vikorRj[0];
            double rEksi = vikorRj[0];
            double sArti = vikorSj[0];
            double sEksi = vikorSj[0];

            for (int i = 0; i < x; i++)
            {
                if (vikorRj[i] < rArti)
                {
                    rArti = vikorRj[i];
                }
                if (vikorRj[i] > rEksi)
                {
                    rEksi = vikorRj[i];
                }

                if (vikorSj[i] > sEksi)
                {
                    sEksi = vikorSj[i];
                }
                if (vikorSj[i] < sArti)
                {
                    sArti = vikorSj[i];
                }
            }

            vikorSNegatif = sEksi;
            vikorSPozitif = sArti;
            vikorRNegatif = rEksi;
            vikorRPozitif = rArti;



            

            // Q Hesaplaması

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    vikorQDegeri[j, i] = vikorQSabiti[i] * (vikorSj[j] - vikorSPozitif) / (vikorSNegatif - vikorSPozitif) + (1 - vikorQSabiti[i]) * (vikorRj[j] - vikorRPozitif) / (vikorRNegatif - vikorRPozitif);
                    
                }
            }


            dataGridView1.RowCount = x;
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Sj";
            dataGridView1.Columns[1].HeaderText = "Rj";
            dataGridView1.Columns[2].HeaderText = "Qj";

            //Sonuçları Göster
            for (int i = 0; i < x; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = vikorSj[i];
                dataGridView1.Rows[i].Cells[1].Value = vikorRj[i];
                dataGridView1.Rows[i].Cells[2].Value = vikorQDegeri[i,2];
            }
            

            for (int i = 0; i < x; i++)
            {
                vikorSonuc[i] = vikorQDegeri[i, 2];
            }


            // Sıralama
            for (int i = 0; i < vikorSonuc.Length - 1; i++)
            {
                for (int j = 1; j < vikorSonuc.Length - i; j++)
                {
                    if (vikorSonuc[j] < vikorSonuc[j - 1])
                    {
                        double gecici = vikorSonuc[j - 1];
                        vikorSonuc[j - 1] = vikorSonuc[j];
                        vikorSonuc[j] = gecici;
                    }
                }
            }


            int count = 1000;
            for (int i = 0; i < x; i++)
            {
                if (vikorSonuc[0] == vikorQDegeri[i, 2])
                {
                    count = i;
                }
            }


            if (count == 1000)
            {
                MessageBox.Show("Hata.");
            }
            else
            {
                label1.Text = "En iyi seçenek olarak " + dgv.Rows[count].HeaderCell.Value.ToString() + " alternatifi belirlenmiştir.";
            }


        }





        void Topsis()
        {

            Hesapla();

            // -------------- Normalize Matris Bulma ------------------
            for (int i = 0; i < y; i++)
            {
                double a = 0;
                for (int j = 0; j < x; j++)
                {
                    a += (data[j, i] * data[j, i]);
                }
                this.topsisBolum[i] = a;
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    topsisRMatris[i, j] = data[i, j] / topsisBolum[j];
                }
            }


            // -------------- Ağırlıklandırılmış Matris Bulma ------------------


            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    topsisAgirlikRMatris[i, j] = topsisRMatris[i, j] * agirlik[j];



                }
            }


            // İdeal Çözüm

            for (int i = 0; i < y; i++)
            {
                double max = topsisAgirlikRMatris[0, i];
                double min = topsisAgirlikRMatris[0, i];
                for (int j = 0; j < x; j++)
                {
                    if (topsisAgirlikRMatris[j, i] > max)
                    {
                        max = topsisAgirlikRMatris[j, i];
                    }
                    if (topsisAgirlikRMatris[j, i] < min)
                    {
                        min = topsisAgirlikRMatris[j, i];
                    }
                }
                negatifIdeal[i] = min;
                pozitifIdeal[i] = max;
            }


            // Pozitif ve Negatif Ayırım Ölçülerinin Yapılması
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    topsisPozitifMatris[j, i] = Math.Pow(topsisAgirlikRMatris[j, i] - pozitifIdeal[i], 2);
                    topsisNegatifMatris[j, i] = Math.Pow(topsisAgirlikRMatris[j, i] - negatifIdeal[i], 2);

                }
            }


            // Si + ve - Değerleri

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    topsisSiPozitif[i] += topsisPozitifMatris[i, j];
                    topsisSiNegatif[i] += topsisNegatifMatris[i, j];
                }
                topsisSiPozitif[i] = Math.Sqrt(topsisSiPozitif[i]);
                topsisSiNegatif[i] = Math.Sqrt(topsisSiNegatif[i]);
            }


            // c Değeri Hesaplama

            for (int i = 0; i < x; i++)
            {
                cDegeri[i] = (topsisSiNegatif[i] / (topsisSiPozitif[i] + topsisSiNegatif[i]));

            }

     


            // Sonuç Yazdırma
            double sonucMax = cDegeri[0];
            int indisMax = 0;
            for (int i = 0; i < x; i++)
            {
                if (cDegeri[i] > sonucMax)
                {
                    sonucMax = cDegeri[i];
                    indisMax = i;
                }

            }


            dataGridView1.RowCount = x;
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Si-";
            dataGridView1.Columns[1].HeaderText = "Si+";
            dataGridView1.Columns[2].HeaderText = "C*";



            for (int j = 0; j < x; j++)
            {

                dataGridView1.Rows[j].HeaderCell.Value = dgv.Rows[j].HeaderCell.Value;
                dataGridView1.Rows[j].Cells[0].Value = topsisSiNegatif[j];  // Si-
                dataGridView1.Rows[j].Cells[1].Value = topsisSiPozitif[j];  // Si+
                dataGridView1.Rows[j].Cells[2].Value = cDegeri[j];  // C*
            } 
            
            label1.Text = "En iyi seçenek olarak " + dgv.Rows[indisMax].HeaderCell.Value.ToString()+" alternatifi belirlenmiştir.";

        }


    }
}
