using EvonaZadatak.Contracts;
using EvonaZadatak.Dto;
using EvonaZadatak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvonaZadatak.Controllers
{
    [Route("awards")]
    public class AwardController : Controller
    {

        private readonly IAwardRepository _awardRepo;
        

       

        public AwardController(IAwardRepository awardRepo) 
        {
           _awardRepo = awardRepo;
        }
        [HttpGet("/show")]
        public async Task<IActionResult> Show()
        {
            var awards = await _awardRepo.GetAwards();
            if (awards == null)
            {
                awards = new List<Award>(); // Ensure we pass an empty list if no awards are found
            }
            return View(awards);
        }
        [HttpGet("/create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet("Award/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var award = await _awardRepo.GetAward(id);
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }



        [HttpGet]
        public async Task<IActionResult> GetAwards()
        { 
            var awards = await _awardRepo.GetAwards();
            return Ok(awards);
        }

        
        


        [HttpGet("{id}",Name ="AwardById")]
        public async Task <IActionResult> GetAward(int id)
        {
            var award = await _awardRepo.GetAward(id);
            if(award == null)
            {
                return NotFound();
            }
            return Ok(award);

        }

        [HttpPost("create")]
        public async Task <IActionResult> CreateAward([FromForm]AwardForCreationDto award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdAward = await _awardRepo.CreateAward(award);

            return RedirectToAction("Show");
        }


        [HttpPost("uodate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAward(int id, [FromForm] AwardForUpdateDto award)

        {
            var dbAward = await _awardRepo.GetAward(id);
            if(dbAward is null)
                return NotFound();

            await _awardRepo.UpdateAward(id, award);

            return RedirectToAction("Show"); 

        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAward(int id)
        {
            var dbAward = await _awardRepo.GetAward(id);
            if (dbAward is null)
                return NotFound();

            await _awardRepo.DeleteAward(id);

            return RedirectToAction("Show");
        }

    }

    
    
}
