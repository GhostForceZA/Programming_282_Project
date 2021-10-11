using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace project_prg282.BusinessLogicLayer
{
    class Student: Person, IComparable
    {
        string studentNUmber;
        Bitmap studentImage;
        List<string> moduleCodes;

        public Student(string studentNUmber, Bitmap studentImage, List<string> moduleCodes)
        {
            this.StudentNUmber = studentNUmber;
            this.StudentImage = studentImage;
            this.ModuleCodes = moduleCodes;
        }

        public string StudentNUmber { get => studentNUmber; set => studentNUmber = value; }
        public Bitmap StudentImage { get => studentImage; set => studentImage = value; }
        public List<string> ModuleCodes { get => moduleCodes; set => moduleCodes = value; }

        public int CompareTo(object obj)
        {
            Student otherStudent = (Student)obj;

            return this.NameAndSurname.CompareTo(otherStudent.NameAndSurname);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
