using BuildReportAPI.Services.IServices;

namespace BuildReportAPI.Services
{
    public class ReportBuilder : IReportBuilder
    {
        public byte[] Build()
        {
            var random = new Random();
            var timeBuild = random.Next(5, 45);

            var report = new byte[timeBuild];

            for (var i = 0; i < timeBuild; i++)
            {
                Thread.Sleep(1000);
                report[i] = (byte)i;

                if (i == 3 && random.Next(0, 99) < 20)
                    throw new Exception("Report failed");
            }

            return report;
        }
    }
}
