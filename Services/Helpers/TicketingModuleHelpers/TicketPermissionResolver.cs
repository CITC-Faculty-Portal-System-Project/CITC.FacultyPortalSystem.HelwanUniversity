using Domain.Enums;

namespace Services.Helpers.TicketingModuleHelpers
{
    public static class TicketPermissionResolver
    {
        public static string GetModuleName(TicketType ticketType)
        {
            return ticketType switch
            {
                TicketType.AccountIssue => "UserAccount",

                TicketType.FacultyMemberPersonalDataIssue => "FacultyMemberData",
                TicketType.FacultyMemberContributionsDataIssue => "FacultyMemberContributionsData",
                TicketType.FacultyMemberExperincesDataIssue => "FacultyMemberExperincesData",
                TicketType.FacultyMemberHigherStudiesDataIssue => "FacultyMemberHigherStudiesData",
                TicketType.FacultyMemberMissionsDataIssue => "FacultyMemberMissionsData",
                TicketType.FacultyMemberPrizesDataIssue => "FacultyMemberPrizesData",
                TicketType.FacultyMemberProjectsAndComiteesDataIssue => "FacultyMemberProjectsAndComiteesData",
                TicketType.FacultyMemberResearchesDataIssue => "FacultyMemberResearchesData",
                TicketType.FacultyMemberScientificProgressionDataIssue => "FacultyMemberScientificProgressionData",
                TicketType.FacultyMemberWritingsDataIssue => "FacultyMemberWritingsData",

                _ => throw new InvalidOperationException($"No permission mapping found for ticket type {ticketType}")
            };
        }

        public static string[] GetFullCrudPermissions(string moduleName)
        {
            return
            [
                $"{moduleName}.Create",
                $"{moduleName}.Read",
                $"{moduleName}.Update",
                $"{moduleName}.Delete"
            ];
        }

        public static List<string> GetRequiredPermissionsForAssignment(TicketType ticketType)
        {
            var moduleName = GetModuleName(ticketType);

            return
            [
                .. GetFullCrudPermissions(moduleName)
            ];
        }
    }
}
