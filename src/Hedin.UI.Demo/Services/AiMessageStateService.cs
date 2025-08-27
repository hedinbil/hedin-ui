namespace Hedin.UI.Demo.Services;

public class AiMessageStateService
{
    public event Action<string>? OnMessage;

    public void Send(string message)
    {
        OnMessage?.Invoke(message);
    }
}
