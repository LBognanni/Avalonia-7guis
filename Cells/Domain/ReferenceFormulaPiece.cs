using System;

namespace Cells.Domain
{
    class ReferenceFormulaPiece : IFormulaPiece
    {
        public IFormulaPiece Left => throw new NotImplementedException();

        public IFormulaPiece Right => throw new NotImplementedException();

        public CellAddress Address { get; set; }
     
        public ReferenceFormulaPiece(string column, int row)
        {
            Address = new CellAddress(column, row);
        }

        public string Evaluate(ISpreadSheet spreadsheet)
            => spreadsheet.GetCellValue(Address);
    }
}

