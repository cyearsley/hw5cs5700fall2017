using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using utility_objects;
using hw5_sudoku_algorithms;
using System.Text.RegularExpressions;
using hw5_sudokuAlgorithm_factory;
using hw5_puzzle;

namespace hw5_testProject
{
    [TestClass]
    public class sudokuTests
    {
        [TestMethod]
        public void testFileHandler_getFileName_valid()
        {
            fileHandler fh = new utility_objects.fileHandler();
            Assert.IsTrue(fh.getFileName("4x4.txt"));

        }
        [TestMethod]
        public void testFileHandler_getFileName_invalid()
        {
            fileHandler fh = new utility_objects.fileHandler();
            Assert.IsFalse(fh.getFileName("fileThatDoesNotExist.txt"));
        }

        [TestMethod]
        public void testFileHandler_readFileAsString_valid()
        {
            String expectedString = "4 2 - 1\n- - -2\n3 - 2 -\n- 4 - 3";
            fileHandler fh = new utility_objects.fileHandler();
            fh.getFileName("4x4.txt");
            Assert.AreEqual(Regex.Replace(expectedString, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085 ]+", String.Empty), Regex.Replace(fh.readFileAsString(), @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085 ]+", String.Empty));
        }
        [TestMethod]
        public void testFileHandler_readFileAsString_invalid()
        {
            String expectedString = "empty";
            fileHandler fh = new utility_objects.fileHandler();
            Assert.AreEqual(expectedString, fh.readFileAsString());
        }

        [TestMethod]
        public void testFileHandler_readFileAsArray_valid()
        {
            fileHandler fh = new utility_objects.fileHandler();
            fh.getFileName("4x4.txt");
            Assert.AreEqual("4 2 - 1", fh.readFileAsArray()[0]);
            Assert.AreEqual("- - - 2", fh.readFileAsArray()[1]);
            Assert.AreEqual("3 - 2 -", fh.readFileAsArray()[2]);
            Assert.AreEqual("- 4 - 3", fh.readFileAsArray()[3]);
        }
        [TestMethod]
        public void testFileHandler_readFileAsArray_invalid()
        {
            fileHandler fh = new utility_objects.fileHandler();
            Assert.AreSame(new String[] { }.GetType(), fh.readFileAsArray().GetType());
        }

        // Test puzzles with recursive algorithm (can perform up to 25x25 fast enough)
        [TestMethod]
        public void testRescursiveAlgorithm_solvePuzzle_valid_4x4()
        {
            sudokuAlgorithm algorithmMethod = new sudokuRecursiveAlgorithm();
            Assert.IsTrue(algorithmMethod.solvePuzzle("4x4.txt"));
        }
        [TestMethod]
        public void testRescursiveAlgorithm_solvePuzzle_valid_9x9()
        {
            sudokuAlgorithm algorithmMethod = new sudokuRecursiveAlgorithm();
            Assert.IsTrue(algorithmMethod.solvePuzzle("9x9.txt"));
        }
        [TestMethod]
        public void testRescursiveAlgorithm_solvePuzzle_valid_16x16()
        {
            sudokuAlgorithm algorithmMethod = new sudokuRecursiveAlgorithm();
            Assert.IsTrue(algorithmMethod.solvePuzzle("16x16.txt"));
        }
        [TestMethod]
        public void testRescursiveAlgorithm_solvePuzzle_valid_25x25()
        {
            sudokuAlgorithm algorithmMethod = new sudokuRecursiveAlgorithm();
            Assert.IsTrue(algorithmMethod.solvePuzzle("25x25.txt"));
        }
        [TestMethod]
        public void testRescursiveAlgorithm_solvePuzzle_invalid()
        {
            sudokuAlgorithm algorithmMethod = new sudokuRecursiveAlgorithm();
            Assert.IsFalse(algorithmMethod.solvePuzzle("puzzleWithBadFormat.txt"));
        }

        // Test puzzles with Naive Dancing Links Algorithm (can only performs 4x4 fast enough)
        [TestMethod]
        public void testNaiveDLAlgorithm_solvePuzzle_valid_4x4()
        {
            sudokuAlgorithm algorithmMethod = new sudokuNaiveDancingLinksAlgorithm();
            Assert.IsTrue(algorithmMethod.solvePuzzle("4x4.txt"));
        }
        [TestMethod]
        public void testNaiveDLAlgorithm_solvePuzzle_invalid()
        {
            sudokuAlgorithm algorithmMethod = new sudokuNaiveDancingLinksAlgorithm();
            Assert.IsFalse(algorithmMethod.solvePuzzle("puzzleWithBadFormat.txt"));
        }

        // Test puzzles with Dancing Links Algorithm (can only perform up to 9x9 fast enough)
        [TestMethod]
        public void testDLAlgorithm_solvePuzzle_valid_4x4()
        {
            sudokuAlgorithm algorithmMethod = new sudokuDancingLinksAlgorithm();
            Assert.IsTrue(algorithmMethod.solvePuzzle("4x4.txt"));
        }
        [TestMethod]
        public void testDLAlgorithm_solvePuzzle_valid_9x9()
        {
            sudokuAlgorithm algorithmMethod = new sudokuDancingLinksAlgorithm();
            Assert.IsTrue(algorithmMethod.solvePuzzle("9x9.txt"));
        }
        [TestMethod]
        public void testDLAlgorithm_solvePuzzle_invalid()
        {
            sudokuAlgorithm algorithmMethod = new sudokuDancingLinksAlgorithm();
            Assert.IsFalse(algorithmMethod.solvePuzzle("puzzleWithBadFormat.txt"));
        }

        // Test Factory
        // Naive dancing Links
        [TestMethod]
        public void testFactory_NDL()
        {
            algorithmSudokuFactory factory = new algorithmSudokuFactory();
            sudokuAlgorithm algorithmObject = factory.createAlgorithm("NDLA");
            Assert.IsTrue(algorithmObject.GetType() == new sudokuNaiveDancingLinksAlgorithm().GetType());
        }

        // Dancing Links
        [TestMethod]
        public void testFactory_DL()
        {
            algorithmSudokuFactory factory = new algorithmSudokuFactory();
            sudokuAlgorithm algorithmObject = factory.createAlgorithm("DLA");
            Assert.IsTrue(algorithmObject.GetType() == new sudokuDancingLinksAlgorithm().GetType());
        }

        // Recursive
        [TestMethod]
        public void testFactory_recursive()
        {
            algorithmSudokuFactory factory = new algorithmSudokuFactory();
            sudokuAlgorithm algorithmObject = factory.createAlgorithm("RA");
            Assert.IsTrue(algorithmObject.GetType() == new sudokuRecursiveAlgorithm().GetType());
        }

        // Invalid
        [TestMethod]
        public void testFactory_invalid()
        {
            algorithmSudokuFactory factory = new algorithmSudokuFactory();
            sudokuAlgorithm algorithmObject = factory.createAlgorithm("invalid string");
            Assert.IsTrue(algorithmObject == null);
        }

        //Puzzle Validator Tests
        [TestMethod]
        public void testPuzzleValidator_checkFormat_invalid()
        {
            puzzleValidator pv = new puzzleValidator();
            fileHandler fh = new fileHandler();
            fh.getFileName("4x4.txt");
            Assert.IsTrue(pv.checkFormat("bad_file.txt", fh) == false);
        }

        //sudokuBehavior
        // Naive Dancing Links
        [TestMethod]
        public void sudokuPromptBehavior_NDL_valid()
        {
            puzzle sudokuPuzzle = new puzzleSudoku();
            sudokuPuzzle.promptBehavior = new sudokuPromptBehavior();
            Assert.IsTrue(sudokuPuzzle.promptBehavior.prompt("a") == "NDLA");
        }

        // Dancing Links
        [TestMethod]
        public void sudokuPromptBehavior_DL_valid()
        {
            puzzle sudokuPuzzle = new puzzleSudoku();
            sudokuPuzzle.promptBehavior = new sudokuPromptBehavior();
            Assert.IsTrue(sudokuPuzzle.promptBehavior.prompt("b") == "DLA");
        }

        // Recursive
        [TestMethod]
        public void sudokuPromptBehavior_Recursive_valid()
        {
            puzzle sudokuPuzzle = new puzzleSudoku();
            sudokuPuzzle.promptBehavior = new sudokuPromptBehavior();
            Assert.IsTrue(sudokuPuzzle.promptBehavior.prompt("c") == "RA");
        }

        // Invalid
        [TestMethod]
        public void sudokuPromptBehavior_invalid()
        {
            puzzle sudokuPuzzle = new puzzleSudoku();
            sudokuPuzzle.promptBehavior = new sudokuPromptBehavior();
            Assert.IsTrue(sudokuPuzzle.promptBehavior.prompt("invalid") == "INVALID");
        }
    }
}
