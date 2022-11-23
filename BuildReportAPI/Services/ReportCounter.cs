using BuildReportAPI.Services.IServices;

namespace BuildReportAPI.Services
{
    public class ReportCounter : IReportCounter
    {
        private int _reportId;

        public int GetNextId()
        {
            var id = _reportId;
            _reportId += 1;

            return id;
        }
    }
}
