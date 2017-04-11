using System;

using SudokuApplication.Core.Contracts;
using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core
{
    /// <summary>
    /// This class has the functionality for turning a solved sudoku into a sudoku that can be solved by a player.
    /// </summary>
    public class SudokuTransformer : ISudokuTransformer
    {
        private const int CellsToEraseOnEasyDifficulty = 40;
        private const int CellsToEraseOnMediumDifficulty = 45;
        private const int CellsToEraseOnHardDifficulty = 50;
        private const int CellsToEraseOnImpossibleDifficulty = 55;

        private readonly Random random;
        private const string InvalidSudokuBoardMessage = "SudokuBoard must be a 9x9 jagged byte array!";

        public SudokuTransformer()
        {
            this.random = new Random();
        }

        /// <summary>
        /// Erases different number of sudoku cells based on the passed sudoku difficulty.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        public void EraseCells(byte[][] sudokuBoard, SudokuDifficultyType sudokuDifficulty)
        {
            if (!SudokuUtils.IsSudokuBoardValid(sudokuBoard))
            {
                throw new ArgumentException(InvalidSudokuBoardMessage);
            }

            int cellsToErase = 0;
            if (sudokuDifficulty == SudokuDifficultyType.Easy)
            {
                cellsToErase = CellsToEraseOnEasyDifficulty;
            }
            else if (sudokuDifficulty == SudokuDifficultyType.Medium)
            {
                cellsToErase = CellsToEraseOnMediumDifficulty;
            }
            else if (sudokuDifficulty == SudokuDifficultyType.Hard)
            {
                cellsToErase = CellsToEraseOnHardDifficulty;
            }
            else
            {
                cellsToErase = CellsToEraseOnImpossibleDifficulty;
            }
            
            // the "algorithm" here is that we take a random cell from each row, erase it 
            // and then erase the cell opposite to it by the minor diagonal
            while (cellsToErase > 0)
            {
                for (int row = 0; row < 9; row++)
                {
                    int col = this.random.Next(0, 9);
                    if (sudokuBoard[row][col] != 0)
                    {
                        sudokuBoard[row][col] = 0;
                        cellsToErase--;
                    }

                    if (cellsToErase <= 0)
                    {
                        return;
                    }
                    
                    int oppositeRow = 9 - col - 1;
                    int oppositeCol = 9 - row - 1;
                    if (sudokuBoard[oppositeRow][oppositeCol] != 0)
                    {
                        sudokuBoard[oppositeRow][oppositeCol] = 0;
                        cellsToErase--;
                    }

                    if (cellsToErase <= 0)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Generates a valid sudoku from another valid sudoku by shuffling it.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged byte array.</param>
        public void ShuffleSudoku(byte[][] sudokuBoard)
        {
            if (!SudokuUtils.IsSudokuBoardValid(sudokuBoard))
            {
                throw new ArgumentException(InvalidSudokuBoardMessage);
            }

            int transformationsToPerform = this.random.Next(20, 31);
            for (int transformationsCount = 0; transformationsCount < transformationsToPerform; transformationsCount++)
            {
                var transformationType = (SudokuBoardTransformationType)this.random.Next(0, 6);
                this.TransformSudokuBoard(sudokuBoard, transformationType);
            }
        }

        /// <summary>
        /// Performs different types of transformations on the sudoku without breaking its validity.
        /// </summary>
        /// <param name="sudokuBoard">A 9x9 jagged array.</param>
        private void TransformSudokuBoard(byte[][] sudokuBoard, SudokuBoardTransformationType type)
        {
            switch (type)
            {
                case SudokuBoardTransformationType.HorizontalIn9x3Group:
                    // swaps each row with random other from its 9x3 group 5 times
                    for (int row = 0; row < 9; row += 3)
                    {
                        for (int timesToSwap = 0; timesToSwap < 5; timesToSwap++)
                        {
                            int rowToSwap = this.random.Next(row, row + 3);
                            var tempRow = new byte[9];
                            sudokuBoard[row].CopyTo(tempRow, 0);
                            sudokuBoard[rowToSwap].CopyTo(sudokuBoard[row], 0);
                            tempRow.CopyTo(sudokuBoard[rowToSwap], 0);
                        }
                    }

                    break;
                case SudokuBoardTransformationType.HorizontalAroundFourthRow:
                    // swaps each row with its opposite
                    for (int row = 0; row < 4; row++)
                    {
                        int rowToSwap = 9 - row - 1;
                        var tempRow = new byte[9];
                        sudokuBoard[row].CopyTo(tempRow, 0);
                        sudokuBoard[rowToSwap].CopyTo(sudokuBoard[row], 0);
                        tempRow.CopyTo(sudokuBoard[rowToSwap], 0);
                    }

                    break;
                case SudokuBoardTransformationType.VerticalIn3x9Group:
                    // swaps each column with random other from its 3x9 group 5 times
                    for (int col = 0; col < 9; col += 3)
                    {
                        for (int timesToSwap = 0; timesToSwap < 5; timesToSwap++)
                        {
                            int colToSwap = this.random.Next(col, col + 3);
                            byte tempValue = 0;
                            for (int row = 0; row < 9; row++)
                            {
                                tempValue = sudokuBoard[row][col];
                                sudokuBoard[row][col] = sudokuBoard[row][colToSwap];
                                sudokuBoard[row][colToSwap] = tempValue;
                            }
                        }
                    }

                    break;
                case SudokuBoardTransformationType.VerticalAroundFourthColumn:
                    // swaps each column with its opposite
                    for (int col = 0; col < 4; col++)
                    {
                        byte tempValue = 0;
                        int colToSwap = 9 - col - 1;
                        for (int row = 0; row < 9; row++)
                        {
                            tempValue = sudokuBoard[row][col];
                            sudokuBoard[row][col] = sudokuBoard[row][colToSwap];
                            sudokuBoard[row][colToSwap] = tempValue;
                        }
                    }

                    break;
                case SudokuBoardTransformationType.AroundMainDiagonal:
                    // swaps each cell with its opposite by the main diagonal
                    for (int row = 0; row < 9; row++)
                    {
                        byte tempValue = 0;
                        for (int col = 0; col < 9; col++)
                        {
                            tempValue = sudokuBoard[row][col];
                            sudokuBoard[row][col] = sudokuBoard[col][row];
                            sudokuBoard[col][row] = tempValue;
                        }
                    }

                    break;
                case SudokuBoardTransformationType.AroundMinorDiagonal:
                    // swaps each cell with its opposite by the minor diagonal
                    for (int row = 0; row < 9; row++)
                    {
                        byte tempValue = 0;
                        for (int col = 0; col < 9; col++)
                        {
                            tempValue = sudokuBoard[row][col];
                            int oppositeRow = 9 - col - 1;
                            int oppositeCol = 9 - row - 1;
                            sudokuBoard[row][col] = sudokuBoard[oppositeRow][oppositeCol];
                            sudokuBoard[oppositeRow][oppositeCol] = tempValue;
                        }
                    }

                    break;
                case SudokuBoardTransformationType.Horizontal9x3Group:
                    break;
                case SudokuBoardTransformationType.Vertical9x3Group:
                    break;
                default:
                    break;
            }
        }
    }
}
