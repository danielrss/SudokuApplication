using System;

using SudokuApplication.Core.Contracts;
using SudokuApplication.Core.Enums;

namespace SudokuApplication.Core.Models.PlayerDecisions
{
    public class FillCellDecision : IPlayerDecision
    {
        private byte row;
        private byte column;
        private byte value;

        public FillCellDecision(byte row, byte column, byte value)
        {
            this.Row = row;
            this.Column = column;
            this.Value = value;
        }

        public byte Row
        {
            get
            {
                return this.row;
            }

            set
            {
                if (value < 0 || 8 < value)
                {
                    throw new ArgumentOutOfRangeException("PlayerDecision row must be between 0 and 8!");
                }

                this.row = value;
            }
        }

        public byte Column
        {
            get
            {
                return this.column;
            }

            set
            {
                if (value < 0 || 8 < value)
                {
                    throw new ArgumentOutOfRangeException("PlayerDecision column must be between 0 and 8!");
                }

                this.column = value;
            }
        }

        public byte Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (value < 1 || 9 < value)
                {
                    throw new ArgumentOutOfRangeException("PlayerDecision value must be between 1 and 9!");
                }

                this.value = value;
            }
        }

        public PlayerDecisionType PlayerDecisionType
        {
            get
            {
                return PlayerDecisionType.FillCell;
            }
        }
    }
}
