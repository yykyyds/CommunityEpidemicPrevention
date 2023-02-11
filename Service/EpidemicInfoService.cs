using IRepository;
using IService;
using Models.Models;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EpidemicInfoService : BaseService<EpidemicInfo>, IEpidemicInfoService
    {
        private readonly IEpidemicInfoRepository epidemicInfoRepository;

        public EpidemicInfoService(IEpidemicInfoRepository epidemicInfoRepository)
        {
            base.repository = epidemicInfoRepository;
            this.epidemicInfoRepository = epidemicInfoRepository;
        }

        public new async Task<bool> DeleteAsync(string ids)
        {
            string[] Ids;
            List<int> Ids1 = new List<int>();
            Ids = ids.Split(',');
            for (int i = 0; i < Ids.Length; i++)
                if (Ids[i] != "")
                    Ids1.Add(Int32.Parse(Ids[i]));
            return await epidemicInfoRepository.DeleteAsync(Ids1.ToArray());
        }
    }
}
