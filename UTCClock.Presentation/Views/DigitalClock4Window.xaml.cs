using System.Windows;
using UTCClock.Business.ViewModels;

namespace UTCClock.Presentation.Views
{
    /// <summary>
    /// Interaktionslogik für DigitalClock4Window.xaml
    /// </summary>
    public partial class DigitalClock4Window : Window
    {
        private DigitalClock4WindowViewModel viewModel;

        public DigitalClock4Window()
        {
            InitializeComponent();

            this.Loaded += DigitalClock4Window_Loaded;
        }

        void DigitalClock4Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = this.DataContext as DigitalClock4WindowViewModel;
        }
    }
}
