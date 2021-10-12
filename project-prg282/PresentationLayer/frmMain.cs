using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_prg282.DataAccessLayer;
using project_prg282.BusinessLogicLayer;
using System.IO;
using System.Drawing.Imaging;

namespace project_prg282.PresentationLayer
{
    public partial class FrmMain : Form
    {
        Point lastPoint;
        List<string> modules = new List<string>();
        DataHandler dh = new DataHandler();
        byte[] temp;
        private static readonly ImageConverter _imageConverter = new ImageConverter();


        public FrmMain()
        {
            InitializeComponent();
            //adding to list
            modules.Add("PRG281");
            modules.Add("PRG282");
            modules.Add("DBD281");
            modules.Add("IOT281");
            modules.Add("INF281");
            modules.Add("STA281");
            modules.Add("MAT281");
            modules.Add("PRG251");
            modules.Add("PRG252");
            modules.Add("DBD251");
            modules.Add("IOT251");
            modules.Add("INF251");
            modules.Add("STA251");
            modules.Add("MAT251");
            modules.Add("PRG252");
            modules.Sort();
        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //filter string from https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.filedialog.filter?view=windowsdesktop-5.0
            openFileDialogImage.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            dtDOB.MaxDate = DateTime.Now;
            foreach (string mod in modules)
            {
                cbModules.Items.Add(mod);
            }
            addToListView();
            
        }
        //create movable app
        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {           
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //got found from https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-5.0
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialogImage.FileName; //image for the user to upload
                Bitmap img = new Bitmap(filePath);
                pbProfile.Image = img;                
                //take file path and process in business logic so we dont use a ton of memory using image data when parsing
            }
        }

        private void btnAddMod_Click(object sender, EventArgs e)
        {
            try
            {            
                string selected = cbModules.SelectedItem.ToString();
                int index = cbModules.SelectedIndex;
                if (rtbModules.Text == "")
                {
                    rtbModules.AppendText(selected);
                }
                else
                {
                    rtbModules.AppendText(", " + selected);
                }
                cbModules.Items.RemoveAt(index);//getting rid of chosen module
            }
            catch(Exception)
            {
                MessageBox.Show("Choose a module", "Module error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnClearModule_Click(object sender, EventArgs e)
        {
            cbModules.Items.Clear();
            rtbModules.Clear();
            foreach(string mod in modules)
            {
                cbModules.Items.Add(mod);
            }
            
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //get all inputs
            try
            {
                lvDetails.Items.Clear();
                string id = txtID.Text;
                string name = txtName.Text;
                string surname = txtSurname.Text;               
                DateTime dob = dtDOB.Value;               
                string gender = cbGender.SelectedItem.ToString();
                string phone = txtPhone.Text;
                string[] modules = rtbModules.Text.Split(','); //array of modules to add
                string address = rtbAddress.Text;

                MemoryStream ms = new MemoryStream();
                pbProfile.Image.Save(ms, ImageFormat.Png);
                byte[] photo_array = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_array, 0, photo_array.Length);

                dh.UpdateStudent(id, name, surname, photo_array, dob, gender, phone, address, modules);

            }
            catch (FormatException)
            {
                MessageBox.Show("Only valid characters to be entered");
            }
            
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                lvDetails.Items.Clear();
                string id = txtID.Text;
                string name = txtName.Text;
                string surname = txtSurname.Text;
                DateTime dob = dtDOB.Value;
                string address = rtbAddress.Text;
                string phone = txtPhone.Text;
                string gender = cbGender.SelectedItem.ToString();
                string[] modules = rtbModules.Text.Split(','); //array of modules to add


                MemoryStream ms = new MemoryStream();
                pbProfile.Image.Save(ms, ImageFormat.Png);
                byte[] photo_array = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_array, 0, photo_array.Length);
                dh.addStudent(id, name, surname, photo_array, dob, gender, phone, address,modules);
                addToListView();
            }
            catch (FormatException)
            {
                MessageBox.Show("Only valid characters to be entered");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lvDetails.Items.Clear();
                string id = txtID.Text;
                dh.DeleteStudent(id);
            }
            catch (FormatException)
            {
                MessageBox.Show("Only valid characters to be entered");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
            
                string id = txtID.Text;
                //searchUser(id); --return the persons data, either as a person object or a formattable string
                //assign to textboxes
                DataRowCollection data = dh.getStudents(id).Rows;

                if (data.Count >= 1)
                {
                
                    lvDetails.Items.Clear();
                    foreach (DataRow row in data)
                    {
                        ListViewItem item = new ListViewItem(row["StudentNumber"].ToString());
                        item.SubItems.Add(row["Name"].ToString());
                        item.SubItems.Add(row["Surname"].ToString());
                        temp = (byte[])row["StudentImage"];
                        item.SubItems.Add(row["DOB"].ToString());
                        item.SubItems.Add(row["Gender"].ToString());
                        item.SubItems.Add(row["Phone"].ToString());
                        item.SubItems.Add(row["Address"].ToString());
                        item.SubItems.Add(row["ModuleCode"].ToString());
                        lvDetails.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Student not found");
                }
                
            }
            catch (FormatException)
            {
                MessageBox.Show("Only valid characters to be entered");
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void addToListView()
        {

            // getUsers()--Return a dataTable
            //need to ensure column names are correct
            foreach (DataRow row in dh.getAllStudents().Rows)
            {
                ListViewItem item = new ListViewItem(row["StudentNumber"].ToString());
                item.SubItems.Add(row["Name"].ToString());
                item.SubItems.Add(row["Surname"].ToString());
                temp = (byte[])row["StudentImage"];
                item.SubItems.Add(row["DOB"].ToString());
                item.SubItems.Add(row["Gender"].ToString());
                item.SubItems.Add(row["Phone"].ToString());
                item.SubItems.Add(row["Address"].ToString());
                item.SubItems.Add(row["ModuleCode"].ToString());
                lvDetails.Items.Add(item);
            }
        }

        private void lvDetails_MouseClick(object sender, MouseEventArgs e)
        {
            txtID.Text = lvDetails.SelectedItems[0].SubItems[0].Text;
            txtName.Text = lvDetails.SelectedItems[0].SubItems[1].Text;
            txtSurname.Text = lvDetails.SelectedItems[0].SubItems[2].Text;
            //byte[] temp = Encoding.ASCII.GetBytes(lvDetails.SelectedItems[0].SubItems[3].Text);//convert to an image
            //pbProfile.Image=conv(temp);            
            dtDOB.Text = lvDetails.SelectedItems[0].SubItems[3].Text; //Year/Month/Day
            cbGender.Text = lvDetails.SelectedItems[0].SubItems[4].Text;
            rtbAddress.Text = lvDetails.SelectedItems[0].SubItems[5].Text;
            txtPhone.Text = lvDetails.SelectedItems[0].SubItems[6].Text;
            rtbModules.Text = lvDetails.SelectedItems[0].SubItems[7].Text;
            
        }
        private Image conv(byte[] byteArray)
        {
            Image returnImage;
            using (var ms = new MemoryStream(byteArray))
            {
                ms.Position = 0;
                returnImage = new Bitmap(ms);
            }
            return returnImage;
        }
    }
}
