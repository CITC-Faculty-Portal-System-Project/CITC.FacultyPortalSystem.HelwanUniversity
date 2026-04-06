using Shared.Dtos.CVGenerationModule;

namespace Services.Abstraction.Contracts.CVGenerationModule
{
    public interface ICVTemplate
    {
        string TemplateName { get; }
        byte[] GeneratePdf(CVResponseDTO cv);
        Task <string> GenerateHtml(CVResponseDTO cv);
    }
}
