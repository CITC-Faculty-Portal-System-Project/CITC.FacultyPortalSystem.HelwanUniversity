namespace Shared.Dtos
{
    public record DepartmentResposneDTO
    {
        public int Id { get; set; }

        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;

    }
}
