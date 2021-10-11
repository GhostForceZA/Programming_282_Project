using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_prg282.PresentationLayer
{
    abstract class Person
    {
        

        public string NameAndSurname { get; set; }

        public  string Gender { get; set; }

        public  string PhoneNumber { get; set; }

        //public address

        public  DateTime DOB { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.NameAndSurname, this.Gender, this.PhoneNumber);
        }
    }
}
