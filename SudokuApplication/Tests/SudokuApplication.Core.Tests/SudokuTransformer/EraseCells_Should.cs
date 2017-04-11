using NUnit.Framework;

using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core.Tests.SudokuTransformer
{
    [TestFixture]
    public class EraseCells_Should
    {
        private const int CellsToEraseOnEasyDifficulty = 40;
        private const int CellsToEraseOnMediumDifficulty = 45;
        private const int CellsToEraseOnHardDifficulty = 50;
        private const int CellsToEraseOnImpossibleDifficulty = 55;

        [TestCase(SudokuDifficultyType.Easy)]
        [TestCase(SudokuDifficultyType.Medium)]
        [TestCase(SudokuDifficultyType.Hard)]
        [TestCase(SudokuDifficultyType.Impossible)]
        public void EraseDifferentNumberOfCells_WhenDifferentDifficultyTypeIsPassed(SudokuDifficultyType sudokuDifficulty)
        {
            var sudokuTransformer = new Core.SudokuTransformer();
            var sudokuBoard = new byte[9][];
            for (int i = 0; i < 9; i++)
            {
                sudokuBoard[i] = new byte[9];
                for (int j = 0; j < 9; j++)
                {
                    sudokuBoard[i][j] = 1;
                }
            }

            sudokuTransformer.EraseCells(sudokuBoard, sudokuDifficulty);

            int emptyCells = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuBoard[i][j] == 0)
                    {
                        emptyCells++;
                    }
                }
            }

            if (sudokuDifficulty == SudokuDifficultyType.Easy)
            {
                Assert.AreEqual(CellsToEraseOnEasyDifficulty, emptyCells);
            }
            else if (sudokuDifficulty == SudokuDifficultyType.Medium)
            {
                Assert.AreEqual(CellsToEraseOnMediumDifficulty, emptyCells);
            }
            else if (sudokuDifficulty == SudokuDifficultyType.Hard)
            {
                Assert.AreEqual(CellsToEraseOnHardDifficulty, emptyCells);
            }
            else
            {
                Assert.AreEqual(CellsToEraseOnImpossibleDifficulty, emptyCells);
            }
        }
    }
}
