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
                    Id = "contact/1",
                    Name = "The Beatles",
                    PhoneNumber = "ScouseLandSomewhere"
                };
            }
        } 
    }
}