using System;

namespace SudokuApplication.Core.Models
{
    public class SudokuCell
    {
        private byte? value;

        public SudokuCell(byte? value, bool isReadOnly)
        {
            this.Value = value;
            this.IsReadOnly = isReadOnly;
        }

        public SudokuCell(SudokuCell sudokuCell)
        {
            if (sudokuCell != null)
            {
                this.Value = sudokuCell.Value;
                this.IsReadOnly = sudokuCell.IsReadOnly;
            }
        }

        public byte? Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (value != null && (value < 1 || 9 < value))
                {
                    throw new ArgumentOutOfRangeException("SudokuCell value must be between 1 and 9.");
                }

                this.value = value;
            }
        }

        public bool IsReadOnly { get; set; }
    }
}
