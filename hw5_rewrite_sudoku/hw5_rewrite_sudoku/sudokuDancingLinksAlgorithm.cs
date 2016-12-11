using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5_sudoku_algorithms
{
    public class sudokuDancingLinksAlgorithm : sudokuAlgorithm
    {
        protected override void executeSudokuAlgorithm(List<List<int>> sudoku)
        {
            solve(sudoku, 0);
            if (numberOfSolutions == 0)
            {
                Console.WriteLine("There were no solutions for the specified puzzle!");
            }
            else if (numberOfSolutions > 1)
            {
                Console.WriteLine("There was more than one solution - making this puzzle invalid!");
            }

            return;
        }

        // Start Reference: https://rafal.io/posts/solving-sudoku-with-dancing-links.html
        // ============================================================================== //

        // ============================================================================== //
        //
        // S O L V E ( )
        //
        // ============================================================================== //
        protected override void solve(List<List<int>> sudoku, int ind, int b = 0)
        {
            int S = sudoku.Count;
            if (ind == S * S)
            {
                // We've definitely reached a solution now
                numberOfSolutions++;
                print(sudoku);
            }
            else
            {
                int row = ind / S;
                int col = ind % S;

                // Advance forward on cells that are prefilled
                if (sudoku[row][col] != 0) solve(sudoku, ind + 1);
                else
                {
                    // we are positioned on something we need to fill in. Try all possibilities
                    for (int i = 1; i <= sudoku.Count; i++)
                    {
                        if (checkIfValid(sudoku, row, col, i))
                        {
                            sudoku[row][col] = i;
                            solve(sudoku, ind + 1);
                            sudoku[row][col] = 0; // unmake move
                        }
                    }
                }
                // no solution
            }

        }

        // ============================================================================== //
        //
        // C H E C K  I F  V A L I D ( )
        //
        // ============================================================================== //
        protected override bool checkIfValid(List<List<int>> board, int row, int col, int c)
        {
            int S = board.Count;
            // check columns/rows
            for (int i = 0; i < S; i++)
            {
                if (board[row][i] == c) return false;
                if (board[i][col] == c) return false;
            }

            // Check subsquare

            int rowStart = row - row % S;//
            int colStart = col - col % S;//

            for (int m = 0; m < S; m++)//
            {
                for (int k = 0; k < S; k++)//
                {
                    //if (board[rowStart + k][colStart + m] == c) return false;
                }
            }
            return true;
        }
        // ============================================================================== //
        // End Reference
    }
}
