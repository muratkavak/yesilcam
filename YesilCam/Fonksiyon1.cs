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
    public partial class Fonksiyon1 : Form
    {
        private const string ConnectionString = "Server=localhost;Port=5432;Database=YesilCam;User Id=postgres;Password=123;";

        public Fonksiyon1()
        {
            InitializeComponent();
            OyunculariListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (oyuncuListBox.SelectedItem != null)
            {
                try
                {
                    string selectedOyuncu = oyuncuListBox.SelectedItem.ToString();
                    string[] oyuncuAdSoyad = selectedOyuncu.Split(' ');
                    string oyuncuAdi = oyuncuAdSoyad[0];
                    string oyuncuSoyadi = oyuncuAdSoyad[1];

                    using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string query = "SELECT f.isim FROM public.\"Filmler\" f " +
                                        "JOIN public.\"Oyuncu_Film\" of ON f.film_id = of.film_id " +
                                        "JOIN public.\"Oyuncu_ve_Yonetmen\" o ON of.kisi_id = o.kisi_id " +
                                        "WHERE o.gercek_isim = @ad AND o.gercek_soyisim = @soyad";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ad", oyuncuAdi);
                            command.Parameters.AddWithValue("@soyad", oyuncuSoyadi);

                            StringBuilder filmListesi = new StringBuilder();

                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string filmAdi = reader["isim"].ToString();
                                    filmListesi.AppendLine($"- {filmAdi}");
                                }
                            }

                            if (filmListesi.Length > 0)
                            {
                                MessageBox.Show($"Oyuncunun oynadığı filmler:\n{filmListesi.ToString()}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Seçilen oyuncu hiç filmde oynamamış.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir oyuncu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void OyunculariListele()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string yonetmenQuery = "SELECT gercek_isim,gercek_soyisim FROM public.\"Oyuncu_ve_Yonetmen\" WHERE kisi_tipi = 'Oyuncu'";
                using (NpgsqlCommand command = new NpgsqlCommand(yonetmenQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string yonetmenAdi = reader["gercek_isim"].ToString();
                            string yonetmenSoyadi = reader["gercek_soyisim"].ToString();
                            oyuncuListBox.Items.Add(yonetmenAdi + " " + yonetmenSoyadi);
                        }
                    }
                }
            }

        }
    }
}
