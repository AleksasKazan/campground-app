using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Read;
using Contracts.Models.Write;
using Persistence.Clients;

namespace Persistence.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private const string CommentsTable = "Comments";

        private readonly ISqlClient _sqlClient;

        public CommentsRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<int> SaveOrUpdateAsync(CommentWriteModel comment)
        {
            var sql = @$"INSERT INTO {CommentsTable} (Id, CampgroundId, Rating, Text, UserId, DateCreated)
                        VALUES(@Id, @CampgroundId, @Rating, @Text, @UserId, @DateCreated)
            ON DUPLICATE KEY UPDATE Rating = @Rating, Text=@Text";

            return _sqlClient.ExecuteAsync(sql, comment);
        }

        public Task<IEnumerable<CommentReadModel>> GetByCampgroundIdAsync(Guid campgroundId)
        {
            //var sql = $@"SELECT * FROM {CommentsTable}";
            var sql = $@"SELECT * FROM {CommentsTable} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.QueryAsync<CommentReadModel>(sql, new
            {
                CampgroundId = campgroundId
            });
        }

        public Task<CommentReadModel> GetByIdAsync(Guid campgroundId, string userId)
        {
            var sql = $@"SELECT * FROM {CommentsTable} WHERE CampgroundId = @CampgroundId AND UserId = @UserId";

            return _sqlClient.QuerySingleOrDefaultAsync<CommentReadModel>(sql, new
            {
                CampgroundId = campgroundId,
                UserId = userId
            });
        }

        public Task<int> DeleteAsync(Guid campgroundId)
        {
            var sql = $"DELETE FROM {CommentsTable} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.ExecuteAsync(sql, new
            {
                CampgroundId = campgroundId
            });
        }
    }
}
