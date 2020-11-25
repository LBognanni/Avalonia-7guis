using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cells.Domain
{


    class Cell
    {
        public CellAddress Address { get; set; }

        private string _formula;
        private string _text;
        private IFormulaPiece _formulaPiece;

        public string Formula
        {
            get => _formula;
            set
            {
                if (_formula != value)
                {
                    _formula = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Update(string value, ISpreadSheet spreadsheet)
        {
            _formulaPiece = FormulaParser.Parse(value);
            Text = _formulaPiece.Evaluate(spreadsheet);
        }

        public override string ToString() => Text;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
