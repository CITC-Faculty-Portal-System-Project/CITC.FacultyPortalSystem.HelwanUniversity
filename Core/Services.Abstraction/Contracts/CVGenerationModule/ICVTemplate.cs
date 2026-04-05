using Shared.Dtos.CVGenerationModule;

namespace Services.Abstraction.Contracts.CVGenerationModule
{
    public interface ICVTemplate
    {
        string TemplateName { get; }
        byte[] GeneratePdf(CVResponseDTO cv);
        string GenerateHtml(CVResponseDTO cv);
    }
}
