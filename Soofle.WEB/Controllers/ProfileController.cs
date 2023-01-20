using System.Security.Claims;
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
            return View(Map(profile, email));
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

    private ProfileViewModel Map(ProfileDto profile, string email)
    {
        var links = profile.Links.Select(dto =>
            new LinkViewModel(dto.Id, dto.User1, dto.User2, dto.IsConfirmed, dto.IsSender));

        var payments = profile.Payments.Select(x =>
            new PaymentViewModel(x.Id, x.Amount, x.CreationDate, x.IsSuccessful, x.PayUrl));
        
        var stats = new StatsViewModel(profile.Stats.ParticipantsCount, profile.Stats.ReportsCount,
            profile.Stats.ReportsThisMonthCount, profile.SubscriptionStart, profile.SubscriptionEnd);
        
        var vk = profile.Vk == null
            ? null
            : new VkViewModel(profile.Vk.Login, profile.Vk.Password, profile.Vk.IsActive);
        var model = new ProfileViewModel(email, User.Identity!.Name!,stats,
            profile.ChatId, vk, links, payments);
        return model;
    }
}