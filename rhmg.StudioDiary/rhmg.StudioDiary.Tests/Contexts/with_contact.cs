namespace rhmg.StudioDiary.Tests.Contexts
{
    public class with_contact
    {
        public static Contact the_beatles;

        public with_contact()
        {
            the_beatles = test_entities.TheBeatles;
        }
    }
}