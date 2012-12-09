using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;

namespace rhmgWebsite.Web
{
    public partial class Competition : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Page.Master as Default;
            master.ConfigurePage(SiteSection.Contact,
                Pages.contact,
                new PageTitle("Rock Hard Music Group - Competition to win awesome prizes with RHMG and Rock FM"),
                new PageSubTitle("Rock Hard Music Group - Providing pro musical services to the musical community"),
                new PageDescription("Competition entry in association with Rock FM"),
                new NavControlPath("CustomControls/HomeSideBar.ascx"),
                new Banner("~/images/Rock Hard Music Group - Running Blackpool's best recording and rehearsal studios.png",
                    "Rock Hard Music Group - Running Blackpool, Lancashire's best recording and rehearsal studios"),
                    false);
        }
        protected void Submit_click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            try
            {

                string nameVar = name.Text;
                string emailVar = email.Text;
                string contactNumberVar = contact_number.Text;
                string url = url_input.Text;


                var message = new MailMessage(emailVar, "enquiries@rhmg.co.uk")
                                  {
                                      IsBodyHtml = true,
                                      Priority = MailPriority.High,
                                      Subject = "Competition Entry."
                                  };
                var sb = new StringBuilder();

                sb.Append("<html><head><title>Website Enquiry</title><body>");
                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
                sb.Append("<tr><td colspan=\"2\">The following details have been input on the website. Please action with 24 hours.</td></tr>");
                sb.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
                Helpers.AddValue(sb, "Contact Name", nameVar);
                Helpers.AddValue(sb, "Email Address", emailVar);
                Helpers.AddValue(sb, "Contact Number", contactNumberVar);
                Helpers.AddValue(sb, "Video Url", url);
                Helpers.AddValue(sb, "Their IP Address", Request.ServerVariables["remote_addr"]);
                sb.Append("</table>");
                sb.Append("</body></html>");

                message.Body = sb.ToString();
                var smtp = new SmtpClient
                                      {
                                          Host = "mail.WebSiteLive.net",
                                          Credentials = new NetworkCredential("enquiries@rhmg.co.uk", "caffrey1")
                                      };
                smtp.Send(message);

                email.Text = string.Empty;
                contact_number.Text = string.Empty;
                Response.Redirect("CompetitionThankyou.aspx");
            }
            catch (Exception ex)
            {
                ErrorLabel.ToolTip = ex.Message;
                ErrorLabel.Visible = true;
            }
        }
    }
}
