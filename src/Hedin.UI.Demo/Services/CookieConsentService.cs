using Microsoft.JSInterop;

namespace Hedin.UI.Demo.Services;

public class CookieConsentService
{
    private readonly IJSRuntime _jsRuntime;
    private bool _hasConsent = false;
    private bool _isInitialized = false;
    private DateTime? _consentDate = null;
    
    // Consent expires after 12 months (GDPR best practice)
    private const int CONSENT_EXPIRY_MONTHS = 12;

    public event Action<bool>? OnConsentChanged;

    public CookieConsentService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public bool HasConsent => _hasConsent;

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        try
        {
            // Check for consent cookie
            var consentCookie = await _jsRuntime.InvokeAsync<string>("eval", 
                "document.cookie.split(';').find(c => c.trim().startsWith('cookieConsent='))?.split('=')[1]");
            
            if (!string.IsNullOrEmpty(consentCookie))
            {
                var parts = consentCookie.Split('|');
                if (parts.Length == 2 && DateTime.TryParse(parts[1], out var consentDateTime))
                {
                    _consentDate = consentDateTime;
                    _hasConsent = parts[0] == "true" && !IsConsentExpired(consentDateTime);
                }
            }
            
            _isInitialized = true;
        }
        catch
        {
            _hasConsent = false;
            _isInitialized = true;
        }
    }

    public async Task SetConsentAsync(bool hasConsent)
    {
        _hasConsent = hasConsent;
        _consentDate = DateTime.Now;
        
        // Store consent in cookie with proper expiry
        var consentValue = $"{hasConsent.ToString().ToLower()}|{_consentDate.Value:O}";
        var expiryDate = DateTime.Now.AddMonths(CONSENT_EXPIRY_MONTHS);
        
        // Set cookie with 12-month expiry
        await _jsRuntime.InvokeVoidAsync("eval", 
            $"document.cookie = 'cookieConsent={consentValue}; expires={expiryDate:ddd, dd MMM yyyy HH:mm:ss} GMT; path=/; SameSite=Lax'");
        
        OnConsentChanged?.Invoke(_hasConsent);
    }

    public async Task<bool> ShouldShowBannerAsync()
    {
        await InitializeAsync();
        // Show banner if no consent decision has been made yet or if consent has expired
        return !_consentDate.HasValue || (_consentDate.HasValue && IsConsentExpired(_consentDate.Value));
    }

    public async Task<bool> IsConsentExpiredAsync()
    {
        await InitializeAsync();
        return _consentDate.HasValue && IsConsentExpired(_consentDate.Value);
    }

    private bool IsConsentExpired(DateTime consentDate)
    {
        return DateTime.Now > consentDate.AddMonths(CONSENT_EXPIRY_MONTHS);
    }

    public async Task<DateTime?> GetConsentDateAsync()
    {
        await InitializeAsync();
        return _consentDate;
    }

    public async Task<int> GetDaysUntilExpiryAsync()
    {
        await InitializeAsync();
        if (!_consentDate.HasValue) return 0;

        var expiryDate = _consentDate.Value.AddMonths(CONSENT_EXPIRY_MONTHS);
        return Math.Max(0, (int)(expiryDate - DateTime.Now).TotalDays);
    }

    public async Task ForceShowBannerAsync()
    {
        // Clear the consent to force the banner to show
        await _jsRuntime.InvokeVoidAsync("eval", "document.cookie = 'cookieConsent=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/;'");
        _hasConsent = false;
        _consentDate = null;
        _isInitialized = false;
        OnConsentChanged?.Invoke(_hasConsent);
    }
}