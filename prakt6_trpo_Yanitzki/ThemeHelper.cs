using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt6_trpo_Yanitzki
{
    public static class ThemeHelper
    {
        private static readonly string[] _themePath = {
            "Styles/Themes/LightTheme.xaml",
            "Styles/Themes/DarkTheme.xaml"
        };

        public static string Current
        {
            get => string.IsNullOrEmpty(prakt6_trpo_Yanitzki.Properties.Settings.Default.ThemePath)
                ? _themePath[0]
                : prakt6_trpo_Yanitzki.Properties.Settings.Default.ThemePath;
            set
            {
                prakt6_trpo_Yanitzki.Properties.Settings.Default.ThemePath = value;
                prakt6_trpo_Yanitzki.Properties.Settings.Default.Save();
            }
        }

        public static void Apply(string themePath)
        {
            var newTheme = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };

            var oldTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => _themePath.Any(path => d.Source != null && d.Source.OriginalString.EndsWith(path, StringComparison.OrdinalIgnoreCase)));

            if (oldTheme != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                Application.Current.Resources.MergedDictionaries[index] = newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            }
            Current = themePath;
        }

        public static void ApplySaved() => Apply(Current);

        public static void Toggle()
        {
            var newTheme = Current == _themePath[0] ? _themePath[1] : _themePath[0];
            Apply(newTheme);
        }
    }
}
