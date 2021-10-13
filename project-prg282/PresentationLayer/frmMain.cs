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
        string historyMods;
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
        public bool validateInputs()
        {
            bool validated = true;
            if (txtName.Text == string.Empty || txtSurname.Text == string.Empty || txtID.Text == string.Empty || txtPhone.Text == string.Empty)
            {
                return !validated;
            }
            if (cbGender.SelectedIndex == -1)
            {
                return !validated;
            }
            if (rtbAddress.Text == string.Empty)
            {
                return !validated;
            }
            return validated;
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

            //adding modules
            foreach (string mod in modules)
            {
                cbModules.Items.Add(mod);
            }
            addToListView();
        }
        //create movable panel
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
            //got code from https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-5.0
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialogImage.FileName; //image for the user to upload
                Bitmap img = new Bitmap(filePath);
                pbProfile.Image = img;                
            }
        }

        private void btnAddMod_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbModules.SelectedIndex != -1)
                {
                    string selected = cbModules.SelectedItem.ToString();
                    int index = cbModules.SelectedIndex;
                    if (rtbModules.Text == "")
                    {
                        rtbModules.AppendText(selected);
                    }
                    else
                    {
                        rtbModules.AppendText(" " + selected.Trim());
                    }
                    cbModules.Items.RemoveAt(index);//getting rid of chosen module
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
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
            if (validateInputs())
            {

                try
                {
                    bool updatedModules = false;
                    lvDetails.Items.Clear();
                    string id = txtID.Text;
                    string name = txtName.Text;
                    string surname = txtSurname.Text;
                    DateTime dob = dtDOB.Value;
                    string gender = cbGender.SelectedItem.ToString();
                    string phone = txtPhone.Text;
                    string[] modules = { };
                    if (historyMods != rtbModules.Text)
                    {
                        modules = rtbModules.Text.Split(' '); //array of modules to add
                        updatedModules = true;
                    }
                    string address = rtbAddress.Text;



                    byte[] photo_array = null;
                    if (pbProfile.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pbProfile.Image.Save(ms, ImageFormat.Png);
                            photo_array = new byte[ms.Length];
                            ms.Position = 0;
                            ms.Read(photo_array, 0, photo_array.Length);
                        }
                    }


                    bool success = dh.UpdateStudent(id, name, surname, photo_array, dob, gender[0].ToString(), phone, address, modules, updatedModules);



                    if (success)
                    {
                        addToListView();
                        MessageBox.Show("Successfully updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Only valid characters to be entered");
                }
            }
            else
            {
                MessageBox.Show("Enter all values", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (validateInputs())
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
                    string[] modules = rtbModules.Text.Split(' '); //array of modules to add

                    if (modules[0] == "")
                    {
                        modules[0] = "DF101";
                    }
                    byte[] photo_array = null;
                    if (pbProfile.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pbProfile.Image.Save(ms, ImageFormat.Png);
                            photo_array = new byte[ms.Length];
                            ms.Position = 0;
                            ms.Read(photo_array, 0, photo_array.Length);
                        }
                    }

                    if (dh.addStudent(id, name, surname, photo_array, dob, gender[0].ToString(), phone, address, modules))
                    {
                        addToListView();
                        MessageBox.Show("Successfully registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                }
                catch (FormatException)
                {
                    MessageBox.Show("Only valid characters to be entered");
                }
                catch (InputException er)
                {
                    MessageBox.Show(er.Message);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
            else
            {
                MessageBox.Show("Enter all values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;

                if (dh.DeleteStudent(id))
                {
                    addToListView();
                    MessageBox.Show("Successfully Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;

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
                        //temp = (byte[])row["StudentImage"];
                        item.SubItems.Add(row["DOB"].ToString());
                        item.SubItems.Add(row["Gender"].ToString());
                        item.SubItems.Add(row["Phone"].ToString());
                        item.SubItems.Add(row["Address"].ToString());
                        //item.SubItems.Add(row["ModuleCode"].ToString());
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
            lvDetails.Items.Clear();
            foreach (DataRow row in dh.getAllStudents().Rows)
            {
                ListViewItem item = new ListViewItem(row["StudentNumber"].ToString());
                item.SubItems.Add(row["Name"].ToString());
                item.SubItems.Add(row["Surname"].ToString());
                item.SubItems.Add(row["DOB"].ToString());
                item.SubItems.Add(row["Gender"].ToString());
                item.SubItems.Add(row["Phone"].ToString());
                item.SubItems.Add(row["Address"].ToString());
           
                lvDetails.Items.Add(item);
            }
        }

        private void lvDetails_MouseClick(object sender, MouseEventArgs e)
        {
            txtID.Text = lvDetails.SelectedItems[0].SubItems[0].Text;
            txtName.Text = lvDetails.SelectedItems[0].SubItems[1].Text;
            txtSurname.Text = lvDetails.SelectedItems[0].SubItems[2].Text;          
            dtDOB.Text = lvDetails.SelectedItems[0].SubItems[3].Text; //Year/Month/Day
            
            //setting gender
            switch (lvDetails.SelectedItems[0].SubItems[4].Text)
            {
                case "M":
                    cbGender.SelectedIndex = 0;
                    break;
                case "F":
                    cbGender.SelectedIndex = 1;
                    break;
                default:
                    cbGender.SelectedIndex = 2;
                    break;
            }

            txtPhone.Text = lvDetails.SelectedItems[0].SubItems[5].Text;
            rtbAddress.Text = lvDetails.SelectedItems[0].SubItems[6].Text;
            //adding modules
            DataTable mods = dh.getModules(lvDetails.SelectedItems[0].SubItems[0].Text);
            rtbModules.Clear();
            foreach (DataRow module in mods.Rows)
            {
                rtbModules.AppendText(module[0].ToString()+" ");
            }
            historyMods = rtbModules.Text;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtSurname.Clear();
            dtDOB.Text = "2001/01/01";
            cbGender.SelectedIndex = -1;
            cbModules.SelectedIndex = -1;
            rtbAddress.Clear();
            rtbModules.Clear();
            pbProfile.Image = null;
            addToListView();
        }
    }
}
