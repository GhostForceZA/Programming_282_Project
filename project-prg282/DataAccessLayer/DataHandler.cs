using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_prg282.BusinessLogicLayer;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace project_prg282.DataAccessLayer
{
    class DataHandler
    {
        SqlConnection DatabaseCon = new SqlConnection("Server=(Local); Initial Catalog=StudentInfo; Integrated Security=SSPI");

        //CRUD - CREATE READ UPDATE DELETE

        //CREATE - Adding a new student to the database

        public DataTable getAllStudents()
        {
            string query = "SELECT * FROM Student LEFT JOIN StudentModule ON Student.StudentNumber = StudentModule.StudentNumber";
            SqlDataAdapter adapter = new SqlDataAdapter(query, DatabaseCon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }
        public void addStudent(string id, string Name, string Surn,byte[] photoArr, DateTime DOB, string Gender, string Phone, string Address, string[] modules)
        {
            DatabaseCon.Open();

            string InsertQry = $"INSERT INTO Student(StudentNumber, Name, Surname, StudentImage, DOB, Gender, Phone, Address) VALUES ('{id}','{Name}','{Surn}', '{photoArr}', '{DOB}','{Gender}','{Phone}','{Address}')";
            SqlCommand Com = new SqlCommand(InsertQry, DatabaseCon);
            Com.ExecuteNonQuery();
            string values = "";

            for (int i = 0; i < modules.Length; i++)
            {
                values += $"('{id}','{modules[i]}')";
                if (i < modules.Length-1)
                {
                    values += ",";
                }

            }
            string InsertQry2 = $"INSERT INTO StudentModule(StudentNumber,ModuleCode) VALUES {values}";
            SqlCommand Com2 = new SqlCommand(InsertQry2, DatabaseCon);
            Com2.ExecuteNonQuery();


            DatabaseCon.Close();
        }

        //CREATE - Adding a new student to the database, but with a stored procedure
        public void addStudentPROC(string Name, string Surn, DateTime DOB, string Gender, string Phone, string Address)
        {
            SqlCommand cmd = new SqlCommand("spInsertStudent", DatabaseCon);
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
        public DataTable getStudents(string ID)
        {
            string Search = $"Select * from Student s LEFT JOIN StudentModule sm ON s.StudentNumber = sm.StudentNumber WHERE s.StudentNumber = '{ID}'";

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
        public void UpdateStudent(string id, string Name, string Surn, byte[] photoArr, DateTime DOB, string Gender, string Phone, string Address, string[] modules)
        {
            DatabaseCon.Open();
            // we need to add the student to the student table and then respectively add each module in the studentModule join table
            string whereClause = $"WHERE StudentNumber = '{id}'";

            string InsertQry = $"UPDATE Student SET StudentNumber = '{id}', [Name] = '{Name}', Surname = '{Surn}', StudentImage = '{photoArr}', DOB = '{DOB}', Gender = '{Gender}', Phone = '{Phone}', [Address] = '{Address}' {whereClause}";
            MessageBox.Show(InsertQry);
            SqlCommand Com = new SqlCommand(InsertQry, DatabaseCon);
            Com.ExecuteNonQuery();

            string values = "";

            for (int i = 0; i < modules.Length; i++)
            {
                values += $"('{id}','{modules[i]}')";
                if (i < modules.Length - 1)
                {
                    values += ",";
                }

            }
            //loop though modules and add each module code beside student number, this links it
          
            string InsertQry2 = $"UPDATE StudentModule SET StudentNumber = '{id}', ModuleCode = {whereClause}";
            SqlCommand Com2 = new SqlCommand(InsertQry2, DatabaseCon);
            Com2.ExecuteNonQuery();


            DatabaseCon.Close();
        }

       

        //DELETE - Delete a student from the database
        public void DeleteStudent(string ID)
        {
            DatabaseCon.Open();

            string DeleteQryStep1 = $"DELETE FROM StudentModule WHERE StudentNumber = '{ID}'";
            string DeleteQryStep2 = $"DELETE FROM Student WHERE StudentNumber = '{ID}'";
            SqlCommand DeleteCom = new SqlCommand();
            DeleteCom.Connection = DatabaseCon;

            DeleteCom.CommandText = DeleteQryStep1;           
            DeleteCom.ExecuteNonQuery();
            DeleteCom.CommandText = DeleteQryStep2;
            DeleteCom.ExecuteNonQuery();
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
