using VkNet.Utils.AntiCaptcha;

namespace Soofle.Infrastructure.VkRequests.AntiCaptcha;

public class CaptchaSolverAdapter : ICaptchaSolver
{
    private long _id;
    private readonly Soofle.Application.Abstractions.VkRequests.ServicesInterfaces.ICaptchaSolver _solver;

    public CaptchaSolverAdapter(Soofle.Application.Abstractions.VkRequests.ServicesInterfaces.ICaptchaSolver solver) => _solver = solver;

    public string Solve(string url)
    {
        var result = _solver.SolveAsync(url).Result;
        _id = result.id;
        return result.response;
    }

    public void CaptchaIsFalse() => _solver.CaptchaIsFalseAsync(_id);
}