using BuildReportAPI.Services.IServices;
using Microsoft.VisualBasic;

namespace BuildReportAPI.Services
{
    public class Reporter : IReporter
    {
        public void ReportError(int id)
        {
            using (BinaryWriter file = new BinaryWriter(File.Create($"Error_{id}.txt")))
                file.Write("Report error");
        }

        public void ReportSuccess(byte[] data, int id)
        {
            using (BinaryWriter file = new BinaryWriter(File.Create($"Report_{ id }.txt")))
                file.Write(data);
        }

        public void ReportTimeout(int id)
        {
            using (BinaryWriter file = new BinaryWriter(File.Create($"Timeout_{id}.txt")))
                file.Write("Report error");
        }
    }
}
