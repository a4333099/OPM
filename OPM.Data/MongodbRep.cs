using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OPM.Core;
using MongoDB;
using OPM.Core.Config;

namespace OPM.Data
{
    public class MongodbRep<T>: IRepository<T> where T:class
    {
        public MongodbRep(OPMConfig config)
        {
                
        }

        private string _connectionString;
        private string _dbName;
        public  void Insert(T value)
        {
            // type.Id = Guid.NewGuid().ToString("N");

            // 首先创建一个连接
            using (Mongo mongo = new Mongo(_connectionString))
            {

                // 打开连接
                mongo.Connect();

                // 切换到指定的数据库
                var db = mongo.GetDatabase(_dbName);

                // 根据类型获取相应的集合
                var collection = db.GetCollection<T>();

                // 向集合中插入对象
                collection.Insert(value);
            }
        }

        public void Delete(Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 从集合中删除指定的对象
                collection.Remove(wherelambada);
            }
        }

        public void Update(T value, Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 更新对象
                collection.Update(value, wherelambada);
            }
        }

        public T GetEntity(Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 查询单个对象
                // return collection.FindOne(wherelambada);
                return collection.FindOne<T>(wherelambada);
            }
        }

        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 查询单个对象
                return collection.Linq().Where(wherelambada).ToList();
            }
        }
    }
}
