using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VkQ.WEB.ViewModels.Reports;

namespace VkQ.WEB.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly ReportService _service;
        private readonly ReportCreatorService _reportCreatorService;
        private readonly TimeService _timeService;

        public ReportsController(ApplicationDbContext db,
            UserManager<User> userManager, ReportService service, ReportCreatorService reportCreatorService,
            TimeService timeService)
        {
            _db = db;
            _userManager = userManager;
            _service = service;
            _reportCreatorService = reportCreatorService;
            _timeService = timeService;
        }

        [HttpGet]
        public async Task<IActionResult> SelectChat(string message)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            var user = await _userManager.GetUserAsync(User);
            return View(_db.Instagrams.Where(instagram => instagram.User == user && instagram.IsActivated).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string url)
        {
            var stream = await _service.GetImageAsync(url);
            string fileType = "application/jpg";
            string fileName = "img.jpg";
            return File(stream, fileType, fileName);
        }

        [HttpGet]
        public async Task<IActionResult> Reports(int id, string message, int page = 1)
        {
            if (!string.IsNullOrEmpty(message)) ViewData["Alert"] = message;
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            var instagram = _db.Instagrams.FirstOrDefault(instagram1 =>
                instagram1.User == user && instagram1.Id == id && instagram1.IsActivated);
            if (instagram == null)
                return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});

            var model = new ReportsViewModel
            {
                Id = id,
                Count = _service.GetReportsCount(instagram),
                User = user
            };

            if ((page - 1) * 30 > model.Count) page = 1;
            model.Page = page;
            model.Reports = _service.GetReports(instagram, page);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LikeReport(MediaReportViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            var report = _db.Reports.Include(report1 => report1.Instagrams)
                .FirstOrDefault(report1 =>
                    report1.Id == model.Id && report1.Type == ReportType.Likes &&
                    report1.Instagrams.Any(instagram1 => instagram1.User == user));
            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});

            model.MediaReports = _service.GetMediaReports(model);
            model.Report = report;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CommentReport(MediaReportViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            var report = _db.Reports.Include(report1 => report1.Instagrams)
                .FirstOrDefault(report1 =>
                    report1.Id == model.Id && report1.Type == ReportType.Comments &&
                    report1.Instagrams.Any(instagram1 => instagram1.User == user));
            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});

            model.MediaReports = _service.GetMediaReports(model);
            model.Report = report;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ThievesReport(ThieveOrTagInfoReportViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            var report = _db.Reports.Include(report1 => report1.Instagrams)
                .FirstOrDefault(report1 =>
                    report1.Id == model.Id && report1.Type == ReportType.Thieves &&
                    report1.Instagrams.Any(instagram1 => instagram1.User == user));
            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});

            model.Reports = _service.GetThieveOrTagInfoReports(model);
            model.Report = report;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TagInfoReport(ThieveOrTagInfoReportViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            var report = _db.Reports.Include(report1 => report1.Instagrams).FirstOrDefault(report1 =>
                report1.Id == model.Id && report1.Type == ReportType.TagInfo &&
                report1.Instagrams.Any(instagram1 => instagram1.User == user));
            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});

            model.Reports = _service.GetThieveOrTagInfoReports(model);
            model.Report = report;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ParticipantsReport(ParticipantReportViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            var report = _db.Reports.Include(report1 => report1.Instagrams).FirstOrDefault(report1 =>
                report1.Id == model.Id && report1.Type == ReportType.Participants &&
                report1.Instagrams.Any(instagram1 => instagram1.User == user));
            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});

            model.ParticipantReports = _service.GetParticipantReports(model);
            model.Report = report;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> StartParticipantReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            var report = new Report
            {
                Instagrams = new List<Instagram> {instagram},
                CreationDate = DateTime.UtcNow,
                Type = ReportType.Participants,
                User = user
            };
            _db.Add(report);
            await _db.SaveChangesAsync();
            report.JobId = _reportCreatorService.CreateParticipantReport(report.Id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> StartThievesReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);

            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            return View(new StartThievesOrTagInfoReportViewModel {Id = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartThievesReport(StartThievesOrTagInfoReportViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            var report = new Report
            {
                Instagrams = new List<Instagram> {instagram},
                CreationDate = DateTime.UtcNow,
                Type = ReportType.Thieves,
                Tag = model.Tag,
                User = user,
                PublicationsType = model.Publications
            };
            _db.Add(report);
            await _db.SaveChangesAsync();
            report.JobId = _reportCreatorService.CreateThievesReport(report.Id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {model.Id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> StartTagInfoReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);

            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartTagInfoReport(StartThievesOrTagInfoReportViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == model.Id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            var report = new Report
            {
                Instagrams = new List<Instagram> {instagram},
                CreationDate = DateTime.UtcNow,
                Type = ReportType.TagInfo,
                Tag = model.Tag,
                User = user,
                CheckTagInfoReportAfterStart = model.CheckAfterStart,
                PublicationsType = model.Publications
            };
            _db.Add(report);
            await _db.SaveChangesAsync();
            report.JobId = _reportCreatorService.CreateTagInfoReport(report.Id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {model.Id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> StartTagInfoReportCheck(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var report =
                _db.Reports.Include(report1 => report1.Instagrams).FirstOrDefault(report1 =>
                    report1.Id == id && report1.User == user && report1.Type == ReportType.TagInfo);
            if (report == null) return RedirectToAction("SelectChat", new {message = "Отчёт не найден."});
            if (!report.IsCompleted)
                return RedirectToAction("Reports",
                    new
                    {
                        report.Instagrams.First().Id,
                        message = "Вы не можете запустить проверку постов для этого отчёта."
                    });

            report.JobId = _reportCreatorService.CreateTagInfoReportCheck(report.Id);
            report.IsCompleted = false;
            report.IsSucceeded = false;
            report.Message = null;
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {report.Instagrams.First().Id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> ContinueLikeReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var report = _db.Reports.Include(report1 => report1.Instagrams).Include(report1 => report1.User)
                .FirstOrDefault(report1 =>
                    report1.Id == id && report1.User == user && report1.IsCompleted && report1.MediaReports.Any() &&
                    report1.Type == ReportType.Likes && report1.PublicationsType != Publications.All &&
                    report1.Count != 0);

            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});
            report.JobId = _reportCreatorService.CreateContinueLikesReport(report.Id);
            report.IsCompleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {report.Instagrams.First().Id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> ContinueCommentReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var report = _db.Reports.Include(report1 => report1.Instagrams).Include(report1 => report1.User)
                .FirstOrDefault(report1 =>
                    report1.Id == id && report1.User == user && report1.IsCompleted && report1.MediaReports.Any() &&
                    report1.Type == ReportType.Comments && report1.PublicationsType != Publications.All &&
                    report1.Count != 0);

            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});
            report.JobId = _reportCreatorService.CreateContinueCommentReport(report.Id);
            report.IsCompleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {report.Instagrams.First().Id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> StartLikeReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);

            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            return View(new StartLikeOrCommentReportViewModel
            {
                AnalysisAuto = new LikeOrCommentReportAutoViewModel {Id = id},
                AnalysisLinks = new LikeOrCommentReportLinksViewModel {Id = id},
                CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartLikeReportAuto(StartLikeOrCommentReportViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == model.AnalysisAuto.Id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            if (!ModelState.IsValid)
            {
                model.AnalysisLinks = new LikeOrCommentReportLinksViewModel {Id = model.AnalysisAuto.Id};
                model.CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram);
                return View("StartLikeReport", model);
            }

            if (model.AnalysisAuto.StartDate.HasValue && model.AnalysisAuto.EndDate.HasValue &&
                model.AnalysisAuto.StartDate > model.AnalysisAuto.EndDate)
            {
                ModelState.AddModelError("", "Время указано неверно.");
                model.AnalysisLinks = new LikeOrCommentReportLinksViewModel {Id = model.AnalysisAuto.Id};
                model.CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram);
                return View("StartLikeReport", model);
            }

            var report = new Report
            {
                User = user,
                Instagrams = new List<Instagram> {instagram},
                CreationDate = DateTime.UtcNow,
                Type = ReportType.Likes,
                Tag = model.AnalysisAuto.Tag,
                Count = model.AnalysisAuto.Count,
                AllParticipants = !model.AnalysisAuto.NotAllParticipants,
                Api = model.AnalysisAuto.Api,
                PublicationsType = model.AnalysisAuto.Publications,
            };
            var timeZone = _timeService.GetTimeZoneInfo(user);
            if (model.AnalysisAuto.StartDate.HasValue)
                report.StartDate = TimeZoneInfo.ConvertTimeToUtc(model.AnalysisAuto.StartDate.Value, timeZone);
            if (model.AnalysisAuto.EndDate.HasValue)
                report.EndDate = TimeZoneInfo.ConvertTimeToUtc(model.AnalysisAuto.EndDate.Value, timeZone);
            if (model.AnalysisAuto.CommonAccounts != null)
            {
                model.AnalysisAuto.CommonAccounts.Remove(instagram.Id);
                report.Instagrams.AddRange(_service.GetCommonInstagrams(model.AnalysisAuto.CommonAccounts, user));
            }

            _db.Add(report);
            await _db.SaveChangesAsync();
            report.JobId = _reportCreatorService.CreateLikeReport(report.Id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {model.AnalysisAuto.Id, message = "Отчет успешно создан."});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartLikeReportLinks(StartLikeOrCommentReportViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == model.AnalysisLinks.Id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            if (!ModelState.IsValid)
            {
                model.AnalysisAuto = new LikeOrCommentReportAutoViewModel {Id = model.AnalysisLinks.Id};
                model.CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram);
                return View("StartLikeReport", model);
            }

            var report = new Report
            {
                User = user,
                Instagrams = new List<Instagram> {instagram},
                CreationDate = DateTime.UtcNow,
                Type = ReportType.Likes,
                Tag = model.AnalysisLinks.Tag,
                AllParticipants = !model.AnalysisLinks.NotAllParticipants,
                Api = model.AnalysisLinks.Api,
                PublicationsType = model.AnalysisLinks.Publications,
                Links = model.AnalysisLinks.Links
            };
            if (model.AnalysisLinks.CommonAccounts != null)
            {
                model.AnalysisLinks.CommonAccounts.Remove(instagram.Id);
                report.Instagrams.AddRange(_service.GetCommonInstagrams(model.AnalysisLinks.CommonAccounts, user));
            }

            _db.Add(report);
            await _db.SaveChangesAsync();
            report.JobId = _reportCreatorService.CreateLikeReport(report.Id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {model.AnalysisLinks.Id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> StartCommentReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный ID."});
            var user = await _userManager.GetUserAsync(User);
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            return View(new StartLikeOrCommentReportViewModel()
            {
                AnalysisAuto = new LikeOrCommentReportAutoViewModel {Id = id},
                AnalysisLinks = new LikeOrCommentReportLinksViewModel {Id = id},
                CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartCommentReportAuto(StartLikeOrCommentReportViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == model.AnalysisAuto.Id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            if (!ModelState.IsValid)
            {
                model.AnalysisLinks = new LikeOrCommentReportLinksViewModel {Id = model.AnalysisAuto.Id};
                model.CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram);
                return View("StartCommentReport", model);
            }

            if (model.AnalysisAuto.StartDate.HasValue && model.AnalysisAuto.EndDate.HasValue &&
                model.AnalysisAuto.StartDate > model.AnalysisAuto.EndDate)
            {
                ModelState.AddModelError("", "Время указано неверно.");
                model.AnalysisLinks = new LikeOrCommentReportLinksViewModel {Id = model.AnalysisAuto.Id};
                model.CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram);
                return View("StartLikeReport", model);
            }

            var report = new Report
            {
                User = user,
                Instagrams = new List<Instagram> {instagram},
                CreationDate = DateTime.UtcNow,
                Type = ReportType.Comments,
                Tag = model.AnalysisAuto.Tag,
                Count = model.AnalysisAuto.Count,
                AllParticipants = !model.AnalysisAuto.NotAllParticipants,
                Api = model.AnalysisAuto.Api,
                PublicationsType = model.AnalysisAuto.Publications,
            };
            var timeZone = _timeService.GetTimeZoneInfo(user);
            if (model.AnalysisAuto.StartDate.HasValue)
                report.StartDate = TimeZoneInfo.ConvertTimeToUtc(model.AnalysisAuto.StartDate.Value, timeZone);
            if (model.AnalysisAuto.EndDate.HasValue)
                report.EndDate = TimeZoneInfo.ConvertTimeToUtc(model.AnalysisAuto.EndDate.Value, timeZone);

            if (model.AnalysisAuto.CommonAccounts != null)
            {
                model.AnalysisAuto.CommonAccounts.Remove(instagram.Id);
                report.Instagrams.AddRange(_service.GetCommonInstagrams(model.AnalysisAuto.CommonAccounts, user));
            }

            _db.Add(report);
            await _db.SaveChangesAsync();
            report.JobId = _reportCreatorService.CreateCommentReport(report.Id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {model.AnalysisAuto.Id, message = "Отчет успешно создан."});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartCommentReportLinks(StartLikeOrCommentReportViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var instagram =
                _db.Instagrams.FirstOrDefault(instagram1 =>
                    instagram1.Id == model.AnalysisLinks.Id && instagram1.User == user && instagram1.IsActivated);
            if (instagram == null) return RedirectToAction("SelectChat", new {message = "Аккаунт не найден."});
            if (!ModelState.IsValid)
            {
                model.AnalysisAuto = new LikeOrCommentReportAutoViewModel() {Id = model.AnalysisLinks.Id};
                model.CommonInstagrams = _service.GetCommonInstagramsSelectList(user, instagram);
                return View("StartCommentReport", model);
            }

            var report = new Report
            {
                User = user,
                Instagrams = new List<Instagram> {instagram},
                CreationDate = DateTime.UtcNow,
                Type = ReportType.Comments,
                Tag = model.AnalysisLinks.Tag,
                AllParticipants = !model.AnalysisLinks.NotAllParticipants,
                Api = model.AnalysisLinks.Api,
                PublicationsType = model.AnalysisLinks.Publications,
                Links = model.AnalysisLinks.Links
            };
            if (model.AnalysisLinks.CommonAccounts != null)
            {
                model.AnalysisLinks.CommonAccounts.Remove(instagram.Id);
                report.Instagrams.AddRange(_service.GetCommonInstagrams(model.AnalysisLinks.CommonAccounts, user));
            }

            _db.Add(report);
            await _db.SaveChangesAsync();
            report.JobId = _reportCreatorService.CreateCommentReport(report.Id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reports", new {model.AnalysisLinks.Id, message = "Отчет успешно создан."});
        }

        [HttpGet]
        public async Task<IActionResult> RestartReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный Id."});
            var user = await _userManager.GetUserAsync(User);

            if (!user.EndOfSubscribe.HasValue || user.EndOfSubscribe < DateTime.UtcNow)
                return RedirectToAction("MyPayments", "Payment");
            var report = _db.Reports.Include(report1 => report1.Instagrams).Include(report1 => report1.User)
                .FirstOrDefault(report1 =>
                    report1.Id == id && report1.User == user && report1.IsCompleted);

            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});
            var success = await _reportCreatorService.RestartReport(report);
            return RedirectToAction("Reports",
                new
                {
                    report.Instagrams.First().Id,
                    message = success.Succeeded
                        ? "Отчет успешно создан."
                        : $"Не удалось создать отчёт ({success.Message})."
                });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReport(int id)
        {
            if (id <= 0) return RedirectToAction("SelectChat", new {message = "Неверный Id."});
            var user = await _userManager.GetUserAsync(User);
            var report = _db.Reports.Include(report1 => report1.Instagrams)
                .Include(report1 => report1.ParticipantsReports)
                .ThenInclude(participantReport => participantReport.UserPosts).Include(report1 => report1.MediaReports)
                .ThenInclude(mediaReport => mediaReport.UserPosts).Include(report => report.MediaReports)
                .ThenInclude(report => report.Values).FirstOrDefault(report1 =>
                    report1.Id == id && report1.User == user);

            if (report == null)
                return RedirectToAction("SelectChat", new {message = "Отчет не найден."});
            var instagramId = report.Instagrams.First().Id;
            var stop = _reportCreatorService.StopReport(report);
            if (!stop)
                return RedirectToAction("Reports",
                    new
                    {
                        id = instagramId,
                        message = "Не удалось остановить отчёт."
                    });
            var result = _service.DeleteReport(report);
            return RedirectToAction("Reports",
                new
                {
                    id = instagramId,
                    message = result.Succeeded && result.Value
                        ? "Отчёт успешно удалён."
                        : $"Не удалось удалить отчёт ({result.Message})."
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReports(List<int> ids)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("SelectChat");
            var user = await _userManager.GetUserAsync(User);
            var reports = _db.Reports.Include(report1 => report1.Instagrams)
                .Include(report1 => report1.ParticipantsReports)
                .ThenInclude(participantReport => participantReport.UserPosts).Include(report1 => report1.MediaReports)
                .ThenInclude(mediaReport => mediaReport.UserPosts).Include(report => report.MediaReports)
                .ThenInclude(report => report.Values)
                .Where(report1 => ids.Contains(report1.Id) && report1.User == user).ToList();

            if (!reports.Any())
                return RedirectToAction("SelectChat", new {message = "Не удалось получить необходимые отчёты."});

            var instagramId = reports.First().Instagrams.First().Id;
            reports = reports.Where(report => _reportCreatorService.StopReport(report)).ToList();
            reports.ForEach(report => _service.DeleteReport(report));
            return RedirectToAction("Reports",
                new
                {
                    id = instagramId,
                    message = "Отчёты удалены."
                });
        }
    }
}