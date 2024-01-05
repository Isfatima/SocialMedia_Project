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
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }
        

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignInForm f1 = new SignInForm();
            f1.ShowDialog();
        }

        private void LogIn_Click_1(object sender, EventArgs e)
        {
            string username = UsernameTextBoxLogIn.Text;
            string password = PasswordTextBoxLogIn.Text;
            User a = new User(username, password);
            string path = "UsersInfo.txt";
            if (File.Exists(path))
            {
                StreamReader Reader = new StreamReader(path);
                string str = Reader.ReadLine();
                while (str != null) // gmbkgm
                {
                    string[] Data = str.Split('|');
                    if (Data.Length >= 2)
                    {
                        string username_ = Data[0]; // username in File
                        string password_ = Data[1]; // password in File
                        if (a.GetUsername() == username_)
                        {
                            if (a.GetPassword() == password_)
                            {
                                Reader.Close();
                                MessageBox.Show(a.GetUsername() , "Welcome");
                                MyApp f = new MyApp(a.GetUsername(), a.GetPassword());
                                f.ShowDialog();
                            }
                            else
                                MessageBox.Show("incorrect password");
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid user data format: " + str);
                    }
                    str = Reader.ReadLine();
                }
                if (str == null)
                    MessageBox.Show("incorrect username");
                Reader.Close();
            }
            else
                MessageBox.Show("there's not a single username in this App yet");
        }
    }
}
