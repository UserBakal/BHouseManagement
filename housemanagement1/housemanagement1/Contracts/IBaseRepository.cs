using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace housemanagement1.Contracts
{
    public enum ErrorCode
    {
        Success,
        Error
    }
    public interface IBaseRepository<T>
    {
        T Get(object id);
        List<T> GetAll();
        ErrorCode Create(T t);
        ErrorCode Update(Object id,T t);
        ErrorCode Delete(object id);

    }
}
