<<<<<<<< HEAD:Shared/Dtos/FacultyMemberDataModule/ThesesResponseDTO.cs
﻿using Shared.Dtos;

namespace Shared.Enums.ResearchesModule
========
﻿using Shared.Enums.AcademicDataModule.HigherStudiesModule;

namespace Shared.Dtos.AcademicDataModule.HigherStudiesModule
>>>>>>>> Development:Shared/Dtos/AcademicDataModule/HigherStudiesModule/ThesesResponseDTO.cs
{
    public record ThesesResponseDTO
    {
        public int Id { get; set; }
        public ThesisType Type { get; set; }
        public string? Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public LookupItemDto Grade { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }

        //public List<Reseat MyProperty { get; set; }


    }
}
