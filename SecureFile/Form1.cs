using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace SecureFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        string path;
        private void button1_Click(object sender, EventArgs e)
        {

            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Select Valid Document(*.txt)|*.txt";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label1.Text = path;

                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                if (filename == null)
                {
                    MessageBox.Show("Please select a valid document.");
                }
                else
                {

                    string newpath = @"C:\Users\Doui\Desktop\test.txt";
                    // EncryptFile(path);
                    var data = File.ReadAllText(newpath);

                    string data1 = EnCode(data);

                    File.WriteAllText(newpath, data1);

                    MessageBox.Show("Document uploaded.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string EnCode(string input)
        {
            try
            {
                // -------------------------------------------------------------------------------------
                //Keys
                string PrivateKey = textBox1.Text;
                string key = "abcdefgh";
                // -------------------------------------------------------------------------------------

                // --------------------------------------------------------------------------------------
                //Converting keys and input into byte
                byte[] PrivateKeyByte = { };

                PrivateKeyByte = Encoding.UTF8.GetBytes(PrivateKey);

                byte[] KeyByte = { };
                KeyByte = Encoding.UTF8.GetBytes(key);

                byte[] inputtextbyte = { };
                inputtextbyte = Encoding.UTF8.GetBytes(input);
                // --------------------------------------------------------------------------------------

                using (DESCryptoServiceProvider dsp = new DESCryptoServiceProvider())
                {
                    var strem = new MemoryStream();
                    var cryptostremobj = new CryptoStream(strem, dsp.CreateEncryptor(KeyByte, PrivateKeyByte), CryptoStreamMode.Write);
                    cryptostremobj.Write(inputtextbyte, 0, inputtextbyte.Length);
                    cryptostremobj.FlushFinalBlock();
                    return Convert.ToBase64String(strem.ToArray());


                }

            }
            catch
            {
                return "Error";
            }


        }

        public string DeCode(string input)
        {
            try
            {
                // -------------------------------------------------------------------------------------
                //Keys
                string PrivateKey = textBox1.Text;
                string key = "abcdefgh";
                // -------------------------------------------------------------------------------------

                // --------------------------------------------------------------------------------------
                //Converting keys and input into byte
                byte[] PrivateKeyByte = { };

                PrivateKeyByte = Encoding.UTF8.GetBytes(PrivateKey);

                byte[] KeyByte = { };
                KeyByte = Encoding.UTF8.GetBytes(key);

                byte[] inputtextbyte = { };
                inputtextbyte = new byte[input.Replace(" ", "+").Length];
                inputtextbyte = Convert.FromBase64String(input.Replace(" ", "+"));

                // --------------------------------------------------------------------------------------
                using (DESCryptoServiceProvider dsp = new DESCryptoServiceProvider())
                {
                    var strem = new MemoryStream();
                    var cryptostremobj = new CryptoStream(strem, dsp.CreateDecryptor(KeyByte, PrivateKeyByte), CryptoStreamMode.Write);
                    cryptostremobj.Write(inputtextbyte, 0, inputtextbyte.Length);
                    cryptostremobj.FlushFinalBlock();
                    return Encoding.UTF8.GetString(strem.ToArray());
                   


                }
            }
            catch
            {
                return "Error";

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string newpath = @"C:\Users\Doui\Desktop\test.txt";
            // EncryptFile(path);
            var data = File.ReadAllText(newpath);

            string data1 = DeCode(data);

            File.WriteAllText(newpath, data1);

            MessageBox.Show("Document uploaded.");
        }
    }
}