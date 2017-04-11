using NUnit.Framework;
using Moq;

using SudokuApplication.Core.Contracts;

namespace SudokuApplication.Core.Tests.SudokuGenerator
{
    [TestFixture]
    public class Constructor_Should
    {
        private const string ConstructorExceptionMessage = "SudokuSolver or sudokuTransformer is null!";

        [Test]
        public void ThrowArgumentNullException_WhenSudokuSolverIsNull()
        {
            var sudokuTransformerMock = new Mock<ISudokuTransformer>();

            Assert.That(
                () => new Core.SudokuGenerator(null, sudokuTransformerMock.Object),
                Throws.ArgumentNullException.With.Message.Matches(ConstructorExceptionMessage));
        }

        [Test]
        public void ThrowArgumentNullException_WhenSudokuTransformerIsNull()
        {
            var sudokuSolverMock = new Mock<ISudokuSolver>();

            Assert.That(
               () => new Core.SudokuGenerator(sudokuSolverMock.Object, null),
               Throws.ArgumentNullException.With.Message.Matches(ConstructorExceptionMessage));
        }

        [Test]
        public void InstantiateSudokuBoardForPlayerWithSize9x9_WhenValidParametersArePassed()
        {
            var sudokuSolverMock = new Mock<ISudokuSolver>();
            var sudokuTransformerMock = new Mock<ISudokuTransformer>();
            var sudokuGenerator = new Core.SudokuGenerator(sudokuSolverMock.Object, sudokuTransformerMock.Object);

            Assert.IsNotNull(sudokuGenerator.SudokuBoardForPlayer);
            Assert.AreEqual(9, sudokuGenerator.SudokuBoardForPlayer.Length);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsNotNull(sudokuGenerator.SudokuBoardForPlayer[i]);
                Assert.AreEqual(9, sudokuGenerator.SudokuBoardForPlayer[i].Length);
            }
        }

        [Test]
        public void InstantiateGeneratedSudokuBoardWithSize9x9_WhenValidParametersArePassed()
        {
            var sudokuSolverMock = new Mock<ISudokuSolver>();
            var sudokuTransformerMock = new Mock<ISudokuTransformer>();
            var sudokuGenerator = new Core.SudokuGenerator(sudokuSolverMock.Object, sudokuTransformerMock.Object);

            Assert.IsNotNull(sudokuGenerator.GeneratedSudokuBoard);
            Assert.AreEqual(9, sudokuGenerator.GeneratedSudokuBoard.Length);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsNotNull(sudokuGenerator.GeneratedSudokuBoard[i]);
                Assert.AreEqual(9, sudokuGenerator.GeneratedSudokuBoard[i].Length);
            }
        }

        [Test]
        public void CallSolveSudokuMethodFromSudokuSolver_WhenValidParametersArePassed()
        {
            var sudokuSolverMock = new Mock<ISudokuSolver>();
            var sudokuTransformerMock = new Mock<ISudokuTransformer>();
            var sudokuGenerator = new Core.SudokuGenerator(sudokuSolverMock.Object, sudokuTransformerMock.Object);

            sudokuSolverMock.Verify(
                s => s.SolveSudoku(It.IsAny<byte[][]>()),
                Times.Once());
        }
    }
}
