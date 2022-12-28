using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkQ.Application.Abstractions.Users.DTOs;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.Manage;
using VkQ.WEB.ViewModels.Profile;

namespace VkQ.WEB.Controllers;

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
            return View(Map(profile, email, id));
        }
        catch (Exception e)
        {
            var message = e switch
            {
                UserNotFoundException => "Пользователь не найден",
                _ => "Произошла ошибка"
            };
            return RedirectToAction("Index", "Home", new {message});
        }
    }

    private ProfileViewModel Map(ProfileDto profile, string email, Guid id)
    {
        var linksEnumerable = profile.Links.Select(dto =>
            new LinkViewModel(dto.Id, dto.User1, dto.User2, dto.IsConfirmed));
        var linksViewModel = new LinksViewModel(id, HttpContext.Request.Scheme, linksEnumerable);

        var paymentsEnumerable = profile.Payments.Select(x =>
            new PaymentViewModel(x.Id, x.Amount, x.CreationDate, x.CompletionDate, x.IsSuccessful, x.PayUrl));
        var paymentsViewModel =
            new PaymentsViewModel(paymentsEnumerable, profile.SubscriptionStart, profile.SubscriptionEnd);
        var stats = new StatsViewModel(profile.Stats.ParticipantsCount, profile.Stats.ReportsCount,
            profile.Stats.ReportsThisMonthCount);
        var vk = profile.Vk == null
            ? null
            : new VkViewModel(profile.Vk.Login, profile.Vk.Password, profile.Vk.IsActive);
        var model = new ProfileViewModel(email, User.Identity!.Name!, linksViewModel, paymentsViewModel, stats,
            profile.ChatId, vk);
        return model;
    }
}