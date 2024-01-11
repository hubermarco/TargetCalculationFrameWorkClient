
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TargetCalculationInterfaces;

namespace TargetCalculationFrameWork
{
    internal class TargetCalculationFrameWork : ITargetCalculationFrameWork
    {
        private string _targetFormulaDllSubString = "TargetFormula";

        public IList<TargetFormula> GetAvailableTargetFormulas()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var files = Directory.GetFiles(currentDirectory);

            // Filter files containing the target substring
            var matchingFiles = files.Where(file => file.Contains(_targetFormulaDllSubString)).ToArray();


            return new List<TargetFormula> { TargetFormula.NalNl2, TargetFormula.DslV5, TargetFormula.ThirdGain };
        }
    }
}
