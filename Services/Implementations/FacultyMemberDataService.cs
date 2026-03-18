using Domain.Entities.IdentityModule;
using FluentFTP;
using Microsoft.Extensions.Logging;
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
using Shared.Enums.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Implementations
{
	public class FacultyMemberDataService(
	IUnitOfWork unitOfWork,
	IMapper mapper,
	IAuthenticationService authenticationService,
	ILogger<FacultyMemberDataService> _logger)
				: BaseService<FacultyMember, Guid>(unitOfWork, authenticationService, mapper), IFacultyMemberDataService
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
			var personalDateLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.PersonalData.ToString(),
			};

			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//Load Personal Data With Includes
			var personalData = await PersonalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email));
			if (personalData is null)
			{
				#region Log
				personalDateLog.Timestamp = DateTime.Now;
				personalDateLog.RenderedMessage = $"Personal Data Not Found for User: {currentUser.UserName}";
				personalDateLog.Level = "Warning";
				personalDateLog.UserIP = GetUserIP();
				personalDateLog.UserName = currentUser.UserName;
				personalDateLog.AdditionalData = $"User tried to get their personal data, but no personal data was found in the database for user with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", personalDateLog);
				#endregion
				throw new NotFoundException("Personal Data is Not Found.");
			}

			//Map Response to Dto
			var personalDataResult = Mapper.Map<PersonalDataResponseDto>(personalData);
			//Add NationalNumber
			personalDataResult.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;
			#region Log
			personalDateLog.Timestamp = DateTime.Now;
			personalDateLog.RenderedMessage = $"Personal Data Retrieved for User: {currentUser.UserName}";
			personalDateLog.Level = "Information";
			personalDateLog.UserIP = GetUserIP();
			personalDateLog.UserName = currentUser.UserName;
			personalDateLog.AdditionalData = $"User retrieved their personal data successfully.";
			_logger.LogInformation("{@LogDetails}", personalDateLog);
			#endregion
			//Return Result
			return personalDataResult;
		}

		public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(PersonalDataUpdateDto personalDataUpdateDto)
		{
			var updatePersonalDataLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.PersonalData.ToString(),
			};
			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//if (personalDataUpdateDto.ProfilePictureId is not null)
			//    await EnsureAttachmentExistance(personalDataUpdateDto.ProfilePictureId ?? Guid.Empty);

			//Load Personal Data With Includes
			var personalData = await PersonalDataRepo.GetAsync(new PersonalDataWithIncludesSpecifications(currentUser.Email));

			#region Log
			if (personalData is null)
			{
				#region Log
				updatePersonalDataLog.Timestamp = DateTime.Now;
				updatePersonalDataLog.RenderedMessage = $"Personal Data Not Found for User: {currentUser.UserName} during Update Attempt";
				updatePersonalDataLog.Level = "Warning";
				updatePersonalDataLog.UserIP = GetUserIP();
				updatePersonalDataLog.UserName = currentUser.UserName;
				updatePersonalDataLog.AdditionalData = $"User tried to update their personal data, but no personal data was found in the database for user with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", updatePersonalDataLog);
				#endregion
				throw new NotFoundException("Personal Data is Not Found.");
			}
			//old value for logging
			var oldPersonalData = Mapper.Map<PersonalDataResponseDto>(personalData);
			#endregion

			//Map Updated Data to Personal Data Entity
			Mapper.Map(personalDataUpdateDto, personalData);

			//Update The Data to Database
			PersonalDataRepo.Update(personalData);
			await SaveChangesAsync();

			//Map updated data and return it
			var updatedPersonalData = Mapper.Map<PersonalDataResponseDto>(personalData);
			#region Log
			updatePersonalDataLog.Timestamp = DateTime.Now;
			updatePersonalDataLog.RenderedMessage = $"Personal Data Updated for User: {currentUser.UserName}";
			updatePersonalDataLog.Level = "Information";
			updatePersonalDataLog.UserIP = GetUserIP();
			updatePersonalDataLog.UserName = currentUser.UserName;
			updatePersonalDataLog.AdditionalData = $"User updated their personal data successfully. \nOld Data: {System.Text.Json.JsonSerializer.Serialize(oldPersonalData)} \nNew Data: {System.Text.Json.JsonSerializer.Serialize(updatedPersonalData)}";
			_logger.LogInformation("{@LogDetails}", updatePersonalDataLog);
			#endregion
			return updatedPersonalData;
		}
		#endregion

		#region Contact Data
		public async Task<ContactDataResponseDto?> GetContactDataAsync()
		{
			var contactDataLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.ContactData.ToString(),
			};
			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//Load Contact Data
			var contactData = await ContactDataRepo.GetAsync(new ContactDataWithFacultyMemberEmailSpecifications(currentUser.Email));

			if (contactData is null)
			{
				#region Log
				contactDataLog.Timestamp = DateTime.Now;
				contactDataLog.RenderedMessage = $"Contact Data Not Found for User: {currentUser.UserName}";
				contactDataLog.Level = "Warning";
				contactDataLog.UserIP = GetUserIP();
				contactDataLog.UserName = currentUser.UserName;
				contactDataLog.AdditionalData = $"User tried to get their contact data, but no contact data was found in the database for user with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", contactDataLog);
				#endregion
				throw new NotFoundException("Contact Data is Not Found.");
			}
			//Map to Dto
			var result = Mapper.Map<ContactDataResponseDto>(contactData);
			#region Log
			contactDataLog.Timestamp = DateTime.Now;
			contactDataLog.RenderedMessage = $"Contact Data Retrieved for User: {currentUser.UserName}";
			contactDataLog.Level = "Information";
			contactDataLog.UserIP = GetUserIP();
			contactDataLog.UserName = currentUser.UserName;
			contactDataLog.AdditionalData = $"User retrieved their contact data successfully.";
			_logger.LogInformation("{@LogDetails}", contactDataLog);
			#endregion
			//Map Response
			return result;
		}

		public async Task<ContactDataResponseDto?> UpdateContactDataAsync(ContactDataUpdateDto contactDataUpdateDto)
		{
			var updateContactDataLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.ContactData.ToString(),
			};
			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//Load Contact Data 
			var contactData = await ContactDataRepo.GetAsync(new ContactDataWithFacultyMemberEmailSpecifications(currentUser.Email));

			#region Log
			if (contactData is null)
			{
				#region Log
				updateContactDataLog.Timestamp = DateTime.Now;
				updateContactDataLog.RenderedMessage = $"Contect Data Not Found for User: {currentUser.UserName} during Update Attempt";
				updateContactDataLog.Level = "Warning";
				updateContactDataLog.UserIP = GetUserIP();
				updateContactDataLog.UserName = currentUser.UserName;
				updateContactDataLog.AdditionalData = $"User tried to update their contact data, but no contact data was found in the database for user with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", updateContactDataLog);
				#endregion
				throw new NotFoundException("Contact Data is Not Found.");
			}
			var oldContactData = Mapper.Map<ContactDataResponseDto>(contactData);
			#endregion

			//Map Updated Data to Contact Data Entity
			Mapper.Map(contactDataUpdateDto, contactData);

			//Update The Data to Database
			ContactDataRepo.Update(contactData);
			await SaveChangesAsync();

			//Map Updated Data and Return It
			var updatedContactData = Mapper.Map<ContactDataResponseDto>(contactData);
			#region Log
			updateContactDataLog.Timestamp = DateTime.Now;
			updateContactDataLog.Level = "Information";
			updateContactDataLog.RenderedMessage = $"Personal Data Updated for User: {currentUser.UserName}";
			updateContactDataLog.UserIP = GetUserIP();
			updateContactDataLog.UserName = currentUser.UserName;
			updateContactDataLog.AdditionalData = $"User updated their contact data successfully. \nOld Data: {System.Text.Json.JsonSerializer.Serialize(oldContactData)} \nNew Data: {System.Text.Json.JsonSerializer.Serialize(updatedContactData)}";
			_logger.LogInformation("{@LogDetails}", updateContactDataLog);
			#endregion
			return updatedContactData;
		}
		#endregion

		#region Identification Card
		public async Task<IdentificationCardDto> GetIdentificationCardAsync()
		{
			var identificationCardDataLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.IdentificationCardData.ToString()
			};
			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//Load Identification Card Data
			var identificationCard = await IdentificationCardRepo.GetAsync(new IdentificationCardWithFacultyMemberEmailSpecifications(currentUser.Email));
			if (identificationCard is not null)
			{
				var data = Mapper.Map<IdentificationCardDto>(identificationCard);
				#region Log
				identificationCardDataLog.Timestamp = DateTime.Now;
				identificationCardDataLog.Level = "Information";
				identificationCardDataLog.UserIP = GetUserIP();
				identificationCardDataLog.UserName = currentUser.UserName;
				identificationCardDataLog.RenderedMessage = $"Identification Card Data Retrieved for User: {currentUser.UserName}";
				identificationCardDataLog.AdditionalData = $"User retrieved their identification card data successfully";
				_logger.LogInformation("{@LogDetails}", identificationCardDataLog);
				#endregion
				return data;
			}

			//Load Faculty Member to Attach New Card
			var facultyMember = await GetFacultyMemberByEmailAsync(currentUser.Email);
			if (facultyMember is null)
			{
				#region Log
				identificationCardDataLog.Timestamp = DateTime.Now;
				identificationCardDataLog.Level = "Warning";
				identificationCardDataLog.UserIP = GetUserIP();
				identificationCardDataLog.UserName = currentUser.UserName;
				identificationCardDataLog.RenderedMessage = $"Faculty Member Not Found";
				identificationCardDataLog.AdditionalData = $"User tried to get identification card data for a faculty member that does not exist in database, No Faculty Member found with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", identificationCardDataLog);
				#endregion
				throw new NotFoundException("Faculty Member is Not Found.");
			}

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
			var resultDTO = Mapper.Map<IdentificationCardDto>(newCard);
			#region Log
			identificationCardDataLog.Timestamp = DateTime.Now;
			identificationCardDataLog.Level = "Information";
			identificationCardDataLog.UserIP = GetUserIP();
			identificationCardDataLog.UserName = currentUser.UserName;
			identificationCardDataLog.RenderedMessage = $"Identification Card Retrieved for User: {currentUser.UserName}";
			identificationCardDataLog.AdditionalData = $"User retrieved new identification card successfully";
			_logger.LogInformation("{@LogDetails}", identificationCardDataLog);
			#endregion
			return resultDTO;
		}

		public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(IdentificationCardDto identificationCardDto)
		{
			var identificationCardDataLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.IdentificationCardData.ToString()
			};

			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//Load Identification Card Data
			var identificationCard = await IdentificationCardRepo.GetAsync(new IdentificationCardWithFacultyMemberEmailSpecifications(currentUser.Email));
			#region Log
			if (identificationCard is null)
			{
				#region Log
				identificationCardDataLog.Timestamp = DateTime.Now;
				identificationCardDataLog.Level = "Warning";
				identificationCardDataLog.RenderedMessage = $"Identification Card Data Not Found for User: {currentUser.UserName} duting Update Attempt";
				identificationCardDataLog.AdditionalData = $"User tried to update their identification card data, but no identification card data was found in the database for user with email : {currentUser.Email}";
				identificationCardDataLog.UserIP = GetUserIP();
				identificationCardDataLog.UserName = currentUser.UserName;
				_logger.LogWarning("{@LogDetails}", identificationCardDataLog);
				#endregion
				throw new NotFoundException("Identification Card is Not Found.");
			}
			//old value for Logging
			var oldIdentificationCard = Mapper.Map<IdentificationCardDto>(identificationCard);
			#endregion

			//Map Updated Data to Entity
			Mapper.Map(identificationCardDto, identificationCard);

			//Update and Save Updated Data to Database
			IdentificationCardRepo.Update(identificationCard);
			await SaveChangesAsync();

			//Return The Updated Data
			var updatedIdentificationCard = Mapper.Map<IdentificationCardDto>(identificationCard);
			#region Log
			identificationCardDataLog.Timestamp = DateTime.Now;
			identificationCardDataLog.RenderedMessage = $"Identification Card Data Updated for User: {currentUser.UserName}";
			identificationCardDataLog.Level = "Information";
			identificationCardDataLog.UserIP = GetUserIP();
			identificationCardDataLog.UserName = currentUser.UserName;
			identificationCardDataLog.AdditionalData = $"User updated their identification card data successfully. \nOld Data: {System.Text.Json.JsonSerializer.Serialize(oldIdentificationCard)} \nNew Data: {System.Text.Json.JsonSerializer.Serialize(updatedIdentificationCard)}";
			_logger.LogInformation("{@LogDetails}", identificationCardDataLog);
			#endregion
			return updatedIdentificationCard;
		}
		#endregion

		#region Social Media
		public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync()
		{
			var socialMediaLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.SocialMediaData.ToString(),
			};
			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//Load Social Media Platforms Data
			var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(new SocialMediaWithFacultyMemberEmailSpecifications(currentUser.Email));
			if (socialMediaPlatforms is not null)
			{
				var data = Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
				#region Log
				socialMediaLog.Timestamp = DateTime.Now;
				socialMediaLog.Level = "Information";
				socialMediaLog.UserIP = GetUserIP();
				socialMediaLog.UserName = currentUser.UserName;
				socialMediaLog.RenderedMessage = $"Social Media Platforms Data Retrieved for User: {currentUser.UserName}";
				socialMediaLog.AdditionalData = $"User retrieved their social Media platforms data successfully";
				_logger.LogInformation("{@LogDetails}", socialMediaLog);
				#endregion
				return data;
			}

			//Load Faculty Member to Attach New Social Media Platforms Data
			var facultyMember = await GetFacultyMemberByEmailAsync(currentUser.Email);
			if (facultyMember is null)
			{
				#region Log
				socialMediaLog.Timestamp = DateTime.Now;
				socialMediaLog.Level = "Warning";
				socialMediaLog.UserIP = GetUserIP();
				socialMediaLog.UserName = currentUser.UserName;
				socialMediaLog.RenderedMessage = $"Faculty Member Not Found";
				socialMediaLog.AdditionalData = $"User tried to get social media platforms data for a faculty member that does not exist in database, No Faculty Member found with email : {currentUser.Email}";
				_logger.LogWarning("{@LogDetails}", socialMediaLog);
				#endregion
				throw new NotFoundException("Faculty Member is Not Found.");
			}

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
			var resultDTO = Mapper.Map<SocialMediaPlatformsDto>(newSocialMediaPlatforms);
			#region Log
			socialMediaLog.Timestamp = DateTime.Now;
			socialMediaLog.Level = "Information";
			socialMediaLog.UserIP = GetUserIP();
			socialMediaLog.UserName = currentUser.UserName;
			socialMediaLog.RenderedMessage = $"Social Media Platforms Data Retrieved for User: {currentUser.UserName}";
			socialMediaLog.AdditionalData = $"User retrieved new social media platforms data successfully";
			_logger.LogInformation("{@LogDetails}", socialMediaLog);
			#endregion
			return resultDTO;
		}

		public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(SocialMediaPlatformsDto socialMediaPlatformsDto)
		{
			var updateSocialMediaLog = new LogEntry
			{
				Category = Category.FacultyMemberService.ToString(),
				CategoryAction = CategoryAction.SocialMediaData.ToString()
			};
			//Get Logged User 
			var currentUser = await GetCurrentUserAsync();

			//Load Social Media Platforms Data
			var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(new SocialMediaWithFacultyMemberEmailSpecifications(currentUser.Email));
			#region Log
			if (socialMediaPlatforms is null)
			{
				#region Log
				updateSocialMediaLog.Timestamp = DateTime.Now;
				updateSocialMediaLog.Level = "Warning";
				updateSocialMediaLog.RenderedMessage = $"Social Media Platforms Data Not Found for User: {currentUser.UserName} duting Update Attempt";
				updateSocialMediaLog.AdditionalData = $"User tried to update their social media platforms data, but no social media data was found in the database for user with email : {currentUser.Email}";
				updateSocialMediaLog.UserIP = GetUserIP();
				updateSocialMediaLog.UserName = currentUser.UserName;
				_logger.LogWarning("{@LogDetails}", updateSocialMediaLog);
				#endregion
				throw new NotFoundException("Social Media Platforms Are Not Found.");
			}
			//old value for logging
			var oldSocialMediaData = Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
			#endregion

			//Map Updated Data to Entity
			Mapper.Map(socialMediaPlatformsDto, socialMediaPlatforms);

			//Update and Save Updated Data to Database
			SocialMediaPlatformsRepo.Update(socialMediaPlatforms);
			await SaveChangesAsync();

			//Return The Updated Data
			var updatedSocialMediaData = Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
			#region Log
			updateSocialMediaLog.Timestamp = DateTime.Now;
			updateSocialMediaLog.RenderedMessage = $"Social Media Platforms Data Updated for User: {currentUser.UserName}";
			updateSocialMediaLog.Level = "Information";
			updateSocialMediaLog.UserIP = GetUserIP();
			updateSocialMediaLog.UserName = currentUser.UserName;
			updateSocialMediaLog.AdditionalData = $"User updated their social media platforms data successfully. \nOld Data: {System.Text.Json.JsonSerializer.Serialize(oldSocialMediaData)} \nNew Data: {System.Text.Json.JsonSerializer.Serialize(updatedSocialMediaData)}";
			_logger.LogInformation("{@LogDetails}", updateSocialMediaLog);
			#endregion
			return updatedSocialMediaData;
		}
		#endregion
	}
}
