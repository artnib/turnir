using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace turnir
{
  internal class HtmlWriter
  {
    internal HtmlWriter(Turnir tur)
    {
      this.tur = tur;
    }

    internal void SaveTable(string file, DataGridView grid)
    {
      var sb = new StringBuilder();
      sb.AppendLine("<html><head>");
      sb.AppendLine("<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">");
      sb.AppendLine("</head>");
      sb.AppendLine("<body>");
      sb.AppendLine(Header(tur.Name, 3));
      sb.AppendFormat("<p>{0}{1}{2}</p>", tur.Place, Spaces(5),
        tur.Date.ToLongDateString());
      sb.Append(TableCode(grid));
      sb.AppendFormat("<p>{0}{1}{4}{2}{3}</p>",
        Ref, tur.Referee, Sec, tur.Secretary, Spaces(5));
      sb.Append("</body></html>");
      File.WriteAllText(file, sb.ToString());
    }

    internal static string TableCode(DataGridView grid)
    {
      var sb = new StringBuilder();
      sb.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"2\">");
      sb.Append("<tr>");
      foreach (DataGridViewColumn col in grid.Columns)
        sb.Append(Cell(col.HeaderText));
      sb.AppendLine("</tr>");
      foreach (DataGridViewRow row in grid.Rows)
      {
        sb.Append("<tr>");
        foreach (DataGridViewCell cell in row.Cells)
        {
          sb.Append(Cell(cell.Value));
        }
        sb.AppendLine("</tr>");
      }
      sb.AppendLine("</table>");
      return sb.ToString();
    }

    internal static string Header(string text, byte level)
    {
      return String.Format("<h{0}>{1}</h{0}>", level, text);
    }

    static string Cell(object content)
    {
      return String.Format("<td>{0}</td>", content);
    }

    static string Spaces(int number)
    {
      var sb = new StringBuilder();
      for (int i = 1; i <= number; i++)
        sb.Append("&nbsp;");
      return sb.ToString();
    }

    const string Ref = "Главный судья _______ ";
    const string Sec = "Главный секретарь _______ ";

    Turnir tur;
  }
}
