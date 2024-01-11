
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

                var targetCalculationParameters =
                    targetCalculationFrameWork.GetTargetCalculationParameters(targetFormula);

                if (targetFormula == TargetFormula.DslV5)
                {
                    var targetCalculationParametersDslV5 =
                        targetCalculationParameters as ITargetCalculationParametersDslV5;

                    targetCalculationParametersDslV5?.SetAC(new FreqCrv(CurveType.ACCurve) {new FreqPt(500, 20)});
                }
                else if(targetFormula == TargetFormula.NalNl2)
                {
                    var targetCalculationParametersNalNl2 =
                        targetCalculationParameters as ITargetCalculationParametersNalNl2;

                    targetCalculationParametersNalNl2?.SetAC(new FreqCrv(CurveType.ACCurve) { new FreqPt(500, 20) });
                }
                
                var targetCalculation = targetCalculationFrameWork.GetTargetCalculation(targetFormula);
                targetCalculation.Calculate(targetCalculationParameters);
            }
        }
    }
}
