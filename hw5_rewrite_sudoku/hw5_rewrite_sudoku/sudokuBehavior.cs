using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hw5_sudoku_algorithms;

namespace hw5_puzzle
{
    class sudokuPromptBehavior : IpromptBehavior
    {
        public String prompt()
        {
            Console.WriteLine("\n\nPlease enter one of the following options:");
            Console.WriteLine("==========================================");
            Console.WriteLine("a) Run naive Dancing Links algorithm");
            Console.WriteLine("b) Run imporoved Dancing Links algorithm");
            Console.WriteLine("c) Run Backtracking algorithm");
            Console.WriteLine("x) Exit");

            String userInput = Console.ReadLine();

            switch (userInput.ToLower())
            {
                case "a":
                    userInput = "NDLA";
                    break;
                case "b":
                    userInput = "DLA";
                    break;
                case "c":
                    userInput = "RA";
                    break;
                case "x":
                    userInput = "X";
                    break;
                default:
                    Console.WriteLine("You selected an invalid option, please try again!\n\n");
                    userInput = "INVALID";
                    break;
            }

            return userInput;
        }
    }

    //class sudokuSolveBehavior : IsolveBehavior
    //{
    //    public bool solve()
    //    {
    //        return true;
    //    }
    //}
}
