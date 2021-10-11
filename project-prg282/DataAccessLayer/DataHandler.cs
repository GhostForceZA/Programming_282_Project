using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_prg282.BusinessLogicLayer;
using System.Data.SqlClient;
using System.Data;



namespace project_prg282.DataAccessLayer
{
    class DataHandler
    {
        SqlConnection DatabaseCon = new SqlConnection("Server=(Local); Initial Catalog=StudentInfo; Integrated Security=SSPI");

        //Filehandler fh;
        public DataHandler() 
        {
          //  fh = new Filehandler();
        }

        public List<User> getUsers()
        {
            
            List<User> allUsers = new List<User>();

            return allUsers;            

        }

        public void addUser() { 
            //waiting on FH for request
        }

        //CRUD - CREATE READ UPDATE DELETE

        //CREATE - Adding a new student in the database
        public void addStudent(string Name, string Surn, DateTime DOB, string Gender, string Phone, string Address)
        {
            DatabaseCon.Open();

            string InsertQry = "INSERT INTO Student (Name, Surname, DOB, Gender, Phone, Address) VALUES ("  + Name + ", " + Surn + ", " + DOB + ", " + Gender + ", " + Phone + ", " + Address + ")";

            SqlCommand Com = new SqlCommand(InsertQry, DatabaseCon);
            Com.ExecuteNonQuery();

            DatabaseCon.Close();
        }

        //
        public DataTable getStudents(string Name)
        {
            string Search = "SELECT * FROM Student WHERE Name = " + Name;

            SqlDataAdapter Adap = new SqlDataAdapter(Search, DatabaseCon);

            DataTable DataT = new DataTable();
            Adap.Fill(DataT);

            return DataT;
        }

        public void UpdateStudent(int ID, string NName, string NSur, DateTime NDOB, string NGender, string NPhone, string NAddress)
        {
            DatabaseCon.Open();

            string UpdateQry = "UPDATE Student SET Name = " + NName +
                " , Surname = " + NSur +
                " , DOB = " + NDOB +
                " , Gender = " + NGender +
                " , Phone = " + NPhone +
                " , Address = " + NAddress +
                " WHERE StudentNumber = " + ID;

            SqlCommand UpdateCom = new SqlCommand(UpdateQry, DatabaseCon);
            UpdateCom.ExecuteNonQuery();

            DatabaseCon.Close();
        }

        public void DeleteStudent(int ID)
        {
            DatabaseCon.Open();

            string DeleteQry = "DELETE FROM Student WHERE StudentNumber = " + ID;

            SqlCommand DeleteCom = new SqlCommand();

            DeleteCom.CommandText = DeleteQry;

            DatabaseCon.Close();
        }
    }
}
