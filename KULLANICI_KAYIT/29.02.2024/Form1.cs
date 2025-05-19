using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace _29._02._2024
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection
     ("Provider=Microsoft.Jet.OleDB.4.0; Data Source =bilgiler.mdb");
        void guncelle()
        {
            DataTable tablo = new DataTable();
            OleDbDataAdapter veriler = new OleDbDataAdapter("SELECT ADI,SOYADI,MEMLEKET FROM TABLO1",baglan);
            veriler.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }
        DialogResult soru;
        private void Form1_Load(object sender, EventArgs e)
        {
            guncelle();
        }
        void kaydet()
        {     
          string sql="INSERT INTO TABLO1(ADI,SOYADI,MEMLEKET) VALUES('"+textBox1.Text+"','"+textBox2.Text+"','"+comboBox1.Text+"')";
          baglan.Open();
           OleDbCommand kul_kaydet = new OleDbCommand(sql, baglan);
           kul_kaydet.ExecuteNonQuery();
           baglan.Close();
           MessageBox.Show("bilgiler kayıt edildi");    
       }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("ADI BİLGİSİNİ BOŞ BIRAKMAYINIZ !");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("SOYADI BİLGİSİNİ BOŞ BIRAKMAYINIZ !");
                return;
            }
            soru = MessageBox.Show(textBox1.Text + " " + textBox2.Text +
                "isimli kayıt yapılsın mı ?", "KAYIT ZAMANI", MessageBoxButtons.YesNo);
            OleDbCommand say = new OleDbCommand("SELECT COUNT(*) FROM TABLO1 WHERE ADI ='" + textBox1.Text +
                                    "' AND SOYADI ='" + textBox2.Text + "'", baglan);
            baglan.Open();
            int sonuc = Convert.ToInt32(say.ExecuteScalar());
            baglan.Close();

            if (sonuc > 0)
            { MessageBox.Show("Aynı isimli müşteri sistemde vardır"); }

            else
            {
                if (soru == DialogResult.Yes)
                {
                    kaydet(); textBox1.Clear(); textBox2.Clear(); comboBox1.SelectedIndex = -1;
                    guncelle();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {        
        baglan.Open();
        OleDbCommand say = new OleDbCommand("SELECT COUNT(*) FROM TABLO1", baglan);
        int sonuc  = Convert.ToInt32(say.ExecuteScalar());
        baglan.Close();

        label1.Text = ("Toplam Kayıt: "+sonuc);
        }
    }
}
