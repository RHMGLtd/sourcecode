namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class BackFills
    {
        public static BackFill standardBackFill
        {
            get
            {
                return BackFill.Create(Contacts.TheBeatles, Dates.monday);
            }
        }
    }
}