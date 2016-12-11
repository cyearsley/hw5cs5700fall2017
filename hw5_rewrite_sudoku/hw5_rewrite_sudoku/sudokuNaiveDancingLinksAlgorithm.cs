using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5_sudoku_algorithms
{
    public class sudokuNaiveDancingLinksAlgorithm : sudokuAlgorithm
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
        protected override void solve(List<List<int>> sudoku, int idx, int a = 0)
        {
            if (numberOfSolutions > 1)
            {
                return;
            }

            int size = sudoku.Count;
            if (idx == size * size)
            {
                if (checkIfValid(sudoku))
                {
                    numberOfSolutions++;
                    print(sudoku);
                }
            }
            else
            {
                int row = idx / size;
                int col = idx % size;
                if (sudoku[row][col] != 0)
                { // the square is already filled in, don't do anything 
                    solve(sudoku, idx + 1);
                }
                else
                {
                    for (int i = 1; i <= size; i++)
                    {
                        sudoku[row][col] = i;
                        solve(sudoku, idx + 1); // continue with the next square
                        sudoku[row][col] = 0; // unmake move
                    }
                }
            }
        }

        // ============================================================================== //
        //
        // C H E C K  I F  V A L I D ( )
        //
        // ============================================================================== //
        protected override bool checkIfValid(List<List<int>> sudoku, int a = 0, int b = 0, int c = 0)
        {
            int N = rawPuzzle.Count;
            int side = (int)Math.Floor(Math.Sqrt(rawPuzzle.Count));
            bool[] mask = new bool[N + 1];

            // Check rows
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int num = sudoku[i][j];
                    if (mask[num]) return false;
                    mask[num] = true;
                }
                for (int ii = 0; ii < mask.Count(); ii++)
                {
                    mask[ii] = false;
                }
            }

            // Check columns
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int num = sudoku[j][i];
                    if (mask[num]) return false;
                    mask[num] = true;
                }
                for (int ii = 0; ii < mask.Count(); ii++)
                {
                    mask[ii] = false;
                }
            }

            // Check subsquares

            for (int i = 0; i < N; i += side)
            {
                for (int j = 0; j < N; j += side)
                {
                    for (int ii = 0; ii < mask.Count(); ii++)
                    {
                        mask[ii] = false;
                    }
                    for (int di = 0; di < side; di++)
                    {
                        for (int dj = 0; dj < side; dj++)
                        {
                            int num = sudoku[i + di][j + dj];
                            if (mask[num]) return false;
                        }
                    }
                }
            }

            return true; // Everything validated!
        }
        // ============================================================================== //
        // End Reference
    }
}
