using PasswordApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordApp.Services.Visuality
{
    public static class VisualController
    {
        public static List<ThemeModel> Themes { get; private set; }
        public static List<ColorModel> Colors { get; private set; }

        public static ThemeModel CurrentTheme { get; private set; }
        public static ColorModel CurrentColor { get; private set; }

        public static void StartUp()
        {
            Themes = new List<ThemeModel>
            {
                new ThemeModel(1,"Темная","Dark"),
                new ThemeModel(2,"Светлая","Light")
            };
            Colors = new List<ColorModel>
            {
                new ColorModel(1,"Синий","Blue"),
                new ColorModel(2,"Красный","Red"),
                new ColorModel(3,"Фиолетовый","Purple"),
                new ColorModel(4,"Зелёный","Green"),
                new ColorModel(5,"Желтый","Yellow"),
                new ColorModel(6,"Оранжевый","Orange")
            };

            ChangeTheme(Themes.Where(obj => obj.Id == Settings.Default.ThemeId).FirstOrDefault());
            ChangeColor(Colors.Where(obj => obj.Id == Settings.Default.ColorId).FirstOrDefault());
        }

        public static void ChangeTheme(ThemeModel model)
        {
            CurrentTheme = model;
            var app = (App)Application.Current;
            app.ChangeTheme(model.UriTheme);
            Settings.Default.ThemeId = model.Id;
            Settings.Default.Save();
        }

        public static void ChangeColor(ColorModel model)
        {
            CurrentColor = model;
            var app = (App)Application.Current;
            app.ChangeColor(model.UriColor);
            Settings.Default.ColorId = model.Id;
            Settings.Default.Save();
        }
    }
}
