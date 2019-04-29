using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VimanetTask.Application.Dto;
using VimanetTask.Entities;

namespace VimanetTask.Application.Services.Interfaces
{
    public interface ISynonymsRecordsService
    {
        Task<IEnumerable<SynonymsRecordDto>> GetSynonymsRecordsAsync();
        Task AddSynonymsRecordAsync(SynonymsRecordForCreationDto synonymsRecordForCreation);
    }
}
