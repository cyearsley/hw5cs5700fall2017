using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hw5_puzzle;

namespace hw5_rewrite_sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            puzzle sudokuPuzzle = new puzzleSudoku();
            sudokuPuzzle.puzzleExecute();
        }
    }
}
