using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adonet_prosedursuz
{
    public partial class YonetimBilgi : Form
    {
        public YonetimBilgi()
        {
            InitializeComponent();
        }

        // listeleme metodu oluşturuldu:
        public void Listeleme()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM YonetimBilgi", connection); // veri çekme için kullanılan class. Veriyi düzensiz çeker.
            DataTable dt = new DataTable(); // veriyi tutmak için kullanılan class. Veriyi düzenli tutar.
            adapter.Fill(dt); // veriyi tabloya doldurur.
            dataGridView1.DataSource = dt; // veriyi datagridview'e aktarır.
        }


        // SQL Server bağlantı cümlesi ADO.NET > SQLConnection
        SqlConnection connection = new SqlConnection("Server = DESKTOP-KGM7OOI; Database = Holding; integrated security = true; "); // her formda bağlantı açmayı unutma!

        private void button1_Click(object sender, EventArgs e) // resim ekleme butonu
        {
            // OpenFileDialog file1 = new OpenFileDialog(); // dosya seçme penceresi açar.
            // file1.ShowDialog();
            openFileDialog1.ShowDialog(); // formdaki openfiledialog nesnesini açar.
            textBox3.Text = openFileDialog1.FileName; // dosya yolunu textbox'a yazdırır.
            pictureBox1.ImageLocation = openFileDialog1.FileName; // resmi picturebox'a yükler.
        }

        private void button2_Click(object sender, EventArgs e) // cv ekleme butonu
        {
            openFileDialog2.ShowDialog();
            textBox4.Text = openFileDialog2.FileName;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // resmi açma linki
        {
            System.Diagnostics.Process.Start(openFileDialog2.FileName); // cv adresini açar.
        }

        private void YonetimBilgi_Load(object sender, EventArgs e) // formun loadı. firma listesi loadlandığında çalışır.
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Firmalar", connection); // veri çekme için kullanılan class. Veriyi düzensiz çeker.
            DataTable table = new DataTable(); // veriyi tutmak için kullanılan class. Veriyi düzenli tutar.
            adapter.Fill(table); // veriyi tabloya doldurur.
            comboBox1.DataSource = table; // veriyi combobox'a aktarır.
            comboBox1.DisplayMember = "FirmaAdi"; // combobox'ta görünecek olan değer. Firmalar tablosundaki FirmaAdi sütununu gösterir.

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e) // eğer firmalar combobox'ı tıkladığında açılmasını istiyorsan load yerine buraya koy. ^^
        {

        }

        private void button5_Click(object sender, EventArgs e) // Listeleme butonu (yukarda metod yapıldı yeniden kullanabilmek için)
        {
            Listeleme(); // Listeleme metodu çağrıldı.
        }

        private void button3_Click(object sender, EventArgs e) // Kaydet butonu
        {
            connection.Open(); // Bağlantıyı aç // Kaydetme, Silme, Güncelleme işlemlerinde bağlantı açık olmalıdır. #NOT : DataAdapter, DataSet, DataTable kullanılan sorgularda open/close yapılmaz!!
            SqlCommand cmd = new SqlCommand("INSERT INTO YonetimBilgi(AdSoyad, Tc, Resim, Cv, Firma) values (@ad, @tc, @resim, @cv, @firma)", connection); // SQL sorgusu için SqlCommand nesnesi oluşturulur.
            cmd.Parameters.AddWithValue("@ad", textBox1.Text); // Parametre ekleme
            cmd.Parameters.AddWithValue("@tc", textBox2.Text);
            cmd.Parameters.AddWithValue("@resim", textBox3.Text);
            cmd.Parameters.AddWithValue("@cv", textBox4.Text);
            cmd.Parameters.AddWithValue("@firma", comboBox1.Text); // veriler datasourceta tutulan veriler. 
            cmd.ExecuteNonQuery(); // Sorguyu çalıştır // ExecuteNonQuery() : Insert, Update, Delete işlemlerinde kullanılır. Veritabanına kayıt eder.
            Listeleme(); // Listeleme metodu çağrıldı.

            connection.Close(); // Bağlantıyı kapat
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // datagridview'de bir hücreye tıklandığında çalışır.
        {
            DataGridViewRow satir = dataGridView1.CurrentRow; // seçili satırı alır.
            textBox1.Text = satir.Cells["AdSoyad"].Value.ToString(); // seçili satırın AdSoyad sütununu textbox'a yazdırır.
            textBox1.Tag = satir.Cells["id"].Value; // seçili satırın id sütununu textbox1'in tag'ine yazdı. (güncelleme işlemi için id gerekli)
            textBox2.Text = satir.Cells["Tc"].Value.ToString();
            textBox3.Text = satir.Cells["Resim"].Value.ToString();
            textBox4.Text = satir.Cells["Cv"].Value.ToString();
            comboBox1.Text = satir.Cells["Firma"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e) // güncelleme butonu
        {
            connection.Open(); // Bağlantıyı aç
            SqlCommand cmd = new SqlCommand("UPDATE YonetimBilgi SET AdSoyad = @ad, Tc = @tc, Resim = @resim, Cv = @cv, Firma = @firma WHERE id = @id", connection); // SQL sorgusu için SqlCommand nesnesi oluşturulur.
            cmd.Parameters.AddWithValue("@id", textBox1.Tag); // textbox1'in tag'indeki id'yi alır. (önceden kaydetmiştik)
            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
            cmd.Parameters.AddWithValue("@tc", textBox2.Text);
            cmd.Parameters.AddWithValue("@resim", textBox3.Text);
            cmd.Parameters.AddWithValue("@cv", textBox4.Text);
            cmd.Parameters.AddWithValue("@firma", comboBox1.Text);
            cmd.ExecuteNonQuery(); // Sorguyu çalıştır
            Listeleme(); // Listeleme metodu çağrıldı.
            connection.Close(); // Bağlantıyı kapat

        }

        private void button6_Click(object sender, EventArgs e) // silme butonu
        {
            connection.Open(); // Bağlantıyı aç
            SqlCommand cmd = new SqlCommand("DELETE FROM YonetimBilgi WHERE id = @id", connection); // SQL sorgusu için SqlCommand nesnesi oluşturulur.
            cmd.Parameters.AddWithValue("@id", textBox1.Tag); // textbox1'in tag'indeki id'yi alır. (önceden kaydetmiştik)
            cmd.ExecuteNonQuery(); // Sorguyu çalıştır
            Listeleme(); // Listeleme metodu çağrıldı.
            connection.Close(); // Bağlantıyı kapat
        }
    }
}
