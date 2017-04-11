using Moq;
using NUnit.Framework;

using SudokuApplication.Core.Contracts;
using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core.Tests.SudokuGenerator
{
    [TestFixture]
    public class GenerateSudoku_Should
    {
        [Test]
        public void CallShuffleSudokuMethodFromSudokuTransformer()
        {
            var sudokuSolverMock = new Mock<ISudokuSolver>();
            var sudokuTransformerMock = new Mock<ISudokuTransformer>();
            var sudokuGenerator = new Core.SudokuGenerator(sudokuSolverMock.Object, sudokuTransformerMock.Object);

            sudokuGenerator.GenerateSudoku(SudokuDifficultyType.Easy);

            sudokuTransformerMock.Verify(
                s => s.ShuffleSudoku(It.IsAny<byte[][]>()),
                Times.Once());
        }

        [Test]
        public void CallEraseCellsMethodFromSudokuTransformer()
        {
            var sudokuSolverMock = new Mock<ISudokuSolver>();
            var sudokuTransformerMock = new Mock<ISudokuTransformer>();
            var sudokuGenerator = new Core.SudokuGenerator(sudokuSolverMock.Object, sudokuTransformerMock.Object);
            var sudokuDifficulty = It.IsAny<SudokuDifficultyType>();

            sudokuGenerator.GenerateSudoku(sudokuDifficulty);

            sudokuTransformerMock.Verify(
                s => s.EraseCells(It.IsAny<byte[][]>(), sudokuDifficulty),
                Times.Once());
        }
    }
}
