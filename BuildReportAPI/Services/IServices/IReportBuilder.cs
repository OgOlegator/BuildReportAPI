namespace BuildReportAPI.Services.IServices
{
    public interface IReportBuilder
    {

        public byte[] Build(CancellationToken token);

    }
}
