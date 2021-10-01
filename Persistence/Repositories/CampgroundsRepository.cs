using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models.Read;
using Contracts.Models.Write;
using Persistence.Clients;

namespace Persistence.Repositories
{
    public class CampgroundsRepository : ICampgroundsRepository
    {
        private const string CampgroundsTable = "Campgrounds";
        private const string ImagesTable = "Images";
        private readonly ISqlClient _sqlClient;

        public CampgroundsRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<CampgroundReadModel>> GetAll()
        {
            var sql = $"SELECT * FROM {CampgroundsTable} ORDER BY DateCreated DESC";
            return _sqlClient.QueryAsync<CampgroundReadModel>(sql);
        }

        public Task<int> SaveOrUpdate(CampgroundWriteModel campground)
        {
            var sql = @$"INSERT INTO {CampgroundsTable} (Id, UserId, Name, Price, Description, DateCreated, Location)
                        VALUES(@Id, @UserId, @Name, @Price, @Description, @DateCreated, @Location)
            ON DUPLICATE KEY UPDATE Name=@Name, Price=@Price, Description=@Description, Location=@Location";

            return _sqlClient.ExecuteAsync(sql, campground);
        }       

        public Task<int> Delete(Guid id)
        {
            var sql = $"DELETE FROM {CampgroundsTable} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new
            {
                Id = id
            });
        }

        public Task<CampgroundReadModel> Get(Guid campgroundId)
        {
            var sql = $"SELECT * FROM {CampgroundsTable} WHERE Id = @CampgroundId";
            return _sqlClient.QuerySingleOrDefaultAsync<CampgroundReadModel>(sql, new
            {
                CampgroundId = campgroundId
            });
        }
    }
}
