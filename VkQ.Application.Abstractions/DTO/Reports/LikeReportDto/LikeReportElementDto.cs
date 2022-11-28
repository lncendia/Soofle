using VkQ.Application.Abstractions.DTO.Reports.Base.PublicationReportBaseDto;

namespace VkQ.Application.Abstractions.DTO.Reports.LikeReportDto;

public class LikeReportElementDto : PublicationReportElementBaseDto
{
    public LikeReportElementDto(LikeReportElementBuilder builder) : base(builder)
    {
        if (builder.Likes != null) Likes.AddRange(builder.Likes);
    }

    public List<LikeDto> Likes { get; } = new();

    public new List<LikeReportElementDto> Children => base.Children.Cast<LikeReportElementDto>().ToList();
}