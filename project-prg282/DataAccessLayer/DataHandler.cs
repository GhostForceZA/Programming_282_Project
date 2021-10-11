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

        public DataTable getStudents(string Username)
        {
            //add queries

            string Qry = "SELECT * FROM table_name WHERE Username = " + Username;

            SqlDataAdapter Adap = new SqlDataAdapter(Qry, DatabaseCon);

            DataTable DataT = new DataTable();
            Adap.Fill(DataT);

            return DataT;
        }

        //public SqlDataReader get
        

    }
}
