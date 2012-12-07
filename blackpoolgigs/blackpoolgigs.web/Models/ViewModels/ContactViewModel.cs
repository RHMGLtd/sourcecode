namespace blackpoolgigs.web.Models.ViewModels
{
    public class ContactViewModel
    {
        public string yourName { get; set; }
        public string yourEmail { get; set; }
        public string whatAbout { get; set; }

        public bool InValid()
        {
            return string.IsNullOrWhiteSpace(yourName) ||
                   string.IsNullOrWhiteSpace(whatAbout) ||
                   string.IsNullOrWhiteSpace(yourEmail);
        }
    }
}