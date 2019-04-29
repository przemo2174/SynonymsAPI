using System;
using System.Collections.Generic;
using System.Text;

namespace VimanetTask.Application.Dto
{
    public class SynonymsRecordDto
    {
        public string Term { get; set; }
        public List<string> Synonyms { get; set; }
    }
}
