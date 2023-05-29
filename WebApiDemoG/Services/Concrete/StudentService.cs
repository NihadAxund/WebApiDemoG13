using WebApiDemoG.Entities;
using WebApiDemoG.Repositories.Abstract;
using WebApiDemoG.Services.Abstract;

namespace WebApiDemoG.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Add(Student entity)
        {
            _studentRepository.Add(entity);
        }

        public void Delete(int id)
        {
            var item = _studentRepository.Get(id);
            _studentRepository.Delete(item);
        }

        public Student Get(int id)
        {
            return _studentRepository.Get(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public void Update(Student entity)
        {
            _studentRepository.Update(entity);
        }
    }
}
