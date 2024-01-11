
using System;
using System.Windows;
using TargetCalculationFrameWork;
using TargetCalculationInterfaces;

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


        private void CalculateTargetButton_OnClick(object sender, RoutedEventArgs e)
        {
            var targetFormulaString = TargetFormulaListBox.SelectedItem as string;

            if (Enum.TryParse<TargetFormula>(targetFormulaString, out var targetFormula))
            {
                var targetCalculationFrameWork = TargetCalculationFrameWorkFactory.Create();
                var targetCalculation = targetCalculationFrameWork.GetTargetCalculation(targetFormula);
                targetCalculation.Calculate(null);
            }
        }
    }
}
