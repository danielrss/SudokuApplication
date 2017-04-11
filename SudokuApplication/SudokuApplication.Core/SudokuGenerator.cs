using System;

using SudokuApplication.Core.Contracts;
using SudokuApplication.Core.Enums;
using SudokuApplication.Core.Models;

namespace SudokuApplication.Core
{
    /// <summary>
    /// This class has the functionality for generating valid sudoku grids ready for solving.
    /// </summary>
    public class SudokuGenerator : ISudokuGenerator
    {
        private readonly ISudokuSolver sudokuSolver;
        private readonly ISudokuTransformer sudokuTransformer;
        private byte[][] generatedSudokuBoard;
        private byte[][] sudokuBoardForPlayer;

        public SudokuGenerator(
            ISudokuSolver sudokuSolver, 
            ISudokuTransformer sudokuTransformer)
        {
            if (sudokuSolver == null || sudokuTransformer == null)
            {
                throw new ArgumentNullException("SudokuSolver or sudokuTransformer is null!");
            }

            this.sudokuSolver = sudokuSolver;
            this.sudokuTransformer = sudokuTransformer;

            this.generatedSudokuBoard = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                this.generatedSudokuBoard[i] = new byte[9];
            }

            // This generates the lexicographically smallest solved sudoku, because until this moment
            // the generatedSudokuBoard array is filled only with zeroes.
            // So we save it and later generate other sudoku boards from it by shuffling it.
            this.sudokuSolver.SolveSudoku(this.generatedSudokuBoard);

            this.sudokuBoardForPlayer = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                this.sudokuBoardForPlayer[i] = new byte[9];
            }
        }

        public SudokuGenerator() 
            : this(new SudokuSolver(), new SudokuTransformer())
        {
        }

        public byte[][] GeneratedSudokuBoard
        {
            get
            {
                return this.generatedSudokuBoard;
            }
        }

        public byte[][] SudokuBoardForPlayer
        {
            get
            {
                return this.sudokuBoardForPlayer;
            }
        }

        /// <summary>
        /// Generates a valid sudoku grid ready for solving.
        /// </summary>
        /// <param name="sudokuDifficulty">The difficulty for the generated sudoku.</param>
        public SudokuRow[] GenerateSudoku(SudokuDifficultyType sudokuDifficulty)
        {
            this.sudokuTransformer.ShuffleSudoku(this.generatedSudokuBoard);
            for (int i = 0; i < 9; i++)
            {
                this.generatedSudokuBoard[i].CopyTo(this.sudokuBoardForPlayer[i], 0);
            }

            this.sudokuTransformer.EraseCells(this.sudokuBoardForPlayer, sudokuDifficulty);

            return SudokuUtils.GenerateSudokuGridFromBoard(this.sudokuBoardForPlayer);
        }
    }
}
