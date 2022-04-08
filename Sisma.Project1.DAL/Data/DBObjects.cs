using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sisma.Project1.DAL.Data
{
    public class School : IDBEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


        //navigation
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Student> Students { get; set; }


    }

    public class Class : IDBEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }

        //navigation
        public virtual School School { get; set; }
        public virtual ICollection<StudentInClass> StudentInClasses { get; set; }


    }

    public class Student : IDBEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


        //navigation
        public virtual School School { get; set; }
        public virtual ICollection<StudentInClass> StudentInClasses { get; set; }



    }

    public class StudentInClass : IDBEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }


        //navigation

        public virtual Student Student { get; set; }
        public virtual Class Class { get; set; }

    }


    public interface IDBEntity
    {
        public Guid RefId { get; set; }
    }
}
