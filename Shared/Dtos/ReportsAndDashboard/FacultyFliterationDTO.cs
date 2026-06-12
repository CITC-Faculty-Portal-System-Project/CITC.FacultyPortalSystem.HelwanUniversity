namespace Shared.Dtos.ReportsAndDashboard
{
    public record FacultyFliterationDTO
    {
        public int FacultyId { get; set; }
        public List<int>? DepartmentsIds { get; set; }
    }
}
