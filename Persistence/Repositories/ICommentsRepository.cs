using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Read;
using Contracts.Models.Write;

namespace Persistence.Repositories
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<CommentReadModel>> GetByCampgroundIdAsync(Guid campgroundId);

        Task<CommentReadModel> GetByIdAsync(Guid campgroundId, string userId);

        Task<int> SaveOrUpdateAsync(CommentWriteModel comment);

        //Task<CommentReadModel> GetComment(string userName, string password);

        Task<int> DeleteAsync(Guid campgroundId);
    }
}
