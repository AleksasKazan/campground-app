using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Read;
using Contracts.Models.Write;
using Persistence.Clients;

namespace Persistence.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private const string ImagesTable = "Images";
        private readonly ISqlClient _sqlClient;

        public ImagesRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<ImageReadModel>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {ImagesTable}";

            return _sqlClient.QueryAsync<ImageReadModel>(sql);
        }

        public Task<IEnumerable<ImageReadModel>> GetByCampgroundIdAsync(Guid campgroundId)
        {
            var sql = $"SELECT * FROM {ImagesTable} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.QueryAsync<ImageReadModel>(sql, new
            {
                CampgroundId = campgroundId
            });
        }

        public Task<int> CreateImageAsync(ImageWriteModel image)
        {
            var sql = @$"INSERT INTO {ImagesTable} (Id, CampgroundId, Url, UserId, DateCreated)
                        VALUES(@Id, @CampgroundId, @Url, @UserId, @DateCreated)
            ON DUPLICATE KEY UPDATE Url=@Url";

            return _sqlClient.ExecuteAsync(sql, image);
        }      
        
        public Task<int> DeleteByCampgroundIdAsync(Guid campgroundId)
        {
            var sql = $"DELETE FROM {ImagesTable} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.ExecuteAsync(sql, new
            {
                CampgroundId = campgroundId
            });
        }

        public Task<int> DeleteByIdAsync(Guid id)
        {
            var sql = $"DELETE FROM {ImagesTable} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new
            {
                Id = id
            });
        }
    }
}
