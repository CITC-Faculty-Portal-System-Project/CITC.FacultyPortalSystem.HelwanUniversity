using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule
{
    public record ReviewArticleUpdateDto
    {
        public string TitleOfArticle { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public DateOnly ReviewingDate { get; set; }
        public string? Description { get; set; }
    }
}
