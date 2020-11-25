using System;

namespace Cells.Domain
{
    class ValueFormulaPiece : IFormulaPiece
    {
        private object _value;

        public ValueFormulaPiece(object value)
        {
            _value = value;
        }

        public IFormulaPiece Left => throw new NotImplementedException();

        public IFormulaPiece Right => throw new NotImplementedException();

        public string Evaluate(ISpreadSheet spreadsheet)
         => _value.ToString();
    }
}

