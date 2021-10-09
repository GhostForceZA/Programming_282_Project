using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_prg282.PresentationLayer
{
    class UserNotFoundException : Exception
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception e): base ( message, e) { }
        
    }
}
