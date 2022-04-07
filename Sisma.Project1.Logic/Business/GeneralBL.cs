using Microsoft.EntityFrameworkCore;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Logic.Models;

namespace Sisma.Project1.Logic.Business
{
    public class GeneralBL
    {


        //public GeneralBL(SismaContext dbContext)
        //{
        //}



        public class CRUD : IDisposable
        {

            public Repository<School> Schools { get; set; } // = new Repository<Location>();
            public Repository<Class> Classes { get; set; } // = new Repository<Artist>();
            public Repository<Student> Students { get; set; } // = new Repository<User>();



            public CRUD()
            {
                this.Schools = new Repository<School>();
                this.Classes = new Repository<Class>();
                this.Students = new Repository<Student>();
            }




            //public class LocationsRepository : Repository<Location>
            //{

            //}


            public void Dispose()
            {
                Schools?.Dispose();
                Classes?.Dispose();
                Students?.Dispose();
            }


            #region Excluded code.

            //public class Locations
            //{
            //    public void Get()
            //    {
            //        using (var db = new KaspitDB3())
            //        {
            //            db
            //        }
            //    }
            //    public void GetAll()
            //    {

            //    }
            //    public void Create(Location location)
            //    {

            //    }
            //    public void Get()
            //    {

            //    }
            //    public void Delete(Guid id)
            //    {

            //    }
            //    public void Get()
            //    {

            //    }
            //} 

            #endregion
        }






        public List<Student> GetClassStudentsByClassId(Guid classId) // – list of all active students of a class
        {
            using (var db = new SismaContextFactory().CreateDbContext(new string[0] { }))
            {
                var x = db.Classes.FirstOrDefault(item => item.ClassRefId == classId);
                var x0 = x.StudentInClasses.Where(kp => kp.Class.ClassRefId == classId).Select(item => item.Student);

                return x0.Where(item => item.IsActive).ToList();
            }
        }

        public List<Class> GetAllStudentClasses(Guid studentId) // – all active classes that the user has.
        {
            using (var db = new SismaContextFactory().CreateDbContext(new string[0] { }))
            {
                var x = db.Students.FirstOrDefault(item => item.StudentRefId == studentId);
                var x0 = x.StudentInClasses.Select(item => item.Class).ToList();

                return x0.Where(item => item.IsActive).ToList();
            }
        }

        public void AddStudentToClass(Guid studentId, Guid classId)
        {
            using (var db = new SismaContextFactory().CreateDbContext(new string[0] { }))
            {
                var x = db.Students.FirstOrDefault(item => item.StudentRefId == studentId);
                var x0 = db.Classes.FirstOrDefault(item => item.ClassRefId == classId);

                x.StudentInClasses.Add(new StudentInClass() { ClassId = x0.ClassId });

                db.SaveChanges();
            }
        }

        public void DeleteSchool(Guid schoolId, bool forceDelete)
        {
            using (var db = new SismaContextFactory().CreateDbContext(new string[0] { }))
            {
                var dbSchool = db.Schools.FirstOrDefault(item => item.SchoolRefId == schoolId);
                bool hasDependants = !(dbSchool.Classes.Count == 0 && dbSchool.Students.Count == 0);

                if (!hasDependants)
                {
                    db.Schools.Remove(dbSchool);
                }
                else
                {
                    if (forceDelete)
                    {
                        //take no action
                    }
                    else
                    {
                        db.Students.RemoveRange(dbSchool.Students); //students
                        db.Classes.RemoveRange(dbSchool.Classes); //classes
                        db.Schools.Remove(dbSchool);
                    }
                }

                db.SaveChanges();
            }
        }


        public void DeleteAllEmptyClasses() // <one endpoint from your imagination>
        {
            using (var db = new SismaContextFactory().CreateDbContext(new string[0] { }))
            {
                var x = db.Classes.Where(item => item.StudentInClasses.Count == 0);

                db.Classes.RemoveRange(x);
                db.SaveChanges();
            }
        }















        public static bool TestDB()
        {
            using (var db = new SismaContextFactory().CreateDbContext(new string[0] { }))
            {
                //var isOK = db.Database.Exists();
                //if (!isOK)
                //    return false;

                try
                {
                    var x = db.Students.Count(); //do some action
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw; //ignore
                    //return false;
                }
            }

        }

    }

    public interface IRepository<T> : IDisposable
        where T : class
    {
        List<T> GetAll();
        T Get(Guid id);
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
        bool Exists(Guid id);
        int SaveChanges();
    }

    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected SismaContext _db;

        public Repository()
        {
            _db = new SismaContextFactory().CreateDbContext(new string[0]);
        }


        public List<T> GetAll()
        {
            return _db.Set<T>().Select(a => a).ToList();
        }

        public T Get(Guid id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _db.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var q = Get(id);
            _db.Set<T>().Remove(q);
        }

        public bool Exists(Guid id)
        {
            return _db.Set<T>().Find(id) != null;
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }


        public void Dispose()
        {
            _db.Dispose();
            _db = null;
        }
    }
}