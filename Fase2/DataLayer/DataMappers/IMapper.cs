
namespace DataLayer.DataMappers
{
    public interface IMapper<T, Tid>
    {
        void Create(T entity);
        T ReadById(Tid id);
        void Update(T entity);
        void Delete(T entity);
        void CreateWithSP(T entity);
    }
}
