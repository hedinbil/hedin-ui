using MudBlazor;

namespace Hedin.UI
{
    public class HUIInformationMessageModel
    {
        public int Id { get; }
        public Severity Severity { get; }
        public DateTime Date { get; }
        public string Header { get; }
        public string Message { get; }
        public bool Read { get; private set; } = false;
        public bool RequireReadVerification;
        public bool Pinned { get; private set; }
        public bool Archived { get; private set; }
        public object? Tag { get; private set; }

        public HUIInformationMessageModel(int id, Severity severity, DateTime date, string header, string message, bool read, bool pinned, bool requireReadVerification, bool archived, object? tag = null)
        {
            Id = id;
            Severity = severity;
            Date = date;
            Header = header;
            Message = message;
            Read = read;
            Pinned = pinned;
            RequireReadVerification = requireReadVerification;
            Archived = archived;
            Tag = tag;
        }

        public void MarkAsRead() => Read = true;
        public void Archive() => Archived = true;
        public void SetAsPinned() => Pinned = true;
    }
}
