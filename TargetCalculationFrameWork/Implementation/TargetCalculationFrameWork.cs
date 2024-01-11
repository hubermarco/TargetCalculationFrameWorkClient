
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TargetCalculationInterfaces;

namespace TargetCalculationFrameWork
{
    internal class TargetCalculationFrameWork : ITargetCalculationFrameWork
    {
        private readonly string _targetFormulaDllSubString = "TargetFormula";
        private readonly string _dllExtension = ".dll";

        public IList<TargetFormula> GetAvailableTargetFormulas()
        {
            var targetFormulaList = new List<TargetFormula>();

            var currentDirectory = Directory.GetCurrentDirectory();

            var files = Directory.GetFiles(currentDirectory);

            // Filter files containing the target substring
            var targetFormulaFilePathes = files.Where(file => file.Contains(_targetFormulaDllSubString) && file.Contains(_dllExtension)).ToArray();

            foreach (var targetFormulaFilePath in targetFormulaFilePathes)
            {
                var targetCalculationFactory = CreateTargetCalculationFactory(targetFormulaFilePath);

                var targetFormula = targetCalculationFactory?.GetTargetFormula();

                if(targetFormula.HasValue)
                    targetFormulaList.Add(targetFormula.Value);
            }

            return targetFormulaList;
        }

        public ITargetCalculation GetTargetCalculation(TargetFormula targetFormula)
        {
            var targetFormulaDllName = _targetFormulaDllSubString + targetFormula;

            var currentDirectory = Directory.GetCurrentDirectory();

            var targetFormulaFilePath = Path.Combine(currentDirectory, targetFormulaDllName);

            var targetCalculationFactory = CreateTargetCalculationFactory(targetFormulaFilePath);

            var targetCalculation = targetCalculationFactory?.Create();

            return targetCalculation;
        }

        private static ITargetCalculationFactory CreateTargetCalculationFactory(string targetFormulaFilePath)
        {
            // Load the assembly from the DLL
            var assembly = Assembly.LoadFile(targetFormulaFilePath);

            // Get the type (class) from the assembly
            var targetCalculationFactoryType =
                assembly.GetTypes().First(type => type.ToString().Contains("TargetCalculationFactory"));

            var targetCalculationFactory =
                Activator.CreateInstance(targetCalculationFactoryType) as ITargetCalculationFactory;
            return targetCalculationFactory;
        }
    }
}
