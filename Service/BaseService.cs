using IRepository;
using IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //从子类传入
        protected IBaseRepository<TEntity> repository { get; set; } = default!;

        public async Task<bool> AddAsync(TEntity entity)
        {
            return await repository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<TEntity> FindAsync(string id)
        {
            return await repository.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> QueryAsync()
        {
            return await repository.QueryAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await repository.QueryAsync(expression);
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await repository.QueryAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression, int page, int size, RefAsync<int> total)
        {
            return await repository.QueryAsync(expression, page, size, total);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await repository.UpdateAsync(entity);
        }
    }
}
