using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_prg282.BusinessLogicLayer
{
    abstract class Person
    {
        //normal person attributes
        public string NameAndSurname { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string address { get; set; }

        public DateTime DOB { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.NameAndSurname, this.Gender, this.PhoneNumber);
        }
    }
}
