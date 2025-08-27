using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MudBlazor.Resources;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hedin.UI.Localization;

namespace Hedin.UI
{
#nullable enable
    /// <summary>
    /// The <see cref="InternaHUIlLocalizer"/> service handles translations, providing english as an included default language,
    /// while allowing users to add custom translations without restricting how they can be implemented.
    /// </summary>
    internal sealed class InternaHUIlLocalizer
    {
        private readonly IStringLocalizer _localizer;
        private readonly HUILocalizer? _huiLocalizer;

        public InternaHUIlLocalizer(ILoggerFactory loggerFactory, HUILocalizer? huiLocalizer = null)
        {
            var factory = new ResourceManagerStringLocalizerFactory(Options.Create(new LocalizationOptions()), loggerFactory);
            _localizer = factory.Create(typeof(Translation));
            _huiLocalizer = huiLocalizer;
        }

        /// <summary>
        /// Gets the translation for the given translation key.
        /// </summary>
        /// <param name="key">the translation key to look up</param>
        /// <returns>The string resource as a <see cref="LocalizedString"/>.</returns>
        //public LocalizedString this[string key]
        //{
        //    get
        //    {
        //        // First check whether custom translations are available or the current ui culture is english, then we want to use the internal translations
        //        var currentCulture = Thread.CurrentThread.CurrentUICulture.Parent.TwoLetterISOLanguageName;
        //        if (_huiLocalizer == null || currentCulture.Equals("en", StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            return _localizer[key];
        //        }

        //        // If CurrentUICulture is not english and a custom MudLocalizer service implementation is registered, try to use user provided languages.
        //        // If no translation was found, fall back to the internal english translation
        //        var res = _huiLocalizer[key];
        //        return res.ResourceNotFound ? _localizer[key] : res;
        //    }
        //}
        public LocalizedString this[string key]
        {
            get
            {
                // Try user-defined translations first
                var custom = _huiLocalizer?[key];
                if (custom != null && !custom.ResourceNotFound && custom.Value != key)
                    return custom;

                // Fallback to internal
                return _localizer[key];
            }
        }
    }



    public class HUILocalizer
    {
        /// <summary>
        /// Gets the translation for the given translation key.
        /// Override this method to provide your custom translations.
        /// </summary>
        /// <param name="key">the translation key to look up</param>
        /// <returns><see cref="LocalizedString"/> with the custom translation. <see cref="LocalizedString.ResourceNotFound"/> should be <c>true</c> if no custom translation is provided for some translation key</returns>
        public virtual LocalizedString this[string key] => new(key, key, true);
    }
}
