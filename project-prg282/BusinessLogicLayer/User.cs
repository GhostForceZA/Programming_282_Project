using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_prg282.DataAccessLayer;
using project_prg282.PresentationLayer;
using System.Windows.Forms;

namespace project_prg282.BusinessLogicLayer
{
    class User: iDefineUser
    {
        string password, username;
        FileHandler fh;
        public User() 
        {
            fh = new FileHandler();
        }
        public User(string password, string username)
        {
            this.Password = password;
            this.Username = username;
            fh = new FileHandler();
        }

        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }

        private bool foundUsername(string username)
        {
            // the difference between this and Userexists, is this will only check if the username is found in case of a new user being added
            //this method is private and is just meant to be used inside the addUser method.
            try
            {
                bool found = false;
                List<User> users = extractUsers(fh.ReadTextFile());

                foreach (User person in users)
                {
                    if (person.username == username)
                    {
                        found = true;
                    }
                }
                return found;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

         
        }

        private List<User> extractUsers(List<string> usersInText)
        {
            List<User> usersInObjects = new List<User>();
            foreach (string being in usersInText)
            {
                string username = being.Substring(0, being.IndexOf(":"));
                string password = being.Substring(being.IndexOf(":")+1);
                usersInObjects.Add(new User(password, username));
            }
            return usersInObjects;
        }

        public bool UserExists(string Username, string password)
        {
            bool found = false;

            foreach (User user in extractUsers(fh.ReadTextFile()))
            {
                if (foundUsername(Username))
                {
                    if (user.password == password)
                    {
                        found = true;
                    }
                }
             
            }


            return found;
        }

        public bool addUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            bool success = true;
            try
            {
                if (foundUsername(username))
                {
                    throw new InputException("Username already exists");
                    
                }
                else
                {
                    fh.WriteToTextFile(this.ToString());
                    return success;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return !success;
            }
        }

        public override string ToString()
        {
            return $"{this.username}:{this.password}";
        }
    }
}
