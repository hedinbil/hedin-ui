namespace Hedin.UI.Demo.Configuration
{
    public class ExternalResourceStatus
    {
        public bool Offline { get; private set; }
        public void SetOffline() => Offline = true;
    }
}
