using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VimanetTask.Application.Dto;

namespace SynonymsManagement.Application.DtoValidators
{
    public class SynonymsRecordForCreationDtoValidator : AbstractValidator<SynonymsRecordForCreationDto>
    {
        public SynonymsRecordForCreationDtoValidator()
        {
            RuleFor(x => x.Term)
                .NotEmpty();

            RuleFor(x => x.Synonyms)
                .NotEmpty();
        }
    }
}
