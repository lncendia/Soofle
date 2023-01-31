﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soofle.Application.Abstractions.Profile.DTOs;
using Soofle.Application.Abstractions.Profile.ServicesInterfaces;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.WEB.ViewModels.Profile;

namespace Soofle.WEB.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService) => _profileService = profileService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        try
        {
            var profile = await _profileService.GetAsync(id);
            var links = await _profileService.GetLinksAsync(id);
            var payments = await _profileService.GetPaymentsAsync(id);
            var stats = await _profileService.GetStatisticAsync(id);
            return View(Map(profile, links, payments, stats, email));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", "Home", new { message });
        }
    }

    private ProfileViewModel Map(ProfileDto profile, IEnumerable<LinkDto> links, IEnumerable<PaymentDto> payments,
        StatsDto stats,
        string email)
    {
        var linkViewModels = links.Select(dto =>
            new LinkViewModel(dto.Id, dto.User1, dto.User2, dto.IsConfirmed, dto.IsSender));

        var paymentViewModels = payments.Select(x =>
            new PaymentViewModel(x.Id, x.Amount, x.CreationDate, x.IsSuccessful, x.PayUrl));

        var statsViewModel = new StatsViewModel(stats.ParticipantsCount, stats.ReportsCount,
            stats.ReportsThisMonthCount, profile.SubscriptionStart, profile.SubscriptionEnd);

        var model = new ProfileViewModel(email, User.Identity!.Name!, statsViewModel,
            profile.ChatId, profile.VkLogin, linkViewModels, paymentViewModels);
        return model;
    }
}