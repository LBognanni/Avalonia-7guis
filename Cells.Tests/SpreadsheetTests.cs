using Cells.Domain;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cells.Tests
{
    class SpreadsheetTests
    {
        [Test]
        public void Spreadsheet_HandlesSimpleFormula()
        {
            var spreadsheet = new SpreadSheet();
     
            spreadsheet.UpdateCell("A", 1, "100");

            spreadsheet.GetCellValue("A", 1).Should().Be("100");
        }

        [Test]
        public void SpreadSheet_HandlesReferenceFormula()
        {
            var spreadsheet = new SpreadSheet();

            spreadsheet.UpdateCell("A", 1, "100");
            spreadsheet.UpdateCell("A", 2, "=A1");

            spreadsheet.GetCellValue("A", 2).Should().Be("100");
        }

        [Test]
        public void SpreadSheet_HandlesOperationFormula()
        {
            var spreadsheet = new SpreadSheet();

            spreadsheet.UpdateCell("A", 2, "=50+30");

            spreadsheet.GetCellValue("A", 2).Should().Be("80");
        }

        [Test]
        public void SpreadSheet_HandlesOperationOverReferenes()
        {
            var spreadsheet = new SpreadSheet();

            spreadsheet.UpdateCell("A", 1, "40");
            spreadsheet.UpdateCell("B", 1, "60");
            spreadsheet.UpdateCell("A", 2, "=A1+B1");

            spreadsheet.GetCellValue("A", 2).Should().Be("100");
        }
    }
}
