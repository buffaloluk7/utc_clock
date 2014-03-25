using System.Collections.ObjectModel;
using System.Timers;
using UTCClock.Business.Common;
using UTCClock.Business.Model;
using ViHo.Service.Navigation;

namespace UTCClock.Business.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Properties

        private readonly Timer timer;
        private readonly ClockModel clock;
        private ObservableCollection<string> commandLog;
        private readonly INavigationService navigationService;

        public ObservableCollection<string> CommandLog
        {
            get { return this.commandLog; }
        }
        
        #endregion

        #region Constructors

        public MainWindowViewModel(INavigationService navigationService) : base()
        {
            this.navigationService = navigationService;

            this.timer = new Timer();
            this.clock = ClockModel.Instance;
            this.commandLog = new ObservableCollection<string>();

            this.SearchCommand = new RelayCommand<string>(onSearchExecuted);

            this.timer.Interval = 1000;
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Start();
        }

        #endregion

        #region Commands

        public RelayCommand<string> SearchCommand
        {
            get;
            private set;
        }

        #endregion

        #region Command Implementations

        private void onSearchExecuted(string input)
        {
            // CommandFactory nach gültigen Command abfragen, returns new ICommand(arguments)
            // Undo, Redo sollte ein eigenständiges Command sein, welches nur ICommand implementiert
            // CommandManager.Instance.isValidCommand(string input, out ICommand command) aufrufen,
            // liefert als out-Variable das fertige Command zurück, danach über CommandManager.Instance.Execute(command)
            // ausführen, welcher sich um den Undo-/Redo-Stack kümmert. Da CommandManager statisch ist, kann Undo/Redo
            // darauf einfach zugreifen und UndoCommand bzw. RedoCommand aufrufen.
            // Frage: Somit kann jedes Command auf Undo/Redo zugreifen. Soll das möglich sein? Wie verhindern?

            switch (input)
            {
                case "beige":
                    this.navigationService.Navigate<DigitalClock1WindowViewModel>();
                    break;

                case "blue":
                    this.navigationService.Navigate<DigitalClock2WindowViewModel>();
                    break;

                case "coral":
                    this.navigationService.Navigate<DigitalClock3WindowViewModel>();
                    break;

                case "grey":
                    this.navigationService.Navigate<DigitalClock4WindowViewModel>();
                    break;

                default:
                    break;
            }

            this.CommandLog.Add(input);
        }

        #endregion

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.clock.Time = this.clock.Time.AddSeconds(1);
        }
    }
}
