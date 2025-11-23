namespace Services.Specifications.LookUpItems
{
    internal class LookUpItemTypeSpecification : BaseSpecifications<Lookup , Guid>
    {
        public LookUpItemTypeSpecification(string type) : base(cd => cd.Type != null && cd.Type == type)
        {
        }
    }
}
