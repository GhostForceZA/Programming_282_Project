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
        

        //CRUD - CREATE READ UPDATE DELETE

        //CREATE - Adding a new student to the database
        public void addStudent(string Name, string Surn, DateTime DOB, string Gender, string Phone, string Address)
        {
            DatabaseCon.Open();

            string InsertQry = "INSERT INTO Student (Name, Surname, DOB, Gender, Phone, Address) VALUES ("  + Name + ", " + Surn + ", " + DOB + ", " + Gender + ", " + Phone + ", " + Address + ")";

            SqlCommand Com = new SqlCommand(InsertQry, DatabaseCon);
            Com.ExecuteNonQuery();

            DatabaseCon.Close();
        }

        //CREATE - Adding a new student to the database, but with a stored procedure
        public void addStudentPROC(string Name, string Surn, DateTime DOB, string Gender, string Phone, string Address)
        {
            SqlCommand cmd = new SqlCommand("spDeleteStudent", DatabaseCon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", Name);
            cmd.Parameters.AddWithValue("@Id", Surn);
            cmd.Parameters.AddWithValue("@Id", DOB);
            cmd.Parameters.AddWithValue("@Id", Gender);
            cmd.Parameters.AddWithValue("@Id", Phone);
            cmd.Parameters.AddWithValue("@Id", Address);

            DatabaseCon.Open();
            cmd.ExecuteNonQuery();
        }

        //READ - Search for a specific student
        public DataTable getStudents(string Name)
        {
            string Search = "SELECT * FROM Student WHERE Name = " + Name;

            SqlDataAdapter Adap = new SqlDataAdapter(Search, DatabaseCon);

            DataTable DataT = new DataTable();
            Adap.Fill(DataT);

            return DataT;
        }

        //READ - Search for a specific student, but with a stored procedure
        public DataTable getStudentPROC(string ID)
        {
            SqlCommand cmd = new SqlCommand("SearchStudent", DatabaseCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", ID);

            DatabaseCon.Open();
            DataTable DT = new DataTable();

            using (SqlDataReader DR = cmd.ExecuteReader())
            {
                DT.Load(DR);
                return DT;
            }
        }

        //UPDATE - Updating an existing student
        public void UpdateStudent(string ID, string NName, string NSur, DateTime NDOB, string NGender, string NPhone, string NAddress)
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

        //UPDATE - Updating an existing student, but with a stored procedure
        public void UpdateStudentPROC(string ID, string NName, string NSur, DateTime NDOB, string NGender, string NPhone, string NAddress)
        {
            SqlCommand cmd = new SqlCommand("spUpdateStudent", DatabaseCon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", ID);
            cmd.Parameters.AddWithValue("@Id", NName);
            cmd.Parameters.AddWithValue("@Id", NSur);
            cmd.Parameters.AddWithValue("@Id", NDOB);
            cmd.Parameters.AddWithValue("@Id", NGender);
            cmd.Parameters.AddWithValue("@Id", NPhone);
            cmd.Parameters.AddWithValue("@Id", NAddress);

            DatabaseCon.Open();
            cmd.ExecuteNonQuery();
        }

        //DELETE - Delete a student from the database
        public void DeleteStudent(string ID)
        {
            DatabaseCon.Open();

            string DeleteQry = "DELETE FROM Student WHERE StudentNumber = " + ID;

            SqlCommand DeleteCom = new SqlCommand();

            DeleteCom.CommandText = DeleteQry;

            DatabaseCon.Close();
        }

        //DELETE - Delete a student from the database, but with a stored procedure
        public void DeleteStudentPROC(string ID)
        {
            SqlCommand cmd = new SqlCommand("spDeleteStudent", DatabaseCon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", ID);

            DatabaseCon.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
