using NUnit.Framework;

namespace SudokuApplication.Core.Tests.SudokuTransformer
{
    [TestFixture]
    public class ShuffleSudoku_Should
    {
        [Test]
        public void MoveAtLeast61CellsFromSudokuBoard_WhenValid9x9BoardIsPassed()
        {
            var sudokuTransformer = new Core.SudokuTransformer();
            var initialSudokuBoard = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                initialSudokuBoard[i] = new byte[9];
                for (int j = 0; j < 9; j++)
                {
                    initialSudokuBoard[i][j] = (byte)(i * 10 + j);
                }
            }

            var shuffledSudokuBoard = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                shuffledSudokuBoard[i] = new byte[9];
                initialSudokuBoard[i].CopyTo(shuffledSudokuBoard[i], 0);
            }

            sudokuTransformer.ShuffleSudoku(shuffledSudokuBoard);

            int movedCells = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (initialSudokuBoard[i][j] != shuffledSudokuBoard[i][j])
                    {
                        movedCells++;
                    }
                }
            }

            Assert.GreaterOrEqual(movedCells, 61);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void NotBreakTheValidityOfThePassedSudokuBoard_WhenValid9x9BoardIsPassed(int timesToShuffle)
        {
            var sudokuTransformer = new Core.SudokuTransformer();
            var sudokuBoard = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                sudokuBoard[i] = new byte[9];
            }

            this.SolveSudoku(sudokuBoard);
            for (int i = 0; i < timesToShuffle; i++)
            {
                sudokuTransformer.ShuffleSudoku(sudokuBoard);
            }

            bool isSudokuCorrect = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!this.IsCellOk(sudokuBoard, i, j, sudokuBoard[i][j]))
                    {
                        isSudokuCorrect = false;
                    }
                }
            }

            Assert.IsTrue(isSudokuCorrect);
        }

        private bool SolveSudoku(byte[][] sudokuBoard, int row = 0, int column = 0)
        {
            if (column == 9)
            {
                row++;
                column = 0;
                if (row == 9)
                {
                    return true;
                }
            }
            
            if (sudokuBoard[row][column] > 0)
            {
                return this.SolveSudoku(sudokuBoard, row, column + 1);
            }

            for (int cellValue = 1; cellValue <= 9; ++cellValue)
            {
                bool isCellValueValid = true;

                for (int i = 0; i < 9; ++i)
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
                    continue;
                }

                sudokuBoard[row][column] = (byte)cellValue;

                if (this.SolveSudoku(sudokuBoard, row, column + 1))
                {
                    return true;
                }

                sudokuBoard[row][column] = 0;
            }

            return false;
        }

        private bool IsCellOk(byte[][] sudokuBoard, int row, int col, byte value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (sudokuBoard[row][i] == value && i != col)
                {
                    return false;
                }

                if (sudokuBoard[i][col] == value && i != row)
                {
                    return false;
                }

                int groupRow = row / 3 * 3 + i / 3;
                int groupCol = col / 3 * 3 + i % 3;
                if (sudokuBoard[groupRow][groupCol] == value && (groupRow != row || groupCol != col))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
