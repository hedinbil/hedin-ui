using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Services
{
    public interface ISEOService
    {
        /// <summary>
        /// Sets SEO metadata for the current page
        /// </summary>
        void SetPageSEO(SEOData seoData);
        
        /// <summary>
        /// Gets current SEO data for the page
        /// </summary>
        SEOData GetPageSEO();
        
        /// <summary>
        /// Sets default SEO data for the application
        /// </summary>
        void SetDefaultSEO(SEOData defaultSeo);
        
        /// <summary>
        /// Clears current page SEO data
        /// </summary>
        void ClearPageSEO();
        
        /// <summary>
        /// Event raised when SEO data changes
        /// </summary>
        event Action<SEOData>? SEODataChanged;
    }
}