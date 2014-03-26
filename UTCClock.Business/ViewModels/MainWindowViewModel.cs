using System.Collections.ObjectModel;
using System.Timers;
using UTCClock.Business.Common;
using UTCClock.Business.Model;
using ViHo.Service.Navigation;
using System;
using UTCClock.Business.Commands;
using UTCClock.Business.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace UTCClock.Business.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Properties

        private readonly Timer timer;
        private readonly ClockModel clock;
        private ObservableCollection<string> commandLog;
        private readonly INavigationService navigationService;
        private string commandText;

        public ObservableCollection<string> CommandLog
        {
            get { return this.commandLog; }
        }

        public string CommandText
        {
            get
            {
                return this.commandText;
            }
            set
            {
                base.Set<string>(ref this.commandText, value);
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel(INavigationService navigationService) : base()
        {
            this.navigationService = navigationService;

            this.timer = new Timer();
            this.clock = ClockModel.Instance;
            this.commandLog = new ObservableCollection<string>();

            this.SearchCommand = new RelayCommand(onSearchExecuted);

            this.timer.Interval = 1000;
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Start();
        }

        #endregion

        #region Commands

        public RelayCommand SearchCommand
        {
            get;
            private set;
        }

        #endregion

        #region Command Implementations

        private void onSearchExecuted()
        {
            // CommandFactory nach gültigen Command abfragen, returns new ICommand(arguments)
            // Undo, Redo sollte ein eigenständiges Command sein, welches nur ICommand implementiert
            // CommandManager.Instance.isValidCommand(string input, out ICommand command) aufrufen,
            // liefert als out-Variable das fertige Command zurück, danach über CommandManager.Instance.Execute(command)
            // ausführen, welcher sich um den Undo-/Redo-Stack kümmert. Da CommandManager statisch ist, kann Undo/Redo
            // darauf einfach zugreifen und UndoCommand bzw. RedoCommand aufrufen.
            // Frage: Somit kann jedes Command auf Undo/Redo zugreifen. Soll das möglich sein? Wie verhindern?

            string commandString = CommandText.Split(' ')[0];
            CommandType commandType;

            if (Enum.TryParse<CommandType>(commandString, true, out commandType))
            {
                switch(commandType)
                {
                    case CommandType.UNDO:
                        CommandManager.Instance.UndoCommand();
                        break;

                    case CommandType.REDO:
                        CommandManager.Instance.RedoCommand();
                        break;

                    case CommandType.HELP:
                        // help hat keine parameter, daher auch hier her verlagern??
                        break;

                    case CommandType.EXIT:
                        // exit application here
                        break;

                    default:
                        ICommand command = CommandFactory.Instance.createCommand(commandType, CommandText);
                        CommandManager.Instance.ExecuteCommand(command);
                        break;
                }

                this.CommandLog.Add(CommandText);
                CommandText = string.Empty;
            }
            else
            {
                // invalid command
            }
        }

        #endregion

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.clock.Time = this.clock.Time.AddSeconds(1);
        }
    }
}
