using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IEpidemicInfoRepository : IBaseRepository<EpidemicInfo>
    {
        public Task<bool> DeleteAsync(int[] Ids);
    }
}
