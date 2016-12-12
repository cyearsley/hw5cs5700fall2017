using System;
using System.Collections.Generic;
using utility_objects;

namespace hw5_sudoku_algorithms
{
    public abstract class sudokuAlgorithm
    {
        protected int numberOfSolutions;
        protected List<List<int>> rawPuzzle;
        protected string[] puzzleArray;
        private fileHandler fh;
        private puzzleValidator pv;
        protected abstract void executeSudokuAlgorithm(List<List<int>> puzzle);
        protected abstract void solve(List<List<int>> puzzle, int a, int b);
        protected abstract bool checkIfValid(List<List<int>> puzzle, int a, int b, int c);

        public sudokuAlgorithm()
        {
            this.numberOfSolutions = 0;
            this.fh = new fileHandler();
            this.pv = new puzzleValidator();
        }

        public bool solvePuzzle(String testFileName = "")
        {
            if (testFileName != "" || this.fh.getFileName())
            {
                if (testFileName != "")
                {
                    fh.getFileName(testFileName);
                }
                if (checkFormat())
                {
                    rawPuzzle = pv.generateValidatedPuzzle(puzzleArray);
                    executeSudokuAlgorithm(rawPuzzle);
                }
                else
                {
                    Console.WriteLine("The puzzle given by this file does NOT have an appropriate format!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("There was a problem identifying the file you specified!");
                return false;
            }
            return true;
        }

        private bool checkFormat()
        {
            string fileContentsString = fh.readFileAsString();
            if (pv.checkFormat(fileContentsString, fh))
            {
                puzzleArray = fh.readFileAsArray();
            }
            else
            {
                return false;
            }

            return true;
        }

        protected void print(List<List<int>> puzzle)
        {
            if (numberOfSolutions == 1)
            {
                Console.WriteLine("\nYou have solved the puzzle! There existed only ONE solution for this puzzle!");
                Console.WriteLine("The solution has been saved to the file: solution.txt\n");
                System.IO.StreamWriter file = new System.IO.StreamWriter(@"../../../solution.txt");
                for (int ii = 1; ii < puzzle.Count + 1; ii++)
                {
                    if (ii > 9)
                    {
                        file.Write(ii + "   ");
                    }
                    else
                    {
                        file.Write(" " + ii + "   ");
                    }
                }
                file.WriteLine(" ");
                for (int ii = 0; ii < puzzle.Count; ii++)
                {
                    file.Write("=====");
                }
                for (int ii = 0; ii < puzzle.Count; ii++)
                {
                    file.WriteLine(" ");
                    for (int jj = 0; jj < puzzle[0].Count; jj++)
                    {
                        if (puzzle[ii][jj] > 9)
                        {
                            file.Write(puzzle[ii][jj] + " | ");
                        }
                        else
                        {
                            file.Write(" " + puzzle[ii][jj] + " | ");
                        }
                    }
                }
                file.Close();
            }
        }
    }
}
