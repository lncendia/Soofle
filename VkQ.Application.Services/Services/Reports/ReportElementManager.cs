using VkQ.Application.Abstractions.Elements.DTOs;
using VkQ.Application.Abstractions.Elements.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports;

public class ReportElementManager : IReportElementManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElementMapper _elementMapper;

    public ReportElementManager(IUnitOfWork unitOfWork, IElementMapper elementMapper)
    {
        _unitOfWork = unitOfWork;
        _elementMapper = elementMapper;
    }


    public async Task<List<LikeReportElementDto>> GetLikeReportElementsAsync(Guid userId, Guid reportId,
        PublicationElementSearchQuery query)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        var elements = report.Elements.GroupBy(x => x.Parent).ToList();
        var result = new List<LikeReportElement>();
        foreach (var element in elements.First(x => x.Key == null).Skip((query.Page - 1) * 50).Take(50))
        {
            var children = elements.FirstOrDefault(x => x.Key == element)?.ToList();
            if (query.HasChildren.HasValue)
            {
                if (query.HasChildren.Value && children == null) continue;
                if (!query.HasChildren.Value && children != null) continue;
            }

            var valid = IsPublicationElementValid(element, query);
            var validChildren = children?.Where(x => IsPublicationElementValid(x, query));
            if (!valid && validChildren?.Any() != true) continue;
            result.Add(element);
            result.AddRange(validChildren ?? new List<LikeReportElement>());
        }

        return _elementMapper.LikeReportElementMapper.Value.Map(result);
    }

    private static bool IsPublicationElementValid(PublicationReportElement element, PublicationElementSearchQuery query)
    {
        if (query.Succeeded.HasValue && element.IsAccepted != query.Succeeded.Value)
            return false;

        if (query.Vip.HasValue && element.Vip != query.Vip.Value)
            return false;

        if (!string.IsNullOrEmpty(query.Name) && !element.Name.Contains(query.Name))
            return false;

        if (!string.IsNullOrEmpty(query.LikeChatName) && !element.LikeChatName.Contains(query.LikeChatName))
            return false;

        return true;
    }

    private static bool IsParticipantElementValid(ParticipantReportElement element, ParticipantElementSearchQuery query)
    {
        if (query.ElementType.HasValue && element.Type != query.ElementType.Value)
            return false;

        if (!string.IsNullOrEmpty(query.Name) && !element.Name.Contains(query.Name))
            return false;

        if (!string.IsNullOrEmpty(element.NewName) && !string.IsNullOrEmpty(query.NewName) &&
            !element.NewName.Contains(query.NewName))
            return false;

        return true;
    }

    public async Task<List<ParticipantReportElementDto>> GetParticipantReportElementsAsync(Guid userId, Guid reportId,
        ParticipantElementSearchQuery query)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        var elements = report.Participants.GroupBy(x => x.Parent).ToList();
        var result = new List<ParticipantReportElement>();
        foreach (var element in elements.First(x => x.Key == null).Skip((query.Page - 1) * 50).Take(50))
        {
            var children = elements.FirstOrDefault(x => x.Key == element)?.ToList();
            if (query.HasChildren.HasValue)
            {
                if (query.HasChildren.Value && children == null) continue;
                if (!query.HasChildren.Value && children != null) continue;
            }

            var valid = IsParticipantElementValid(element, query);
            var validChildren = children?.Where(x => IsParticipantElementValid(x, query));
            if (!valid && validChildren?.Any() != true) continue;
            result.Add(element);
            result.AddRange(validChildren ?? new List<ParticipantReportElement>());
        }

        return _elementMapper.ParticipantReportElementMapper.Value.Map(result);
    }
}