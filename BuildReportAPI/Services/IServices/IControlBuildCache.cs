namespace BuildReportAPI.Services.IServices
{
    public interface IControlBuildCache
    {

        void AddControl(int id, CancellationTokenSource cts);

        bool TryGetControl(int id, out CancellationTokenSource cts);

    }
}
