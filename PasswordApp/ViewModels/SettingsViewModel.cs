using PasswordApp.Services.Cryptography;
using PasswordApp.Services.MVVMCore;
using PasswordApp.Services.Visuality;
using PasswordApp.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordApp.ViewModels
{
    public class SettingsViewModel : MVVMBase
    {
        #region Приватные поля

        private List<ThemeModel> themes;
        private List<ColorModel> colors;
        private List<CryptoModel> cryptos;
        private ThemeModel selectedTheme;
        private ColorModel selectedColor;
        private CryptoModel selectedCrypto;
        private string trueKey;
        private Visibility closeTrueKey = Visibility.Visible;
        private Visibility openTrueKey = Visibility.Collapsed;

        #endregion

        #region Свойства

        public List<ThemeModel> Themes
        {
            get { return themes; }
            set
            {
                themes = value;
                OnPropertyChanged("Themes");
            }
        }

        public List<ColorModel> Colors
        {
            get { return colors; }
            set
            {
                colors = value;
                OnPropertyChanged("Colors");
            }
        }

        public List<CryptoModel> Cryptos
        {
            get { return cryptos; }
            set
            {
                cryptos = value;
                OnPropertyChanged("Cryptos");
            }
        }

        public ThemeModel SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                selectedTheme = value;
                OnPropertyChanged("SelectedTheme");
            }
        }
        public ColorModel SelectedColor
        {
            get { return selectedColor; }
            set
            {
                selectedColor = value;
                OnPropertyChanged("SelectedColor");
            }
        }
        public CryptoModel SelectedCrypto
        {
            get { return selectedCrypto; }
            set
            {
                selectedCrypto = value;
                OnPropertyChanged("SelectedCrypto");
            }
        }

        public string TrueKey
        {
            get { return trueKey; }
            set
            {
                trueKey = value;
                OnPropertyChanged("TrueKey");
            }
        }

        public Visibility CloseTrueKey
        {
            get { return closeTrueKey; }
            set
            {
                closeTrueKey = value;
                OnPropertyChanged("CloseTrueKey");
            }
        }
        public Visibility OpenTrueKey
        {
            get { return openTrueKey; }
            set
            {
                openTrueKey = value;
                OnPropertyChanged("OpenTrueKey");
            }
        }

        #endregion

        #region Комманды

        public ICommand ThemeChangeCommand { get; private set; }
        public ICommand ColorChangeCommand { get; private set; }
        public ICommand CryptoChangeCommand { get; private set; }
        public ICommand KeyChangeCommand { get; private set; }
        public ICommand ShowKeyCommand { get; private set; }
        #endregion

        #region Методы

        private void ThemeChange()
        {
            VisualController.ChangeTheme(SelectedTheme);
        }
        private void ColorChange()
        {
            VisualController.ChangeColor(SelectedColor);
        }

        private void CryptoChange()
        {
            CryptoController.ChangeCrypto(SelectedCrypto);
        }

        private void KeyChange()
        {
            KeyDialog keyDialog = new KeyDialog("Введите новый ключ");
            if (keyDialog.ShowDialog() == true)
            {
                CryptoController.AddKey(keyDialog.Key);
                TrueKey = CryptoController.TrueKey;
            }
        }

        private void ShowKey()
        {
            if (CloseTrueKey == Visibility.Visible)
            {
                CloseTrueKey = Visibility.Collapsed;
                OpenTrueKey = Visibility.Visible;
            }
            else
            {
                CloseTrueKey = Visibility.Visible;
                OpenTrueKey = Visibility.Collapsed;
            }
        }

        #endregion

        #region Конструктор

        public SettingsViewModel()
        {
            Themes = VisualController.Themes;
            Colors = VisualController.Colors;
            Cryptos = CryptoController.Cryptographies;
            SelectedTheme = VisualController.CurrentTheme;
            SelectedColor = VisualController.CurrentColor;
            SelectedCrypto = CryptoController.Current;
            TrueKey = CryptoController.TrueKey;

            ThemeChangeCommand = new RelayCommand(obj => ThemeChange());
            ColorChangeCommand = new RelayCommand(obj => ColorChange());
            CryptoChangeCommand = new RelayCommand(obj => CryptoChange());
            KeyChangeCommand = new RelayCommand(obj => KeyChange());
            ShowKeyCommand = new RelayCommand(obj => ShowKey());
        }

        #endregion

    }
}
