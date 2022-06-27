
using Cosmos.UI.Layoutting.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Cosmos.UI.Layoutting.Wpf
{
    /*
        主题
    */
    public class Theme
    {
        public static IEnumerable<Theme> GetThemesFromThisAssembly()
        {
            var all_uris = GetXamlUrisFromThisAssembly();
            var theme_keys = GetThemeUriDictionary(all_uris);

            var themes = new List<Theme>();
            foreach (var theme_key in theme_keys)
            {
                var theme = new Theme();
                theme.Name.Zh_Cn = theme_key;
                var color_scheme_uris = all_uris.Where(uri => uri.AbsolutePath.Contains($"themes/{theme_key}/colorschemes"));
                theme.ColorSchemes = color_scheme_uris.Select(uri => new ThemeColorScheme(uri));

                var skeleton_uris = all_uris.Where(uri => uri.AbsolutePath.Contains($"themes/{theme_key}/skeleton"));
                theme.Skeleton = new ThemeSkeleton(skeleton_uris);
                themes.Add(theme);
            }
            return themes;
        }

        public MuiString Name { get; set; } = new MuiString();

        public void ApplyTo(ResourceDictionary rd)
        {
            rd.MergedDictionaries.Clear();
            ApplyColorScheme(rd, CurrentColorScheme);
            ApplySkeleton(rd, Skeleton);
        }

        private static void ApplyColorScheme(ResourceDictionary rd, ThemeColorScheme color_scheme)
        {
            if (rd.MergedDictionaries.Count == 0)
            {
                rd.MergedDictionaries.Add(color_scheme.ResourceDictionary);
            }
            else
            {
                rd.MergedDictionaries[0] = color_scheme.ResourceDictionary;
            }
        }

        private static void ApplySkeleton(ResourceDictionary rd, ThemeSkeleton skeleton)
        {
            if (rd.MergedDictionaries.Count == 1)
            {
                rd.MergedDictionaries.Add(skeleton.ResourceDictionary);
            }
            else
            {
                rd.MergedDictionaries[0] = skeleton.ResourceDictionary;
            }
        }

        private ThemeColorScheme _CurrentColorScheme;
        public ThemeColorScheme CurrentColorScheme
        {
            get
            {
                if (_CurrentColorScheme == null)
                {
                    _CurrentColorScheme = ColorSchemes.First();
                }
                return _CurrentColorScheme;
            }
            set
            {
                _CurrentColorScheme = value;
            }
        }
        public IEnumerable<ThemeColorScheme> ColorSchemes { get; private set; }
        public ThemeSkeleton Skeleton { get; private set; }
        private Theme()
        {

        }
        private static MuiString LoadNamesFromResourceDictioanry(ResourceDictionary rd)
        {
            var ms = new MuiString();
            ms.En_Us = rd["Name.En_Us"] as String;
            ms.Zh_Cn = rd["Name.Zh_Cn"] as String;
            ms.Zh_Tw = rd["Name.Zh_Tw"] as String;
            ms.Ja_Jp = rd["Name.Ja_Jp"] as String;
            return ms;
        }
        private static IEnumerable<String> GetThemeUriDictionary(IEnumerable<Uri> uris)
        {
            var theme_uris = uris.Select(uri => uri.Segments[Array.IndexOf(uri.Segments, "themes/") + 1]);
            var theme_keys = theme_uris.Distinct().Where(uri => uri != "/").Select(seg => seg.Trim('/'));
            return theme_keys;
        }
        private static ResourceDictionary LoadResourceDictionaryFromFile(String xaml_path)
        {
            var xaml_string = File.ReadAllText(xaml_path);
            var rd = XamlReader.Parse(xaml_string) as ResourceDictionary;
            return rd;
        }
        private static ResourceDictionary LoadResourceDictionaryFromFiles(IEnumerable<String> xaml_paths)
        {
            var rd = new ResourceDictionary();
            rd.MergedDictionaries.Concat(xaml_paths.Select(xp => LoadResourceDictionaryFromFile(xp)));
            return rd;
        }
        private static ResourceDictionary LoadResourceDictionaryFromUri(Uri uri)
        {
            try
            {
                var rd = new ResourceDictionary()
                {
                    Source = uri
                };
                return rd;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static ResourceDictionary LoadResourceDictionaryFromUris(IEnumerable<Uri> uris)
        {
            var rd = new ResourceDictionary();

            foreach (var uri in uris)
            {
                rd.MergedDictionaries.Add(LoadResourceDictionaryFromUri(uri));
            }

            return rd;
        }
        private static IEnumerable<Uri> GetXamlUrisFromThisAssembly()
        {
            var this_assembly = Assembly.GetExecutingAssembly();
            var uris = GetXamlUrisFromAssembly(this_assembly);
            return uris;
        }
        private static IEnumerable<Uri> GetXamlUrisFromAssembly(Assembly assembly)
        {
            var assembly_name = assembly.GetName().Name;
            var resource_stream = assembly.GetManifestResourceStream(assembly_name + ".g.resources");
            IEnumerable<Uri> uris = null;

            using (var reader = new ResourceReader(resource_stream))
            {
                uris = reader.Cast<DictionaryEntry>().Select(de => new Uri($"pack://application:,,,/{assembly_name};component/{Path.ChangeExtension(de.Key as String, "xaml")}")).ToArray();
            }
            return uris;
        }
        public class ThemeColorScheme
        {
            internal ThemeColorScheme(String xaml_path)
            {
                ResourceDictionary = Theme.LoadResourceDictionaryFromFile(xaml_path);
                UniqueName = LoadNamesFromResourceDictioanry(ResourceDictionary);
            }
            internal ThemeColorScheme(Uri uri)
            {
                ResourceDictionary = Theme.LoadResourceDictionaryFromUri(uri);
                UniqueName = LoadNamesFromResourceDictioanry(ResourceDictionary);
            }
            public MuiString UniqueName { get; private set; }
            public ResourceDictionary ResourceDictionary { get; private set; }

        }

        public class ThemeSkeleton
        {
            internal ThemeSkeleton(IEnumerable<String> xaml_paths)
            {
                ResourceDictionary = Theme.LoadResourceDictionaryFromFiles(xaml_paths);
            }
            internal ThemeSkeleton(IEnumerable<Uri> uris)
            {
                var font_rd = Theme.LoadResourceDictionaryFromUris(uris.Where(uri => uri.AbsolutePath.Contains("fonts")));
                var drawings_rd = Theme.LoadResourceDictionaryFromUris(uris.Where(uri => uri.AbsolutePath.Contains("drawings")));
                var styles_rd = Theme.LoadResourceDictionaryFromUris(uris.Where(uri => uri.AbsolutePath.Contains("styles")));
                var rd = new ResourceDictionary()
                {
                    MergedDictionaries =
                    {
                        font_rd, drawings_rd, styles_rd
                    }
                };
                ResourceDictionary = rd;
            }
            public ResourceDictionary ResourceDictionary { get; private set; }
        }
    }



}
