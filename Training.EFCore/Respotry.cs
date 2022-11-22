using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.EFCore.Context;

namespace Training.EFCore
{
    public class Respotry<T> : IRespotry<T> where T : class
    {

        public SqlContext sqlContext;
        public Respotry(SqlContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            sqlContext.Set<T>().Add(obj);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        public void Del(T obj)
        {
            sqlContext.Set<T>().Remove(obj);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj"></param>
        public void Upt(T obj)
        {
            sqlContext.Set<T>().Update(obj);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Save()
        {
            return sqlContext.SaveChanges();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetList()
        {
            return sqlContext.Set<T>().AsQueryable();
        }
        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(object id)
        {
            return sqlContext.Set<T>().Find(id);
        }
    }
}
