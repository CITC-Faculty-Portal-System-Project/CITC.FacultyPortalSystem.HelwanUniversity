using Domain.Entities.FacultyMemberDataModule;
using Services.Specifications.FacultyMemberDataModule;
using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Implementations
{
    public class FacultyMemberDataService(IUnitOfWork _unitOfWork, IMapper _mapper) : IFacultyMemberDataService
    {
        #region Personal Data
        public async Task<PersonalDataResponseDto?> GetPersonalDataAsync(string facultyMemberEmail)
        {
            //Load Personal Data With Includes
            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();
            var specifications = new PersonalDataWithIncludesSpecifications(facultyMemberEmail);
            var personalData = await personalDataRepo.GetAsync(specifications) ?? throw new NotFoundException("Personal Data is Not Found.");

            //Map Response to Dto
            var personalDataResult = _mapper.Map<PersonalDataResponseDto>(personalData);

            //Add NationalNumber
            personalDataResult.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            //Return Result
            return personalDataResult;

        }

        public async Task<PersonalDataResponseDto?> FetchPersonalDataAsync(PersonalDataCreateDto personalDataCreateDto)
        {
            //Load The Faculty Member with The National Number
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithNationalNumberSpecifications(personalDataCreateDto.NationalNumber);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Load Personal Data If Already Exist
            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();
            var existingSpec = new PersonalDataWithIncludesSpecifications(facultyMember.Email);
            var existingPersonalData = await personalDataRepo.GetAsync(existingSpec);

            if (existingPersonalData != null)
                return _mapper.Map<PersonalDataResponseDto>(existingPersonalData);

            //Map The Dto to The PersonalData Entity and add FacultyMemberId Manually
            var personalData = _mapper.Map<PersonalData>(personalDataCreateDto);
            personalData.FacultyMemberId = facultyMember.Id;

            //Add and Save The Data to Database
            await personalDataRepo.AddAsync(personalData);
            await _unitOfWork.SaveChangesAsync();

            //Return Result
            var createdDataResult = _mapper.Map<PersonalDataResponseDto>(personalData);
            return createdDataResult;
        }

        public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(string facultyMemberEmail, PersonalDataUpdateDto personalDataUpdateDto)
        {
            //Load Personal Data With Includes
            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();
            var specifications = new PersonalDataWithIncludesSpecifications(facultyMemberEmail);
            var personalData = await personalDataRepo.GetAsync(specifications) ?? throw new NotFoundException("Personal Data is Not Found.");

            //Map Updated Data to Personal Data Entity
            _mapper.Map(personalDataUpdateDto, personalData);

            //Update The Data to Database
            personalDataRepo.Update(personalData);
            await _unitOfWork.SaveChangesAsync();

            //Return The Updated Data
            var updatedDataResult = _mapper.Map<PersonalDataResponseDto>(personalData);
            return updatedDataResult;
        }
        #endregion

        #region Contact Data
        public async Task<ContactDataResponseDto?> GetContactDataAsync(string facultyMemberEmail)
        {
            //Load Contact Data
            var contactDataRepo = _unitOfWork.GetRepository<ContactData, int>();
            var specifications = new ContactDataWithFacultyMemberEmailSpecifications(facultyMemberEmail);
            var contactData = await contactDataRepo.GetAsync(specifications) ?? throw new NotFoundException("Contact Data is Not Found.");

            //Map Response to Dto
            var contactDataResult = _mapper.Map<ContactDataResponseDto>(contactData);

            //Return Result
            return contactDataResult;
        }

        public async Task<ContactDataResponseDto?> FetchContactDataAsync(string nationalNumber, ContactDataCreateDto contactDataCreateDto)
        {
            //Load The Faculty Member with The National Number
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithNationalNumberSpecifications(nationalNumber);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Load Contact Data If Already Exist
            var contactDataRepo = _unitOfWork.GetRepository<ContactData, int>();
            var existingSpec = new ContactDataWithFacultyMemberEmailSpecifications(facultyMember.Email);
            var existingContactData = await contactDataRepo.GetAsync(existingSpec);

            if (existingContactData != null)
                return _mapper.Map<ContactDataResponseDto>(existingContactData);

            //Map The Dto to The ContactData Entity and add FacultyMemberId Manually
            var contactData = _mapper.Map<ContactData>(contactDataCreateDto);
            contactData.FacultyMemberId = facultyMember.Id;

            //Add and Save The Data to Database
            await contactDataRepo.AddAsync(contactData);
            await _unitOfWork.SaveChangesAsync();

            //Return Result
            var contactDataResult = _mapper.Map<ContactDataResponseDto>(contactData);
            return contactDataResult;
        }

        public async Task<ContactDataResponseDto?> UpdateContactDataAsync(string facultyMemberEmail, ContactDataUpdateDto contactDataUpdateDto)
        {
            //Load Contact Data 
            var contactDataRepo = _unitOfWork.GetRepository<ContactData, int>();
            var specifications = new ContactDataWithFacultyMemberEmailSpecifications(facultyMemberEmail);
            var contactData = await contactDataRepo.GetAsync(specifications) ?? throw new NotFoundException("Contact Data is Not Found.");

            //Map Updated Data to Contact Data Entity
            _mapper.Map(contactDataUpdateDto, contactData);

            //Update The Data to Database
            contactDataRepo.Update(contactData);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            var updatedDataResult = _mapper.Map<ContactDataResponseDto>(contactData);
            return updatedDataResult;
        }
        #endregion

        #region Identification Card
        public async Task<IdentificationCardDto> GetIdentificationCardAsync(string facultyMemberEmail)
        {
            //Load Identification Card Data
            var identificationCardRepo = _unitOfWork.GetRepository<IdentificationCard, int>();
            var specifications = new IdentificationCardWithFacultyMemberEmailSpecifications(facultyMemberEmail);
            var identificationCard = await identificationCardRepo.GetAsync(specifications);

            if (identificationCard is not null)
                return _mapper.Map<IdentificationCardDto>(identificationCard);

            //Load Faculty Member to Attach New Card
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var facultyMemberSpecifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(facultyMemberSpecifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

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
            await identificationCardRepo.AddAsync(newCard);
            await _unitOfWork.SaveChangesAsync();

            //Return Mapped Dto
            return _mapper.Map<IdentificationCardDto>(newCard);
        }

        public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(string facultyMemberEmail, IdentificationCardDto identificationCardDto)
        {
            //Load Identification Card Data
            var identificationCardRepo = _unitOfWork.GetRepository<IdentificationCard, int>();
            var specifications = new IdentificationCardWithFacultyMemberEmailSpecifications(facultyMemberEmail);
            var identificationCard = await identificationCardRepo.GetAsync(specifications) ?? throw new NotFoundException("Identification Card is Not Found.");

            //Map Updated Data to Entity
            _mapper.Map(identificationCardDto, identificationCard);

            //Update and Save Updated Data to Database
            identificationCardRepo.Update(identificationCard);
            await _unitOfWork.SaveChangesAsync();

            //Return The Updated Data
            return _mapper.Map<IdentificationCardDto>(identificationCard);
        }
        #endregion

        #region Social Media
        public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync(string facultyMemberEmail)
        {
            //Load Social Media Platforms Data
            var socialMediaPlatformsRepo = _unitOfWork.GetRepository<SocialMediaPlatforms, int>();
            var specifications = new SocialMediaWithFacultyMemberEmailSpecifications(facultyMemberEmail);
            var socialMediaPlatforms = await socialMediaPlatformsRepo.GetAsync(specifications);

            if (socialMediaPlatforms is not null)
                return _mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);

            //Load Faculty Member to Attach New Social Media Platforms Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var facultyMemberSpecifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(facultyMemberSpecifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

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
            await socialMediaPlatformsRepo.AddAsync(newSocialMediaPlatforms);
            await _unitOfWork.SaveChangesAsync();

            //Return Mapped Dto
            return _mapper.Map<SocialMediaPlatformsDto>(newSocialMediaPlatforms);
        }

        public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(string facultyMemberEmail, SocialMediaPlatformsDto socialMediaPlatformsDto)
        {
            //Load Social Media Platforms Data
            var socialMediaPlatformsRepo = _unitOfWork.GetRepository<SocialMediaPlatforms, int>();
            var specifications = new SocialMediaWithFacultyMemberEmailSpecifications(facultyMemberEmail);
            var socialMediaPlatforms = await socialMediaPlatformsRepo.GetAsync(specifications) ?? throw new NotFoundException("Social Media Platforms Are Not Found.");

            //Map Updated Data to Entity
            _mapper.Map(socialMediaPlatformsDto, socialMediaPlatforms);

            //Update and Save Updated Data to Database
            socialMediaPlatformsRepo.Update(socialMediaPlatforms);
            await _unitOfWork.SaveChangesAsync();

            //Return The Updated Data
            return _mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
        }
        #endregion
    }
}
