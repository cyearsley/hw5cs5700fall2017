using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hw5_sudoku_algorithms;

namespace hw5_sudokuAlgorithm_factory
{
    class algorithmSudokuFactory
    {
        public sudokuAlgorithm createAlgorithm(String algorithmType)
        {
            sudokuAlgorithm algorithm = null;
            switch (algorithmType.ToUpper())
            {
                case "NDLA":
                    algorithm = new sudokuNaiveDancingLinksAlgorithm();
                    break;
                case "DLA":
                    algorithm = new sudokuDancingLinksAlgorithm();
                    break;
                case "RA":
                    algorithm = new sudokuRecursiveAlgorithm();
                    break;
                default:
                    Console.WriteLine("An invalid algorithm was specified!\n\n");
                    break;
            }

            return algorithm;
        }
    }
}
