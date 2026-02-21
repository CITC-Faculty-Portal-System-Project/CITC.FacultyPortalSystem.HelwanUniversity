using Services.Global;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations
{
    public class FacultyMemberDataService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IAuthenticationService authenticationService,
    IValidationService validationService)
                : BaseService<FacultyMember, Guid>(unitOfWork, authenticationService, mapper, validationService), IFacultyMemberDataService
    {
        protected override string EntityName => "Faculty Member";

        #region Repositories
        private IGenericRepository<PersonalData, int> PersonalDataRepo
            => GetRepository<PersonalData, int>();

        private IGenericRepository<ContactData, int> ContactDataRepo
            => GetRepository<ContactData, int>();

        private IGenericRepository<IdentificationCard, int> IdentificationCardRepo
            => GetRepository<IdentificationCard, int>();

        private IGenericRepository<SocialMediaPlatforms, int> SocialMediaPlatformsRepo
            => GetRepository<SocialMediaPlatforms, int>();
        #endregion

        #region Personal Data
        public async Task<PersonalDataResponseDto?> GetPersonalDataAsync()
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Personal Data With Includes
            var personalData = await PersonalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email))
                ?? throw new NotFoundException("errors.PersonalData.notFound" , currentUser.Email);

            //Map Response to Dto
            var personalDataResult = Mapper.Map<PersonalDataResponseDto>(personalData);
            //Add NationalNumber
            personalDataResult.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            //Return Result
            return personalDataResult;
        }

        public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(PersonalDataUpdateDto personalDataUpdateDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //if (personalDataUpdateDto.ProfilePictureId is not null)
            //    await EnsureAttachmentExistance(personalDataUpdateDto.ProfilePictureId ?? Guid.Empty);

            //Load Personal Data With Includes
            var personalData = await PersonalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("errors.PersonalData.notFound" , currentUser.Email);

            //Map Updated Data to Personal Data Entity
            Mapper.Map(personalDataUpdateDto, personalData);

            //Update The Data to Database
            PersonalDataRepo.Update(personalData);
            await SaveChangesAsync();

            //Return The Updated Data
            return Mapper.Map<PersonalDataResponseDto>(personalData);
        }
        #endregion

        #region Contact Data
        public async Task<ContactDataResponseDto?> GetContactDataAsync()
        {
            //Get Logged User 
            var currentUser= await GetCurrentUserAsync();

            //Load Contact Data
            var contactData = await ContactDataRepo.GetAsync(new ContactDataWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("errors.ContactData.notFound" , currentUser.Email);

            //Map Response to Dto
            return Mapper.Map<ContactDataResponseDto>(contactData);
        }

        public async Task<ContactDataResponseDto?> UpdateContactDataAsync(ContactDataUpdateDto contactDataUpdateDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Contact Data 
            var contactData = await ContactDataRepo.GetAsync(new ContactDataWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("errors.ContactData.notFound" , currentUser.Email);

            //Map Updated Data to Contact Data Entity
            Mapper.Map(contactDataUpdateDto, contactData);

            //Update The Data to Database
            ContactDataRepo.Update(contactData);
            await SaveChangesAsync();

            //Return Updated Data
            return Mapper.Map<ContactDataResponseDto>(contactData);
        }
        #endregion

        #region Identification Card
        public async Task<IdentificationCardDto> GetIdentificationCardAsync()
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Identification Card Data
            var identificationCard = await IdentificationCardRepo.GetAsync(new IdentificationCardWithFacultyMemberEmailSpecifications(currentUser.Email));

            if (identificationCard is not null)
                return Mapper.Map<IdentificationCardDto>(identificationCard);

            //Load Faculty Member to Attach New Card
            var facultyMember = await GetFacultyMemberByEmailAsync(currentUser.Email) ?? throw new NotFoundException("errors.FacultyMember.notFound" , currentUser.Email);

            //Create New Empty Identification Card
            var newCard = new IdentificationCard
            {
                FacultyMemberId = facultyMember.Id,
                ORCID = null,
                EKB = null,
                ResearcherId = null,
                ResearcherGate = null,
                AcademiaEdu = null
            };

            //Save Identification Card
            await IdentificationCardRepo.AddAsync(newCard);
            await SaveChangesAsync();

            //Return Mapped Dto
            return Mapper.Map<IdentificationCardDto>(newCard);
        }

        public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(IdentificationCardDto identificationCardDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Identification Card Data
            var identificationCard = await IdentificationCardRepo.GetAsync(new IdentificationCardWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("errors.IdCard.notFound"  , currentUser.Email);

            //Map Updated Data to Entity
            Mapper.Map(identificationCardDto, identificationCard);

            //Update and Save Updated Data to Database
            IdentificationCardRepo.Update(identificationCard);
            await SaveChangesAsync();

            //Return The Updated Data
            return Mapper.Map<IdentificationCardDto>(identificationCard);
        }
        #endregion

        #region Social Media
        public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync()
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Social Media Platforms Data
            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(new SocialMediaWithFacultyMemberEmailSpecifications(currentUser.Email));

            if (socialMediaPlatforms is not null)
                return Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);

            //Load Faculty Member to Attach New Social Media Platforms Data
            var facultyMember = await GetFacultyMemberByEmailAsync(currentUser.Email) ?? throw new NotFoundException("errors.FacultyMember.notFound" , currentUser.Email);

            //Create New Empty Social Media Platforms
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
                YouTube = null,
            };

            //Save Social Media Platforms Data
            await SocialMediaPlatformsRepo.AddAsync(newSocialMediaPlatforms);
            await SaveChangesAsync();

            //Return Mapped Dto
            return Mapper.Map<SocialMediaPlatformsDto>(newSocialMediaPlatforms);
        }

        public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(SocialMediaPlatformsDto socialMediaPlatformsDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Social Media Platforms Data
            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(new SocialMediaWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("errors.SocialMedia.notFound" , currentUser.Email);

            //Map Updated Data to Entity
            Mapper.Map(socialMediaPlatformsDto, socialMediaPlatforms);

            //Update and Save Updated Data to Database
            SocialMediaPlatformsRepo.Update(socialMediaPlatforms);
            await SaveChangesAsync();

            //Return The Updated Data
            return Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
        }
        #endregion
    }
}
