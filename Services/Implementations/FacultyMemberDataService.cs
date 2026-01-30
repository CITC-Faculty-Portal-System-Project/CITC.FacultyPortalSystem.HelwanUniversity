using Domain.Entities.Attachments;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.ScientificProgressionModule;

namespace Services.Implementations
{
    public class FacultyMemberDataService(IUnitOfWork _unitOfWork, IMapper _mapper, IAuthenticationService _authenticationService) : IFacultyMemberDataService
    {
        #region Helper Methods
        //Get Current Logged User 
        private async Task<UserResultDto> GetCurrentUserAsync()
        {
            var email = _authenticationService.GetLoggedUserEmail();
            var user = await _authenticationService.GetCurrentUserAsync(email)
                       ?? throw new UnauthorizedAccessException("Unauthorized.");
            return user;
        }

        //Get Faculty Member By Email
        private async Task<FacultyMember> GetFacultyMemberByEmailAsync(string email)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var spec = new FacultyMemberWithEmailSpecifications(email);

            return await repo.GetAsync(spec)
                   ?? throw new NotFoundException("Faculty Member Not Found.");
        }

        //Get Faculty Member By National Number
        private async Task<FacultyMember> GetFacultyMemberByNationalNumberAsync(string nationalNumber)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var spec = new FacultyMemberWithNationalNumberSpecifications(nationalNumber);

            return await repo.GetAsync(spec)
                   ?? throw new NotFoundException("Faculty Member Not Found.");
        }

        //Get Repositories
        private IGenericRepository<PersonalData, int> PersonalDataRepo
            => _unitOfWork.GetRepository<PersonalData, int>();

        private IGenericRepository<ContactData, int> ContactDataRepo
            => _unitOfWork.GetRepository<ContactData, int>();

        private IGenericRepository<IdentificationCard, int> IdentificationCardRepo
            => _unitOfWork.GetRepository<IdentificationCard, int>();

        private IGenericRepository<SocialMediaPlatforms, int> SocialMediaRepo
            => _unitOfWork.GetRepository<SocialMediaPlatforms, int>();

        private IGenericRepository<AttachmentReference, Guid> AttachmentsRepo
           => _unitOfWork.GetRepository<AttachmentReference, Guid>();

        //Enusure Attachment Exist
        private async Task EnsureAttachmentExistance(Guid attachmentId)
        {
            if (await AttachmentsRepo.GetByIdAsync(attachmentId) is null)
                throw new NotFoundException($"Desired Attachment Wasn't Found");
        }

        #endregion

        #region Personal Data
        public async Task<PersonalDataResponseDto?> GetPersonalDataAsync()
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Personal Data With Includes
            var personalData = await PersonalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email))
                ?? throw new NotFoundException("Personal Data is Not Found.");

            //Map Response to Dto
            var personalDataResult = _mapper.Map<PersonalDataResponseDto>(personalData);

            //Add NationalNumber
            personalDataResult.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            //Return Result
            return personalDataResult;

        }

        public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(PersonalDataUpdateDto personalDataUpdateDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            if (personalDataUpdateDto.ProfilePictureId is not null)
                await EnsureAttachmentExistance(personalDataUpdateDto.ProfilePictureId ?? Guid.Empty);

            //Load Personal Data With Includes
            var personalData = await PersonalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("Personal Data is Not Found.");

            //Map Updated Data to Personal Data Entity
            _mapper.Map(personalDataUpdateDto, personalData);

            //Update The Data to Database
            PersonalDataRepo.Update(personalData);
            await _unitOfWork.SaveChangesAsync();

            //Return The Updated Data
            var updatedDataResult = _mapper.Map<PersonalDataResponseDto>(personalData);
            return updatedDataResult;
        }
        #endregion

        #region Contact Data
        public async Task<ContactDataResponseDto?> GetContactDataAsync()
        {
            //Get Logged User 
            var currentUser= await GetCurrentUserAsync();

            //Load Contact Data
            var contactData = await ContactDataRepo.GetAsync(new ContactDataWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("Contact Data is Not Found.");

            //Map Response to Dto
            var contactDataResult = _mapper.Map<ContactDataResponseDto>(contactData);

            //Return Result
            return contactDataResult;
        }

        public async Task<ContactDataResponseDto?> UpdateContactDataAsync(ContactDataUpdateDto contactDataUpdateDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Contact Data 
            var contactData = await ContactDataRepo.GetAsync(new ContactDataWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("Contact Data is Not Found.");

            //Map Updated Data to Contact Data Entity
            _mapper.Map(contactDataUpdateDto, contactData);

            //Update The Data to Database
            ContactDataRepo.Update(contactData);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            var updatedDataResult = _mapper.Map<ContactDataResponseDto>(contactData);
            return updatedDataResult;
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
                return _mapper.Map<IdentificationCardDto>(identificationCard);

            //Load Faculty Member to Attach New Card
            var facultyMember = await GetFacultyMemberByEmailAsync(currentUser.Email) ?? throw new NotFoundException("Faculty Member is Not Found.");

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
            await _unitOfWork.SaveChangesAsync();

            //Return Mapped Dto
            return _mapper.Map<IdentificationCardDto>(newCard);
        }

        public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(IdentificationCardDto identificationCardDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Identification Card Data
            var identificationCard = await IdentificationCardRepo.GetAsync(new IdentificationCardWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("Identification Card is Not Found.");

            //Map Updated Data to Entity
            _mapper.Map(identificationCardDto, identificationCard);

            //Update and Save Updated Data to Database
            IdentificationCardRepo.Update(identificationCard);
            await _unitOfWork.SaveChangesAsync();

            //Return The Updated Data
            return _mapper.Map<IdentificationCardDto>(identificationCard);
        }
        #endregion

        #region Social Media
        public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync()
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Social Media Platforms Data
            var socialMediaPlatforms = await SocialMediaRepo.GetAsync(new SocialMediaWithFacultyMemberEmailSpecifications(currentUser.Email));

            if (socialMediaPlatforms is not null)
                return _mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);

            //Load Faculty Member to Attach New Social Media Platforms Data
            var facultyMember = await GetFacultyMemberByEmailAsync(currentUser.Email) ?? throw new NotFoundException("Faculty Member is Not Found.");

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
            await SocialMediaRepo.AddAsync(newSocialMediaPlatforms);
            await _unitOfWork.SaveChangesAsync();

            //Return Mapped Dto
            return _mapper.Map<SocialMediaPlatformsDto>(newSocialMediaPlatforms);
        }

        public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(SocialMediaPlatformsDto socialMediaPlatformsDto)
        {
            //Get Logged User 
            var currentUser = await GetCurrentUserAsync();

            //Load Social Media Platforms Data
            var socialMediaPlatforms = await SocialMediaRepo.GetAsync(new SocialMediaWithFacultyMemberEmailSpecifications(currentUser.Email)) 
                ?? throw new NotFoundException("Social Media Platforms Are Not Found.");

            //Map Updated Data to Entity
            _mapper.Map(socialMediaPlatformsDto, socialMediaPlatforms);

            //Update and Save Updated Data to Database
            SocialMediaRepo.Update(socialMediaPlatforms);
            await _unitOfWork.SaveChangesAsync();

            //Return The Updated Data
            return _mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
        }
        #endregion
    }
}
