# SEO Implementation for Hedin UI

## Overview

This document outlines the comprehensive SEO implementation added to Hedin UI, covering meta tags, Open Graph support, Twitter Cards, dynamic sitemap generation, and centralized SEO management.

## Features Implemented

### 1. Centralized SEO Service

- **ISEOService / SEOService**: Central service for managing page metadata
- **SEOData Model**: Comprehensive data structure for SEO metadata
- **Dynamic SEO Updates**: Real-time SEO data updates across the application

### 2. SEO Components

- **SEOHead Component**: Renders all SEO meta tags dynamically
- **Enhanced App.razor**: Integrated SEO head component with server-side rendering

### 3. Meta Tags Support

- **Basic SEO**: Title, description, keywords, author, canonical URLs
- **Open Graph Tags**: Complete OG support for social media sharing
- **Twitter Cards**: Twitter-specific meta tags for rich previews
- **Additional Meta Tags**: Support for custom meta tags

### 4. Dynamic Sitemap Generation

- **SitemapService**: Automatic sitemap generation using reflection
- **SEOController**: REST endpoints for sitemap.xml and robots.txt
- **Route Discovery**: Automatic discovery of all routable components

### 5. Enhanced Page Settings

- **HUIPageSettingsAttribute**: Extended with SEO properties
- **Automatic SEO**: Components automatically get SEO data from attributes
- **Flexible Configuration**: Optional SEO data with intelligent defaults

## Usage

### Adding SEO to Component Pages

```csharp
[Route("/components/my-component")]
[HUIPageSettings("My Component", 
    Description = "Amazing component description for SEO",
    Keywords = "blazor, component, ui, hedin",
    Image = "/my-component-preview.png")]
```

### Setting SEO Programmatically

```csharp
@inject ISEOService SEOService

protected override void OnInitialized()
{
    var seoData = SEOData.Create(
        title: "My Page Title",
        description: "Detailed description for search engines",
        keywords: "relevant, keywords, here",
        image: "/social-preview.png"
    );
    
    SEOService.SetPageSEO(seoData);
}
```

### Layout Integration

The SEO system is automatically integrated into:

- **DemoPageLayout**: Automatically applies SEO from HUIPageSettingsAttribute
- **MainLayout**: Provides default SEO for main sections
- **App.razor**: Renders SEO meta tags in the head section

## Generated URLs

- **Sitemap**: `/seo/sitemap.xml`
- **Robots**: `/seo/robots.txt`

## SEO Data Structure

```csharp
public class SEOData
{
    // Basic SEO
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string? Author { get; set; }
    public string? Canonical { get; set; }
    
    // Open Graph
    public string? OGTitle { get; set; }
    public string? OGDescription { get; set; }
    public string? OGImage { get; set; }
    public string? OGUrl { get; set; }
    public string? OGType { get; set; }
    
    // Twitter Cards
    public string? TwitterCard { get; set; }
    public string? TwitterTitle { get; set; }
    public string? TwitterDescription { get; set; }
    public string? TwitterImage { get; set; }
    
    // Additional meta tags
    public Dictionary<string, string> AdditionalMetaTags { get; set; }
}
```

## Automatic Features

1. **Route Discovery**: All components with `[Route]` and `[HUIPageSettings]` attributes are automatically discovered
2. **Sitemap Generation**: Dynamic sitemap.xml created from discovered routes
3. **SEO Defaults**: Intelligent defaults based on component names and structure
4. **Social Media Optimization**: Automatic Open Graph and Twitter Card generation
5. **Canonical URLs**: Automatic canonical URL generation based on current page

## Service Registration

SEO services are automatically registered in `UIConfigurationExtension.cs`:

```csharp
services.AddScoped<ISEOService, SEOService>();
services.AddScoped<ISitemapService, SitemapService>();
```

## Best Practices

1. **Always provide descriptions**: Use meaningful descriptions for better search results
2. **Include relevant keywords**: Add component and feature-specific keywords
3. **Use social images**: Provide preview images for better social media sharing
4. **Test regularly**: Check `/seo/sitemap.xml` and `/seo/robots.txt` endpoints
5. **Monitor performance**: Use SEO tools to validate implementation

## Example Implementation

See these files for implementation examples:
- `Pages/Components/Page/Page.razor`
- `Pages/Components/Tooltip/Tooltip.razor`
- `Pages/GettingStarted/GettingStarted.razor`

## Future Enhancements

Consider adding:
- Schema.org structured data
- Multi-language SEO support
- SEO analytics integration
- Performance monitoring
- Advanced social media card customization

## Technical Details

- **Server-side rendering**: SEO tags are rendered server-side for optimal crawling
- **Dynamic updates**: SEO data updates automatically when navigating between pages
- **Reflection-based discovery**: Uses .NET reflection to discover all routable components
- **Caching**: Sitemap responses are cached for performance
- **Error handling**: Fallback sitemaps ensure reliability

This implementation provides a solid foundation for excellent search engine optimization and social media sharing for the Hedin UI component library.