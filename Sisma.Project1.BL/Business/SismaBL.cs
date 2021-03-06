using Microsoft.EntityFrameworkCore;
using Sisma.Project1.DAL.Data;
using Sisma.Project1.Shared.Exceptions;

namespace Sisma.Project1.BL.Business
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

            public SchoolRepository Schools { get; set; } // = new Repository<Location>();
            public ClassRepository Classes { get; set; } // = new Repository<Artist>();
            public StudentRepository Students { get; set; } // = new Repository<User>();
            public StudentInClassRepository StudentInClasses { get; set; } // = new Repository<User>();



            public CRUD()
            {
                this.Schools = new SchoolRepository();
                this.Classes = new ClassRepository();
                this.Students = new StudentRepository();
                this.StudentInClasses = new StudentInClassRepository();
            }








            public void Dispose()
            {
                Schools?.Dispose();
                Classes?.Dispose();
                Students?.Dispose();
            }



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
                Console.WriteLine(e);   //Todo LOG for the current layer (=BLL)
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
                Console.WriteLine(e);   //Todo LOG for the current layer (=BLL)
                throw;
            }

        }

        public void AddStudentToClass(Guid studentId, Guid classId)
        {
            try
            {
                using (var db = _dbContextFactory.CreateDbContext(new string[0] { }))
                {
                    var dbClass = db.Classes
                        .Include(item => item.StudentInClasses)
                        .FirstOrDefault(item => item.RefId == classId);
                    var dbStudent = db.Students
                        .Include(item => item.StudentInClasses)
                        .FirstOrDefault(item => item.RefId == studentId);

                    if (dbClass.SchoolId != dbStudent.SchoolId)
                    {
                        throw new SismaException(SismaExceptionTypes.StudentSchoolMismatch);
                    }

                    dbStudent.StudentInClasses.Add(new StudentInClass() { ClassId = dbClass.Id });

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);   //Todo LOG for the current layer (=BLL)
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
                                          .ThenInclude(item0 => item0.StudentInClasses)
                                    .Include(item => item.Students)
                                          .ThenInclude(item0 => item0.StudentInClasses)
                                    .FirstOrDefault(item => item.RefId == schoolId);
                    bool hasDependants = !(dbSchool.Classes.Count == 0 && dbSchool.Students.Count == 0);

                    if (!hasDependants)
                    {
                        db.Schools.Remove(dbSchool);
                    }
                    else
                    {
                        if (!forceDelete)
                        {
                            throw new SismaException(SismaExceptionTypes.ObjectDependencyExists, "School has dependencies");
                        }
                        else
                        {
                            db.StudentInClasses.RemoveRange(dbSchool.Students.SelectMany(item => item.StudentInClasses));
                            db.StudentInClasses.RemoveRange(dbSchool.Classes.SelectMany(item => item.StudentInClasses));
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
                Console.WriteLine(e);   //Todo LOG for the current layer (=BLL)
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
                Console.WriteLine(e);   //Todo LOG for the current layer (=BLL)
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


    public class SchoolRepository : Repository<School>
    {
        protected override School GetInternal(Guid id)
        {
            return _db.Schools
                        .Include(item => item.Classes)
                        .Include(item => item.Students)
                .FirstOrDefault(item => item.RefId == id);
        }

        public override void Create(School entity, bool saveChanges = true)
        {
            //fix input::
            entity.Id = 0;
            entity.RefId = Guid.NewGuid();
            entity.RecordCreateDate = DateTime.Now;
            entity.IsActive = true;

            //var dbSchool = GetInternal(entity.RefId);
            //if (dbSchool != null)
            //{
            //    throw new SismaException(SismaExceptionTypes.ObjectAlreadyExists);
            //}

            base.Create(entity, saveChanges);
        }

        public override void Update(School entity, bool saveChanges = true)
        {
            base.Update(entity, saveChanges);
        }

        public override void Delete(Guid id, bool saveChanges = true)
        {
            var dbClass = GetInternal(id);
            if (dbClass.Classes.Count != 0 || dbClass.Students.Count != 0)
            {
                throw new SismaException(SismaExceptionTypes.ObjectDependencyExists);
            }
            base.Delete(id, saveChanges);
        }
    }

    public class ClassRepository : Repository<Class>
    {
        protected override Class GetInternal(Guid id)
        {
            return _db.Classes
                .Include(item => item.StudentInClasses)
                .FirstOrDefault(item => item.RefId == id);
        }

        public override void Create(Class entity, bool saveChanges = true)
        {
            //fix input::
            entity.Id = 0;
            entity.RefId = Guid.NewGuid();
            entity.RecordCreateDate = DateTime.Now;
            entity.IsActive = true;

            //var dbClass = GetInternal(entity.RefId);
            //if (dbClass != null)
            //{
            //    throw new SismaException(SismaExceptionTypes.ObjectAlreadyExists);
            //}

            var dbSchool = _db.Schools.Find(entity.SchoolId);
            if (dbSchool == null)
            {
                throw new SismaException(SismaExceptionTypes.ObjectNotFound, "A school of the given id does not exist");
            }

            base.Create(entity, saveChanges);
        }

        public override void Update(Class entity, bool saveChanges = true)
        {
            var dbSchool = _db.Schools.Find(entity.SchoolId);
            if (dbSchool == null)
            {
                throw new SismaException(SismaExceptionTypes.ObjectNotFound, "A school of the given id does not exist");
            }

            base.Update(entity, saveChanges);
        }

        public override void Delete(Guid id, bool saveChanges = true)
        {
            var dbClass = GetInternal(id);
            if (dbClass.StudentInClasses.Count != 0)
            {
                throw new SismaException(SismaExceptionTypes.ObjectDependencyExists);
            }
            base.Delete(id, saveChanges);
        }
    }

    public class StudentRepository : Repository<Student>
    {
        protected override Student GetInternal(Guid id)
        {
            return _db.Students
                .Include(item => item.StudentInClasses)
                .FirstOrDefault(item => item.RefId == id);
        }

        public override void Create(Student entity, bool saveChanges = true)
        {
            //fix input::
            entity.Id = 0;
            entity.RefId = Guid.NewGuid();
            entity.RecordCreateDate = DateTime.Now;
            entity.IsActive = true;

            //var dbStudent = GetInternal(entity.RefId);
            //if (dbStudent != null)
            //{
            //    throw new SismaException(SismaExceptionTypes.ObjectAlreadyExists);
            //}

            var dbSchool = _db.Schools.Find(entity.SchoolId);
            if (dbSchool == null)
            {
                throw new SismaException(SismaExceptionTypes.ObjectNotFound, "A school of the given id does not exist");
            }

            base.Create(entity, saveChanges);
        }

        public override void Update(Student entity, bool saveChanges = true)
        {
            var dbSchool = _db.Schools.Find(entity.SchoolId);
            if (dbSchool == null)
            {
                throw new SismaException(SismaExceptionTypes.ObjectNotFound, "A school of the given id does not exist");
            }

            base.Update(entity, saveChanges);
        }

        public override void Delete(Guid id, bool saveChanges = true)
        {
            var dbClass = GetInternal(id);
            if (dbClass.StudentInClasses.Count != 0)
            {
                throw new SismaException(SismaExceptionTypes.ObjectDependencyExists);
            }
            base.Delete(id, saveChanges);
        }
    }

    public class StudentInClassRepository : Repository<StudentInClass>
    {
        public override void Create(StudentInClass entity, bool saveChanges = true)
        {
            //fix input::
            entity.Id = 0;
            entity.RefId = Guid.NewGuid();

            //var dbStudentInClass = GetInternal(entity.RefId);
            //if (dbStudentInClass != null)
            //{
            //    throw new SismaException(SismaExceptionTypes.ObjectAlreadyExists);
            //}

            var dbStudent = _db.Students.Find(entity.StudentId);
            var dbClass = _db.Classes.Find(entity.ClassId);

            if (dbStudent == null)
            {
                throw new SismaException(SismaExceptionTypes.ObjectNotFound, "The specified student does not exist in the system");
            }

            if (dbClass == null)
            {
                throw new SismaException(SismaExceptionTypes.ObjectNotFound, "The specified class does not exist in the system");
            }

            bool isExists = _db.StudentInClasses.Any(item => item.ClassId == entity.ClassId
                                                             && item.StudentId == entity.StudentId);
            if (isExists)
            {
                throw new SismaException(SismaExceptionTypes.ObjectAlreadyExists, $@"The specified student is already a part of the given class (studentId='{entity.StudentId}', classId='{entity.ClassId}'");
            }

            if (dbClass.SchoolId != dbStudent.SchoolId)
            {
                throw new SismaException(SismaExceptionTypes.StudentSchoolMismatch);
            }

            base.Create(entity, saveChanges);
        }

        public override void Update(StudentInClass entity, bool saveChanges = true)
        {
            var dbStudent = _db.Students.Find(entity.StudentId);
            var dbClass = _db.Classes.Find(entity.ClassId);

            if (dbClass.SchoolId != dbStudent.SchoolId)
            {
                throw new SismaException(SismaExceptionTypes.StudentSchoolMismatch);
            }

            base.Update(entity, saveChanges);
        }

        public override void Delete(Guid id, bool saveChanges = true)
        {
            base.Delete(id, saveChanges);
        }
    }

    public class Repository<T> : IRepository<T>
        where T : class, IDBEntity
    {
        protected SismaContext _db;

        public Repository()
        {
            _db = new SismaContextFactory().CreateDbContext(new string[0]);
        }


        public virtual List<T> GetAll()
        {
            return _db.Set<T>().Select(a => a).ToList();
        }

        public virtual T Get(Guid id)
        {
            return GetInternal(id);
        }

        protected virtual T GetInternal(Guid id)
        {
            return _db.Set<T>().FirstOrDefault(item => item.RefId == id);
        }

        public virtual void Create(T entity, bool saveChanges = true)
        {
            _db.Set<T>().Add(entity);
            if (saveChanges)
            {
                _db.SaveChanges();
            }
        }

        public virtual void Update(T entity, bool saveChanges = true)
        {
            _db.Entry<T>(entity).State = EntityState.Modified;
            if (saveChanges)
            {
                _db.SaveChanges();
            }
        }

        public virtual void Delete(Guid id, bool saveChanges = true)
        {
            var q = Get(id);
            _db.Set<T>().Remove(q);
            if (saveChanges)
            {
                _db.SaveChanges();
            }
        }

        public virtual bool Exists(Guid id)
        {
            var res = GetInternal(id);
            return res != null;
        }

        public virtual int SaveChanges()
        {
            return _db.SaveChanges();
        }


        public virtual void Dispose()
        {
            _db.Dispose();
            _db = null;
        }
    }

}