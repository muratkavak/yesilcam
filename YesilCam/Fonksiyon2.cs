using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace YesilCam
{
    public partial class Fonksiyon2 : Form
    {
        private const string ConnectionString = "Server=localhost;Port=5432;Database=YesilCam;User Id=postgres;Password=123;";

        public Fonksiyon2()
        {
            InitializeComponent();
            YonetmenleriListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (yonetmenListBox.SelectedItem != null)
            {
                string selectedYonetmen = yonetmenListBox.SelectedItem.ToString();
                string[] yonetmenAdSoyad = selectedYonetmen.Split(' ');
                string yonetmenAdi = yonetmenAdSoyad[0];
                string yonetmenSoyadi = yonetmenAdSoyad[1];

                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT f.isim AS film_adi, od.isim AS odul_isim " +
                                   "FROM public.\"Filmler\" f " +
                                   "JOIN public.\"Oyuncu_ve_Yonetmen\" o ON f.kisi_id = o.kisi_id " +
                                   "JOIN public.\"Oduller\" od ON f.film_id = od.film_id " +
                                   "WHERE o.gercek_isim = @ad AND o.gercek_soyisim = @soyad";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", yonetmenAdi);
                        command.Parameters.AddWithValue("@soyad", yonetmenSoyadi);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            StringBuilder filmVeOdulListesi = new StringBuilder();

                            while (reader.Read())
                            {
                                string filmAdi = reader["film_adi"].ToString();
                                string odulIsim = reader["odul_isim"].ToString();

                                filmVeOdulListesi.AppendLine($"Film: {filmAdi}\nAldığı ödül: {odulIsim}\n");
                            }

                            if (filmVeOdulListesi.Length > 0)
                            {
                                MessageBox.Show(filmVeOdulListesi.ToString(), "Yönetmenin Filmleri ve Aldığı Ödüller", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Seçilen yönetmenin çektiği film veya aldığı ödül bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir yönetmen seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void YonetmenleriListele()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string yonetmenQuery = "SELECT gercek_isim,gercek_soyisim FROM public.\"Oyuncu_ve_Yonetmen\" WHERE kisi_tipi = 'Yonetmen'";
                using (NpgsqlCommand command = new NpgsqlCommand(yonetmenQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string yonetmenAdi = reader["gercek_isim"].ToString();
                            string yonetmenSoyadi = reader["gercek_soyisim"].ToString();
                            yonetmenListBox.Items.Add(yonetmenAdi + " " + yonetmenSoyadi);
                        }
                    }
                }
            }
        }
    }
}
