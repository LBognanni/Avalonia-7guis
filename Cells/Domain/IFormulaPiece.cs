namespace Cells.Domain
{
    interface IFormulaPiece
    {
        IFormulaPiece Left { get; }
        IFormulaPiece Right { get; }

        string Evaluate(ISpreadSheet spreadsheet);
    }
}

