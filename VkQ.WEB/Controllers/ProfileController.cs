using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid)!);
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        var profile = await _profileService.GetAsync(id);
        var linksEnumerable = profile.Links.Select(dto =>
            new LinksViewModel.LinkViewModel(dto.Id, dto.User1, dto.User2, dto.IsConfirmed));
        var linksViewModel = new LinksViewModel(id, HttpContext.Request.Scheme, linksEnumerable);

        var paymentsEnumerable = profile.Payments.Select(x =>
            new PaymentsViewModel.PaymentViewModel(x.Id, x.Amount, x.CreationDate, x.CompletionDate, x.IsSuccessful,
                x.PayUrl));
        var paymentsViewModel =
            new PaymentsViewModel(paymentsEnumerable, profile.SubscriptionStart, profile.SubscriptionEnd);
        var model = new ProfileViewModel(email, User.Identity!.Name!, linksViewModel, paymentsViewModel,
            profile.ParticipantsCount, profile.ReportsCount, profile.ReportsThisMonthCount);
        return View(model);
    }
}