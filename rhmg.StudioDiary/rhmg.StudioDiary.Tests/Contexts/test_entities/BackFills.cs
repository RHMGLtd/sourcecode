namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class BackFills
    {
        public static BackFill standardBackFill
        {
            get
            {
                return new BackFill
                           {
                               MainContactId = Contacts.TheBeatles.Id,
                               Date = Dates.monday
                           };
            }
        }
    }
}