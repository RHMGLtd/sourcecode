using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace rhmgWebsite.Web.CustomControls
{
    public partial class ThumbnailNavigator : UserControl
    {
        internal int maxColumnLength = 7;

        internal void Populate(string xmlDocName)
        {
            var doc = Helpers.LoadPhotosetXml(xmlDocName);
            MainLabel.Text = doc.DocumentElement.Attributes["pageTitle"].Value;
            var length = Convert.ToInt32(doc.DocumentElement.Attributes["length"].Value);
            var spacerRowColSpan = (length > maxColumnLength) ? maxColumnLength : length;
            var photopath = doc.DocumentElement.Attributes["photopath"].Value;
            var currentColumn = 1;
            var row = new TableRow();

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (currentColumn > maxColumnLength)
                {
                    MainTable.Rows.Add(row);
                    row = new TableRow();
                    var spacerRowCell = new TableCell {ColumnSpan = spacerRowColSpan};
                    var spacerLabel = new Label {Text = "&nbsp;"};
                    spacerRowCell.Controls.Add(spacerLabel);
                    row.Cells.Add(spacerRowCell);
                    MainTable.Rows.Add(row);
                    row = new TableRow();
                    currentColumn = 1;
                }
                var spacer = new TableCell {Width = new Unit(25, UnitType.Pixel)};
                row.Cells.Add(spacer);
                var cell = new TableCell();
                var image = new HyperLink
                                {
                                    ImageUrl =
                                        string.Format("{0}{1}_.jpg", photopath, node.Attributes["filename"].Value),
                                    ToolTip = node.Attributes["tooltip"].Value,
                                    NavigateUrl =
                                        string.Format("~/{0}.aspx?PhotoIndex={1}", xmlDocName,
                                                      node.Attributes["index"].Value)
                                };
                cell.Controls.Add(image);
                row.Cells.Add(cell);
                currentColumn++;
            }
            if (currentColumn != 1)
                MainTable.Rows.Add(row);
        }
    }
}