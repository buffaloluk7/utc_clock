using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using UTCClock.Business.ViewModels;

namespace UTCClock.Presentation.Views
{
    /// <summary>
    /// Interaktionslogik für DigitalClock3Window.xaml
    /// </summary>
    public partial class DigitalClock3Window : Window
    {
        private DigitalClock3WindowViewModel viewModel;

        public DigitalClock3Window()
        {
            InitializeComponent();

            this.Loaded += DigitalClock3Window_Loaded;
        }

        void DigitalClock3Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = this.DataContext as DigitalClock3WindowViewModel;
            this.viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                switch (e.PropertyName)
                {
                    case "Hour":
                        this.Hour.Text = this.viewModel.Hour.ToString();
                        break;

                    case "Minute":
                        this.Minute.Text = this.viewModel.Minute.ToString();
                        break;

                    case "Second":
                        this.Second.Text = this.viewModel.Second.ToString();
                        break;
                }
            }), DispatcherPriority.DataBind);
        }
    }
}
