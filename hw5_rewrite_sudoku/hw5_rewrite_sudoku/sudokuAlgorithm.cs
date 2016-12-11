using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using file_handler;
using System.Text.RegularExpressions;

namespace hw5_sudoku_algorithms
{
    public abstract class sudokuAlgorithm
    {
        protected int numberOfSolutions;
        protected List<List<int>> rawPuzzle;
        private fileHandler fh;
        protected abstract void executeSudokuAlgorithm(List<List<int>> puzzle);
        protected abstract void solve(List<List<int>> puzzle, int a, int b);
        protected abstract bool checkIfValid(List<List<int>> puzzle, int a, int b, int c);

        public sudokuAlgorithm()
        {
            this.numberOfSolutions = 0;
            this.fh = new fileHandler();
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
            Regex regexp = new Regex(@"[^0-9\-\s]*", RegexOptions.Singleline);
            MatchCollection matches = regexp.Matches(fileContentsString);
            List<String> errorList = new List<String>();
            for (int ii = 0; ii < matches.Count; ii++)
            {
                if (matches[ii].ToString() != "" && matches[ii].ToString() != null)
                {
                    errorList.Add("Invalid Syntax: " + matches[ii].ToString());
                }
            }

            int[] acceptedSizes = { 4, 9, 16, 25, 36 };
            string[] fileContentsArray = fh.readFileAsArray();
            if (!acceptedSizes.Contains(fileContentsArray.Length))
            {
                errorList.Add("Invalid Format - Wrong number of rows!\n " +
                    "Given: " + fileContentsArray.Length + "Expected: 4, 9, 16, 25, or 36.");
            }

            for (int ii = 0; ii < fileContentsArray.Length; ii++)
            {
                int columnCount = fileContentsArray[ii].Split(' ').Length;
                if (!acceptedSizes.Contains(columnCount))
                {
                    errorList.Add("Invalid Format - Wrong number of columns!\n " +
                        "Given: " + columnCount + ". Expected: 4, 9, 16, 25, or 36.");
                }
                if (columnCount != fileContentsArray.Length)
                {
                    errorList.Add("The number of columns do not match the number of rows!\n" +
                        "Given: " + columnCount + ". Expected: " + fileContentsArray.Length);
                }
            }

            if (errorList.Count > 0)
            {
                Console.WriteLine("\nThere was a problem reading your puzzle, here are the errors found:");
                for (int ii = 0; ii < errorList.Count; ii++)
                {
                    Console.WriteLine(ii + 1 + ") " + errorList[ii] + "\n");
                }
                return false;
            }

            rawPuzzle = new List<List<int>>();
            List<List<String>> puzzleTableString = new List<List<String>>();
            for (int ii = 0; ii < fileContentsArray.Length; ii++)
            {
                puzzleTableString.Add(fileContentsArray[ii].Split(' ').ToList());
            }

            for (int ii = 0; ii < puzzleTableString.Count; ii++)
            {
                rawPuzzle.Add(new List<int>());
                for (int jj = 0; jj < puzzleTableString.Count; jj++)
                {
                    if (puzzleTableString[ii][jj] == "-")
                    {
                        rawPuzzle[ii].Add(0);
                    }
                    else
                    {
                        rawPuzzle[ii].Add(Int32.Parse(puzzleTableString[ii][jj]));
                    }
                }
            }

            //Console.WriteLine("Solving the following puzzle: ");
            //for (int ii = 0; ii < rawPuzzle[0].Count; ii++)
            //{
            //    for (int jj = 0; jj < rawPuzzle[0].Count; jj++)
            //    {
            //        Console.Write(rawPuzzle[ii][jj] + " ");
            //    }
            //    Console.WriteLine("\n");
            //}
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
