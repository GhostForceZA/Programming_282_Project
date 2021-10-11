using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace project_prg282
{
    class FileHandler : IFileControl, IDatabaseControl
    {
        string fileName = "Users.txt";
        string connection = "Server=(local); Initial Catalog=StudentInfo; Integrated Security= SSPI";

        public DataTable requestData(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }
        public void requestInsert(SqlConnection cn, string query)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.ExecuteNonQuery();
        }
        public void requestUpdate(SqlConnection cn, string query)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.ExecuteNonQuery();
        }
        public void requestDelete(SqlConnection cn, string query)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.ExecuteNonQuery();
        }
        public void CloseDatabaseConnection(SqlConnection cn)
        {
            cn.Close();
        }

        public SqlConnection OpenDatabaseConnection()
        {
            SqlConnection cn = new SqlConnection(connection);
            try
            {
                cn.Open();
                MessageBox.Show("Connection successful");
            }
            catch (Exception)
            {

                MessageBox.Show("Connection error");
            }
            return cn;
        }

        public List<string> ReadTextFile()
        {
            StreamReader reader = new StreamReader(fileName);
            string line;
            List<string> users = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                users.Add(line);
            }

            return users;
        }

        public void WriteToTextFile(string text)
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(text);
            }
        }
    }
}
