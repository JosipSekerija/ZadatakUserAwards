using EvonaZadatak.Contracts;
using EvonaZadatak.Dto;
using EvonaZadatak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvonaZadatak.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IActionResult> Show()
        {
            var users = await _userRepo.GetUsers();
            if (users == null)
            {
                users = new List<User>(); 
            }
            return View(users);
        }






        public IActionResult Create()
        {
            return View();
        }


        [HttpGet("User/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }



        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UserForCreationDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdUser = await _userRepo.CreateUser(user);

            return RedirectToAction("Show");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserForUpdateDto user)

        {
            var dbUser = await _userRepo.GetUser(id);
            if (dbUser is null)
                return NotFound();

            await _userRepo.UpdateUser(id, user);

            return RedirectToAction("Show");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var dbUser = await _userRepo.GetUser(id);
            if (dbUser is null)
                return NotFound();

            await _userRepo.DeleteUser(id);

            return RedirectToAction("Show");
        }
    }
}
