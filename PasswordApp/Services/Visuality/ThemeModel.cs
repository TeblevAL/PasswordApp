using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.Visuality
{
    public class ThemeModel
    {
        public int Id { get; private set; }

        public string DisplayName { get; private set; }

        public string Value { get; private set; }

        public Uri UriTheme
        {
            get { return new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + Value + ".xaml"); }
        }

        public ThemeModel(int id, string displayName, string value)
        {
            Id = id;
            DisplayName = displayName;
            Value = value;
        }
    }
}
