using Services.Global;
using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Implementations
{
    public class FacultyMemberDataService(
     IUnitOfWork unitOfWork,
     IMapper mapper,
     IAuthenticationService authenticationService)
     : BaseService<FacultyMember, Guid>(unitOfWork, authenticationService, mapper),
       IFacultyMemberDataService
    {
        protected override string EntityName => "Faculty Member";

        private IGenericRepository<PersonalData, int> PersonalDataRepo
            => GetRepository<PersonalData, int>();

        private IGenericRepository<ContactData, int> ContactDataRepo
            => GetRepository<ContactData, int>();

        private IGenericRepository<IdentificationCard, int> IdentificationCardRepo
            => GetRepository<IdentificationCard, int>();

        private IGenericRepository<SocialMediaPlatforms, int> SocialMediaPlatformsRepo
            => GetRepository<SocialMediaPlatforms, int>();

        public async Task<PersonalDataResponseDto?> GetPersonalDataAsync(string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var personalData = await PersonalDataRepo.GetAsync(
                new PersonalDataWithIncludesSpecifications(email))
                ?? throw new NotFoundException("Personal Data is Not Found.");

            await EnsureOwnershipIfClientAsync(
                personalData.FacultyMemberId,
                facultyMemberEmail);

            var result = Mapper.Map<PersonalDataResponseDto>(personalData);
            result.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            return result;
        }

        public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(
            PersonalDataUpdateDto personalDataUpdateDto,
            string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var personalData = await PersonalDataRepo.GetAsync(
                new PersonalDataWithIncludesSpecifications(email))
                ?? throw new NotFoundException("Personal Data is Not Found.");

            await EnsureOwnershipIfClientAsync(
                personalData.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(personalDataUpdateDto, personalData);

            PersonalDataRepo.Update(personalData);
            await SaveChangesAsync();

            var result = Mapper.Map<PersonalDataResponseDto>(personalData);
            result.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            return result;
        }

        public async Task<ContactDataResponseDto?> GetContactDataAsync(string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var contactData = await ContactDataRepo.GetAsync(
                new ContactDataWithFacultyMemberEmailSpecifications(email))
                ?? throw new NotFoundException("Contact Data is Not Found.");

            await EnsureOwnershipIfClientAsync(
                contactData.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ContactDataResponseDto>(contactData);
        }

        public async Task<ContactDataResponseDto?> UpdateContactDataAsync(
            ContactDataUpdateDto contactDataUpdateDto,
            string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var contactData = await ContactDataRepo.GetAsync(
                new ContactDataWithFacultyMemberEmailSpecifications(email))
                ?? throw new NotFoundException("Contact Data is Not Found.");

            await EnsureOwnershipIfClientAsync(
                contactData.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(contactDataUpdateDto, contactData);

            ContactDataRepo.Update(contactData);
            await SaveChangesAsync();

            return Mapper.Map<ContactDataResponseDto>(contactData);
        }

        public async Task<IdentificationCardDto> GetIdentificationCardAsync(string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var identificationCard = await IdentificationCardRepo.GetAsync(
                new IdentificationCardWithFacultyMemberEmailSpecifications(email));

            if (identificationCard is not null)
            {
                await EnsureOwnershipIfClientAsync(
                    identificationCard.FacultyMemberId,
                    facultyMemberEmail);

                return Mapper.Map<IdentificationCardDto>(identificationCard);
            }

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            await EnsureOwnershipIfClientAsync(
                facultyMember.Id,
                facultyMemberEmail);

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
            await SaveChangesAsync();

            return Mapper.Map<IdentificationCardDto>(newCard);
        }

        public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(
            IdentificationCardDto identificationCardDto,
            string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var identificationCard = await IdentificationCardRepo.GetAsync(
                new IdentificationCardWithFacultyMemberEmailSpecifications(email))
                ?? throw new NotFoundException("Identification Card is Not Found.");

            await EnsureOwnershipIfClientAsync(
                identificationCard.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(identificationCardDto, identificationCard);

            IdentificationCardRepo.Update(identificationCard);
            await SaveChangesAsync();

            return Mapper.Map<IdentificationCardDto>(identificationCard);
        }

        public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync(string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(
                new SocialMediaWithFacultyMemberEmailSpecifications(email));

            if (socialMediaPlatforms is not null)
            {
                await EnsureOwnershipIfClientAsync(
                    socialMediaPlatforms.FacultyMemberId,
                    facultyMemberEmail);

                return Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
            }

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            await EnsureOwnershipIfClientAsync(
                facultyMember.Id,
                facultyMemberEmail);

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
            await SaveChangesAsync();

            return Mapper.Map<SocialMediaPlatformsDto>(newSocialMediaPlatforms);
        }

        public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(
            SocialMediaPlatformsDto socialMediaPlatformsDto,
            string? facultyMemberEmail = null)
        {
            var email = facultyMemberEmail ?? (await GetCurrentUserAsync()).Email;

            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(
                new SocialMediaWithFacultyMemberEmailSpecifications(email))
                ?? throw new NotFoundException("Social Media Platforms Are Not Found.");

            await EnsureOwnershipIfClientAsync(
                socialMediaPlatforms.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(socialMediaPlatformsDto, socialMediaPlatforms);

            SocialMediaPlatformsRepo.Update(socialMediaPlatforms);
            await SaveChangesAsync();

            return Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
        }
    }
}
