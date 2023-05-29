namespace WebApiDemoG.Entities
{
    public class Student
    {
        public Student() { }

        public Student(string name, string surname, string seriaNo, int age, string password)
        {
            Username = name;
            Fullname = name + " " + surname;
            SeriaNo = seriaNo;
            Age = age;
            Password = password;
        }

       
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string SeriaNo { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
