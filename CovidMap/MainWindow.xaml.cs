using System;

namespace CovidMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ApiService _apiService = new ApiService();
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = _apiService.getData(_apiService.GetLatestReportByCountryCode());
            DataContext = _apiService.getData(_apiService.GetLatestReportAllCountries());
            Console.WriteLine();
        }
    }
}