using WebApiDemoG.Data;
using WebApiDemoG.Entities;
using WebApiDemoG.Repositories.Abstract;

namespace WebApiDemoG.Repositories.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDBContext _dbContext;

        public StudentRepository(StudentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Student entity)
        {
            _dbContext.Students.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Student entity)
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }

        public Student Get(int id)
        {
            var student = _dbContext.Students.SingleOrDefault(x=>x.Id==id);
            return student;
        }

        public IEnumerable<Student> GetAll()
        {
            var students = _dbContext.Students;
            return students;
        }

        public void Update(Student entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
