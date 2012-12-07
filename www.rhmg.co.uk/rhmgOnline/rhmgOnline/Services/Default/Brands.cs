using System.Collections.Generic;
using rhmgOnline.Models;
using rhmgOnline.Services.Interfaces;

namespace rhmgOnline.Services.Default
{
    public class Brands : IDoBranding
    {
        public List<Brand> GetThem()
        {
            return new List<Brand>
                       {
                           new Brand
                               {
                                   MainBrandLink = "http://www.rhmg.co.uk/Studios_recording.aspx",
                                   Name = "Rock Hard Studios",
                                   SubName = "Pro Tools Studio",
                                   Introduction = "Blackpool's finest, most professional and modern recording studio; based around a state-of-the-art digital pro-tools suite, 400 sq foot double height live room and extensive out board and back line.",
                                   ImageName = "Rock Hard Studios Pro Tools Studio.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Prices"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Look around"
                                                       }
                                                }
                               },
                           new Brand
                               {
                                   MainBrandLink = "http://www.rhmg.co.uk/Studios_recording.aspx",
                                   Name = "Rock Hard Studios",
                                   SubName = "Record a Demo",
                                   Introduction = "Specialist sessions designed to get the best out of your music and distil it onto CD so you can take yourself to the next level; whether it be booking gigs or getting signed, demos recorded at Rock Hard Studios have a proven track record.",
                                   ImageName = "Rock Hard Studios Record A Demo.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Prices"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Listen in"
                                                       }
                                                }
                               },
                           new Brand
                               {
                                   MainBrandLink = "http://www.rhmg.co.uk/Studios_recording.aspx",
                                   Name = "Rock Hard Studios",
                                   SubName = " Voice Overs / Jingles",
                                   Introduction = "With our state of the art video synching and professional digital and analogue recording suite we can provide a swift and economical service for all voice over, jingle, and video editing needs.",
                                   ImageName = "Voice Overs.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Testimonials"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Packages"
                                                       }
                                                }
                               },
                           new Brand
                               {
                                   MainBrandLink = "http://www.rhmg.co.uk/Studios_rehearsals.aspx",
                                   Name = "Rock Hard Studios",
                                   SubName = "Flexible Rehearsal Space",
                                   Introduction = "Our rehearsal spaces are suitable for any purpose, whether you are an acoustic solo artist or a death metal band, a dance troupe or a cabaret singer. We can cater to large and small groups, any time of day or night.",
                                   ImageName = "Rehearsals.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Prices"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Look around"
                                                       }
                                                }
                               },
                           new Brand
                               {
                                   MainBrandLink = "http://www.rhmg.co.uk/Studios_recording.aspx",
                                   Name = "Rock Hard Studios",
                                   SubName = "Professional Mastering",
                                   Introduction = "We are experts at taking externally recorded material and applying the polish that sets us apart from other studios. We can do this either in person or remotely, and provide both a discount, and full on professional service.",
                                   ImageName = "Pro Mastering.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Rates"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Upload space"
                                                       }
                                                }
                               },
                           new Brand
                               {
                                   MainBrandLink = "http://www.rhmg.co.uk/Academy/index.html",
                                   Name = "Rock Hard Academy",
                                   Introduction = "Our fully qualified tutors provide beginner to advanced training in Drums, Guitar, Bass, Keyboards and Vocals. Uniquely taking place in our state of the art recording and rehearsal facility, each term concludes with a professional recording session to help you track your progress.",
                                   ImageName = "Rock Hard Academy.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Instruments"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Our Tutors"
                                                       }
                                                }
                               },
                           new Brand
                               {
                                   Name = "Creative Tuition Trust",
                                   Introduction = "The Creative Tuition Trust is a new venture established to encourage and facilitate people of all ages in creative arts. We have entered into a partnership with the Rock Hard Music Group Ltd and Rock Hard Studios for use of their state of the art rehearsal and recording facility in Blackpool, Lancashire and are sure that this relationship will extend to other providers as our provision grows.",
                                   ImageName = "Charity.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Projects"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Join"
                                                       }
                                                },
                                   MainBrandLink = "http://www.creativetuitiontrust.org"
                               },
                           new Brand
                               {
                                   MainBrandLink = "http://www.popstar-party.com/",
                                   Name = "Pop Star Parties",
                                   Introduction = "Have a birthday to remember at Rock Hard Studios. You and up to five of your friends get to come into the stduio for tow hours and sing along to a song of your choice. At the end of the day each of you will go away with a personalised CD to play to all your fans.",
                                   ImageName = "Popstar Parties.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Tracks"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Happy Pop Stars"
                                                       }
                                                }
                               },
                           new Brand
                               {
                                   MainBrandLink = "http://www.blackpoolgigs.co.uk",
                                   Name = "Blackpool Gigs",
                                   Introduction = "The best place to find out what is going on where and when in Blackpool and the surrounding area. Also, hints and tips on how to get gigs and loads of other gig related shennanigans.",
                                   ImageName = "Blackpool Gigs.png",
                                   Links = new[]
                                               {
                                                   new BrandLink
                                                       {
                                                           Label = "Why",
                                                           Link = "http://www.blackpoolgigs.co.uk/about"
                                                       },
                                                   new BrandLink
                                                       {
                                                           Label = "Get gigs",
                                                           Link = "http://www.blackpoolgigs.co.uk/bandlookingforagig"
                                                       }
                                                }
                               }
                       };
        }
    }
}