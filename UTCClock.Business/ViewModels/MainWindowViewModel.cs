using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Timers;
using UTCClock.Business.Commands;
using UTCClock.Business.Common;
using UTCClock.Business.Enums;
using UTCClock.Business.Interfaces;
using UTCClock.Business.Model;

namespace UTCClock.Business.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Properties

        private readonly Timer timer;
        private readonly ClockModel clock;
        private ObservableCollection<string> commandLog;
        private string commandInput;

        public ObservableCollection<string> CommandLog
        {
            get { return this.commandLog; }
        }

        public string CommandInput
        {
            get
            {
                return this.commandInput;
            }
            set
            {
                base.Set<string>(ref this.commandInput, value);
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel() : base()
        {
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
            string commandString = this.commandInput.Split(' ')[0];
            CommandType commandType;

            if (!Enum.TryParse<CommandType>(commandString, true, out commandType))
            {
                commandType = CommandType.Custom;
            }

            switch(commandType)
            {
                case CommandType.Undo:
                    CommandManager.Instance.UndoCommand();
                    break;

                case CommandType.Redo:
                    CommandManager.Instance.RedoCommand();
                    break;

                case CommandType.Exit:
                    Environment.Exit(0);
                    break;

                default:
                    CommandBase command = CommandFactory.Instance.CreateCommand(this.commandInput);
                    CommandManager.Instance.ExecuteCommand(command);
                    break;
            }

            this.CommandLog.Add(this.commandInput);
            this.CommandInput = string.Empty;
        }

        #endregion

        #region Timer Implementation

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.clock.Time = this.clock.Time.AddSeconds(1);
        }

        #endregion
    }
}
