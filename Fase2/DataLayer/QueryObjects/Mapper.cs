using DataLayer.DataMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.QueryObjects
{
    public abstract class Mapper
    {
        protected ISession session;


        protected Mapper(ISession s)
        {
            this.session = s;
        }
    }
}
