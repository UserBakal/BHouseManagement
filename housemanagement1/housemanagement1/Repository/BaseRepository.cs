using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using housemanagement1.Contracts;

namespace housemanagement1.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class

    {
        private DbContet _db;
        private DbSet<T> _table;


        public BaseRepository()
        {
            _db = new BhouseManagementEntities();
            _table = _db.Set<T>();
        }
        public T Get(Object id)
        {
            return _table.Find(id);
        }
        public List<T> GetAll()
        {
            return _table.ToList();
        }


        public ErrorCode Create(T t)
        {
            try
            {
                _table.Add(t);
                _db.SaveChanges();
                return ErrorCode.success;

            }catch (Exception ex)
            {
                return ErrorCode.error;
            }
        }
        public ErrorCode Delete(T t)
        {
            try
            {
                var obj = Get(id);
                _table.Remove(obj);
                _db.SaveChanges();
                return ErrorCode.success;

            }
            catch (Exception ex)
            {
                return ErrorCode.error;
            }
        }
        public ErrorCode Update(T t)
        {
            try
            {
                var oldOjb = Get(id);
                _db.Entry(oldOjb).CurrentValues.SetValues(t);
                _db.SaveChanges();
                return ErrorCode.success;

            }
            catch (Exception ex)
            {
                return ErrorCode.error;
            }
        }


    }
}
