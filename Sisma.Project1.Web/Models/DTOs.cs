using System.ComponentModel.DataAnnotations;

namespace Sisma.Project1.Web.Models
{
    public class SchoolDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        /// <summary>
        /// The name of the school
        /// </summary>
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The address of the school
        /// </summary>
        [MaxLength(150)]
        [Required]
        public string Address { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }

    }

    public class ClassDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        /// <summary>
        /// The id (=incremental) of the school that the current class is associated with.
        /// </summary>
        [Required]
        public int SchoolId { get; set; }
        /// <summary>
        /// The name of the school
        /// </summary>
        [MaxLength(150)]
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


    }

    public class StudentDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        /// <summary>
        /// The id (=incremental) of the school that the current student is associated with.
        /// </summary>
        [Required]
        public int SchoolId { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public bool IsActive { get; set; }


    }
    public class StudentInClassDTO
    {
        public int Id { get; set; }
        public Guid RefId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ClassId { get; set; }
    }

}
