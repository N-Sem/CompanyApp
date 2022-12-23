using CompanyApp.UI.Commands;
using CompanyApp.UI.ViewModels.Base;
using System.Windows;
using System.Runtime.CompilerServices;
using CompanyApp.Dal.Initialization;
using CompanyApp.UI.Services.Helpers;

namespace CompanyApp.UI.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel()
        {
            _connectionString = DbConnectionSettingsHelpers.CurrentConnectionString;
        }

        #region Title : string - Заголовок окна
        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _title = "Настройка подключения к базе данных";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (Equals(_title, value))
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region string ConnectionString
        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (_connectionString == value)
                    return;
                _connectionString = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region bool _dbConnectionTestResult
        private bool _dbConnectionTestResul;
        public bool DbConnectionTestResult
        {
            get => _dbConnectionTestResul;
            private set
            {
                if (_dbConnectionTestResul == value)
                    return;
                _dbConnectionTestResul = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        #region CheckConnectionStringCommand
        public RelayCommand CheckConnectionStringCommand =>
        new RelayCommand(
            () =>
            {
                var connectionTestResult = DbConnectionSettingsHelpers.TestConnectionToMySqlServer(ConnectionString);
                DbConnectionTestResult = connectionTestResult.Result;
                MessageBox.Show(connectionTestResult.Message);
            },
            () => true
            );
        #endregion

        #region SaveConnectionStringCommand
        public RelayCommand SaveConnectionStringCommand =>
        new RelayCommand(
            () =>
            {
                var savingResult = DbConnectionSettingsHelpers.UpdateConnectionStringsAppSetting(ConnectionString);
                MessageBox.Show(savingResult.Message);
            },
            () => DbConnectionTestResult
            );
        #endregion

        #region CreateDatabaseAndAddTables
        public RelayCommand CreateDatabaseAndAddTables =>
        new RelayCommand(
            () =>
            {
                var creatingDbResult = DbConnectionSettingsHelpers.DropAndCreateDatabase(ConnectionString);
                MessageBox.Show(creatingDbResult.Message);
                
            },
            () => DbConnectionTestResult
            );
        #endregion

        #region AddSeedDataCommand
        public RelayCommand AddSeedDataCommand =>
        new RelayCommand(
            () =>
            {
                var creatingDbResult = DbConnectionSettingsHelpers.CreateDatabaseAndAddSeedData(ConnectionString);
                MessageBox.Show(creatingDbResult.Message);
            },
            () => DbConnectionTestResult
            );
        #endregion

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == nameof(ConnectionString))
                DbConnectionTestResult = false;
            base.OnPropertyChanged(propertyName);
        }
    }
}
