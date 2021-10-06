using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_prg282.PresentationLayer
{
    class User: iDefineUser
    {
        string password, username;
        public User(){}
        public User(string password, string username)
        {
            this.Password = password;
            this.Username = username;
        }

        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }

        public bool ValidateUsername(string username)
        {
            // the difference between this and Userexists, is this will only check if the username is found in case of a new user being added
            //this method is private and is just meant to be used inside the addUser method.
            return true;
        }

        public bool UserExists(string Username, string password)// incomplete waiting for dataHandler
        {
            bool found = false;

            return false;
        }

       public void addUser(string Username, string password)// waiting for datahandler
        {

        }
    }
}
