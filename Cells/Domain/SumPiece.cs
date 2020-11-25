using System;

namespace Cells.Domain
{
    class SumPiece : IFormulaPiece
    {
        public IFormulaPiece Left => throw new NotImplementedException();

        public IFormulaPiece Right => throw new NotImplementedException();

        public string Evaluate(ISpreadSheet spreadsheet)
        {
            throw new NotImplementedException();
        }
    }
}

