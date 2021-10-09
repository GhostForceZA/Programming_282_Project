using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_prg282.DataAccessLayer;

namespace project_prg282.BusinessLogicLayer
{
    class User: iDefineUser
    {
        string password, username;
        DataHandler dh;
        public User() 
        {
            dh = new DataHandler();
        }
        public User(string password, string username)
        {
            this.Password = password;
            this.Username = username;
            dh = new DataHandler();
        }

        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }

        public bool ValidateUsername(string username)
        {
            // the difference between this and Userexists, is this will only check if the username is found in case of a new user being added
            //this method is private and is just meant to be used inside the addUser method.
            return true;
        }

        public bool UserExists(string Username, string password)
        {
            bool found = false;

            foreach (User userCycle in dh.getUsers())
            {
                if(userCycle.Username == Username)
                {
                    if(userCycle.Password == password)
                    {
                        found = true;
                    }
                }
            }


            return found;
        }

        public void addUser(string Username, string password)// waiting for datahandler
        {

        }
    }
}
