namespace Hedin.UI.Services
{

    public interface IStateService
    {
        public void SaveState<T>(string key, T state);
        public T GetState<T>(string key);
        public void ClearState(string key);

    }

    public class StateService : IStateService
    {
        private Dictionary<string, object> _pageStates = new Dictionary<string, object>();
        private Stack<string> _navigationHistory = new Stack<string>();

        public void SaveState<T>(string key, T state)
        {
            _pageStates[key] = state;
        }

        public T GetState<T>(string key)
        {
            if (_pageStates.TryGetValue(key, out var state))
            {
                return (T)state;
            }
            return default;
        }

        public void ClearState(string key)
        {
            _pageStates.Remove(key);
        }
    }
}
