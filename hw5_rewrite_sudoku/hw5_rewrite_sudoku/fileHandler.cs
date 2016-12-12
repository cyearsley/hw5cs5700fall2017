using System;

namespace utility_objects
{
    public class fileHandler
    {
        private String puzzleFile;

        public fileHandler()
        {
            this.puzzleFile = "empty";
        }

        public String getPuzzleFile()
        {
            return this.puzzleFile;
        }

        public bool getFileName(String isTest = "false")
        {
            Console.WriteLine("Enter the file name that contains the sudoku puzzle.");
            Console.Write("Enter: ");
            String userInput = isTest;
            if (isTest == "false")
            {
                userInput = Console.ReadLine();
            }

            try
            {
                string[] fileContents = System.IO.File.ReadAllLines(@"../../" + userInput);
            }
            catch (Exception exp)
            {
                return false;
            }

            this.puzzleFile = "../../" + userInput;
            return true;
        }

        public String readFileAsString()
        {
            if (puzzleFile == "empty")
            {
                Console.WriteLine("You need to specify a puzzle file first!");
                return "empty";
            }
            return System.IO.File.ReadAllText(@puzzleFile);
        }

        public String[] readFileAsArray()
        {
            if (puzzleFile == "empty")
            {
                Console.WriteLine("You need to specify a puzzle file first!");
                return new String[] { };
            }
            return System.IO.File.ReadAllLines(@puzzleFile);
        }
    }
}
