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

namespace project_prg282
{
    public partial class FormLogin : Form
    {
        Point lastPoint;
        public FormLogin()
        {
            InitializeComponent();
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
            //validate input, then push to business logic, returns true or false
            //if true close and open next one

            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                Regex re = new Regex(@"^[a - zA - Z0 - 9_.-] *$"); //allowable characters for a username
                if (username.Length == 0 || password.Length == 0)
                {
                    throw new LoginException("I think you forgot to enter your Username or password, or both, I don't have my glasses, so please try again");
                }
                else if(!re.IsMatch(username)) 
                {
                    throw new LoginException("I don't understand your Username, please use normal characters so my brain won't hurt");
                }
                else
                {
                    //call business logic operations to check if this exists in the database
                    //if(userExists(username, password))
                    //{
                    //    //open the next form
                    //}else{
                    //  //throw exception
                    //}
                }

            }
            catch(LoginException err)//create custom exceptions for this
            {
                MessageBox.Show(err.Message, "Invalid Entry", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            //open the signup form and close this one
        }
    }



}
