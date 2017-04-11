namespace SudokuApplication.Core.Models.PlayerDecisions
{
    public class HintDecision : FillCellDecision
    {
        public HintDecision(byte row, byte column, byte value) : base(row, column, value)
        {
        }
    }
}
