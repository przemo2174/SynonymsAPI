using System;

namespace VimanetTask.Entities
{
    public class SynonymsRecord
    {
        public int Id { get; protected set; }
        public string Term { get; protected set; }
        public string Synonyms { get; protected set; }

        protected SynonymsRecord()
        {

        }

        public SynonymsRecord(string term, string synonyms)
        {
            //ToDo implement validation

            Term = term;
            Synonyms = synonyms;
        }
    }
}
