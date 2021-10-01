using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Read;
using Contracts.Models.Response;
using Contracts.Models.Write;

namespace Persistence.Repositories
{
    public interface ICampgroundsRepository
    {
        Task<IEnumerable<CampgroundReadModel>> GetAll();

        Task <CampgroundReadModel> Get(Guid campgroundId);

        Task <int> SaveOrUpdate(CampgroundWriteModel campground);

        Task <int>Delete(Guid id);
    }
}
