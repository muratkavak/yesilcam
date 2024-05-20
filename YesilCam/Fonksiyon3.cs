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
    public partial class Fonksiyon3 : Form
    {
        private const string ConnectionString = "Server=localhost;Port=5432;Database=YesilCam;User Id=postgres;Password=123;";
        public Fonksiyon3()
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

                    string query = "SELECT o.gercek_isim, o.gercek_soyisim, COUNT(of.film_id) AS film_sayisi FROM public.\"Oyuncu_Film\" of " +
                                   "JOIN public.\"Oyuncu_ve_Yonetmen\" o ON of.kisi_id = o.kisi_id " +
                                   "JOIN public.\"Filmler\" f ON of.film_id = f.film_id " +
                                   "WHERE f.kisi_id = (SELECT kisi_id FROM public.\"Oyuncu_ve_Yonetmen\" WHERE gercek_isim = @ad AND gercek_soyisim = @soyad) " +
                                   "GROUP BY o.gercek_isim, o.gercek_soyisim";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ad", yonetmenAdi);
                        command.Parameters.AddWithValue("@soyad", yonetmenSoyadi);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string oyuncuAdi = reader["gercek_isim"].ToString();
                                string oyuncuSoyadi = reader["gercek_soyisim"].ToString();
                                int filmSayisi = Convert.ToInt32(reader["film_sayisi"]);
                                MessageBox.Show($"Yönetmenin çalıştığı oyuncu: {oyuncuAdi} {oyuncuSoyadi}\nYaptığı film sayısı: {filmSayisi}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
