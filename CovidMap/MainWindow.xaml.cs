using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

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
            DataContext = _apiService.getData(_apiService.GetLatestReportAllCountries());
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0,15,0);
            dispatcherTimer.Start();
        }

        public void FilterListView()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListViewCountries.ItemsSource);
            view.Filter = TextFilter;
            CollectionViewSource.GetDefaultView(ListViewCountries.ItemsSource).Refresh();
        }

        public bool TextFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((CountrySummary) item).country.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CovidMapObject.Center = ((CountrySummary) ListViewCountries.SelectedItem).gps;
            }
            catch (NullReferenceException exception)
            {
                
            }
            
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterListView();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DataContext = _apiService.getData(_apiService.GetLatestReportAllCountries());
            
        }
    }
}