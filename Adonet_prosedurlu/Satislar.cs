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
    public partial class Satislar : Form
    {
        public Satislar()
        {
            InitializeComponent();
        }

        public void Listeleme(string procedure)
        {
            SqlCommand cmd = new SqlCommand(procedure, connection); // prosedürümüzü(parametre olarak ne gelirse) çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz
            SqlDataAdapter adapter = new SqlDataAdapter(cmd); // prosedürümüzü adaptere atıyoruz
            DataTable table = new DataTable(); // datatable oluşturuyoruz
            adapter.Fill(table); // adapteri datatable'a dolduruyoruz
            dataGridView1.DataSource = table; // datagridview'e datatable'ı atıyoruz
        }


        SqlConnection connection = new SqlConnection("Server = DESKTOP-KGM7OOI; Database = SatisFirmasi; integrated security = true; ");

       

        private void button3_Click(object sender, EventArgs e) // listeleme butonu
        {
            Listeleme("SListele");
        }

        private void Satislar_Load(object sender, EventArgs e)
        {
            Listeleme("SListele"); // açılır açılmaz listele 
            //comboboxın içindeki verileri çek ve göster açılır açılmaz (prosedür oluşturuldu UListele)
            SqlCommand cmd = new SqlCommand("UListele", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "UrunAdi"; // ürün adını göster combobox'ta
            comboBox1.ValueMember = "id"; // id'yi value olarak belirle kaydette id'yi almak için
        }

        private void button1_Click(object sender, EventArgs e) // kaydet butonu
        {
            connection.Open(); // bağlantıyı açıyoruz
            SqlCommand cmd = new SqlCommand("SKaydet", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz
            // parametreleri ekliyoruz (prosedür parametre istediği için)
            cmd.Parameters.AddWithValue("musteriid", textBox1.Text);
            cmd.Parameters.AddWithValue("urunid", comboBox1.SelectedValue); // comboboxtan seçilen değeri alıyoruz (id) listelemede id'yi value olarak belirlemiştik yukarıda!!!
            cmd.Parameters.AddWithValue("fiyat", textBox3.Text);
            cmd.Parameters.AddWithValue("adet", textBox4.Text);
            cmd.Parameters.AddWithValue("odeme", textBox5.Text);
            cmd.ExecuteNonQuery(); // prosedürü çalıştırıyoruz
            connection.Close(); // bağlantıyı kapatıyoruz
            Listeleme("SListele"); // listeleme fonksiyonunu çağırıyoruz
        }
    }
}
