using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hw5_sudokuAlgorithm_factory;
using hw5_sudoku_algorithms;

namespace hw5_puzzle
{
    public class puzzleSudoku : puzzle
    {
        algorithmSudokuFactory factory = new algorithmSudokuFactory();
        public puzzleSudoku()
        {
            //solveBehavior = new sudokuSolveBehavior();
            promptBehavior = new sudokuPromptBehavior();
        }

        public override void puzzleExecute()
        {
            while(true)
            {
                sudokuAlgorithm createdAlgorithm = null;
                String algorithmType = promptBehavior.prompt();
                if (algorithmType.ToUpper() == "X")
                {
                    break;
                }
                else if (algorithmType.ToUpper() != "INVALID")
                {
                    createdAlgorithm = factory.createAlgorithm(algorithmType);
                    createdAlgorithm.solvePuzzle();
                }
            }
        }
    }
}
