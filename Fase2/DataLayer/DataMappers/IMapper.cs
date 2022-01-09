using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataMappers
{
    interface IMapper<T, Tid>
    {
        void Create(T entity);
        T ReadById(Tid id);
        void Update(T entity);
        void Delete(T entity);
    }
}
