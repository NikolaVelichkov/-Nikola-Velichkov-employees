using EmployeesUI.Client.Contracts;
using EmployeesUI.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeesUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUserHttpClient _userHttpClient;
        private readonly IRequestHandler _requestHandler;
        public MainWindow(IUserHttpClient userHttpClient, IRequestHandler requestHandler)
        {
            InitializeComponent();
            _userHttpClient = userHttpClient;
            _requestHandler = requestHandler;

        }

        private async void Compose(object sender, RoutedEventArgs e)
        {
            bool status = await UploadFile();

            if (!status)
            {
                return;
            }

            MaxDaysEntity? maxDaysEntity = await _requestHandler.GetMostworkedPairAsync();

            if (maxDaysEntity is null)
            {
                return;
            }
            DisplayMaxWorkedDaysGridData(maxDaysEntity);
            List<CommonProjectEntity>? commonProjectEntities = await _requestHandler.GetListCommonProjectPairsAsync();

            if (commonProjectEntities is null)
            {
                return;
            }
            DisplayCommondPairsGridData(commonProjectEntities);
        }

        private async Task<bool> UploadFile()
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == false)
            {
                return false;
            }

            string fileName = fd.FileName;
            return await _requestHandler.UploadRequest(fileName);
            
        }

        private void DisplayCommondPairsGridData(List<CommonProjectEntity>? commonProjectEntities)
        {
            if (commonProjectEntities is null)
            {
                return;
            }

            EmployeesDataGrid.ItemsSource = commonProjectEntities;

            EmployeesDataGrid.ColumnWidth = DataGridLength.SizeToCells;
        }

        private void DisplayMaxWorkedDaysGridData(MaxDaysEntity? maxDaysEntity)
        {
            List<MaxDaysEntity> maxDaysEntities = new List<MaxDaysEntity>();
            maxDaysEntities.Add(maxDaysEntity);
            MostWorkedDaysDataGrid.ItemsSource = maxDaysEntities;
            MostWorkedDaysDataGrid.ColumnWidth = DataGridLength.SizeToCells;
        }
    }
}
