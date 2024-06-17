using EvonaZadatak.Contracts;
using EvonaZadatak.Dto;
using EvonaZadatak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvonaZadatak.Controllers
{
    public class UserAwardController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IUserAward _userAwardRepository;
        public UserAwardController(IUserRepository userRepository, IAwardRepository awardRepository, IUserAward userAwardRepository)
        {
            _userRepository = userRepository;
            _awardRepository = awardRepository;
            _userAwardRepository = userAwardRepository;
        }

        [HttpGet("UserAward/Index")]
        public async Task<IActionResult> Index(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            var awards = await _awardRepository.GetAwards();

            var viewModel = new UserAwardViewModel
            {
                UserId = userId,
                UserName = $"{user.FirstName} {user.LastName}",
                Awards = awards.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
            };
            return View(viewModel);
        }
        [HttpGet("UserAward/Search")]
        public async Task<IActionResult> Search(int userId)
        {
            Console.WriteLine($"Received userId: {userId}");
            ViewBag.UserId = userId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAward(UserAwardViewModel model)
        {
            await _userAwardRepository.CreateUserAward(new UserAwardForCreationDto
            {
                UserId = model.UserId,
                AwardId = model.SelectedAwardId
            });
            return RedirectToAction("Show", "User");
        }

        [HttpGet("UserAward/SearchByDate")]

        public async Task<IActionResult> SearchUserAwards(int userId, DateTime searchDate)
        {
            var userAwards = await _userAwardRepository.SearchUserAwards(userId, searchDate);

            var viewModel = new UserAwardSearchDto
            {
                SearchDate = searchDate,
                UserAwards = userAwards
            };
            if (searchDate != null)
            {
                viewModel.UserAwards = await _userAwardRepository.SearchUserAwards(userId, searchDate);
            }
            return View("Search", viewModel);
        }




    }
    }
