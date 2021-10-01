using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Read;
using Contracts.Models.Write;
using Persistence.Clients;

namespace Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private const string UsersTable = "Users";
        private readonly ISqlClient _sqlClient;

        public UsersRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<UserReadModel>> GetAllUsers()
        {
            var sql = $"SELECT * FROM {UsersTable}";
            return _sqlClient.QueryAsync<UserReadModel>(sql);
        }

        public Task<UserReadModel> GetUserByFirebaseId(string firebaseId)
        {
            var sql = $"SELECT * FROM {UsersTable} WHERE FirebaseId = @firebaseId";
            return _sqlClient.QuerySingleOrDefaultAsync<UserReadModel>(sql, new
            {
                firebaseId = firebaseId
            });
        }

        public Task<UserReadModel> GetUser(string userName, string password)
        {
            var sql = @$"SELECT * FROM {UsersTable} WHERE UserName = @UserName AND Password = @Password";

            return _sqlClient.QuerySingleOrDefaultAsync<UserReadModel>(sql, new
            {
                UserName = userName,
                Password = password
            });
        }

        public Task<UserReadModel> GetUserByName(string userName)
        {
            var sql = @$"SELECT * FROM {UsersTable} WHERE UserName = @UserName";

            return _sqlClient.QuerySingleOrDefaultAsync<UserReadModel>(sql, new
            {
                UserName = userName
            });
        }

        public Task<int> CreateUser(UserWriteModel user)
        {
            var sql = @$"INSERT INTO {UsersTable} (Id, FirebaseId, Email, DateCreated, UserName)
                        VALUES(@Id, @FirebaseId, @Email, @DateCreated, @UserName)
            ON DUPLICATE KEY UPDATE Email = @Email";

            return _sqlClient.ExecuteAsync(sql, user);

            //return _sqlClient.ExecuteAsync(sql, new UserReadModel
            //{
            //    Id = user.Id,
            //    FirebaseId = user.FirebaseId,
            //    Email = user.Email,
            //    DateCreated = user.DateCreated,
            //    UserName = user.UserName
            //});
        }

        public Task<int> DeleteUser(string email)
        {
            var sql = $"DELETE FROM {UsersTable} WHERE Email = @Email";

            return _sqlClient.ExecuteAsync(sql, new
            {
                Email = email
            });
        }
    }
}
