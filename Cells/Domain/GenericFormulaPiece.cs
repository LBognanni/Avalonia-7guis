using System;

namespace Cells.Domain
{
    class GenericFormulaPiece : IFormulaPiece
    {
        public IFormulaPiece Left { get; set; }

        public IFormulaPiece Right { get; set; }

        public string Operator { get; set; }

        public string Evaluate(ISpreadSheet spreadsheet)
        {
            decimal left = decimal.Parse(Left.Evaluate(spreadsheet));
            decimal right = decimal.Parse(Right.Evaluate(spreadsheet));
            switch(Operator)
            {
                case "+":
                    return (left + right).ToString();
                case "-":
                    return (left - right).ToString();
                case "*":
                    return (left * right).ToString();
                case "/":
                    return (left / right).ToString();
            }

            throw new NotImplementedException($"{Operator} is not supported");
        }
    }
}

