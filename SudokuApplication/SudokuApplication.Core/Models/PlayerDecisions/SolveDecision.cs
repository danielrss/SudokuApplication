using System;

using SudokuApplication.Core.Contracts;
using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core.Models.PlayerDecisions
{
    public class SolveDecision : CompletelyChangeSudokuGridDecision, IPlayerDecision
    {
        public SolveDecision(SudokuRow[] sudokuGrid) : base(sudokuGrid)
        {
        }

        public PlayerDecisionType PlayerDecisionType
        {
            get
            {
                return PlayerDecisionType.Solve;
            }
        }
    }
}
