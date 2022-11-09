using VkNet.Utils.AntiCaptcha;

namespace VkQ.Infrastructure.Publications.AntiCaptcha;

public class CaptchaSolver : ICaptchaSolver
{
    private long _id;
    private readonly Infrastructure.AntiCaptcha.Interfaces.ICaptchaSolver _solver;

    public CaptchaSolver(Infrastructure.AntiCaptcha.Interfaces.ICaptchaSolver solver)
    {
        _solver = solver;
    }

    public string Solve(string url)
    {
        var result = _solver.SolveAsync(url).Result;
        _id = result.id;
        return result.response;
    }

    public void CaptchaIsFalse() => _solver.CaptchaIsFalseAsync(_id);
}