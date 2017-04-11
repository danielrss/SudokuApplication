using NUnit.Framework;

namespace SudokuApplication.Core.Tests.SudokuSolver
{
    [TestFixture]
    public class SolveSudoku_Should
    {
        private const string InvalidSudokuBoardMessage = "SudokuBoard must be a 9x9 jagged byte array!";

        [TestCase(0, 9)]
        [TestCase(5, 9)]
        [TestCase(10, 9)]
        [TestCase(9, 0)]
        [TestCase(9, 5)]
        [TestCase(9, 10)]
        public void ThrowArgumentException_WhenInvalidSudokuBoardIsPassed(byte firstDimensionLength, byte secondDimensionLength)
        {
            var sudokuSolver = new Core.SudokuSolver();
            byte[][] sudokuBoard;
            if (firstDimensionLength != 0)
            {
                sudokuBoard = new byte[firstDimensionLength][];
                if (secondDimensionLength != 0)
                {
                    for (int i = 0; i < firstDimensionLength; i++)
                    {
                        sudokuBoard[i] = new byte[secondDimensionLength];
                    }
                }
                else
                {
                    for (int i = 0; i < firstDimensionLength; i++)
                    {
                        sudokuBoard[i] = null;
                    }
                }
            }
            else
            {
                sudokuBoard = null;
            }

            Assert.That(
                () => sudokuSolver.SolveSudoku(sudokuBoard),
                Throws.ArgumentException.With.Message.Matches(InvalidSudokuBoardMessage));
        }

        [Test]
        public void NotThrow_WhenValidSudokuBoardIsPassed()
        {
            var sudokuSolver = new Core.SudokuSolver();
            var sudokuBoard = this.CreateSudokuBoard();

            Assert.DoesNotThrow(() => sudokuSolver.SolveSudoku(sudokuBoard));
        }

        [Test]
        public void SolveSudokuBoardCorrectly_WhenValidSudokuBoardIsPassed()
        {
            var sudokuSolver = new Core.SudokuSolver();
            var sudokuBoard = this.CreateSudokuBoard();
            sudokuSolver.SolveSudoku(sudokuBoard);

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

        private byte[][] CreateSudokuBoard()
        {
            var sudokuBoard = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                sudokuBoard[i] = new byte[9];
            }

            return sudokuBoard;
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
