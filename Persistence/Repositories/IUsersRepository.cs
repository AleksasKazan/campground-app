﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Models.Read;
using Contracts.Models.Write;

namespace Persistence.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserReadModel>> GetAllUsers();

        Task<UserReadModel> GetUserByFirebaseId(string firebaseId);

        Task<int> CreateUser(UserWriteModel user);

        Task<int> DeleteUser(string email);

        Task<UserReadModel> GetUser(string userName, string password);

        Task<UserReadModel> GetUserByName(string userName);
    }
}
