namespace Sisma.Project1.Web.Models
{
    public class SchoolDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }

    }

    public class ClassDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


    }

    public class StudentDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


    }
   public class StudentInClassDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
    }

}
