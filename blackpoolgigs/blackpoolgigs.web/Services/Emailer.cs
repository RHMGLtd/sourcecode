using System.Net;
using System.Net.Mail;
using System.Text;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;

namespace blackpoolgigs.web.Services
{
    public class Emailer : IEmail
    {
        public void Send(ClaimBandViewModel claim, string ipAddress)
        {
            var message = new MailMessage(claim.ContactEmail ?? "contact@blackpoolgigs.co.uk", "contact@blackpoolgigs.co.uk")
            {
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Subject = "bg Website Enquiry."
            };
            var sb = new StringBuilder();

            sb.Append("<html><head><title>Band Being Claimed</title><body>");
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            sb.Append("<tr><td colspan=\"2\">The following details have been input on the website. Please action with 24 hours.</td></tr>");
            sb.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
            AddValue(sb, "Band Name", claim.BandName);
            AddValue(sb, "Main Contact Name", claim.MainContactName);
            foreach (var contact in claim.OtherContacts)
                AddValue(sb, "Other Contact", contact);
            AddValue(sb, "Average Age", claim.AverageAge);
            AddValue(sb, "Bio", claim.Bio);
            AddValue(sb, "Style", claim.Style);
            AddValue(sb, "Cover Percentage", claim.CoverPercentage.ToString());
            AddValue(sb, "Original Percentage", claim.OriginalPercentage.ToString());
            AddValue(sb, "MySpace Link", claim.MySpaceLink);
            AddValue(sb, "Facebook Link", claim.FacebookLink);
            AddValue(sb, "Number of Members", claim.NumberOfBandMembers);
            foreach (var link in claim.OtherLinks)
                AddValue(sb, "Other Link", link);

            AddValue(sb, "Their IP Address", ipAddress);
            sb.Append("</table>");
            sb.Append("</body></html>");

            message.Body = sb.ToString();
            var smtp = new SmtpClient
            {
                Host = "mail.WebSiteLive.net",
                Credentials = new NetworkCredential("contact@blackpoolgigs.co.uk", "caffrey1")
            };
            smtp.Send(message);
        }

        public void Send(ContactViewModel contact, string ipAddress)
        {
            var message = new MailMessage(contact.yourEmail, "contact@blackpoolgigs.co.uk")
            {
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Subject = "bg Website Enquiry."
            };
            var sb = new StringBuilder();

            sb.Append("<html><head><title>Website Enquiry</title><body>");
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            sb.Append("<tr><td colspan=\"2\">The following details have been input on the website. Please action with 24 hours.</td></tr>");
            sb.Append("<tr><td colspan=\"2\">&nbsp;</td></tr>");
            AddValue(sb, "Contact Name", contact.yourName);
            AddValue(sb, "Email Address", contact.yourEmail);
            AddValue(sb, "What About", contact.whatAbout);
            AddValue(sb, "Their IP Address", ipAddress);
            sb.Append("</table>");
            sb.Append("</body></html>");

            message.Body = sb.ToString();
            var smtp = new SmtpClient
            {
                Host = "mail.WebSiteLive.net",
                Credentials = new NetworkCredential("contact@blackpoolgigs.co.uk", "caffrey1")
            };
            smtp.Send(message);
        }

        static void AddValue(StringBuilder sb, string label, string value)
        {
            sb.Append("<tr><td width=\"200\">");
            sb.Append(label);
            sb.Append("</td><td width=\"300\">");
            sb.Append(value);
            sb.Append("</td></tr>");
        }
    }
}