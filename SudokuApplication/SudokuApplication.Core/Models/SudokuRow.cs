using System;

namespace SudokuApplication.Core.Models
{
    public class SudokuRow
    {
        private const string SudokuRowIndexOutOfRangeMessage = "The sudoku row has only nine elements indexed from zero.";

        public SudokuRow()
        {
        }

        public SudokuRow(SudokuRow sudokuRow)
        {
            if (sudokuRow != null)
            {
                this.Cell1 = new SudokuCell(sudokuRow.Cell1);
                this.Cell2 = new SudokuCell(sudokuRow.Cell2);
                this.Cell3 = new SudokuCell(sudokuRow.Cell3);
                this.Cell4 = new SudokuCell(sudokuRow.Cell4);
                this.Cell5 = new SudokuCell(sudokuRow.Cell5);
                this.Cell6 = new SudokuCell(sudokuRow.Cell6);
                this.Cell7 = new SudokuCell(sudokuRow.Cell7);
                this.Cell8 = new SudokuCell(sudokuRow.Cell8);
                this.Cell9 = new SudokuCell(sudokuRow.Cell9);
            }
        }

        public SudokuCell Cell1 { get; set; }
        public SudokuCell Cell2 { get; set; }
        public SudokuCell Cell3 { get; set; }
        public SudokuCell Cell4 { get; set; }
        public SudokuCell Cell5 { get; set; }
        public SudokuCell Cell6 { get; set; }
        public SudokuCell Cell7 { get; set; }
        public SudokuCell Cell8 { get; set; }
        public SudokuCell Cell9 { get; set; }

        public SudokuCell this[int index]  
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.Cell1;
                    case 1:
                        return this.Cell2;
                    case 2:
                        return this.Cell3;
                    case 3:
                        return this.Cell4;
                    case 4:
                        return this.Cell5;
                    case 5:
                        return this.Cell6;
                    case 6:
                        return this.Cell7;
                    case 7:
                        return this.Cell8;
                    case 8:
                        return this.Cell9;
                    default:
                        throw new IndexOutOfRangeException(SudokuRowIndexOutOfRangeMessage);
                }

            }

            set
            {
                switch (index)
                {
                    case 0:
                        this.Cell1 = value;
                        break;
                    case 1:
                        this.Cell2 = value;
                        break;
                    case 2:
                        this.Cell3 = value;
                        break;
                    case 3:
                        this.Cell4 = value;
                        break;
                    case 4:
                        this.Cell5 = value;
                        break;
                    case 5:
                        this.Cell6 = value;
                        break;
                    case 6:
                        this.Cell7 = value;
                        break;
                    case 7:
                        this.Cell8 = value;
                        break;
                    case 8:
                        this.Cell9 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException(SudokuRowIndexOutOfRangeMessage);
                }
            }
        }
    }
}
