using System;

namespace SudokuApplication.Core.Models.PlayerDecisions
{
    /// <summary>
    /// Represents a player decision where the whole sudoku grid changes.
    /// </summary>
    public abstract class CompletelyChangeSudokuGridDecision
    {
        private SudokuRow[] sudokuGridBeforeDecision;

        public CompletelyChangeSudokuGridDecision(SudokuRow[] sudokuGrid)
        {
            this.SudokuGridBeforeDecision = sudokuGrid;
        }

        public SudokuRow[] SudokuGridBeforeDecision
        {
            get
            {
                return this.sudokuGridBeforeDecision;
            }

            private set
            {
                if (value == null || value.Length != 9)
                {
                    throw new ArgumentException("SudokuGrid must have nine elements!");
                }

                this.sudokuGridBeforeDecision = value;
            }
        }
    }
}
