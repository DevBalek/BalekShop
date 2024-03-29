﻿using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BalekShop.Repositories.Language
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

		public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }
        public LanguageService(IStringLocalizer localizer)
        {
            _localizer = localizer;
		}

        public LocalizedString Getkey(string key)
        {
            return _localizer[key];
        }

    }
}
