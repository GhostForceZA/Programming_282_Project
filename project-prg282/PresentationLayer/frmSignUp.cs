using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_prg282.PresentationLayer;

namespace project_prg282.PresentationLayer
{
    public partial class FrmSignUp : Form
    {
        Point lastPoint;
        public FrmSignUp()
        {
            InitializeComponent();
        }
        //load event callback
        private void FrmSignUp_Load(object sender, EventArgs e)
        {
            TtipPassword.SetToolTip(TxtPassword, "Password requires at least: 1 uppercase, 1 lowercase, 1 number, 1 special character\nLonger than 8 characters");
            TtipPassword.SetToolTip(TxtUname, "Minimum length of 4 characters");
        }
        //Used to make the window draggable/moveable
        private void FrmSignUp_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void FrmSignUp_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        //Provides "Placeholder" text for textbox
        private void TxtUname_MouseClick(object sender, MouseEventArgs e)
        {
            if (TxtUname.Text == "Username")
            {
                TxtUname.Clear();
            }
        }

        private void TxtUname_Leave(object sender, EventArgs e)
        {
            if (TxtUname.Text.Trim().Length == 0)
            {
                TxtUname.Text = "Username";
            }
        }
        //Provides "Placeholder" text for textbox
        private void TxtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (TxtPassword.Text == "Password")
            {
                TxtPassword.Clear();
            }
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (TxtPassword.Text.Trim().Length == 0)
            {
                TxtPassword.Text = "Password";
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            FrmLogin.Instance.Show();
            this.Close();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string username = TxtUname.Text;
                string password = TxtPassword.Text;
                //validation on user input
                Regex re = new Regex(@"^([a-zA-Z0-9_-])(?=.{3,})");//at least 4 in length (more than 3)
                Regex rePass = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
                if (username == "Username" || password == "Password")
                {
                    //throw custom exception (Please enter values different from placeholder)
                }
                if (!re.IsMatch(username) || !rePass.IsMatch(password))
                {
                    //throw custom exception (Invalid characters used)
                    MessageBox.Show("WORKING");
                }
                //addUser(username, password)
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Invalid Input", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            //regex for password from https://www.thepolyglotdeveloper.com/2015/05/use-regex-to-test-password-strength-in-javascript/
            Regex re = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            if (!re.IsMatch(TxtPassword.Text))
            {
                //means that it does not meet the required length or characters
                TxtPassword.ForeColor = Color.White;
            }
            else
            {
                TxtPassword.ForeColor = Color.Green;
            }
        }

        private void TxtUname_TextChanged(object sender, EventArgs e)
        {
            if (TxtUname.Text == "Username")//compensating for when it changes to the placeholder text
            {
                TxtUname.ForeColor = Color.White;
            }
            else if(TxtUname.Text.Length >= 4)
            {
                TxtUname.ForeColor = Color.Green;
            }
            else
            {
                TxtUname.ForeColor = Color.White;
            }
        }
    }
}
