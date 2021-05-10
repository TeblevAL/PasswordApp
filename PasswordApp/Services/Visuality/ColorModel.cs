using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.Visuality
{
    public class ColorModel
    {
        public int Id { get; private set; }

        public string DisplayName { get; private set; }

        public string Value { get; private set; }

        public Uri UriColor
        {
            get { return new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + Value + ".xaml"); }
        }

        public ColorModel(int id, string displayName, string value)
        {
            Id = id;
            DisplayName = displayName;
            Value = value;
        }
    }
}
