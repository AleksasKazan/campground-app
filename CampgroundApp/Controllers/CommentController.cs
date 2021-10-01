using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models.Request;
using Contracts.Models.Response;
using Contracts.Models.Write;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace CampgroundApp.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICampgroundsRepository _campgroundsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ICommentsRepository _commentsRepository;
        public CommentController(ICommentsRepository commentsRepository, ICampgroundsRepository campgroundsRepository, IUsersRepository usersRepository)
        {
            _campgroundsRepository = campgroundsRepository;
            _usersRepository = usersRepository;
            _commentsRepository = commentsRepository;
        }

        [Authorize]
        [HttpDelete]
        [Route("{commentId}")]
        public async Task<ActionResult> DeleteComment(Guid commentId)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var campground = await _campgroundsRepository.Get(commentId);

            //if (campground.UserId != userId)
            //{
            //    return Unauthorized($"Campground with id: '{commentId}' does not belong to user: {userId}");
            //}

            await _commentsRepository.DeleteAsync(commentId);

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("{campgroundId}")]
        public async Task<ActionResult<CommentResponseModel>> AddComment(CommentRequestModel request, Guid campgroundId)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var campground = await _commentsRepository.GetByIdAsync(campgroundId, userId);

            if (campground.UserId == userId)
            {
                return BadRequest($"You already commented: '{campground.Text}' and rated: {campground.Rating} this campground");
            }
            await _commentsRepository.SaveOrUpdateAsync(new CommentWriteModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CampgroundId = campgroundId,
                Rating = request.Rating,
                Text = request.Text,
                DateCreated = DateTime.Now,
            });

            return Ok();
        }
    }
}
