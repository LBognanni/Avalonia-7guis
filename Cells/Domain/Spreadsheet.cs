using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cells.Domain
{

    public interface ISpreadSheet
    {
        string GetCellValue(CellAddress address);
    }

    public class SpreadSheet : ISpreadSheet
    {
        public DataTable Table { get; set; }
        public DataView View => Table.DefaultView;

        private List<Cell> _cells;

        public SpreadSheet()
        {
            Table = new DataTable();
            for(char c='A'; c<='Z';++c)
            {
                Table.Columns.Add(c.ToString(), typeof(string));
            }

            for(int iRow =0; iRow <100; ++iRow)
            {
                var row = Table.NewRow();
                Table.Rows.Add(row);
            }
            _cells = new List<Cell>();
        }

        public void UpdateCell(string column, int row, string value)
            => UpdateCell(new CellAddress(column, row), value);

        public void UpdateCell(CellAddress address, string value)
        {
            var cell = _cells.FirstOrDefault(c => c.Address.Equals(address));
            if(cell == null)
            {
                cell = new Cell
                {
                    Address = address,
                };
                _cells.Add(cell);
            }
            cell.Update(value, this);
        }

        public string GetCellValue(string column, int row) =>
            GetCellValue(new CellAddress(column, row));

        public string GetCellValue(CellAddress address) =>
            _cells.FirstOrDefault(c => c.Address.Equals(address)).Text;
    }
}

