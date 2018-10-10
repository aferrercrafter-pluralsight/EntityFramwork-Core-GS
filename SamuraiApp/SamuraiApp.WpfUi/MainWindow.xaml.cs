using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.WpfUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConnectedData _repo = new ConnectedData();
        private Samurai _currentSamurai;
        private bool _isListChanging;
        private bool _isLoading;
        private ObjectDataProvider _samuraiViewSource;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoading = true;            
            samuraiListBox.ItemsSource = _repo.SamuraisListInMemory();
            _samuraiViewSource = (ObjectDataProvider)FindResource("SamuraiViewSource");
            _isLoading = false;
            samuraiListBox.SelectedIndex = 0;            
        }

        private void SamuraiDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(!_isLoading)
            {
                _isListChanging = true;
                _currentSamurai = _repo.LoadSamuraiGraph((int)samuraiListBox.SelectedValue);
                _samuraiViewSource.ObjectInstance = _currentSamurai;
                _isListChanging = false;
            }
        }

        public void SaveChanges()
        {
            _repo.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _currentSamurai = _repo.NewSamurai();
            _samuraiViewSource.ObjectInstance = _currentSamurai;
            samuraiListBox.SelectedItem = _currentSamurai;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }

        private void RealName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!_isLoading && !_isListChanging)
            {
                if(_currentSamurai.SecretIdentity is null)
                {
                    _currentSamurai.SecretIdentity = new SecretIdentity();
                }
                _currentSamurai.SecretIdentity.RealName = ((TextBox)sender).Text;
                _currentSamurai.IsDirty = true;
            }
        }
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isLoading && !_isListChanging)
            {
                _currentSamurai.IsDirty = true;
            }
        }

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            samuraiListBox.Items.Refresh();
        }

        private void Quotes_CellEditing(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(!_isLoading && !_isListChanging)
            {
                _currentSamurai.IsDirty = true;
            }
        }
    }
}
