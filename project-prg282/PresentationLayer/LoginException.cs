using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_prg282.PresentationLayer
{
    class LoginException: Exception
    {
        public LoginException() { }

        public LoginException(string message) : base(message) { }

        public LoginException(string message, Exception inner) : base(message, inner) { }

       
    }
}
