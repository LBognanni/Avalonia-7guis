using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Cells.Domain;

namespace Cells
{
    class GridViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Row> Rows { get; set; }

        public GridViewModel()
        {
            Rows = new ObservableCollection<Row>(
                Enumerable.Range(0, 4).Select(_ => new Row()
                {
                    Cells = new ObservableCollection<Cell>(
                        Enumerable.Range(0, 5).Select(n => CreateCell(n))
                    )
                })
            );
                
        }

        private Cell CreateCell(int n)
        {
            var cell = new Cell();
            cell.PropertyChanged += Cell_PropertyChanged;
            cell.Text = n.ToString();
            return cell;
        }

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    class Row
    {
        public ObservableCollection<Cell> Cells { get; set; }
    }
}
