using Moq;
using NUnit.Framework;

namespace SudokuApplication.Core.Tests.SudokuSolver
{
    [TestFixture]
    public class IsNewCellValid_Should
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

            var byteValue = It.IsAny<byte>();

            Assert.That(
                () => sudokuSolver.IsNewCellValid(sudokuBoard, byteValue, byteValue, byteValue),
                Throws.ArgumentException.With.Message.Matches(InvalidSudokuBoardMessage));
        }

        [Test]
        public void NotThrow_WhenValidSudokuBoardIsPassed()
        {
            var sudokuSolver = new Core.SudokuSolver();
            var sudokuBoard = this.CreateSudokuBoard();
            var byteValue = It.IsAny<byte>();

            Assert.DoesNotThrow(() => sudokuSolver.IsNewCellValid(sudokuBoard, byteValue, byteValue, byteValue));
        }

        [Test]
        public void ReturnFalse_WhenThereIsACellWithTheSameValueInTheSameRow()
        {
            var sudokuSolver = new Core.SudokuSolver();
            var sudokuBoard = this.CreateSudokuBoard();
            var row = It.IsInRange<byte>(0, 8, Range.Inclusive);
            var col = It.IsInRange<byte>(0, 8, Range.Inclusive);
            var cellValue = It.IsAny<byte>();
            sudokuBoard[row][col] = cellValue;
            var newCellCol = It.Is<byte>(c => c != col);

            bool isNewCellOk = sudokuSolver.IsNewCellValid(sudokuBoard, row, newCellCol, cellValue);

            Assert.IsFalse(isNewCellOk);
        }

        [Test]
        public void ReturnFalse_WhenThereIsACellWithTheSameValueInTheSameColumn()
        {
            var sudokuSolver = new Core.SudokuSolver();
            var sudokuBoard = this.CreateSudokuBoard();
            var row = It.IsInRange<byte>(0, 8, Range.Inclusive);
            var col = It.IsInRange<byte>(0, 8, Range.Inclusive);
            var cellValue = It.IsAny<byte>();
            sudokuBoard[row][col] = cellValue;
            var newCellRow = It.Is<byte>(r => r != row);
            while (newCellRow == row)
            {
                newCellRow++;
                if (newCellRow > 8)
                {
                    newCellRow -= 2;
                }
            }

            bool isNewCellOk = sudokuSolver.IsNewCellValid(sudokuBoard, newCellRow, col, cellValue);

            Assert.IsFalse(isNewCellOk);
        }

        [Test]
        public void ReturnFalse_WhenThereIsACellWithTheSameValueInTheSame3x3Grid()
        {
            var sudokuSolver = new Core.SudokuSolver();
            var sudokuBoard = this.CreateSudokuBoard();
            var row = It.IsInRange<byte>(0, 8, Range.Inclusive);
            var col = It.IsInRange<byte>(0, 8, Range.Inclusive);
            var cellValue = It.IsAny<byte>();
            sudokuBoard[row][col] = cellValue;
            var newCellRow = (byte)(row / 3 * 3 + 1);
            var newCellCol = (byte)(col / 3 * 3 + 1);

            bool isNewCellOk = sudokuSolver.IsNewCellValid(sudokuBoard, newCellRow, newCellCol, cellValue);

            Assert.IsFalse(isNewCellOk);
        }

        [Test]
        public void ReturnTrue_WhenNewCellIsOk()
        {
            var sudokuSolver = new Core.SudokuSolver();
            var sudokuBoard = this.CreateSudokuBoard();
            var row = It.IsInRange<byte>(0, 8, Range.Inclusive);
            var col = It.IsInRange<byte>(0, 8, Range.Inclusive);
            byte cellValue = 1;

            bool isNewCellOk = sudokuSolver.IsNewCellValid(sudokuBoard, row, col, cellValue);

            Assert.IsTrue(isNewCellOk);
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
    }
}
