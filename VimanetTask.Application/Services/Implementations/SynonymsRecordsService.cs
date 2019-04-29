using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimanetTask.Application.Dto;
using VimanetTask.Application.Services.Interfaces;
using VimanetTask.Entities;
using VimanetTask.Persistence;

namespace VimanetTask.Application.Services.Implementations
{
    public class SynonymsRecordsService : ISynonymsRecordsService
    {
        private readonly AppDbContext _dbContext;
        public SynonymsRecordsService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }       

        public async Task<IEnumerable<SynonymsRecordDto>> GetSynonymsRecordsAsync()
        {
            var synonymsRelationsDict = new Dictionary<string, List<string>>();

            var termDictionary = await _dbContext.SynonymsRecords
                .GroupBy(x => x.Term)
                .ToDictionaryAsync(x => x.Key, x => x.Select(s => s.Synonyms)
                        .Aggregate("", (total, next) => total + next + ',')
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Distinct()
                        .ToList()
                );

            //Basic relation Term -> Synonyms
            foreach (var dict in termDictionary)
            {
                synonymsRelationsDict.Add(dict.Key, dict.Value);
            }

            //Reverse relation synonym -> term
            foreach (var dict in termDictionary)
            {
                var synonymsForTerm = dict.Value;

                foreach (var synonym in synonymsForTerm)
                {
                    if (!synonymsRelationsDict.ContainsKey(synonym))
                    {
                        //Create new key in dictionary and populate it with parent term
                        synonymsRelationsDict.Add(synonym, new List<string> { dict.Key });
                    }
                    else
                    {
                        if (!synonymsRelationsDict[synonym].Contains(dict.Key))
                        {
                            synonymsRelationsDict[synonym].Add(dict.Key);
                        }
                    }
                }
            }

            var termSynonymsList = synonymsRelationsDict.Select(x => new SynonymsRecordDto
            {
                Term = x.Key,
                Synonyms = x.Value
            })
            .ToList();

            return termSynonymsList;
        }

        public async Task AddSynonymsRecordAsync(SynonymsRecordForCreationDto synonymsRecordForCreation)
        {
            //Remove unncessary spaces and convert to lower
            var synonyms = string.Join(',', synonymsRecordForCreation.Synonyms
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower()));

            var synonymsRecord = new SynonymsRecord(synonymsRecordForCreation.Term, synonyms);

            await _dbContext.AddAsync(synonymsRecord);

            await _dbContext.SaveChangesAsync();
        }
    }
}
