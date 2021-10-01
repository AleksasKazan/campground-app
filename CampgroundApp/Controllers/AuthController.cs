using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using Contracts.Models.Request;
using Contracts.Models.Write;
using Contracts.Models.Response;
using Microsoft.AspNetCore.Authorization;
using CampgroundApp.FireBaseModels;

namespace CampgroundApp.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IFireBaseClient _fireBaseClient;
        public AuthController(IUsersRepository usersRepository, IFireBaseClient fireBaseClient)
        {
            _usersRepository = usersRepository;
            _fireBaseClient = fireBaseClient;
        }
        [HttpPost]
        [Route("signUp")]
        public async Task<ActionResult<UserSignUpResponseModel>> SignUp(UserSignUpRequestModel request)
        {
            var user = await _fireBaseClient.SignUp(request);
            
            if (user.LocalId is null)
            {
                //throw new Exception($"The email address {user.Email} is incorect or already in use by another account or password is too short");
                return BadRequest(new ErrorResponse
                {
                    error = user.error
                });
            }
            var newUser = new UserWriteModel
            {
                Id = Guid.NewGuid(),
                FirebaseId = user.LocalId,
                Email = user.Email, 
                DateCreated = DateTime.Now,
                UserName = request.UserName,
            };
            await _usersRepository.CreateUser(newUser);
            return Ok(new UserSignUpResponseModel
            {
                Id = newUser.Id,
                FirebaseId = newUser.FirebaseId,
                Email = newUser.Email,
                DateCreated = newUser.DateCreated,
                UserName = newUser.UserName,
                IdToken = newUser.IdToken,
                RefreshToken = newUser.RefreshToken
            });
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<UserSignInResponseModel>> SignIn(UserSignInRequestModel request)
        {
            var user = await _fireBaseClient.SignIn(request);
            if (user.Email is null)
            {
                throw new Exception($"The email address {user.Email} or the password is invalid");
            }

            var userSql = await _usersRepository.GetUserByFirebaseId(user.LocalId);
            return new UserSignInResponseModel
            {
                UserName = userSql.UserName,
                FirebaseId = user.LocalId,
                IdToken = user.IdToken,
                Email = user.Email
            };
        }
        [HttpPost]
        //[Authorize]
        [Route("deleteAccount")]
        public async Task<ErrorResponse> DeleteAccount(UserSignInRequestModel request)
        {
            //var userId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
            //HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _fireBaseClient.SignIn(request);
            var response2 = await _fireBaseClient.DeleteAccount(response.IdToken);
            if (response.IdToken is not null)
            {
                await _usersRepository.DeleteUser(request.Email);
            }
            //return Ok();
            return new ErrorResponse
            {
                error = response2.error
            };
        }

        [HttpPost]
        //[Authorize]
        [Route("passwordReset")]
        public async Task<PasswordResetResponseModel> PasswordReset(PasswordResetRequestModel request)
        {
            var response = await _fireBaseClient.PasswordReset(request);

            if (response.Email is null)
            {
                throw new Exception($"The email address {request.Email} was not found");
            }
            return new PasswordResetResponseModel
            {
                Email = response.Email
            };
        }

        [HttpPost]
        //[Authorize]
        [Route("changeEmail")]
        public async Task<ChangeEmailResponseModel> ChangeEmail(UserSignInRequestModel request, string newEmail)
        {
            var response = await _fireBaseClient.SignIn(request);
            var response2 = await _fireBaseClient.ChangeEmail(newEmail, response.IdToken);

            if (response.Email is null)
            {
                throw new Exception($"The email address {request.Email} is already in use by another account");
            }
            var updatedUser = new UserWriteModel
            {
                FirebaseId = response2.LocalId,
                Email = newEmail,
                UserName = newEmail
            };
            await _usersRepository.CreateUser(updatedUser);

            return new ChangeEmailResponseModel
            {
                Email = response.Email
            };
        }

        [HttpPost]
        //[Authorize]
        [Route("changePassword")]
        public async Task<ChangePasswordResponseModel> ChangePassword(UserSignInRequestModel request, string newPassword)
        {
            var response = await _fireBaseClient.SignIn(request);
            var response2 = await _fireBaseClient.ChangePassword(newPassword, response.IdToken);

            if (response.Email is null)
            {
                throw new Exception($"The password {request.Password} is incorect");
            }
            return new ChangePasswordResponseModel
            {
                Email = response.Email
            };
            //return new ErrorResponse
            //{
            //    error = response2.error
            //};
        }
    }
}
