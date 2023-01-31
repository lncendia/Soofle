﻿namespace Soofle.Application.Abstractions.ReportsManagement.DTOs;

public class PublicationReportCreateDto
{
    public PublicationReportCreateDto(Guid userId, string hashtag, bool allParticipants,
        DateTimeOffset? searchStartDate, List<Guid>? coAuthors)
    {
        UserId = userId;
        Hashtag = hashtag;
        SearchStartDate = searchStartDate;
        CoAuthors = coAuthors;
        AllParticipants = allParticipants;
    }

    public Guid UserId { get; }
    public string Hashtag { get; }
    public bool AllParticipants { get; }
    public DateTimeOffset? SearchStartDate { get; }
    public List<Guid>? CoAuthors { get; }
}