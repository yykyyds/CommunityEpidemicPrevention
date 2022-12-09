using IRepository;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        private void InitialDB()
        {
            //创建数据库(没有才创建)
            base.Context.DbMaintenance.CreateDatabase();
            //创建表
            base.Context.CodeFirst.InitTables(
                typeof(User),
                typeof(EntryRecord),
                typeof(OutRecord),
                typeof(EscalationInfo),
                typeof(PurchaseOrder),
                typeof(Goods),
                typeof(EpidemicInfo));
        }

        public BaseRepository()
        {
            base.Context = DbScoped.Sugar;//.GetConnection("1");
            InitialDB();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        public async Task<TEntity> FindAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public virtual async Task<List<TEntity>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await base.GetListAsync(expression);
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
                .ToPageListAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().Where(expression)
                .ToPageListAsync(page, size, total);
        }

        public async new Task<bool> UpdateAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }
    }
}
