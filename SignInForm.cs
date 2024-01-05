using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace socialmedia
{
    public partial class SignInForm : Form
    {
        public SignInForm()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            User a = new User(username, password);
            try
            {
                string path = "UsersInfo.txt";
                if (!File.Exists(path))
                {
                    StreamWriter Pen = new StreamWriter(path);
                    Pen.Write(a.GetUsername() + "|" + a.GetPassword() + "|" + "|");
                    Pen.Write("\n");
                    Pen.Close();
                    MyApp f = new MyApp(a.GetUsername(), a.GetPassword());
                    f.ShowDialog();
                }
                else
                {
                    StreamReader Reader = new StreamReader(path);
                    string str = Reader.ReadLine();
                    while (str != null)
                    {
                        string[] Data = str.Split('|');
                        if (Data.Length >= 1)
                        {
                            string username_ = Data[0];
                            if (a.GetUsername() == username_)
                                break;
                            else
                                str = Reader.ReadLine();
                        }
                        else
                        {
                            MessageBox.Show("Invalid user data format: " + str);
                        }
                    }
                    Reader.Close();
                    if (str == null)
                    {
                        StreamWriter Pen = File.AppendText(path);
                        Pen.Write(a.GetUsername() + "|" + a.GetPassword() + "|" + "|");
                        Pen.Write("\n");
                        Pen.Close();
                        MyApp f = new MyApp(a.GetUsername(), a.GetPassword());
                        f.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing user data: " + ex.Message);
            }
            SignInButton.Enabled = false;
            this.Close();

        }
        
    }
}
