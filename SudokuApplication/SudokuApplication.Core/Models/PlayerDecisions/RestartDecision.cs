using System;

using SudokuApplication.Core.Contracts;
using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core.Models.PlayerDecisions
{
    public class RestartDecision : CompletelyChangeSudokuGridDecision, IPlayerDecision
    {
        public RestartDecision(SudokuRow[] sudokuGrid) : base(sudokuGrid)
        {
        }

        public PlayerDecisionType PlayerDecisionType
        {
            get
            {
                return PlayerDecisionType.Restart;
            }
        }
    }
}
