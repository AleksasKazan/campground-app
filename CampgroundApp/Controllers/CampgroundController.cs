using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampgroundApp.Options;
using Contracts.Models.Request;
using Contracts.Models.Response;
using Contracts.Models.Write;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Repositories;

namespace CampgroundApp.Controllers
{
    [ApiController]
    [Route("campgrounds")]
    public class CampgroundController : ControllerBase
    {
        private readonly ICampgroundsRepository _campgroundsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly AppSettings _appSettings;
        public CampgroundController(IImagesRepository imagesRepository, ICommentsRepository commentsRepository, ICampgroundsRepository campgroundsRepository, IUsersRepository usersRepository, IOptions<AppSettings> appSettings)
        {
            _campgroundsRepository = campgroundsRepository;
            _usersRepository = usersRepository;
            _appSettings = appSettings.Value;
            _imagesRepository = imagesRepository;
            _commentsRepository = commentsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampgroundResponseModel>>> GetAll()
        {
            var campgrounds = await _campgroundsRepository.GetAll();
            var images = await _imagesRepository.GetAllAsync();

            var response = campgrounds.Select(campground => new CampgroundResponseModel
            {
                Id = campground.Id,
                UserId = campground.UserId,
                Name = campground.Name,
                Description = campground.Description,
                Price = campground.Price,
                Location = campground.Location,
                DateCreated = campground.DateCreated,
                DefaultImageUrl = images.FirstOrDefault(image => image.CampgroundId == campground.Id)?.Url
                //DefaultImageUrl = images.FirstOrDefault(image => image.CampgroundId == campground.Id).Url
            });

            return Ok(response);
        }

        [HttpGet]
        //[Authorize]
        [Route("{campgroundId}")]
        public async Task<ActionResult<CampgroundResponseModel>> Get(Guid campgroundId)
        {
            var campground = await _campgroundsRepository.Get(campgroundId);
            var comments = await _commentsRepository.GetByCampgroundIdAsync(campgroundId);
            var images = await _imagesRepository.GetByCampgroundIdAsync(campgroundId);

            var commentsResponse = comments.Select(comment => new CommentResponseModel
            {
                Id = comment.Id,
                UserId = comment.UserId,
                CampgroundId = comment.CampgroundId,
                Text = comment.Text,
                Rating = comment.Rating,
                DateCreated = comment.DateCreated
            });

            var imagesResponse = images.Select(image => new ImageResponseModel
            {
                Url = image.Url,
                ImagesId = image.Id
            });

            if (campground is null)
            {
                return NotFound($"Campground with id: '{campgroundId}' does not exist");
            }
            return Ok(new CampgroundResponseModel
            {
                Id = campground.Id,
                UserId = campground.UserId,
                Name = campground.Name,
                Description = campground.Description,
                Price = campground.Price,
                DateCreated = campground.DateCreated,
                Location = campground.Location,
                Comments = commentsResponse,
                Images = imagesResponse
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CampgroundResponseModel>> AddCampground([FromBody] CampgroundRequestModel request)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
            var campgruondId = Guid.NewGuid();
            await _campgroundsRepository.SaveOrUpdate(new CampgroundWriteModel
            {
                Id = campgruondId,
                UserId = userId,
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                DateCreated = DateTime.Now,
                Location = request.Location,
            });
            await _imagesRepository.CreateImageAsync(new ImageWriteModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CampgroundId = campgruondId,
                Url = request.Url,
                DateCreated = DateTime.Now
            });
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("{campgroundId}")]
        public async Task<ActionResult> DeleteCampground(Guid campgroundId)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
            var campground = await _campgroundsRepository.Get(campgroundId);

            if (campground is null)
            {
                return NotFound($"Campground with id: '{campgroundId}' does not exist");
            }
            await _campgroundsRepository.Delete(campgroundId);
            await _imagesRepository.DeleteByCampgroundIdAsync(campgroundId);
            await _commentsRepository.DeleteAsync(campgroundId);

            return NoContent();
        }

        [Authorize]
        [HttpPut]
        [Route("{campgroundId}")]
        public async Task<ActionResult<CampgroundResponseModel>> UpdateCampground([FromBody] CampgroundRequestModel request, Guid campgroundId)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
            var campground = await _campgroundsRepository.Get(campgroundId);
            var images = await _imagesRepository.GetByCampgroundIdAsync(campgroundId);
            var image = images.FirstOrDefault(image => image.CampgroundId == campground.Id);

            if (request is null)
            {
                return BadRequest();
            }
            if (campground is null)
            {
                return NotFound($"Campground with id: '{campgroundId}' does not exist");
            }

            await _campgroundsRepository.SaveOrUpdate(new CampgroundWriteModel
            {
                Id = campground.Id,
                UserId = userId,
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                DateCreated = campground.DateCreated,
                Location = request.Location                
            });
            await _imagesRepository.CreateImageAsync(new ImageWriteModel
            {
                Id = image.Id,
                CampgroundId = image.CampgroundId,
                DateCreated = image.DateCreated,
                UserId = userId,
                Url = request.Url
            });
            return Ok();
        }
    }
}
