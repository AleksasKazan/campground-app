using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Read;
using Contracts.Models.Write;

namespace Persistence.Repositories
{
    public interface IImagesRepository
    {
        Task<IEnumerable<ImageReadModel>> GetAllAsync();

        Task<IEnumerable<ImageReadModel>> GetByCampgroundIdAsync(Guid campgroundId);

        Task<int> CreateImageAsync(ImageWriteModel image);

        Task<int> DeleteByIdAsync(Guid Id);

        Task<int> DeleteByCampgroundIdAsync(Guid campgroundId);
    }
}
