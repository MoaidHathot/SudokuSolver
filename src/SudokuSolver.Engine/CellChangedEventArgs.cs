using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Engine
{
    public class CellChangedEventArgs
    {
        public ISudokuBoard Board { get; }
        public int Row { get; }
        public int Column { get; }
        public int? Value { get; }

        public CellChangedEventArgs(int row, int column, int? value, ISudokuBoard board)
        {
            Board = board;
            Row = row;
            Column = column;
            Value = value;
        }
    }

}
