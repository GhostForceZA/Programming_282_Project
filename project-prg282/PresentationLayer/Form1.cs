using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_prg282.PresentationLayer;

namespace project_prg282
{
<<<<<<< Updated upstream:project-prg282/PresentationLayer/Form1.cs
    public partial class Form1 : Form
    {
        public Form1()
=======
    public partial class FrmSignUp : Form
    {
        Point lastPoint;
        public FrmSignUp()
>>>>>>> Stashed changes:project-prg282/PresentationLayer/frmSignUp.cs
        {
            InitializeComponent();
        }


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

        private void FrmSignUp_Load(object sender, EventArgs e)
        {
            //FrmLogin.ActiveForm.Close(); //cant use this as it will end the application in the Program.cs file
        }
    }
}
