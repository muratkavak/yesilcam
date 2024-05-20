using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YesilCam
{
    public partial class FilmGuncelle : Form
    {
        private const string ConnectionString = "Server=localhost;Port=5432;Database=YesilCam;User Id=postgres;Password=123;";
        private int filmID;


        public FilmGuncelle(int filmID, string filmAdi, string yapimYili, string rating, decimal butce, int giseSayisi, string afisPatch)
        {
            InitializeComponent();
            this.filmID = filmID;

            // Film bilgilerini form kontrol elemanlarına yerleştir
            guncelle_id.Text = filmID.ToString();
            guncelle_isim.Text = filmAdi;
            guncelle_yil.Text = yapimYili;
            guncelle_rating.Text = rating;
            guncelle_butce.Text = butce.ToString();
            guncelle_gise.Text = giseSayisi.ToString();
            guncelle_patch.Text = afisPatch;
            TurListele();
            YonetmenleriListele();
            OyunculariListele();
        }



        private void TurListele()
        {
            turGuncelle.Items.Clear(); // Mevcut öğeleri temizle

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string turQuery = "SELECT tur_ismi FROM public.\"FilmTurleri\"";
                using (NpgsqlCommand command = new NpgsqlCommand(turQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string turAdi = reader["tur_ismi"].ToString();
                            turGuncelle.Items.Add(turAdi);
                        }
                    }
                }
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
                            yonetmenGuncelle.Items.Add(yonetmenAdi + " " + yonetmenSoyadi);
                        }
                    }
                }
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
                            oyuncuGuncelle.Items.Add(yonetmenAdi + " " + yonetmenSoyadi);
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Film bilgilerini güncelle
                    string updateQuery = "UPDATE public.\"Filmler\" SET " +
                                        "isim = @isim, " +
                                        "yapim_yili = @yapimYili, " +
                                        "rating = @rating, " +
                                        "butce = @butce, " +
                                        "gise_sayisi = @giseSayisi, " +
                                        "afis_patch = @afisPatch, " +
                                        "kisi_id = @yonetmenID, " +
                                        "tur_id = @turID " +
                                        "WHERE film_id = @filmID";

                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@filmID", filmID);
                        updateCommand.Parameters.AddWithValue("@isim", guncelle_isim.Text);
                        updateCommand.Parameters.AddWithValue("@yapimYili", Convert.ToDateTime(guncelle_yil.Text));
                        updateCommand.Parameters.AddWithValue("@rating", Convert.ToDouble(guncelle_rating.Text));
                        updateCommand.Parameters.AddWithValue("@butce", Convert.ToDecimal(guncelle_butce.Text));
                        updateCommand.Parameters.AddWithValue("@giseSayisi", Convert.ToInt32(guncelle_gise.Text));
                        updateCommand.Parameters.AddWithValue("@afisPatch", guncelle_patch.Text);

                        // Yönetmenin ID'sini al
                        int yonetmenID = GetYonetmenID(yonetmenGuncelle.SelectedItem.ToString());
                        updateCommand.Parameters.AddWithValue("@yonetmenID", yonetmenID);

                        // Türün ID'sini al
                        int turID = GetTurID(turGuncelle.SelectedItem.ToString());
                        updateCommand.Parameters.AddWithValue("@turID", turID);

                        updateCommand.ExecuteNonQuery();
                    }

                    // Oyuncu_Film tablosuna seçili oyuncuları ekle
                    string deleteQuery = "DELETE FROM public.\"Oyuncu_Film\" WHERE film_id = @filmID";
                    using (NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@filmID", filmID);
                        deleteCommand.ExecuteNonQuery();
                    }

                    foreach (var selectedOyuncu in oyuncuGuncelle.SelectedItems)
                    {
                        int oyuncuID = GetOyuncuID(selectedOyuncu.ToString());
                        if (oyuncuID != -1)
                        {
                            string insertQuery = "INSERT INTO public.\"Oyuncu_Film\" (kisi_id, film_id) VALUES (@oyuncuID, @filmID)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@oyuncuID", oyuncuID);
                                insertCommand.Parameters.AddWithValue("@filmID", filmID);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Film bilgileri güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private int GetTurID(string turAdi)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT tur_id FROM public.\"FilmTurleri\" WHERE tur_ismi = @turAdi";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@turAdi", turAdi);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return -1; // Tür bulunamadı durumu
        }

        private int GetOyuncuID(string adSoyad)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string[] splitName = adSoyad.Split(' ');
                string ad = splitName[0];
                string soyad = splitName[1];

                string query = "SELECT kisi_id FROM public.\"Oyuncu_ve_Yonetmen\" WHERE gercek_isim = @ad AND gercek_soyisim = @soyad AND kisi_tipi = 'Oyuncu'";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ad", ad);
                    command.Parameters.AddWithValue("@soyad", soyad);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return -1; // Oyuncu bulunamadı durumu
        }
        private int GetYonetmenID(string yonetmenAdSoyad)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string[] adSoyad = yonetmenAdSoyad.Split(' ');
                string yonetmenAdi = adSoyad[0];
                string yonetmenSoyadi = adSoyad[1];

                string query = "SELECT kisi_id FROM public.\"Oyuncu_ve_Yonetmen\" WHERE gercek_isim = @ad AND gercek_soyisim = @soyad AND kisi_tipi = 'Yonetmen'";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ad", yonetmenAdi);
                    command.Parameters.AddWithValue("@soyad", yonetmenSoyadi);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return -1; // Yönetmen bulunamadı durumu
        }
    }
}
