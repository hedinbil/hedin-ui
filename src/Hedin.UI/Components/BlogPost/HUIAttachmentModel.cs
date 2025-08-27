namespace Hedin.UI.Components.BlogPost
{
    public class HUIAttachmentModel
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string BlobPath { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public string UploadedBy { get; set; }
    }
}
