namespace WebApiDemoG.Services.Asbtract
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
