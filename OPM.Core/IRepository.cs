using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Core
{
    public interface IRepository<T> where T : class
    {
        void Insert(T value);
        void Delete(Expression<Func<T, bool>> wherelambada);

        void Update(T value, Expression<Func<T, bool>> wherelambada);

        T GetEntity(Expression<Func<T, bool>> wherelambada);

        IEnumerable<T> GetEntities(Expression<Func<T, bool>> wherelambada);



    }
}
