using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sisma.Project1.Logic.Data
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public Guid SchoolRefId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


        //navigation
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Student> Students { get; set; }

    }

    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public Guid ClassRefId { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }

        //navigation
        public virtual School School { get; set; }
        public virtual ICollection<StudentInClass> StudentInClasses { get; set; }

    }

    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public Guid StudentRefId { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


        //navigation
        public virtual School School { get; set; }
        public virtual ICollection<StudentInClass> StudentInClasses { get; set; }

    }

    public class StudentInClass
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }


        //navigation

        public virtual Student Student { get; set; }
        public virtual Class Class { get; set; }

    }

}
