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

namespace Adonet_prosedurlu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Listeleme()
        {
            SqlCommand cmd = new SqlCommand("MListele", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz

            SqlDataAdapter adapter = new SqlDataAdapter(cmd); // prosedürümüzü adaptere atıyoruz
            DataTable dt = new DataTable(); // datatable oluşturuyoruz
            adapter.Fill(dt); // adapteri datatable'a dolduruyoruz
            dataGridView1.DataSource = dt; // datagridview'e datatable'ı atıyoruz
        }

        // connection stringimizi tanımlıyoruz
        SqlConnection connection = new SqlConnection("Server = DESKTOP-KGM7OOI; Database = SatisFirmasi; integrated security = true; ");

        private void button3_Click(object sender, EventArgs e) // listeleme butonu
        {
            Listeleme();
        }

        private void button1_Click(object sender, EventArgs e) // kaydet
        {
            connection.Open(); // bağlantıyı açıyoruz

            SqlCommand cmd = new SqlCommand("MKaydet", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz

            /* SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MKaydet"; */ // yukarıdaki 3 satırın aynısı

            // prosedür parametre istediği için parametreleri ekliyoruz // @ koyulmadı var olan parametreye değer atandı
            cmd.Parameters.AddWithValue("adsoyad", textBox1.Text);
            cmd.Parameters.AddWithValue("tc", textBox2.Text);
            cmd.Parameters.AddWithValue("adres", textBox3.Text);
            cmd.Parameters.AddWithValue("il", textBox4.Text);
            cmd.Parameters.AddWithValue("telefon", textBox5.Text);

            cmd.ExecuteNonQuery(); // prosedürü çalıştırıyoruz
            connection.Close(); // bağlantıyı kapatıyoruz
            Listeleme(); // listeleme fonksiyonunu çağırıyoruz
        }

        private void button2_Click(object sender, EventArgs e) // güncelleme butonu
        {
            connection.Open(); // bağlantıyı açıyoruz

            SqlCommand cmd = new SqlCommand("MGuncelle", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz

            /* SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MGuncelle"; */ // yukarıdaki 3 satırın aynısı

            // prosedür parametre istediği için parametreleri ekliyoruz // @ koyulmadı var olan parametreye değer atandı
            cmd.Parameters.AddWithValue("id", textBox1.Tag);
            cmd.Parameters.AddWithValue("adsoyad", textBox1.Text);
            cmd.Parameters.AddWithValue("tc", textBox2.Text);
            cmd.Parameters.AddWithValue("adres", textBox3.Text);
            cmd.Parameters.AddWithValue("il", textBox4.Text);
            cmd.Parameters.AddWithValue("telefon", textBox5.Text);

            cmd.ExecuteNonQuery(); // prosedürü çalıştırıyoruz
            connection.Close(); // bağlantıyı kapatıyoruz
            Listeleme(); // listeleme fonksiyonunu çağırıyoruz
        }

        private void button4_Click(object sender, EventArgs e) // silme butonu 
        {
            connection.Open(); // bağlantıyı açıyoruz

            SqlCommand cmd = new SqlCommand("MSil", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz

            // prosedür parametre istediği için parametreleri ekliyoruz // @ koyulmadı var olan parametreye değer atandı
            cmd.Parameters.AddWithValue("id", textBox1.Tag);
            cmd.ExecuteNonQuery(); // prosedürü çalıştırıyoruz
            connection.Close(); // bağlantıyı kapatıyoruz
            Listeleme(); // listeleme fonksiyonunu çağırıyoruz
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // datagriedviewdeki değerleri texboxlarda görmek için
        {
            DataGridViewRow satir = dataGridView1.CurrentRow; // seçili satırı alıyoruz
            textBox1.Tag = satir.Cells["id"].Value.ToString(); // id'yi tag'e atıyoruz
            textBox1.Text = satir.Cells["AdSoyad"].Value.ToString(); // adsoyad'ı textboxa atıyoruz
            textBox2.Text = satir.Cells["Tc"].Value.ToString(); // tc'yi textboxa atıyoruz
            textBox3.Text = satir.Cells["Adres"].Value.ToString(); // adresi textboxa atıyoruz
            textBox4.Text = satir.Cells["il"].Value.ToString(); // il'i textboxa atıyoruz
            textBox5.Text = satir.Cells["Telefon"].Value.ToString(); // telefonu textboxa atıyoruz
        }
    }
}
