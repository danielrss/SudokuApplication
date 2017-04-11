using System;

using SudokuApplication.Core.Contracts;

namespace SudokuApplication.Core
{
    /// <summary>
    /// This class has the functionality for validating a new sudoku cell and solving a sudoku.
    /// </summary>
    public class SudokuSolver : ISudokuSolver
    {
        private const string InvalidSudokuBoardMessage = "SudokuBoard must be a 9x9 jagged byte array!";

        /// <summary>
        /// Checks if the new cell is valid according to the sudoku rules. 
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        /// <param name="row">Row of the new cell.</param>
        /// <param name="column">Column of the new cell.</param>
        /// <param name="value">Value of the new cell.</param>
        /// <returns>Whether the new cell is valid.</returns>
        public bool IsNewCellValid(byte[][] sudokuBoard, byte row, byte column, byte value)
        {
            if (!SudokuUtils.IsSudokuBoardValid(sudokuBoard))
            {
                throw new ArgumentException(InvalidSudokuBoardMessage);
            }

            if ((row < 0 || row > 8) || (column < 0 || column > 8))
            {
                throw new IndexOutOfRangeException("Row and column values must be between 0 and 8!");
            }

            for (int i = 0; i < 9; i++)
            {
                // check row
                if (sudokuBoard[row][i] == value && i != column)
                {
                    return false;
                }

                // check column
                if (sudokuBoard[i][column] == value && i != row)
                {
                    return false;
                }

                // check 3x3 grid
                int groupRow = row / 3 * 3 + i / 3;
                int groupCol = column / 3 * 3 + i % 3;
                if (sudokuBoard[groupRow][groupCol] == value && (groupRow != row || groupCol != column))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Fills all empty cells of the sudoku board according to the sudoku rules.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        /// <returns>Whether the sudoku is solvable.</returns>
        public bool SolveSudoku(byte[][] sudokuBoard)
        {
            if (!SudokuUtils.IsSudokuBoardValid(sudokuBoard))
            {
                throw new ArgumentException(InvalidSudokuBoardMessage);
            }

            return this.SolveSudokuRec(sudokuBoard);
        }

        /// <summary>
        /// Solves a sudoku with a backtracking algorithm.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        /// <returns>Whether the sudoku can be solved.</returns>
        private bool SolveSudokuRec(byte[][] sudokuBoard, int row = 0, int column = 0)
        {
            if (column == 9)
            {
                // we've hit the end of the row and go to the next one
                row++;
                column = 0;
                if (row == 9)
                {
                    // end of recursion
                    return true;
                }
            }

            // if the cell isn't empty, we go to the next
            if (sudokuBoard[row][column] > 0)
            {
                return this.SolveSudokuRec(sudokuBoard, row, column + 1);
            }

            for (int cellValue = 1; cellValue <= 9; cellValue++)
            {
                bool isCellValueValid = true;

                for (int i = 0; i < 9; i++)
                {
                    if (sudokuBoard[row][i] == cellValue ||
                        sudokuBoard[i][column] == cellValue ||
                        sudokuBoard[row / 3 * 3 + i / 3][column / 3 * 3 + i % 3] == cellValue)
                    {
                        isCellValueValid = false;
                        break;
                    }
                }

                if (!isCellValueValid)
                {
                    // try next value
                    continue;
                }

                // save the value
                sudokuBoard[row][column] = (byte)cellValue;

                // repeat the algorithm for the next cell
                if (this.SolveSudokuRec(sudokuBoard, row, column + 1))
                {
                    return true;
                }

                sudokuBoard[row][column] = 0;
            }

            // if we've hit the next line, there is no solution
            return false;
        }
    }
}
