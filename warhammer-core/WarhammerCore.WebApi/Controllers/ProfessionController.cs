using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models;
using WarhammerCore.WebApi.Models.Request;

namespace WarhammerCore.WebApi.Controllers
{
    public class ProfessionController : ApiControllerBase
    {
        private readonly IProfessionService _professionService;

        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService ?? throw new ArgumentNullException(nameof(professionService));
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetProfessions()
        {
            IEnumerable<string> professions = await _professionService.GetProfessionsAsync();
            return Ok(professions);
        }

        [HttpPost]
        public async Task<ActionResult<Profession>> GetProfession(GetProfessionRequest request)
        {
            return await _professionService.GetProfessionAsync(request.ProfessionId);
        }
    }
}
