
using System.Windows;
using TargetCalculationFrameWork;

namespace TargetCalculationFrameWorkClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var targetCalculationFrameWork = TargetCalculationFrameWorkFactory.Create();
            var targetFormulas = targetCalculationFrameWork.GetAvailableTargetFormulas();

            TargetFormulaListBox.Items.Clear();
            foreach (var targetFormula in targetFormulas)
                TargetFormulaListBox.Items.Add(targetFormula.ToString());
        }
    }
}
