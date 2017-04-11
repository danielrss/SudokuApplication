using System;

using SudokuApplication.Core.Models;

namespace SudokuApplication.Core
{
    /// <summary>
    /// A static class with helpful methods for the other sudoku-related classes.
    /// </summary>
    public static class SudokuUtils
    {
        private const string InvalidSudokuGridMessage = "SudokuGrid must have 9 elements!";
        private const string InvalidSudokuBoardMessage = "SudokuBoard must be a 9x9 jagged byte array!";

        /// <summary>
        /// Validates if the passed jagged array is an instantiated 9x9 jagged array.
        /// </summary>
        public static bool IsSudokuBoardValid(byte[][] sudokuBoard)
        {
            if (sudokuBoard == null || sudokuBoard.Length != 9)
            {
                return false;
            }

            for (int i = 0; i < 9; i++)
            {
                if (sudokuBoard[i] == null || sudokuBoard[i].Length != 9)
                {
                    return false;
                }
            }

            return true;
        }

        public static void CopySudokuGrid(SudokuRow[] sudokuGridFrom, SudokuRow[] sudokuGridTo)
        {
            if (sudokuGridFrom == null || sudokuGridFrom.Length != 9 ||
                sudokuGridTo == null || sudokuGridTo.Length != 9)
            {
                throw new ArgumentException(InvalidSudokuGridMessage);
            }

            for (int i = 0; i < 9; i++)
            {
                sudokuGridTo[i] = new SudokuRow(sudokuGridFrom[i]);
            }
        }

        /// <summary>
        /// Finds how many cells from the sudoku grid have values.
        /// </summary>
        /// <param name="isFromPlayer">Whether to count the cells filled from the player or the initially filled cells only.</param>
        /// <returns>The number of filled cells.</returns>
        public static int GetFilledSudokuCellsCount(SudokuRow[] sudokuGrid, bool isFromPlayer)
        {
            if (sudokuGrid == null || sudokuGrid.Length != 9)
            {
                throw new ArgumentException(InvalidSudokuGridMessage);
            }

            int filledCells = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuGrid[i][j] != null &&
                        sudokuGrid[i][j].Value != null)
                    {
                        if (isFromPlayer)
                        {
                            if (!sudokuGrid[i][j].IsReadOnly)
                            {
                                filledCells++;
                            }
                        }
                        else
                        {
                            if (sudokuGrid[i][j].IsReadOnly)
                            {
                                filledCells++;
                            }
                        }
                    }
                }
            }

            return filledCells;
        }

        /// <summary>
        /// Turns the SudokuRow array into a jagged byte array.
        /// </summary>
        public static byte[][] GenerateSudokuBoardFromGrid(SudokuRow[] sudokuGrid)
        {
            if (sudokuGrid == null || sudokuGrid.Length != 9)
            {
                throw new ArgumentException(InvalidSudokuGridMessage);
            }

            var sudokuBoard = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                sudokuBoard[i] = new byte[9];
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuGrid[i][j] != null && sudokuGrid[i][j].Value != null)
                    {
                        sudokuBoard[i][j] = (byte)sudokuGrid[i][j].Value;
                    }
                    else
                    {
                        sudokuBoard[i][j] = 0;
                    }
                }
            }

            return sudokuBoard;
        }

        /// <summary>
        /// Turns the jagged byte array array into a SudokuRow array.
        /// </summary>
        public static SudokuRow[] GenerateSudokuGridFromBoard(byte[][] sudokuBoard)
        {
            if (!IsSudokuBoardValid(sudokuBoard))
            {
                throw new ArgumentException(InvalidSudokuBoardMessage);
            }

            var sudokuGrid = new SudokuRow[9];
            for (int i = 0; i < 9; i++)
            {
                sudokuGrid[i] = new SudokuRow();
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuBoard[i][j] == 0)
                    {
                        sudokuGrid[i][j] = new SudokuCell(null, false); ;
                    }
                    else
                    {
                        sudokuGrid[i][j] = new SudokuCell(sudokuBoard[i][j], true);
                    }
                }
            }

            return sudokuGrid;
        }
    }
}
