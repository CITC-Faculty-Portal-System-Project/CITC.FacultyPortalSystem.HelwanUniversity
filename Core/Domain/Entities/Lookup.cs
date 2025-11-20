namespace Domain.Entities
{
    public class Lookup : BaseEntity<Guid>
    {

        public string Type { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty ;

        public string ValueAr { get; set; } = string.Empty;
        public string ValueEn {  get; set; } = string.Empty ;

        public int SortOrder { get; set; } = 0;
    }
}
