using Services.Abstraction.Contracts.CVGenerationModule;

namespace Services.Implementations.CVGenerationModule.Factories
{
    public class CVTemplatesFactory(IEnumerable<ICVTemplate> _templates)
    {
        public ICVTemplate Resolve(string templateName)
        {
            return _templates.FirstOrDefault(t => t.TemplateName.Equals(templateName, StringComparison.CurrentCultureIgnoreCase))
                ?? throw new Exception("Template not found");
        }
    }
}