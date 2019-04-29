using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VimanetTask.Entities;

namespace VimanetTask.Persistence.Configurations
{
    public class SynonymsRecordConfiguration : IEntityTypeConfiguration<SynonymsRecord>
    {
        public void Configure(EntityTypeBuilder<SynonymsRecord> builder)
        {
            builder.Property(x => x.Term).IsRequired();
            builder.Property(x => x.Synonyms).IsRequired();
        }
    }
}
