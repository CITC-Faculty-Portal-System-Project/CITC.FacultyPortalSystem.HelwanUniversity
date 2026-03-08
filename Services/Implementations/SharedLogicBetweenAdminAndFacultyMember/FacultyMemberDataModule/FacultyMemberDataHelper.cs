using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.FacultyMemberDataModule;
using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.FacultyMemberDataModule
{
    public class FacultyMemberDataHelper(
     IUnitOfWork unitOfWork,
     IMapper mapper)
     : IFacultyMemberDataHelper
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        #region Repositories
        private IGenericRepository<FacultyMember, Guid> FacultyMemberRepo
            => _unitOfWork.GetRepository<FacultyMember, Guid>();

        private IGenericRepository<PersonalData, int> PersonalDataRepo
            => _unitOfWork.GetRepository<PersonalData, int>();

        private IGenericRepository<ContactData, int> ContactDataRepo
            => _unitOfWork.GetRepository<ContactData, int>();

        private IGenericRepository<IdentificationCard, int> IdentificationCardRepo
            => _unitOfWork.GetRepository<IdentificationCard, int>();

        private IGenericRepository<SocialMediaPlatforms, int> SocialMediaPlatformsRepo
            => _unitOfWork.GetRepository<SocialMediaPlatforms, int>();
        #endregion

        #region Private Helpers
        private async Task<FacultyMember> GetFacultyMemberByEmailAsync(string facultyMemberEmail)
        {
            var facultyMember = await FacultyMemberRepo.GetAsync(
                new FacultyMemberWithEmailSpecifications(facultyMemberEmail))
                ?? throw new NotFoundException("Faculty Member is Not Found.");

            return facultyMember;
        }
        #endregion

        #region Personal Data
        public async Task<PersonalDataResponseDto?> GetPersonalDataAsync(string facultyMemberEmail)
        {
            var personalData = await PersonalDataRepo.GetAsync(
                new PersonalDataWithIncludesSpecifications(facultyMemberEmail))
                ?? throw new NotFoundException("Personal Data is Not Found.");

            var result = _mapper.Map<PersonalDataResponseDto>(personalData);
            result.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            return result;
        }

        public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(
            PersonalDataUpdateDto dto,
            string facultyMemberEmail)
        {
            var personalData = await PersonalDataRepo.GetAsync(
                new PersonalDataWithIncludesSpecifications(facultyMemberEmail))
                ?? throw new NotFoundException("Personal Data is Not Found.");

            _mapper.Map(dto, personalData);

            PersonalDataRepo.Update(personalData);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<PersonalDataResponseDto>(personalData);
            result.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            return result;
        }
        #endregion

        #region Contact Data
        public async Task<ContactDataResponseDto?> GetContactDataAsync(string facultyMemberEmail)
        {
            var contactData = await ContactDataRepo.GetAsync(
                new ContactDataWithFacultyMemberEmailSpecifications(facultyMemberEmail))
                ?? throw new NotFoundException("Contact Data is Not Found.");

            return _mapper.Map<ContactDataResponseDto>(contactData);
        }

        public async Task<ContactDataResponseDto?> UpdateContactDataAsync(
            ContactDataUpdateDto dto,
            string facultyMemberEmail)
        {
            var contactData = await ContactDataRepo.GetAsync(
                new ContactDataWithFacultyMemberEmailSpecifications(facultyMemberEmail))
                ?? throw new NotFoundException("Contact Data is Not Found.");

            _mapper.Map(dto, contactData);

            ContactDataRepo.Update(contactData);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ContactDataResponseDto>(contactData);
        }
        #endregion

        #region Identification Card
        public async Task<IdentificationCardDto> GetIdentificationCardAsync(string facultyMemberEmail)
        {
            var identificationCard = await IdentificationCardRepo.GetAsync(
                new IdentificationCardWithFacultyMemberEmailSpecifications(facultyMemberEmail));

            if (identificationCard is not null)
                return _mapper.Map<IdentificationCardDto>(identificationCard);

            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var newCard = new IdentificationCard
            {
                FacultyMemberId = facultyMember.Id,
                ORCID = null,
                EKB = null,
                ResearcherId = null,
                ResearcherGate = null,
                AcademiaEdu = null
            };

            await IdentificationCardRepo.AddAsync(newCard);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<IdentificationCardDto>(newCard);
        }

        public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(
            IdentificationCardDto dto,
            string facultyMemberEmail)
        {
            var identificationCard = await IdentificationCardRepo.GetAsync(
                new IdentificationCardWithFacultyMemberEmailSpecifications(facultyMemberEmail))
                ?? throw new NotFoundException("Identification Card is Not Found.");

            _mapper.Map(dto, identificationCard);

            IdentificationCardRepo.Update(identificationCard);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<IdentificationCardDto>(identificationCard);
        }
        #endregion

        #region Social Media
        public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync(string facultyMemberEmail)
        {
            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(
                new SocialMediaWithFacultyMemberEmailSpecifications(facultyMemberEmail));

            if (socialMediaPlatforms is not null)
                return _mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);

            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var newSocialMediaPlatforms = new SocialMediaPlatforms
            {
                FacultyMemberId = facultyMember.Id,
                LinkedIn = null,
                Instagram = null,
                PersonalWebsite = null,
                GoogleScholar = null,
                Scopus = null,
                Facebook = null,
                X = null,
                YouTube = null
            };

            await SocialMediaPlatformsRepo.AddAsync(newSocialMediaPlatforms);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SocialMediaPlatformsDto>(newSocialMediaPlatforms);
        }

        public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(
            SocialMediaPlatformsDto dto,
            string facultyMemberEmail)
        {
            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(
                new SocialMediaWithFacultyMemberEmailSpecifications(facultyMemberEmail))
                ?? throw new NotFoundException("Social Media Platforms Are Not Found.");

            _mapper.Map(dto, socialMediaPlatforms);

            SocialMediaPlatformsRepo.Update(socialMediaPlatforms);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
        }
        #endregion
    }
}
