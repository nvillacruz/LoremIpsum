using System;
using System.Linq;
using System.Windows;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A class that locates the selected theme
    /// </summary>
    public class ResourceLocator
    {
        /// <summary>
        /// Uri for Light color scheme
        /// </summary>
        public static Uri LightColorScheme => new Uri("pack://application:,,,/LoremIpsum.WpfApp;component/Resources/ColorScheme/Light.xaml", UriKind.Absolute);

        /// <summary>
        /// Uri for Dark color scheme
        /// </summary>
        public static Uri DarkColorScheme => new Uri("pack://application:,,,/LoremIpsum.WpfApp;component/Resources/ColorScheme/Dark.xaml", UriKind.Absolute);

        /// <summary>
        /// Adds a resource dictionary with the specified uri to the MergedDictionaries collection of the rootResourceDictionary/>.
        /// Additionally all child ResourceDictionaries are traversed recursively to find the current color scheme which is removed if found.
        /// </summary>
        /// <param name="rootResourceDictionary">The resource dictionary containing the currently active color scheme. It will receive the new color scheme in its MergedDictionaries. Expected are the resource dictionaries of the app or window.</param>
        /// <param name="colorSchemeResourceUri">The Uri of the color scheme to be set. Can be taken from the <see cref="ResourceLocator"/> class.</param>
        /// <param name="currentColorSchemeResourceUri">Optional uri to an external color scheme.</param>
        public static void SetColorScheme(ResourceDictionary rootResourceDictionary, Uri colorSchemeResourceUri, Uri currentColorSchemeResourceUri = null)
        {
            var knownColorSchemes = currentColorSchemeResourceUri != null ? new[] { currentColorSchemeResourceUri } : new[] { LightColorScheme, DarkColorScheme };

            var currentTheme = FindFirstContainedResourceDictionaryByUri(rootResourceDictionary, knownColorSchemes);

            if (currentTheme != null)
            {
                if (!RemoveResourceDictionaryFromResourcesDeep(currentTheme, rootResourceDictionary))
                    throw new Exception("The currently active color scheme was found but could not be removed.");
            }

            rootResourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = colorSchemeResourceUri });
        }

        private static ResourceDictionary FindFirstContainedResourceDictionaryByUri(ResourceDictionary resourceDictionary, Uri[] knownColorSchemes)
        {
            if (knownColorSchemes.Any(scheme => resourceDictionary.Source != null && resourceDictionary.Source.IsAbsoluteUri && resourceDictionary.Source.AbsoluteUri.Equals(scheme.AbsoluteUri)))
                return resourceDictionary;

            if (!resourceDictionary.MergedDictionaries.Any())
                return null;

            return resourceDictionary.MergedDictionaries.FirstOrDefault(d => FindFirstContainedResourceDictionaryByUri(d, knownColorSchemes) != null);
        }

        private static bool RemoveResourceDictionaryFromResourcesDeep(ResourceDictionary resourceDictionaryToRemove, ResourceDictionary rootResourceDictionary)
        {
            if (!rootResourceDictionary.MergedDictionaries.Any())
                return false;

            if (rootResourceDictionary.MergedDictionaries.Contains(resourceDictionaryToRemove))
            {
                rootResourceDictionary.MergedDictionaries.Remove(resourceDictionaryToRemove);
                return true;
            }

            return rootResourceDictionary.MergedDictionaries.Any(dict => RemoveResourceDictionaryFromResourcesDeep(resourceDictionaryToRemove, dict));
        }
    }
}
