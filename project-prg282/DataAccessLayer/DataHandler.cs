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
            string query = "SELECT DISTINCT s.StudentNumber, s.[Name], s.Surname, s.DOB, s.Gender, s.Phone, s.Address FROM Student s LEFT JOIN StudentModule ON s.StudentNumber = StudentModule.StudentNumber ";
            SqlDataAdapter adapter = new SqlDataAdapter(query, DatabaseCon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }
        public bool addStudent(string id, string Name, string Surn,byte[] photoArr, DateTime DOB, string Gender, string Phone, string Address, string[] modules)
        {
            DatabaseCon.Open();

            //check if student exists
            string selectQry = $"SELECT s.StudentNumber, s.[Name], s.Surname, s.DOB, s.Gender, s.Phone, s.Address FROM Student s LEFT JOIN StudentModule ON s.StudentNumber = StudentModule.StudentNumber WHERE s.StudentNumber = '{id}'";
            SqlCommand selectCommand = new SqlCommand(selectQry, DatabaseCon);

            SqlDataReader reader = selectCommand.ExecuteReader();


            if (reader.HasRows)
            {
                MessageBox.Show("this student already exists, try updating");
                DatabaseCon.Close();
                reader.Close();
                return false;
            }
            else
            {
                reader.Close();

                string InsertQry = $"INSERT INTO Student(StudentNumber, Name, Surname, StudentImage, DOB, Gender, Phone, Address) VALUES ('{id}','{Name}','{Surn}', '{photoArr}', '{DOB}','{Gender}','{Phone}','{Address}')";
                SqlCommand Com = new SqlCommand(InsertQry, DatabaseCon);
                Com.ExecuteNonQuery();
                string values = "";

                for (int i = 0; i < modules.Length; i++)
                {
                    values += $"('{id}','{modules[i].Trim()}')";
                    if (i < modules.Length - 1)
                    {
                        values += ",";
                    }

                }
                string InsertQry2 = $"INSERT INTO StudentModule(StudentNumber,ModuleCode) VALUES {values}";
                SqlCommand Com2 = new SqlCommand(InsertQry2, DatabaseCon);
                Com2.ExecuteNonQuery();

            }
            DatabaseCon.Close();
            return true;

            
        }

        //READ - Search for a specific student
        public DataTable getStudents(string ID)
        {
            string Search = $"Select DISTINCT s.StudentNumber, s.[Name], s.Surname, s.DOB, s.Gender, s.Phone, s.Address FROM Student s LEFT JOIN StudentModule ON s.StudentNumber = StudentModule.StudentNumber WHERE s.StudentNumber = '{ID}'";

            SqlDataAdapter Adap = new SqlDataAdapter(Search, DatabaseCon);

            DataTable DataT = new DataTable();
            Adap.Fill(DataT);

            return DataT;
        }

        //READ - Search for a specific student, but with a stored procedure
        public DataTable getStudentPROC(string ID)
        {
            SqlCommand cmd = new SqlCommand("SearchStudent", DatabaseCon) { CommandType = CommandType.StoredProcedure};
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
        public bool UpdateStudent(string id, string Name, string Surn, byte[] photoArr, DateTime DOB, string Gender, string Phone, string Address, string[] modules =null, bool modulesUpdated = true)
        {
            DatabaseCon.Open();
            //deconstruct query for conveniece and to make it simpler
            string whereClause = $"WHERE StudentNumber = '{id}'";

            //check if student exists
            string selectQry = $"SELECT s.StudentNumber, s.[Name], s.Surname, s.DOB, s.Gender, s.Phone, s.Address FROM Student s LEFT JOIN StudentModule ON s.StudentNumber = StudentModule.StudentNumber WHERE s.StudentNumber = '{id}'";
            SqlCommand selectCommand = new SqlCommand(selectQry, DatabaseCon);

            SqlDataReader reader = selectCommand.ExecuteReader();


            if (!reader.HasRows)
            {
                MessageBox.Show("user does not exist yet, you can only update a user that already exists");
                DatabaseCon.Close();
                reader.Close();
                return false;
                
            }
            else
            {
                reader.Close();

                /*
                 * Summary: update information in student table but delete student data from studentModule joining table because of possible ghost reads and stores and
                 * because the update will overwrite all rows of student number
                 * insert new student module infomration and then the update is complete.  
                 */


                //step 1: normally update the student Info in the student table
                string updateQry = $"UPDATE Student SET StudentNumber = '{id}', [Name] = '{Name}', Surname = '{Surn}', StudentImage = '{photoArr}', DOB = '{DOB}', Gender = '{Gender}', Phone = '{Phone}', [Address] = '{Address}' {whereClause}";
                SqlCommand Com = new SqlCommand(updateQry, DatabaseCon);
                Com.ExecuteNonQuery();

                if (modulesUpdated)
                {
                    //step 2: Delete student data from joining table
                    string deleteQry = $"DELETE FROM StudentModule {whereClause}";
                    SqlCommand deleteCommand = new SqlCommand(deleteQry, DatabaseCon);
                    deleteCommand.ExecuteNonQuery();
                    //step 3: insert the student's repsective modules in the studentModule table          
                    string values = "";
                    //here we generate the last bit the the query first to include all the value at once in correct format
                    for (int i = 0; i < modules.Length; i++)
                    {
                        if (modules[i].Trim().Length > 1)
                        {
                            values += $"('{id}','{modules[i].Trim()}')";
                            if (i < modules.Length - 1)
                            {
                                //syntax check.
                                values += ",";
                            }
                        }
                    }
                    //execute insert
                    string InsertQry = $"INSERT INTO StudentModule(StudentNumber,ModuleCode) VALUES {values}";
                    SqlCommand insertCommand = new SqlCommand(InsertQry, DatabaseCon);
                    insertCommand.ExecuteNonQuery();
                    //update complete
                }
                DatabaseCon.Close();
                return true;
            }
        }
        //DELETE - Delete a student from the database
        public bool DeleteStudent(string ID)
        {
            DatabaseCon.Open();
            //check if student exists
            string selectQry = $"SELECT s.StudentNumber, s.[Name], s.Surname, s.DOB, s.Gender, s.Phone, s.Address FROM Student s LEFT JOIN StudentModule ON s.StudentNumber = StudentModule.StudentNumber WHERE s.StudentNumber = '{ID}'";
            SqlCommand selectCommand = new SqlCommand(selectQry, DatabaseCon);
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (!reader.HasRows)
            {
                MessageBox.Show("User does not exist");
                DatabaseCon.Close();
                reader.Close();
                return false;
            }
            else
            {
                reader.Close();

                string DeleteQryStep1 = $"DELETE FROM StudentModule WHERE StudentNumber = '{ID}'";
                string DeleteQryStep2 = $"DELETE FROM Student WHERE StudentNumber = '{ID}'";
                SqlCommand DeleteCom = new SqlCommand();
                DeleteCom.Connection = DatabaseCon;

                DeleteCom.CommandText = DeleteQryStep1;
                DeleteCom.ExecuteNonQuery();
                DeleteCom.CommandText = DeleteQryStep2;
                DeleteCom.ExecuteNonQuery();
            }            
            DatabaseCon.Close();
            return true;
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

        public DataTable getModules(string id)
        {
            string query = $"SELECT Module.ModuleCode FROM Module INNER JOIN StudentModule ON Module.ModuleCode = StudentModule.ModuleCode WHERE StudentModule.StudentNumber = '{id}'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, DatabaseCon);
            DataTable DT = new DataTable();
            adapter.Fill(DT);
            return DT;
        }
    }
}
