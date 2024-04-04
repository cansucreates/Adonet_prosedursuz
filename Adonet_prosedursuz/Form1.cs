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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // listeleme metodu oluşturuldu:
        public void Listeleme()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Firmalar", connection); // veri çekme için kullanılan class. Veriyi düzensiz çeker.
            DataTable table = new DataTable(); // veriyi tutmak için kullanılan class. Veriyi düzenli tutar.
            adapter.Fill(table); // veriyi tabloya doldurur.
            dataGridView1.DataSource = table; // veriyi datagridview'e aktarır.
        }

        // SQL Server bağlantı cümlesi ADO.NET > SQLConnection

        SqlConnection connection = new SqlConnection("Server = DESKTOP-KGM7OOI; Database = Holding; integrated security = true; "); // bağlantı için gerekli olan connection string
        // "Server = DESKTOP-KGM7OOI; Database = Holding; uid = <KullanıcıAdı>; pwd = <Sifre>; " >>> SQL Server Authentication
        // Trusted_Connection = True; >>> Windows Authentication hata verirse bu kodu ekle!

        private void button1_Click(object sender, EventArgs e) // Kaydet butonu
        {
            // Bağlantılı Yöntem
            connection.Open(); // Bağlantıyı aç // Kaydetme, Silme, Güncelleme işlemlerinde bağlantı açık olmalıdır. #NOT : DataAdapter, DataSet, DataTable kullanılan sorgularda open/close yapılmaz!! 
            // SQL sorgusu için SqlCommand nesnesi oluşturulur
            SqlCommand cmd = new SqlCommand(" INSERT INTO Firmalar( FirmaAdi, Tanim, ElemanSayisi, KurulusTarihi) VALUES (@FirmaAdi, @Tanim, @ElemanSayisi, @Tarih)" , connection);
            // Parametre ekleme
            cmd.Parameters.AddWithValue("@FirmaAdi", textBox1.Text);
            cmd.Parameters.AddWithValue("@Tanim", textBox2.Text);
            cmd.Parameters.AddWithValue("@ElemanSayisi", Convert.ToInt32(textBox3.Text));
            cmd.Parameters.AddWithValue("@Tarih", Convert.ToDateTime(dateTimePicker1.Text));

            cmd.ExecuteNonQuery(); // Sorguyu çalıştır // ExecuteNonQuery() : Insert, Update, Delete işlemlerinde kullanılır. Veritabanına kayıt eder.
            connection.Close(); // Bağlantıyı kapat
            Listeleme(); // Listeleme metodu çağrıldı.

        }

        private void button3_Click(object sender, EventArgs e) // Listeleme
        {
            Listeleme(); // Listeleme metodu çağrıldı.
        }
    }
}
