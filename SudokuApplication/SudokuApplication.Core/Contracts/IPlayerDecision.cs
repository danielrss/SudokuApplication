using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core.Contracts
{
    /// <summary>
    /// All implementations of this interface have a PlayerDecisionType property.
    /// </summary>
    public interface IPlayerDecision
    {
        /// <summary>
        /// The type of the player decision.
        /// </summary>
        PlayerDecisionType PlayerDecisionType { get; }
    }
}
