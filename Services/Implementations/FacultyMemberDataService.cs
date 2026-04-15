using Domain.Entities.FacultyMemberDataModule;
using Microsoft.Extensions.Logging;
using Services.Global;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Enums.Logging;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations
{
	public class FacultyMemberDataService(
     IUnitOfWork unitOfWork,
     IMapper mapper,
     IAuthenticationService authenticationService, ILogger<FacultyMemberDataService> _logger)
     : BaseService<FacultyMember, Guid>(unitOfWork, authenticationService, mapper),
       IFacultyMemberDataService
    {
        protected override string EntityName => "Faculty Member";

        #region Repos
        private IGenericRepository<PersonalData, int> PersonalDataRepo
            => GetRepository<PersonalData, int>();

        private IGenericRepository<ContactData, int> ContactDataRepo
            => GetRepository<ContactData, int>();

        private IGenericRepository<IdentificationCard, int> IdentificationCardRepo
            => GetRepository<IdentificationCard, int>();

        private IGenericRepository<SocialMediaPlatforms, int> SocialMediaPlatformsRepo
            => GetRepository<SocialMediaPlatforms, int>();

        #endregion

        #region PersonalData

        public async Task<PersonalDataResponseDto?> GetPersonalDataAsync(string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();

            var personalDateLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.PersonalDataActions.ToString(),
            };

            var email = facultyMemberEmail ?? currentUser.Email;

            var personalData = await PersonalDataRepo.GetAsync(
                new PersonalDataWithIncludesSpecifications(email));

            if (personalData is null)
            {
                #region Log
                personalDateLog.Timestamp = DateTime.Now;
                personalDateLog.RenderedMessage = $"Personal data not found for user: {currentUser.UserName}.";
                personalDateLog.Level = "Warning";
                personalDateLog.UserIP = GetUserIP();
                personalDateLog.UserName = currentUser.UserName;
                personalDateLog.AdditionalData = $"User tried to get their personal data, but no personal data was found in the database for user with email : {currentUser.Email}.";
                _logger.LogWarning("{@LogDetails}", personalDateLog);
                #endregion
                throw new NotFoundException("Personal Data is Not Found.");
            }

            try
            {
                await EnsureOwnershipIfClientAsync(
                        personalData.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to access personal data.",
					AdditionalData = $"User tried to access personal data that does not belong to them. Personal data faculty member id: {personalData.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            var result = Mapper.Map<PersonalDataResponseDto>(personalData);
            result.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            #region Log
            personalDateLog.Timestamp = DateTime.Now;
            personalDateLog.RenderedMessage = $"Personal data retrieved for user: {currentUser.UserName}.";
            personalDateLog.Level = "Information";
            personalDateLog.UserIP = GetUserIP();
            personalDateLog.UserName = currentUser.UserName;
            personalDateLog.AdditionalData = $"User retrieved their personal data successfully.";
            _logger.LogInformation("{@LogDetails}", personalDateLog);
            #endregion
            return result;
        }

        public async Task<PersonalDataResponseDto?> UpdatePersonalDataAsync(
            PersonalDataUpdateDto personalDataUpdateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var updatePersonalDataLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.PersonalDataActions.ToString(),
            };

            var email = facultyMemberEmail ?? currentUser.Email;

            var personalData = await PersonalDataRepo.GetAsync(
                new PersonalDataWithIncludesSpecifications(email));

            if (personalData is null)
            {
                #region Log
                updatePersonalDataLog.Timestamp = DateTime.Now;
                updatePersonalDataLog.RenderedMessage = $"Personal data not found for user: {currentUser.UserName}.";
                updatePersonalDataLog.Level = "Warning";
                updatePersonalDataLog.UserIP = GetUserIP();
                updatePersonalDataLog.UserName = currentUser.UserName;
                updatePersonalDataLog.AdditionalData = $"User tried to update their personal data, but no personal data was found in the database for user with email : {currentUser.Email}.";
                _logger.LogWarning("{@LogDetails}", updatePersonalDataLog); 
                #endregion
                throw new NotFoundException("Personal Data is Not Found.");
            }
            var oldPersonalData = Mapper.Map<PersonalDataResponseDto>(personalData);

            try
            {
                await EnsureOwnershipIfClientAsync(
                        personalData.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (Exception ex)
            {
				#region Log
                var ensureOwnershipLog = new LogEntry
                {
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to update personal data.",
					AdditionalData = $"User tried to update personal data that does not belong to them. Personal data faculty member id: {personalData.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            Mapper.Map(personalDataUpdateDto, personalData);

            PersonalDataRepo.Update(personalData);
            await SaveChangesAsync();

            var result = Mapper.Map<PersonalDataResponseDto>(personalData);
            result.NationalNumber = personalData.FacultyMember?.NationalNumber ?? string.Empty;

            #region Log
            updatePersonalDataLog.Timestamp = DateTime.Now;
            updatePersonalDataLog.RenderedMessage = $"Personal data updated for user: {currentUser.UserName}.";
            updatePersonalDataLog.Level = "Information";
            updatePersonalDataLog.UserIP = GetUserIP();
            updatePersonalDataLog.UserName = currentUser.UserName;
            updatePersonalDataLog.AdditionalData = $"User updated their personal data successfully. \nOld Data: {JsonSerializer.Serialize(oldPersonalData, jsonOptions)} \nNew Data: {JsonSerializer.Serialize(result, jsonOptions)}.";
            _logger.LogInformation("{@LogDetails}", updatePersonalDataLog);
            #endregion
            return result;
        }

        #endregion

        #region ContactData

        public async Task<ContactDataResponseDto?> GetContactDataAsync(string? facultyMemberEmail = null)
        {

            var currentUser = await GetCurrentUserAsync();

            var contactDataLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.ContactDataActions.ToString(),
            };

            var email = facultyMemberEmail ?? currentUser.Email;
            var contactData = await ContactDataRepo.GetAsync(
                new ContactDataWithFacultyMemberEmailSpecifications(email));

            if (contactData is null)
            {
                #region Log
                contactDataLog.Timestamp = DateTime.Now;
                contactDataLog.RenderedMessage = $"Contact data not found for user: {currentUser.UserName}.";
                contactDataLog.Level = "Warning";
                contactDataLog.UserIP = GetUserIP();
                contactDataLog.UserName = currentUser.UserName;
                contactDataLog.AdditionalData = $"User tried to get their contact data, but no contact data was found in the database for user with email : {currentUser.Email}.";
                _logger.LogWarning("{@LogDetails}", contactDataLog);
                #endregion
                throw new NotFoundException("Contact Data is Not Found.");
            }

            try
            {
                await EnsureOwnershipIfClientAsync(
                        contactData.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to access contact data.",
					AdditionalData = $"User tried to access contatct data that does not belong to them. Contact data faculty member id: {contactData.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            #region Log
            contactDataLog.Timestamp = DateTime.Now;
            contactDataLog.RenderedMessage = $"Contact data retrieved for user: {currentUser.UserName}.";
            contactDataLog.Level = "Information";
            contactDataLog.UserIP = GetUserIP();
            contactDataLog.UserName = currentUser.UserName;
            contactDataLog.AdditionalData = $"User retrieved their contact data successfully.";
            _logger.LogInformation("{@LogDetails}", contactDataLog);
            #endregion
            return Mapper.Map<ContactDataResponseDto>(contactData);
        }

        public async Task<ContactDataResponseDto?> UpdateContactDataAsync(
            ContactDataUpdateDto contactDataUpdateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();

			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			var updateContactDataLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.ContactDataActions.ToString(),
            };

            var email = facultyMemberEmail ?? currentUser.Email;

            var contactData = await ContactDataRepo.GetAsync(
                new ContactDataWithFacultyMemberEmailSpecifications(email));

            if (contactData is null)
            {
                #region Log
                updateContactDataLog.Timestamp = DateTime.Now;
                updateContactDataLog.RenderedMessage = $"Contect data not found for user: {currentUser.UserName}";
                updateContactDataLog.Level = "Warning";
                updateContactDataLog.UserIP = GetUserIP();
                updateContactDataLog.UserName = currentUser.UserName;
                updateContactDataLog.AdditionalData = $"User tried to update their contact data, but no contact data was found in the database for user with email : {currentUser.Email}";
                _logger.LogWarning("{@LogDetails}", updateContactDataLog);
                #endregion
                throw new NotFoundException("Contact Data is Not Found.");
            }
            var oldContactData = Mapper.Map<ContactDataResponseDto>(contactData);

            try
            {
                await EnsureOwnershipIfClientAsync(
                        contactData.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to update contact data.",
					AdditionalData = $"User tried to update contact data that does not belong to them. Contact data faculty member id: {contactData.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            Mapper.Map(contactDataUpdateDto, contactData);

            ContactDataRepo.Update(contactData);
            await SaveChangesAsync();

            //Map Updated Data and Return It
            var updatedContactData = Mapper.Map<ContactDataResponseDto>(contactData);

            #region Log
            updateContactDataLog.Timestamp = DateTime.Now;
            updateContactDataLog.Level = "Information";
            updateContactDataLog.RenderedMessage = $"Contact data updated for user: {currentUser.UserName}";
            updateContactDataLog.UserIP = GetUserIP();
            updateContactDataLog.UserName = currentUser.UserName;
            updateContactDataLog.AdditionalData = $"User updated their contact data successfully. \nOld Data: {JsonSerializer.Serialize(oldContactData, jsonOptions)} \nNew Data: {JsonSerializer.Serialize(updatedContactData, jsonOptions)}";
            _logger.LogInformation("{@LogDetails}", updateContactDataLog);
            #endregion

            return Mapper.Map<ContactDataResponseDto>(contactData);
        }

        #endregion

        #region IdentificationCard

        public async Task<IdentificationCardDto> GetIdentificationCardAsync(string? facultyMemberEmail = null)
        {

            var currentUser = await GetCurrentUserAsync();

            var identificationCardDataLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.IdentificationCardDataActions.ToString()
            };

            var email = facultyMemberEmail ?? currentUser.Email;

            var identificationCard = await IdentificationCardRepo.GetAsync(
                new IdentificationCardWithFacultyMemberEmailSpecifications(email));

            if (identificationCard is not null)
            {
                try
                {
                    await EnsureOwnershipIfClientAsync(
                                identificationCard.FacultyMemberId,
                                facultyMemberEmail);
                }
                catch (Exception ex)
                {
					#region Log
					var ensureOwnershipLog = new LogEntry
					{
						Category = Category.FacultyMemberService.ToString(),
						CategoryAction = CategoryAction.EnsureOwnership.ToString(),
						Timestamp = DateTime.Now,
						RenderedMessage = $"User unauthorized to access identification card data.",
						AdditionalData = $"User tried to access identification card data that does not belong to them. identification card data faculty member id: {identificationCard.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
						Exception = ex.ToString(),
						ExceptionDetail = ex.StackTrace,
						ExceptionMessage = ex.Message,
						UserIP = GetUserIP(),
						UserName = currentUser.UserName,
						Level = "Error"
					};
					_logger.LogError("{@LogDetails}", ensureOwnershipLog);
					#endregion
					throw;
                }

                #region Log
                identificationCardDataLog.Timestamp = DateTime.Now;
                identificationCardDataLog.Level = "Information";
                identificationCardDataLog.UserIP = GetUserIP();
                identificationCardDataLog.UserName = currentUser.UserName;
                identificationCardDataLog.RenderedMessage = $"Identification card retrieved for user: {currentUser.UserName}";
                identificationCardDataLog.AdditionalData = $"User retrieved their identification card data successfully";
                _logger.LogInformation("{@LogDetails}", identificationCardDataLog);
                #endregion
                return Mapper.Map<IdentificationCardDto>(identificationCard);
            }

			FacultyMember facultyMember = null!;
			try
            {
				facultyMember = await GetFacultyMemberByEmailAsync(email);
				await EnsureOwnershipIfClientAsync(
                       facultyMember.Id,
                       facultyMemberEmail);
            }
            catch (NotFoundException)
            {
				#region Log
				identificationCardDataLog.Timestamp = DateTime.Now;
				identificationCardDataLog.Level = "Warning";
				identificationCardDataLog.UserIP = GetUserIP();
				identificationCardDataLog.UserName = currentUser.UserName;
				identificationCardDataLog.RenderedMessage = $"Faculty Member Not Found.";
				identificationCardDataLog.AdditionalData = $"User tried to get identification card data for a faculty member that does not exist in database, No faculty member found with email : {currentUser.Email}.";
				_logger.LogWarning("{@LogDetails}", identificationCardDataLog);
                #endregion
                throw;
			}
			catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to access identification card data.",
					AdditionalData = $"Faculty member id does not match the logged in user id. Faculty member id: {facultyMember.Id}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

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

            #region Log
            identificationCardDataLog.Timestamp = DateTime.Now;
            identificationCardDataLog.Level = "Information";
            identificationCardDataLog.UserIP = GetUserIP();
            identificationCardDataLog.UserName = currentUser.UserName;
            identificationCardDataLog.RenderedMessage = $"Identification card retrieved for user: {currentUser.UserName}.";
            identificationCardDataLog.AdditionalData = $"User retrieved new identification card successfully.";
            _logger.LogInformation("{@LogDetails}", identificationCardDataLog);
            #endregion

            return Mapper.Map<IdentificationCardDto>(newCard);
        }

        public async Task<IdentificationCardDto> UpdateIdentificationCardAsync(
            IdentificationCardDto identificationCardDto,
            string? facultyMemberEmail = null)
        {

            var currentUser = await GetCurrentUserAsync();

			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			var identificationCardDataLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.IdentificationCardDataActions.ToString()
            };

            var email = facultyMemberEmail ?? currentUser.Email;

            var identificationCard = await IdentificationCardRepo.GetAsync(
                new IdentificationCardWithFacultyMemberEmailSpecifications(email));

            if (identificationCard is null)
            {
                #region Log
                identificationCardDataLog.Timestamp = DateTime.Now;
                identificationCardDataLog.Level = "Warning";
                identificationCardDataLog.RenderedMessage = $"Identification card data not found for user: {currentUser.UserName}.";
                identificationCardDataLog.AdditionalData = $"User tried to update their identification card data, but no identification card data was found in the database for user with email : {currentUser.Email}.";
                identificationCardDataLog.UserIP = GetUserIP();
                identificationCardDataLog.UserName = currentUser.UserName;
                _logger.LogWarning("{@LogDetails}", identificationCardDataLog);
                #endregion
                throw new NotFoundException("Identification Card is Not Found.");
            }
            var oldIdentificationCard = Mapper.Map<IdentificationCardDto>(identificationCard);

            try
            {
                await EnsureOwnershipIfClientAsync(
                        identificationCard.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to update identification card data.",
					AdditionalData = $"User tried to update identification card data that does not belong to them. identification card data faculty member id: {identificationCard.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            Mapper.Map(identificationCardDto, identificationCard);

            IdentificationCardRepo.Update(identificationCard);
            await SaveChangesAsync();

            //Return The Updated Data
            var updatedIdentificationCard = Mapper.Map<IdentificationCardDto>(identificationCard);

            #region Log
            identificationCardDataLog.Timestamp = DateTime.Now;
            identificationCardDataLog.RenderedMessage = $"Identification card data updated for user: {currentUser.UserName}.";
            identificationCardDataLog.Level = "Information";
            identificationCardDataLog.UserIP = GetUserIP();
            identificationCardDataLog.UserName = currentUser.UserName;
            identificationCardDataLog.AdditionalData = $"User updated their identification card data successfully. \nOld Data: {JsonSerializer.Serialize(identificationCard, jsonOptions)} \nNew Data: {JsonSerializer.Serialize(updatedIdentificationCard, jsonOptions)}.";
            _logger.LogInformation("{@LogDetails}", identificationCardDataLog);
            #endregion

            return Mapper.Map<IdentificationCardDto>(identificationCard);
        }

        #endregion

        #region SocialMedia
        public async Task<SocialMediaPlatformsDto> GetSocialMediaPlatformsAsync(string? facultyMemberEmail = null)
        {

            var currentUser = await GetCurrentUserAsync();

            var socialMediaLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.SocialMediaDataActions.ToString(),
            };

            var email = facultyMemberEmail ?? (currentUser.Email);

            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(
                new SocialMediaWithFacultyMemberEmailSpecifications(email));

            if (socialMediaPlatforms is not null)
            {
                try
                {
                    await EnsureOwnershipIfClientAsync(
                                socialMediaPlatforms.FacultyMemberId,
                                facultyMemberEmail);
                }
                catch (Exception ex)
                {
					#region Log
					var ensureOwnershipLog = new LogEntry
					{
						Category = Category.FacultyMemberService.ToString(),
						CategoryAction = CategoryAction.EnsureOwnership.ToString(),
						Timestamp = DateTime.Now,
						RenderedMessage = $"User unauthorized to access social media platforms data.",
						AdditionalData = $"User tried to access social media platforms data that does not belong to them. social media platforms data faculty member id: {socialMediaPlatforms.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
						Exception = ex.ToString(),
						ExceptionDetail = ex.StackTrace,
						ExceptionMessage = ex.Message,
						UserIP = GetUserIP(),
						UserName = currentUser.UserName,
						Level = "Error"
					};
					_logger.LogError("{@LogDetails}", ensureOwnershipLog);
					#endregion
					throw;
                }

                #region Log
                socialMediaLog.Timestamp = DateTime.Now;
                socialMediaLog.Level = "Information";
                socialMediaLog.UserIP = GetUserIP();
                socialMediaLog.UserName = currentUser.UserName;
                socialMediaLog.RenderedMessage = $"Social media platforms data retrieved for user: {currentUser.UserName}";
                socialMediaLog.AdditionalData = $"User retrieved their social media platforms data successfully";
                _logger.LogInformation("{@LogDetails}", socialMediaLog);
                #endregion
                return Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
            }

            FacultyMember facultyMember = null!;
            try
            {
				facultyMember = await GetFacultyMemberByEmailAsync(email);
				await EnsureOwnershipIfClientAsync(
                        facultyMember.Id,
                        facultyMemberEmail);
            }
            catch (NotFoundException)
            {
				#region Log
				socialMediaLog.Timestamp = DateTime.Now;
				socialMediaLog.Level = "Warning";
				socialMediaLog.UserIP = GetUserIP();
				socialMediaLog.UserName = currentUser.UserName;
				socialMediaLog.RenderedMessage = $"Faculty member not found.";
				socialMediaLog.AdditionalData = $"User tried to get social media platforms data for a faculty member that does not exist in database, no faculty member found with email : {currentUser.Email}.";
				_logger.LogWarning("{@LogDetails}", socialMediaLog);
                #endregion
                throw;
			}
			catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to access social media platforms data.",
					AdditionalData = $"Faculty member id does not match the logged in user id. social media platforms data faculty member id: {facultyMember.Id}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            var newSocialMediaPlatforms = new SocialMediaPlatforms
            {
                FacultyMemberId = facultyMember.Id,
                LinkedIn = null,
                Instagram = null,
                PersonalWebsite = null,
                Facebook = null,
                X = null,
                YouTube = null
            };

            await SocialMediaPlatformsRepo.AddAsync(newSocialMediaPlatforms);
            await SaveChangesAsync();

            #region Log
            socialMediaLog.Timestamp = DateTime.Now;
            socialMediaLog.Level = "Information";
            socialMediaLog.UserIP = GetUserIP();
            socialMediaLog.UserName = currentUser.UserName;
            socialMediaLog.RenderedMessage = $"Social media platforms data retrieved for user: {currentUser.UserName}.";
            socialMediaLog.AdditionalData = $"User retrieved new social media platforms data successfully.";
            _logger.LogInformation("{@LogDetails}", socialMediaLog);
            #endregion

            return Mapper.Map<SocialMediaPlatformsDto>(newSocialMediaPlatforms);
        }

        public async Task<SocialMediaPlatformsDto> UpdateSocialMediaPlatformsAsync(
            SocialMediaPlatformsDto socialMediaPlatformsDto,
            string? facultyMemberEmail = null)
        {

            var currentUser = await GetCurrentUserAsync();

			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			var updateSocialMediaLog = new LogEntry
            {
                Category = Category.FacultyMemberService.ToString(),
                CategoryAction = CategoryAction.SocialMediaDataActions.ToString()
            };

            var email = facultyMemberEmail ?? currentUser.Email;

            var socialMediaPlatforms = await SocialMediaPlatformsRepo.GetAsync(
                new SocialMediaWithFacultyMemberEmailSpecifications(email));

            if (socialMediaPlatforms is null)
            {
                #region Log
                updateSocialMediaLog.Timestamp = DateTime.Now;
                updateSocialMediaLog.Level = "Warning";
                updateSocialMediaLog.RenderedMessage = $"Social media platforms data not found for user: {currentUser.UserName}.";
                updateSocialMediaLog.AdditionalData = $"User tried to update their social media platforms data, but no social media data was found in the database for user with email : {currentUser.Email}.";
                updateSocialMediaLog.UserIP = GetUserIP();
                updateSocialMediaLog.UserName = currentUser.UserName;
                _logger.LogWarning("{@LogDetails}", updateSocialMediaLog);
                #endregion
                throw new NotFoundException("Social Media Platforms Are Not Found.");
            }
            var oldSocialMediaData = Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);

            try
            {
                await EnsureOwnershipIfClientAsync(
                        socialMediaPlatforms.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (Exception ex)
            {
				#region Log
				var ensureOwnershipLog = new LogEntry
				{
					Category = Category.FacultyMemberService.ToString(),
					CategoryAction = CategoryAction.EnsureOwnership.ToString(),
					Timestamp = DateTime.Now,
					RenderedMessage = $"User unauthorized to update social media platforms data.",
					AdditionalData = $"User tried to upate social media platforms data that does not belong to them. social media platforms data faculty member id: {socialMediaPlatforms.FacultyMemberId}, Logged in user id: {currentUser.UserId}.",
					Exception = ex.ToString(),
					ExceptionDetail = ex.StackTrace,
					ExceptionMessage = ex.Message,
					UserIP = GetUserIP(),
					UserName = currentUser.UserName,
					Level = "Error"
				};
				_logger.LogError("{@LogDetails}", ensureOwnershipLog);
				#endregion
				throw;
            }

            Mapper.Map(socialMediaPlatformsDto, socialMediaPlatforms);
            SocialMediaPlatformsRepo.Update(socialMediaPlatforms);
            await SaveChangesAsync();

            //Return The Updated Data
            var updatedSocialMediaData = Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);

            #region Log
            updateSocialMediaLog.Timestamp = DateTime.Now;
            updateSocialMediaLog.RenderedMessage = $"Social media platforms data updated for user: {currentUser.UserName}.";
            updateSocialMediaLog.Level = "Information";
            updateSocialMediaLog.UserIP = GetUserIP();
            updateSocialMediaLog.UserName = currentUser.UserName;
            updateSocialMediaLog.AdditionalData = $"User updated their social media platforms data successfully. \nOld Data: {JsonSerializer.Serialize(oldSocialMediaData, jsonOptions)} \nNew Data: {JsonSerializer.Serialize(updatedSocialMediaData, jsonOptions)}.";
            _logger.LogInformation("{@LogDetails}", updateSocialMediaLog);
            #endregion

            return Mapper.Map<SocialMediaPlatformsDto>(socialMediaPlatforms);
        }

        #endregion
    }
}