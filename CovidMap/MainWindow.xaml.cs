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
            DataContext = _apiService.GetLatestReportByCountryCode();
            Console.WriteLine();
        }
    }
}