
namespace DataLayer.DataMappers
{
    public interface IMapper<T, Tid>
    {
        void Create(T entity);
        T ReadById(Tid id);
        void Update(T entity);
        void Delete(Tid id);
        void CreateWithSP(T entity);
    }
}
