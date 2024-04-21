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
    public partial class Urunler : Form
    {
        public Urunler()
        {
            InitializeComponent();
        }

        public void Listeleme() // listeleme metodu oluşturdum.
        {
            SqlCommand cmd = new SqlCommand("UListele", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz
            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd); // prosedürümüzü adaptere atıyoruz
            DataTable dt = new DataTable(); // datatable oluşturuyoruz
            adapter.Fill(dt); // adapteri datatable'a dolduruyoruz
            dataGridView1.DataSource = dt; // datagridview'e datatable'ı atıyoruz
        }

        SqlConnection connection = new SqlConnection("Server = DESKTOP-KGM7OOI; Database = SatisFirmasi; integrated security = true; "); // connection stringimizi tanımlıyoruz

        private void button4_Click(object sender, EventArgs e) // listeleme butonu
        {
            Listeleme(); 
        }

        private void Urunler_Load(object sender, EventArgs e) // form yüklendiğinde çalışacak kodlar
        {
            Listeleme(); // açılır açılmaz listele
        }

        private void button1_Click(object sender, EventArgs e) // kaydet butonu
        {
            connection.Open(); // bağlantıyı açıyoruz   
            SqlCommand cmd = new SqlCommand("UKaydet", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz

            // prosedür parametre istediği için parametreleri ekliyoruz
            cmd.Parameters.AddWithValue("urunadi", textBox1.Text);
            cmd.Parameters.AddWithValue("uruncinsi", textBox2.Text);
            cmd.Parameters.AddWithValue("stok", textBox3.Text);
            cmd.Parameters.AddWithValue("firma", textBox4.Text);

            cmd.ExecuteNonQuery(); // prosedürü çalıştır
            connection.Close();
            Listeleme(); 
        }

        private void button2_Click(object sender, EventArgs e) // güncelleme
        {
            connection.Open(); // bağlantıyı açıyoruz

            SqlCommand cmd = new SqlCommand("UGuncelle", connection); // prosedürümüzü çağırıyoruz
            cmd.CommandType = CommandType.StoredProcedure; // prosedür olduğunu belirtiyoruz

            // prosedür parametre istediği için parametreleri ekliyoruz
            cmd.Parameters.AddWithValue("id", textBox1.Tag); // tag'den id'yi alıyoruz
            cmd.Parameters.AddWithValue("urunadi", textBox1.Text);
            cmd.Parameters.AddWithValue("uruncinsi", textBox2.Text);
            cmd.Parameters.AddWithValue("stok", textBox3.Text);
            cmd.Parameters.AddWithValue("firma", textBox4.Text);

            cmd.ExecuteNonQuery(); // prosedürü çalıştır
            connection.Close();
            Listeleme();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // datagridview'de bir hücreye tıkladığımızda
        {
            DataGridViewRow satir = dataGridView1.CurrentRow; // tıkladığımız satırı al
            textBox1.Tag = satir.Cells["id"].Value; // tıkladığımız satırın id hücresini al ve textbox1'in tag'ine yaz
            textBox1.Text = satir.Cells["UrunAdi"].Value.ToString(); // tıkladığımız satırın UrunAdi hücresini al ve textbox1'e yaz
            textBox2.Text = satir.Cells["UrunCinsi"].Value.ToString(); // tıkladığımız satırın UrunCinsi hücresini al ve textbox2'e yaz
            textBox3.Text = satir.Cells["Stok"].Value.ToString(); // tıkladığımız satırın Stok hücresini al ve textbox3'e yaz
            textBox4.Text = satir.Cells["Firma"].Value.ToString(); // tıkladığımız satırın Firma hücresini al ve textbox4'e yaz

        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("USil", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", textBox1.Tag);
            cmd.ExecuteNonQuery();
            connection.Close();
            Listeleme();
        }
    }
}
