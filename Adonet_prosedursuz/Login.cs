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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Server = DESKTOP-KGM7OOI; Database = Holding; integrated security = true; ");

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre", connection);
            cmd.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
            cmd.Parameters.AddWithValue("@Sifre", textBox2.Text);

            SqlDataReader reader = cmd.ExecuteReader(); // ExecuteReader() sql command altında çalışan bir metod. datareader: veritabanından veri okumak için kullanılır.
            if (reader.Read()) // true dönerse veri var demektir.
            {
                Form1 form1 = new Form1(); // Form1 nesnesi oluşturuldu. 
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            }


            connection.Close();
        }
    }
}
