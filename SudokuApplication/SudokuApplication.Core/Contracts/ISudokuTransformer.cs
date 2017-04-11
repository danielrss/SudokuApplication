using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core.Contracts
{
    /// <summary>
    /// All implementations of this interface have the functionality for turning a solved sudoku 
    /// into a sudoku that can be solved by a player.
    /// </summary>
    public interface ISudokuTransformer
    {
        /// <summary>
        /// Generates a valid sudoku from another valid sudoku by shuffling it.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        void ShuffleSudoku(byte[][] sudokuBoard);

        /// <summary>
        /// Erases cells from a solved sudoku so that it can be solved by a player.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        /// <param name="difficultyType">On different difficulty types, the method erases different number of cells.</param>
        void EraseCells(byte[][] sudokuBoard, SudokuDifficultyType difficultyType);
    }
}