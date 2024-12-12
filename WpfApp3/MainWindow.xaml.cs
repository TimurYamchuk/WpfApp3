using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private string _operations = string.Empty;
        private bool _isResultCalculated = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnNumberButtonClick(object sender, RoutedEventArgs e)
        {
            string number = (string)((Button)sender).Tag;

            if (_isResultCalculated)
                _isResultCalculated = false;

            _operations += number;
            UpdateDisplay();
        }

        private void OnOperatorButtonClick(object sender, RoutedEventArgs e)
        {
            string operatorSymbol = (string)((Button)sender).Tag;

            if (_isResultCalculated)
                _isResultCalculated = false;

            if (_operations.Length > 0 && !char.IsDigit(_operations[^1]))
                _operations = _operations.Remove(_operations.Length - 1) + operatorSymbol;
            else
                _operations += $" {operatorSymbol} ";

            UpdateDisplay();
        }

        private void OnEqualsButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_operations))
            {
                try
                {
                    var result = new DataTable().Compute(_operations, null);
                    CurrentNumber.Text = result.ToString();
                    _isResultCalculated = true;
                }
                catch
                {
                    CurrentNumber.Text = "Error";
                }
            }
        }

        private void OnClearButtonClick(object sender, RoutedEventArgs e)
        {
            _operations = string.Empty;
            UpdateDisplay();
            CurrentNumber.Text = "0";
            _isResultCalculated = false;
        }

        private void OnClearEntryButtonClick(object sender, RoutedEventArgs e)
        {
            if (_operations.Length > 0)
            {
                if (char.IsDigit(_operations[^1]))
                {
                    int lastSpaceIndex = _operations.LastIndexOf(' ');
                    _operations = lastSpaceIndex != -1 ? _operations.Substring(0, lastSpaceIndex) : string.Empty;
                }
                else
                {
                    _operations = _operations.Remove(_operations.Length - 1);
                }
            }

            UpdateDisplay();
            _isResultCalculated = false;
        }

        private void OnBackspaceButtonClick(object sender, RoutedEventArgs e)
        {
            if (_operations.Length > 0)
            {
                _operations = _operations.Remove(_operations.Length - 1);
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            InputTextBox1.Text = _operations;
        }
    }
}
