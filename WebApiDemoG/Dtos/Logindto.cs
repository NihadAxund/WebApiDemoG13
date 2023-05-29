using WebApiDemoG.Entities;

namespace WebApiDemoG.Dtos
{
    public class SiginDto
    {


        public string Name { get; set; }
        public string Surname { get; set; }
        public string SeriaNo { get; set; }
        public int Age { get; set; }
        public string password { get; set; }
        public bool Check() => Name.Length > 1 && Surname.Length > 2 && SeriaNo.Length != 0&&Age>4&&password.Length>3;

        public Student ReturnStudent()
        {
            if (Check())
            {
                var student = new Student(Name, Surname, SeriaNo,Age,password);
                return student;
            }
            return null;
        }

    }
}
