using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sisma.Project1.Logic.Data;

namespace Sisma.Project1.Logic.Business
{
    public class SismaBL
    {

        private readonly SismaContextFactory _dbContextFactory;

        public SismaBL(SismaContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }



        public class CRUD : IDisposable
        {

            public Repository<School> Schools { get; set; } // = new Repository<Location>();
            public Repository<Class> Classes { get; set; } // = new Repository<Artist>();
            public Repository<Student> Students { get; set; } // = new Repository<User>();
            public Repository<StudentInClass> StudentInClasses { get; set; } // = new Repository<User>();



            public CRUD()
            {
                this.Schools = new Repository<School>();
                this.Classes = new Repository<Class>();
                this.Students = new Repository<Student>();
                this.StudentInClasses = new Repository<StudentInClass>();
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
            try
            {
                using (var db = _dbContextFactory.CreateDbContext(new string[0] { }))
                {
                    var res = db.Classes
                                .Include(item => item.StudentInClasses)
                                      .ThenInclude(item0 => item0.Student)
                                .FirstOrDefault(item => item.RefId == classId);
                    var res0 = res.StudentInClasses.Where(kp => kp.Class.RefId == classId).Select(item => item.Student);

                    return res0.Where(item => item.IsActive).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);  //TODO log
                throw;
            }

        }

        public List<Class> GetAllStudentClasses(Guid studentId) // – all active classes that the user has.
        {
            try
            {
                using (var db = _dbContextFactory.CreateDbContext(new String[0]))
                {
                    var res = db.Students
                        .Include(item => item.StudentInClasses)
                              .ThenInclude(item0 => item0.Class)
                        .FirstOrDefault(item => item.RefId == studentId);
                    var res0 = res.StudentInClasses.Select(item => item.Class).ToList();

                    return res0.Where(item => item.IsActive).ToList();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);  //TODO log
                throw;
            }

        }

        public void AddStudentToClass(Guid studentId, Guid classId)
        {
            try
            {
                using (var db = _dbContextFactory.CreateDbContext(new string[0] { }))
                {
                    var res = db.Students
                        .Include(item => item.StudentInClasses)
                        .FirstOrDefault(item => item.RefId == studentId);
                    var res0 = db.Classes
                        .Include(item => item.StudentInClasses)
                        .FirstOrDefault(item => item.RefId == classId);

                    res.StudentInClasses.Add(new StudentInClass() { ClassId = res0.Id });

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);  //TODO log
                throw;
            }

        }

        public void DeleteSchool(Guid schoolId, bool forceDelete)
        {
            try
            {
                using (var db = _dbContextFactory.CreateDbContext(new string[0] { }))
                {
                    var dbSchool = db.Schools
                                    .Include(item => item.Classes)
                                    .Include(item => item.Students)
                                    .FirstOrDefault(item => item.RefId == schoolId);
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
            catch (Exception e)
            {
                Console.WriteLine(e);  //TODO log
                throw;
            }

        }


        public void DeleteAllEmptyClasses() // <one endpoint from your imagination>
        {
            try
            {
                using (var db = _dbContextFactory.CreateDbContext(new string[0] { }))
                {
                    var res = db.Classes
                        .Include(item => item.StudentInClasses)
                        .Where(item => item.StudentInClasses.Count == 0);

                    db.Classes.RemoveRange(res);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);  //TODO log
                throw;
            }

        }















        public bool TestDB()
        {
            using (var db = _dbContextFactory.CreateDbContext(new string[0] { }))
            {
                //var isOK = db.Database.Exists();
                //if (!isOK)
                //    return false;

                try
                {
                    var res = db.Students.Count(); //do some action
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
        void Create(T entity, bool saveChanges = true);
        void Update(T entity, bool saveChanges = true);
        void Delete(Guid id, bool saveChanges = true);
        bool Exists(Guid id);
        int SaveChanges();
    }

    public class Repository<T> : IRepository<T>
        where T : class, IDBEntity
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
            return GetInternal(id);
        }

        private T GetInternal(Guid id)
        {
            return _db.Set<T>().FirstOrDefault(item => item.RefId == id);
        }

        public void Create(T entity, bool saveChanges = true)
        {
            _db.Set<T>().Add(entity);
            if (saveChanges)
            {
                _db.SaveChanges();
            }
        }

        public void Update(T entity, bool saveChanges = true)
        {
            _db.Entry<T>(entity).State = EntityState.Modified;
            if (saveChanges)
            {
                _db.SaveChanges();
            }
        }

        public void Delete(Guid id, bool saveChanges = true)
        {
            var q = Get(id);
            _db.Set<T>().Remove(q);
            if (saveChanges)
            {
                _db.SaveChanges();
            }
        }

        public bool Exists(Guid id)
        {
            var res = GetInternal(id);
            return res != null;
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

    public interface IDBEntity
    {
        public Guid RefId { get; set; }
    }
}