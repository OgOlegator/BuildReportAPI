namespace BuildReportAPI.Services.IServices
{
    public class ControlBuildCache : IControlBuildCache
    {
        private Dictionary<int, CancellationTokenSource> _controlCache = new Dictionary<int, CancellationTokenSource>();

        public void AddControl(int id, CancellationTokenSource cts)
            => _controlCache.Add(id, cts);

        public bool TryGetControl(int id, out CancellationTokenSource cts)
            => _controlCache.TryGetValue(id, out cts);
        
    }
}
