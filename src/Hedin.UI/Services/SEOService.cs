using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Services
{
    /// <summary>
    /// Central SEO service for managing page metadata
    /// </summary>
    public class SEOService : ISEOService
    {
        private SEOData _currentPageSEO = new();
        private SEOData _defaultSEO = new();
        private readonly TitleService _titleService;
        
        public event Action<SEOData>? SEODataChanged;
        
        public SEOService(IOptions<TitleService> titleService)
        {
            _titleService = titleService.Value;
            InitializeDefaults();
        }
        
        private void InitializeDefaults()
        {
            _defaultSEO = new SEOData
            {
                Title = _titleService.AppTitle,
                Description = "Hedin UI - A comprehensive Blazor component library built for modern web applications. Built on top of MudBlazor with additional enterprise components.",
                Keywords = "Blazor, UI Components, MudBlazor, Web Development, .NET, C#, Component Library",
                SiteName = _titleService.AppTitle,
                OGSiteName = _titleService.AppTitle,
                OGType = "website",
                TwitterCard = "summary_large_image",
                TwitterSite = "@hedinit",
                Type = "website",
                Author = "Hedin IT"
            };
            
            // Set initial current SEO to defaults
            _currentPageSEO = _defaultSEO.Clone();
        }
        
        public void SetPageSEO(SEOData seoData)
        {
            // Merge with defaults for missing values
            _currentPageSEO = MergeWithDefaults(seoData);
            SEODataChanged?.Invoke(_currentPageSEO);
        }
        
        public SEOData GetPageSEO()
        {
            return _currentPageSEO;
        }
        
        public void SetDefaultSEO(SEOData defaultSeo)
        {
            _defaultSEO = defaultSeo;
        }
        
        public void ClearPageSEO()
        {
            _currentPageSEO = _defaultSEO.Clone();
            SEODataChanged?.Invoke(_currentPageSEO);
        }
        
        private SEOData MergeWithDefaults(SEOData pageData)
        {
            var merged = _defaultSEO.Clone();
            
            // Override with page-specific data where available
            if (!string.IsNullOrEmpty(pageData.Title))
            {
                merged.Title = pageData.Title;
                // Auto-generate full title with app name if not explicitly set
                if (string.IsNullOrEmpty(pageData.OGTitle))
                    merged.OGTitle = $"{pageData.Title} - {_titleService.AppTitle}";
                else
                    merged.OGTitle = pageData.OGTitle;
                    
                if (string.IsNullOrEmpty(pageData.TwitterTitle))
                    merged.TwitterTitle = merged.OGTitle;
                else
                    merged.TwitterTitle = pageData.TwitterTitle;
            }
            
            if (!string.IsNullOrEmpty(pageData.Description))
            {
                merged.Description = pageData.Description;
                merged.OGDescription = pageData.OGDescription ?? pageData.Description;
                merged.TwitterDescription = pageData.TwitterDescription ?? pageData.Description;
            }
            
            if (!string.IsNullOrEmpty(pageData.Keywords))
                merged.Keywords = pageData.Keywords;
                
            if (!string.IsNullOrEmpty(pageData.Author))
                merged.Author = pageData.Author;
                
            if (!string.IsNullOrEmpty(pageData.Canonical))
                merged.Canonical = pageData.Canonical;
                
            if (!string.IsNullOrEmpty(pageData.Image))
            {
                merged.Image = pageData.Image;
                merged.OGImage = pageData.OGImage ?? pageData.Image;
                merged.TwitterImage = pageData.TwitterImage ?? pageData.Image;
            }
            
            if (!string.IsNullOrEmpty(pageData.OGUrl))
                merged.OGUrl = pageData.OGUrl;
                
            if (!string.IsNullOrEmpty(pageData.OGType))
                merged.OGType = pageData.OGType;
                
            if (!string.IsNullOrEmpty(pageData.TwitterCard))
                merged.TwitterCard = pageData.TwitterCard;
                
            if (!string.IsNullOrEmpty(pageData.TwitterSite))
                merged.TwitterSite = pageData.TwitterSite;
                
            if (!string.IsNullOrEmpty(pageData.TwitterCreator))
                merged.TwitterCreator = pageData.TwitterCreator;
            
            // Merge additional meta tags
            foreach (var kvp in pageData.AdditionalMetaTags)
            {
                merged.AdditionalMetaTags[kvp.Key] = kvp.Value;
            }
            
            return merged;
        }
    }
}