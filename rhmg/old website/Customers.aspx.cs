using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace rhmgWebsite.Web
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Default master = Page.Master as Default;
            master.ConfigurePage(SiteSection.Customers,
                Pages.customers,
                new PageTitle("Rock Hard Music Group - A list of companies, bands and individuals who have used our Recording and Rehearsal facilities in Blackpool, Lancashire"),
                new PageSubTitle("Rock Hard Music Group - Providing pro musical services to the musical community"),
                new PageDescription("Happy customers of Rock Hard Music Group and Rock Hard Studios, including links to press and external sites."),
                new NavControlPath("CustomControls/HomeSideBar.ascx"),
                new Banner("~/images/Rock Hard Music Group - Running Blackpool's best recording and rehearsal studios.png",
                    "Rock Hard Music Group - Running Blackpool, Lancashire's best recording and rehearsal studios"),
                    false);
            var bob = Helpers.LoadCustomers();
            foreach (var customer in bob)
            {
                CustomerTable.Rows.Add(
                    new TableRow
                    {
                        Cells =
                                {
                                    new TableCell
                                        {
                                            Text = "<HR />"
                                        }
                                }
                    }
                    );
                var row = new TableRow();
                row.Cells.Add(new TableCell
                                   {
                                       Text = customer.name
                                   });
                CustomerTable.Rows.Add(row);
                var links = customer.web_presence == null ? 0 : customer.web_presence.Count();
                if (links == 0)
                    continue;
                var table = new Table();
                var row2 = new TableRow();
                foreach (var web in customer.web_presence)
                {
                    var link = new HyperLink()
                                   {
                                       Text = web.text,
                                       NavigateUrl = web.link,
                                       Target = "_newPage"
                                   };
                    row2.Cells.Add(new TableCell()
                                       {
                                           Controls = { link }
                                       });
                    table.Rows.Add(row2);
                }
                CustomerTable.Rows.Add(row2);
            }
        }
    }
}
