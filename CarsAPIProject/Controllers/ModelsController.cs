using CarsAPIProject.Interfaces;
using CarsAPIProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPIProject.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ICarModelService _ICarModelService;
        public ModelsController(ICarModelService oICarModelService)
        {
            _ICarModelService = oICarModelService ?? throw new ArgumentNullException(nameof(oICarModelService));
        }

        #region Methods :: GetModels
        [HttpGet("GetModels")]
        public async Task<ActionResult<string[]>> GetModels([FromQuery] string Make, [FromQuery] int Modelyear)
        {
            var sArrModels = await _ICarModelService.GetModelsForMakeAndYear(Make, Modelyear);

            if (sArrModels != null && sArrModels.Length > 0)
            {
                return Ok(new { Models = sArrModels });
            }
            else
            {
                return NotFound();
            }
        }
        #endregion
    }
}
