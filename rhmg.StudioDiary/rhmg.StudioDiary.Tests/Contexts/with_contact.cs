using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests.Contexts
{
    public class with_contact
    {
        public static Contact the_beatles;

        public with_contact()
        {
            the_beatles = Contacts.TheBeatles;
        }
    }
}