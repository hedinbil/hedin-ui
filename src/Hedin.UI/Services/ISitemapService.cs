using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Services
{
    /// <summary>
    /// Service for generating sitemap.xml dynamically
    /// </summary>
    public interface ISitemapService
    {
        /// <summary>
        /// Generates sitemap XML content
        /// </summary>
        Task<string> GenerateSitemapXmlAsync();
        
        /// <summary>
        /// Gets all discoverable routes for sitemap generation
        /// </summary>
        Task<List<SitemapEntry>> GetSitemapEntriesAsync();
    }
    
    /// <summary>
    /// Represents an entry in the sitemap
    /// </summary>
    public class SitemapEntry
    {
        public string Url { get; set; } = string.Empty;
        public DateTime? LastModified { get; set; }
        public SitemapChangeFrequency? ChangeFrequency { get; set; }
        public double? Priority { get; set; }
        
        public SitemapEntry(string url)
        {
            Url = url;
        }
        
        public SitemapEntry(string url, DateTime lastModified, SitemapChangeFrequency changeFrequency, double priority)
        {
            Url = url;
            LastModified = lastModified;
            ChangeFrequency = changeFrequency;
            Priority = priority;
        }
    }
    
    /// <summary>
    /// Sitemap change frequency values
    /// </summary>
    public enum SitemapChangeFrequency
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }
}