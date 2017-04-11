using SudokuApplication.Core.Enums;
using SudokuApplication.Core.Models;

namespace SudokuApplication.Core.Contracts
{
    /// <summary>
    /// All implementations of this interface have the functionality for generating a valid sudoku grid.
    /// </summary>
    public interface ISudokuGenerator
    {
        /// <summary>
        /// Generates a valid sudoku grid ready for solving.
        /// </summary>
        /// <param name="sudokuDifficulty">The difficulty of the generated sudoku grid.</param>
        /// <returns>A valid sudoku grid of the required difficulty.</returns>
        SudokuRow[] GenerateSudoku(SudokuDifficultyType sudokuDifficulty);
    }
}
