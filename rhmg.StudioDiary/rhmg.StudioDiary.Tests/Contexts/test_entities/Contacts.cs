namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class Contacts
    {
        public static Contact TheBeatles
        {
            get
            {
                return new Contact
                {
                    Name = "The Beatles",
                    PhoneNumber = "ScouseLandSomewhere"
                };
            }
        } 
    }
}