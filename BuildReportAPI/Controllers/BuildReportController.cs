using BuildReportAPI.Services;
using BuildReportAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BuildReportAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildReportController : ControllerBase
    {
        private readonly IReportBuilder _reportBuilder;
        private readonly IReporter _reporter;
        private readonly IReportCounter _reportCounter;
        private readonly IControlBuildCache _controlCache;

        public BuildReportController(IReportBuilder reportBuilder, IReporter reporter, IReportCounter reportCounter, IControlBuildCache controlCache)
        {
            _reportBuilder = reportBuilder;
            _reporter = reporter;
            _reportCounter = reportCounter;
            _controlCache = controlCache;
        }

        [HttpGet(Name = "Build")]
        public int Build()
        {
            var id = _reportCounter.GetNextId();

            var cts = new CancellationTokenSource();
            var token = cts.Token;

            _controlCache.AddControl(id, cts);

            var buildTask = Task.Run(() =>
            {
                var report = _reportBuilder.Build();

                if(token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();

                return report;
            }, token);

            buildTask.ContinueWith(antecedent => { _reporter.ReportSuccess(antecedent.Result, id); }, 
            TaskContinuationOptions.OnlyOnRanToCompletion);

            buildTask.ContinueWith(antecedent => { _reporter.ReportError(id); },
            TaskContinuationOptions.OnlyOnFaulted);

            buildTask.ContinueWith(antecedent => { _reporter.ReportTimeout(id); },
            TaskContinuationOptions.OnlyOnCanceled);

            return id;
        }

        [HttpPost(Name = "Stop")]
        public void Stop(int reportId)
        {
            if (!_controlCache.TryGetControl(reportId, out var cts))
                return;

            cts.Cancel();
        }
        
    }
}
