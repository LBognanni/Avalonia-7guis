using System;

namespace Cells.Domain
{
    public struct CellAddress
    {
        public int Row { get; set; }
        public string Column { get; set; }

        public CellAddress(string column, int row)
        {
            Column = column;
            Row = row;
        }

        internal bool Equals(string column, int row)
        {
            return ((Column == column) && (Row == row));
        }
    }
}

