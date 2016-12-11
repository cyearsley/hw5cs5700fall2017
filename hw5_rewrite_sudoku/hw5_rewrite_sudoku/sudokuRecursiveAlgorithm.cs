using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5_sudoku_algorithms
{
    public class sudokuRecursiveAlgorithm : sudokuAlgorithm
    {
        List<List<List<int>>> listOfSolutions = new List<List<List<int>>> { };

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

        // ============================================================================== //
        // solve()* author: Caleb Yearsley
        // checkIfValid* author: Caleb Yearsley
        //
        // *relative to this file.
        // ============================================================================== //

        // ============================================================================== //
        //
        // S O L V E ( )
        //
        // ============================================================================== //
        protected override void solve(List<List<int>> puzzle, int x = 0, int y = 0)
        {
            int size = puzzle.Count;
            int currentValue = puzzle[1][0];

            //We don't want to the indices to be out of bounds OR have more than one solution to the puzzle.
            if (x > size - 1 || y > size - 1 || numberOfSolutions > 1)
            {
                return;
            }

            //Is this node already populated?
            if (puzzle[x][y] != 0)
            {
                // If this 'if' passes, we have reached the end of the puzzle!
                if (x >= size - 1 && y >= size - 1)
                {
                    // Check to see if we have already found this solution, if not, add it.
                    if (!listOfSolutions.Contains(puzzle))
                    {
                        listOfSolutions.Add(puzzle);
                        numberOfSolutions++;
                        print(puzzle);
                    }
                    return;
                }
                if (x >= size - 1)
                {
                    solve(puzzle, 0, y + 1);
                }
            }

            // If the node is empty, figure out if we can populate it.
            else
            {
                for (int ii = 1; ii <= size; ii++)
                {
                    if (checkIfValid(puzzle, x, y, ii))
                    {
                        puzzle[x][y] = ii;
                        if (x >= size - 1 && y >= size - 1)
                        {
                            if (!listOfSolutions.Contains(puzzle))
                            {
                                listOfSolutions.Add(puzzle);
                                numberOfSolutions++;
                                print(puzzle);
                            }
                        }
                        else if (x >= size - 1)
                        {
                            solve(puzzle, 0, y + 1);
                        }
                        else
                        {
                            solve(puzzle, x + 1, y);
                        }
                    }
                    else if (ii >= size)
                    {
                        return;
                    }
                }
            }
            if (x >= size - 1)
            {
                solve(puzzle, 0, y + 1);
            }
            else
            {
                solve(puzzle, x + 1, y);
            }
        }

        // ============================================================================== //
        //
        // C H E C K  I F  V A L I D ( )
        //
        // ============================================================================== //
        protected override bool checkIfValid(List<List<int>> puzzle, int x, int y, int num)
        {
            int size = puzzle.Count;
            int divisionSize = (int)Math.Sqrt(size);

            //check rows and columns
            for (int ii = 0; ii < size; ii++)
            {
                if (ii != x)
                {
                    if (num == puzzle[ii][y] || num == puzzle[x][ii] || num == 0)
                    {
                        return false;
                    }
                }
            }

            //check subdivisions
            int startX = 0, endX = 0, startY = 0, endY = 0;

            for (int ii = 0; ii < divisionSize; ii++)
            {
                if (x >= ii * divisionSize && x <= divisionSize * (ii + 1))
                {
                    startX = ii * divisionSize;
                    endX = (ii + 1) * divisionSize;
                }
                if (y >= ii * divisionSize && y <= divisionSize * (ii + 1))
                {
                    startY = ii * divisionSize;
                    endY = (ii + 1) * divisionSize;
                }
            }

            //Iterate through subdivision and make sure num isn't already in this subdivision
            for (int ii = startX; ii < endX; ii++)
            {
                for (int jj = startY; jj < endY; jj++)
                {
                    if (puzzle[ii][jj] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
