using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace sifrelemefinal
{
    //© 2017 Eyyüb TEZCAN ALL RIGHTS RESERVED
    //Computer Engineering
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        byte[] dosyabyte;

        public static byte[] Encrypt(byte[] input)
        {
            PasswordDeriveBytes pdb =
              new PasswordDeriveBytes("sifrelediM92345232BoooooAhoHAshdksj42323",new byte[] { 0x43, 0x87, 0x23, 0x72 }); 
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        public static byte[] Decrypt(byte[] input)
        {
            PasswordDeriveBytes pdb =new PasswordDeriveBytes("sifrelediM92345232BoooooAhoHAshdksj42323",new byte[] { 0x43, 0x87, 0x23, 0x72 }); // Change this
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog fileopen = new OpenFileDialog();
           //fileopen.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (fileopen.ShowDialog() == DialogResult.OK)
            {               
            url.Text = fileopen.FileName;
            MessageBox.Show("Dosya Açıldı");
            //sifresiz byte değerlerini dosyabyte[] arrayine atar
            this.dosyabyte = File.ReadAllBytes(url.Text);
            //şifresiz byte değerlerini string olarak richtextboxa yazdırma
            richTextBox1.Text = String.Join(":", (dosyabyte));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] datalar = File.ReadAllBytes(url.Text);
            byte[] sifrelidosyabyte=Encrypt(datalar);
            //Şifrelenmiş byte array değerleri sifrelidosyabye[] arrayine eklenip, dosya güncellendi.
            File.WriteAllBytes(url.Text, sifrelidosyabyte);
            //şifreli byte değerlerini string olarak richtextboxa yazdırma
            string bytestringtext = String.Join(":", sifrelidosyabyte);
            richTextBox2.Text = bytestringtext;           
            MessageBox.Show("Şifrelendi");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[]datalar = File.ReadAllBytes(url.Text);
            File.WriteAllBytes(url.Text, Decrypt(datalar));
            MessageBox.Show("Şifreleme Çözüldü");
 
        }

        
    }
}
