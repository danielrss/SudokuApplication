namespace SudokuApplication.Core.Contracts
{
    /// <summary>
    /// All implementations of this interface have the functionality for validating a new cell in a sudoku and solving a sudoku.
    /// </summary>
    public interface ISudokuSolver
    {
        /// <summary>
        /// Validates the new cell by the sudoku rules.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        /// <param name="row">Row of the new cell.</param>
        /// <param name="column">Column of the new cell.</param>
        /// <param name="value">Value of the new cell.</param>
        /// <returns>Whether the new cell is valid.</returns>
        bool IsNewCellValid(byte[][] sudokuBoard, byte row, byte column, byte value);

        /// <summary>
        /// Solves a sudoku by the sudoku rules.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        /// <returns>Whether the sudoku can be solved.</returns>
        bool SolveSudoku(byte[][] sudokuBoard);
    }
}