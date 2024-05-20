using System;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace YesilCam
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Server=localhost;Port=5432;Database=YesilCam;User Id=postgres;Password=123;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VerileriCekVeGuncelle();
            dataGridView1.Columns["afis_patch"].Visible = false;
            dataGridView1.Columns["kisi_id"].Visible = false;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            YonetmenleriListele();
            OyunculariListele();
            TurListele();
        }

        private void TurListele()
        {
            turListBox.Items.Clear(); // Mevcut öğeleri temizle

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
                            turListBox.Items.Add(turAdi);
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
                            yonetmenListBox.Items.Add(yonetmenAdi + " " + yonetmenSoyadi);
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
                            oyuncuListBox.Items.Add(yonetmenAdi + " " + yonetmenSoyadi);
                        }
                    }
                }
            }
        }

        private void VerileriCekVeGuncelle()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                // Veritabanındaki verileri çek
                string query = "SELECT film_id, isim, rating, butce, yapim_yili, gise_sayisi, afis_patch, kisi_id FROM public.\"Filmler\" ORDER BY rating DESC";
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView güncelle
                    dataGridView1.DataSource = dataTable;

                    // PictureBox güncelle
                    if (dataGridView1.Rows.Count > 0)
                    {
                        int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                        string afisPatch = dataGridView1.Rows[selectedRowIndex].Cells["afis_patch"].Value.ToString();
                        pictureBox1.ImageLocation = "C:\\Users\\MURAT\\OneDrive\\Masaüstü\\YesilCam\\YesilCam\\YesilCam" + afisPatch;
                        nameLabel.Text = dataGridView1.Rows[selectedRowIndex].Cells["isim"].Value.ToString();
                        object yapimYiliCellValue = dataGridView1.Rows[selectedRowIndex].Cells["yapim_yili"].Value;

                        if (yapimYiliCellValue != null && yapimYiliCellValue != DBNull.Value)
                        {
                            // Değeri DateTime türüne dönüştür
                            DateTime yapimYiliDateTime = Convert.ToDateTime(yapimYiliCellValue);

                            // Tarih bölümünü al ve yapimyiliLabel.Text'e ata
                            yapimyiliLabel.Text = yapimYiliDateTime.ToShortDateString();
                        }
                        else
                        {
                            yapimyiliLabel.Text = string.Empty; // Eğer hücrede değer yoksa boş bir metin ata veya başka bir işlem yapabilirsiniz.
                        }
                        ratingLabel.Text = dataGridView1.Rows[selectedRowIndex].Cells["rating"].Value.ToString();
                        // Filmin yönetmen kisi_id'sini al
                        int yonetmenKisiID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["kisi_id"].Value);

                        // Yönetmen bilgisini al ve yonetmenLabel'a ata
                        string yonetmenIsim = GetYonetmenIsim(yonetmenKisiID);
                        yonetmenLabel.Text = yonetmenIsim;
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // DataGridView'da herhangi bir satır seçildiğinde PictureBox'u güncelle
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string afisPatch = dataGridView1.Rows[selectedRowIndex].Cells["afis_patch"].Value.ToString();
                pictureBox1.ImageLocation = "C:\\Users\\MURAT\\OneDrive\\Masaüstü\\YesilCam\\YesilCam\\YesilCam" + afisPatch;
                pictureBox1.Load();
                nameLabel.Text = dataGridView1.Rows[selectedRowIndex].Cells["isim"].Value.ToString();

                // Filmin yönetmen kisi_id'sini al
                int yonetmenKisiID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["kisi_id"].Value);

                // Yönetmen bilgisini al ve yonetmenLabel'a ata
                string yonetmenIsim = GetYonetmenIsim(yonetmenKisiID);
                yonetmenLabel.Text = yonetmenIsim;

            }
        }
        private string GetYonetmenIsim(int kisiID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT gercek_isim, gercek_soyisim FROM public.\"Oyuncu_ve_Yonetmen\" WHERE kisi_id = @kisiID AND kisi_tipi = 'Yonetmen'";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kisiID", kisiID);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string gercekIsim = reader["gercek_isim"].ToString();
                            string gercekSoyisim = reader["gercek_soyisim"].ToString();
                            return $"{gercekIsim} {gercekSoyisim}";
                        }
                    }
                }
            }

            return string.Empty; // Yönetmen bulunamadı durumu
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Diğer film bilgilerini al
            string filmAdi = filmNameTextBox.Text;
            DateTime yapimYili;

            // Yapım yılı bilgisini TextBox'tan al ve DateTime tipine dönüştür
            if (!DateTime.TryParse(yapimYiliTextBox.Text, out yapimYili))
            {
                MessageBox.Show("Geçerli bir tarih formatı girmelisiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double rating = Convert.ToDouble(ratingTextBox.Text);
            decimal butce = Convert.ToDecimal(butceTextBox.Text);
            int giseSayisi = Convert.ToInt32(giseTextBox.Text);
            string afisPatch = patchTextBox.Text;

            // Yönetmen bilgisini al
            string selectedYonetmen = yonetmenListBox.SelectedItem.ToString();
            string[] yonetmenAdSoyad = selectedYonetmen.Split(' ');
            string yonetmenAdi = yonetmenAdSoyad[0];
            string yonetmenSoyadi = yonetmenAdSoyad[1];
            int yonetmenID = GetYonetmenID(yonetmenAdi, yonetmenSoyadi);

            // Yeni filmi veritabanına ekle ve film ID'sini al
            int filmID = InsertFilm(filmAdi, yapimYili, rating, butce, giseSayisi, afisPatch, yonetmenID);

            // Seçilen oyuncuların ID'lerini al
            List<int> selectedOyuncuIDs = GetSelectedOyuncuIDs();

            // Oyuncu_Film tablosuna ekle
            foreach (int oyuncuID in selectedOyuncuIDs)
            {
                InsertOyuncuFilm(oyuncuID, filmID);
            }

            // Verileri çek ve güncelle
            VerileriCekVeGuncelle();
        }

        private List<int> GetSelectedOyuncuIDs()
        {
            List<int> selectedOyuncuIDs = new List<int>();

            foreach (var selectedOyuncu in oyuncuListBox.SelectedItems)
            {
                string[] oyuncuAdSoyad = selectedOyuncu.ToString().Split(' ');
                string oyuncuAdi = oyuncuAdSoyad[0];
                string oyuncuSoyadi = oyuncuAdSoyad[1];
                int oyuncuID = GetOyuncuID(oyuncuAdi, oyuncuSoyadi);

                if (oyuncuID != -1)
                {
                    selectedOyuncuIDs.Add(oyuncuID);
                }
            }

            return selectedOyuncuIDs;
        }

        private int GetOyuncuID(string ad, string soyad)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

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

        private int InsertFilm(string isim, DateTime yapimYili, double rating, decimal butce, int giseSayisi, string afisPatch, int yonetmenID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO public.\"Filmler\" (isim, yapim_yili, rating, butce, gise_sayisi, afis_patch, kisi_id) VALUES (@isim, @yapimYili, @rating, @butce, @giseSayisi, @afisPatch, @yonetmenID) RETURNING film_id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@isim", isim);
                    command.Parameters.AddWithValue("@yapimYili", yapimYili);
                    command.Parameters.AddWithValue("@rating", rating);
                    command.Parameters.AddWithValue("@butce", butce);
                    command.Parameters.AddWithValue("@giseSayisi", giseSayisi);
                    command.Parameters.AddWithValue("@afisPatch", afisPatch);
                    command.Parameters.AddWithValue("@yonetmenID", yonetmenID);

                    // Yeni eklenen film ID'sini al
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private void InsertOyuncuFilm(int oyuncuID, int filmID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO public.\"Oyuncu_Film\" (kisi_id, film_id) VALUES (@oyuncuID, @filmID)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@oyuncuID", oyuncuID);
                    command.Parameters.AddWithValue("@filmID", filmID);

                    command.ExecuteNonQuery();
                }
            }
        }
        private int GetYonetmenID(string ad, string soyad)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT kisi_id FROM public.\"Oyuncu_ve_Yonetmen\" WHERE gercek_isim = @ad AND gercek_soyisim = @soyad AND kisi_tipi = 'Yonetmen'";
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

            return -1; // Yönetmen bulunamadı durumu
        }



        private void button2_Click(object sender, EventArgs e)
        {
            DeleteSelectedFilm();
        }
        private void DeleteSelectedFilm()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int filmID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["film_id"].Value);

                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query1 = "DELETE FROM public.\"Oyuncu_Film\" WHERE film_id=@filmID";
                    using (NpgsqlCommand command = new NpgsqlCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@filmID", filmID);
                        command.ExecuteNonQuery();
                    }

                    string query = "DELETE FROM public.\"Filmler\" WHERE film_id = @filmID";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@filmID", filmID);
                        command.ExecuteNonQuery();
                    }
                }

                // Verileri çek ve güncelle
                VerileriCekVeGuncelle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int filmID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["film_id"].Value);
                string filmName = dataGridView1.Rows[selectedRowIndex].Cells["isim"].Value.ToString();
                string yapimYili = dataGridView1.Rows[selectedRowIndex].Cells["yapim_yili"].Value.ToString();
                string rating = dataGridView1.Rows[selectedRowIndex].Cells["rating"].Value.ToString();
                decimal butce = Convert.ToDecimal(dataGridView1.Rows[selectedRowIndex].Cells["butce"].Value);
                int giseSayisi = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["gise_sayisi"].Value);
                string afisPatch = dataGridView1.Rows[selectedRowIndex].Cells["afis_patch"].Value.ToString();


                // FilmGuncelle formunu oluştur ve bilgileri ileterek göster
                FilmGuncelle filmGuncelle = new FilmGuncelle(filmID, filmName, yapimYili, rating, butce, giseSayisi, afisPatch);
                filmGuncelle.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VerileriCekVeGuncelle();
        }

        private void f2_Click(object sender, EventArgs e)
        {
            Fonksiyon1 fonksiyon1 = new Fonksiyon1();
            fonksiyon1.Show();
        }

        private void f3_Click(object sender, EventArgs e)
        {
            Fonksiyon3 fonksiyon3 = new Fonksiyon3();
            fonksiyon3.Show();
        }

        private void f4_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT o1.gercek_isim AS oyuncu1_adi, o1.gercek_soyisim AS oyuncu1_soyadi, " +
                               "o2.gercek_isim AS oyuncu2_adi, o2.gercek_soyisim AS oyuncu2_soyadi, " +
                               "COUNT(of1.film_id) AS film_sayisi " +
                               "FROM public.\"Oyuncu_Film\" of1 " +
                               "JOIN public.\"Oyuncu_Film\" of2 ON of1.film_id = of2.film_id AND of1.kisi_id < of2.kisi_id " +
                               "JOIN public.\"Oyuncu_ve_Yonetmen\" o1 ON of1.kisi_id = o1.kisi_id " +
                               "JOIN public.\"Oyuncu_ve_Yonetmen\" o2 ON of2.kisi_id = o2.kisi_id " +
                               "GROUP BY o1.gercek_isim, o1.gercek_soyisim, o2.gercek_isim, o2.gercek_soyisim " +
                               "ORDER BY film_sayisi DESC";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string oyuncu1Adi = reader["oyuncu1_adi"].ToString();
                            string oyuncu1Soyadi = reader["oyuncu1_soyadi"].ToString();
                            string oyuncu2Adi = reader["oyuncu2_adi"].ToString();
                            string oyuncu2Soyadi = reader["oyuncu2_soyadi"].ToString();
                            int filmSayisi = Convert.ToInt32(reader["film_sayisi"]);

                            MessageBox.Show($"Oyuncular: {oyuncu1Adi} {oyuncu1Soyadi} ve {oyuncu2Adi} {oyuncu2Soyadi}\nBirlikte oynadıkları film sayısı: {filmSayisi}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void fonksiyon2_Click(object sender, EventArgs e)
        {
            Fonksiyon2 fonksiyon2 = new Fonksiyon2();
            fonksiyon2.Show();
        }
    }
}
