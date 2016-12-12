using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace utility_objects
{
    public class puzzleValidator
    {
        public bool checkFormat(String fileString, fileHandler fh)
        {
            Regex regexp = new Regex(@"[^0-9\-\s]*", RegexOptions.Singleline);
            MatchCollection matches = regexp.Matches(fileString);
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

            return true;
        }

        public List<List<int>> generateValidatedPuzzle(string[] fileContentsArray)
        {
            List<List<int>> rawPuzzle = new List<List<int>>();
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

            return rawPuzzle;
        }
    }
}
