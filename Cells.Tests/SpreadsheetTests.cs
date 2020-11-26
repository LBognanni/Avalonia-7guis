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

        [Test]
        public void SpreadSheet_HandlesMultipleOperations()
        {
            var spreadsheet = new SpreadSheet();

            spreadsheet.UpdateCell("A", 1, "40");
            spreadsheet.UpdateCell("B", 1, "60");
            spreadsheet.UpdateCell("A", 2, "=A1+B1-20");

            spreadsheet.GetCellValue("A", 2).Should().Be("80");
        }

        [TestCase("=A1+(B1*2)", "160")]
        [TestCase("=((B1+B2)*2)-A1", "146")]
        [TestCase("=((B1+B2)-(A1+B2))-20", "0")]
        [TestCase("=10+(20)+(30-10)+(30-(10+20))-4", "46")]
        [TestCase("=10+(20+30-10)+(30-(10+20))-4", "46")]
        public void SpreadSheet_HandlesParens(string formula, string expected)
        {
            var spreadsheet = new SpreadSheet();

            spreadsheet.UpdateCell("A", 1, "40");
            spreadsheet.UpdateCell("B", 1, "60");
            spreadsheet.UpdateCell("B", 2, "33");
            spreadsheet.UpdateCell("A", 2, formula);

            spreadsheet.GetCellValue("A", 2).Should().Be(expected);
        }
    }
}
