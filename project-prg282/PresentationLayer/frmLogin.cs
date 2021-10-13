using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_prg282.PresentationLayer;
using project_prg282.BusinessLogicLayer;

namespace project_prg282
{
    public partial class FrmLogin : Form
    {
        Point lastPoint;
        static FrmLogin instance;

        public static FrmLogin Instance { get => instance; set => instance = value; }

        public FrmLogin()
        {
            InitializeComponent();
            Instance = this;
        }
        //Mouse movement code taken from https://www.youtube.com/watch?v=KTTkDhuPV_c&ab_channel=TeckDoctor
        private void FormLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                Regex re = new Regex(@"^[a-zA-Z0-9_-]*$"); //allowable characters for a username
                if (username.Length == 0 || password.Length == 0)
                {
                    throw new InputException("You cannot have an empty field");
                }
                if(!re.IsMatch(username)) //means that it contains a character that is not part of this string
                {
                    throw new InputException("Username does not contain special characters");
                }
                else
                {
                    User user = new User();
                    if(user.UserExists(username, password))
                    {
                        //move to main form
                        FrmMain main = new FrmMain();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        throw new UserNotFoundException("UserName or Password is incorrect");
                    }
                }

            }
            catch(InputException err)
            {
                MessageBox.Show(err.Message, "Invalid Entry", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            catch(UserNotFoundException err)
            {
                MessageBox.Show(err.Message, "Invalid Entry", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            //open the signup form and close this one
            FrmSignUp signUp = new FrmSignUp();
            signUp.Show();
            this.Hide();
        }
    }
}
