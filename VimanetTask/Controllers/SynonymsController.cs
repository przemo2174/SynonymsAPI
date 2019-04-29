using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VimanetTask.Application.Dto;
using VimanetTask.Application.Services.Interfaces;

namespace VimanetTask.Controllers
{
    [Route("api/synonyms")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class SynonymsController : ControllerBase
    {
        private readonly ISynonymsRecordsService _synonymsRecordsService;
        public SynonymsController(ISynonymsRecordsService synonymsRecordsService)
        {
            _synonymsRecordsService = synonymsRecordsService ?? throw new ArgumentNullException(nameof(synonymsRecordsService));
        }

        [HttpGet]
        public async Task<IActionResult> GetSynonymsRecord()
        {
            var synonyms = await _synonymsRecordsService.GetSynonymsRecordsAsync();
            return Ok(synonyms);
        }

        [HttpPost]
        public async Task<IActionResult> AddSynonymsRecord(SynonymsRecordForCreationDto synonymsRecordForCreation)
        {
            await _synonymsRecordsService.AddSynonymsRecordAsync(synonymsRecordForCreation);

            return NoContent();
        }
    }
}