using System.Collections.Generic;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Services.Interfaces;
using coolbunny.common.Extensions;

namespace blackpoolgigs.web.Services
{
    public class Blobs : IProvideBlobs
    {
        List<Blob> blobs { get; set; }

        public Blobs()
        {
                blobs = new List<Blob>
                            {
                                new Blob
                                    {
                                        Name = "Tired Blob",
                                        Chat = "I have rushed through our database and not found what you've asked for I'm exhausted now!",
                                        Path = "blob-grins-while-looking-very-tired-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "Defeated Blob",
                                        Chat = "I'm sorry I have looked EVERYWHERE and nothing we have matches your requirements. I don't know where else to look.",
                                        Path = "blob-looking-defeated-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "Miserable Blob",
                                        Chat = "I feel dreadful :( I've tried to find your data, I really have, but I just haven't been able to...",
                                        Path = "blob-miserable-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "Running Blob",
                                        Chat = "*puffs* well i've ... *pants* ... dashed here ... *gasps* ... and dashed there ... *snorts* ... but we ain't got what ya looking for! *faints*",
                                        Path = "blob-on-the-run-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "Despairing Blob",
                                        Chat = "Don't you just HATE that, when you KNOW you put it down just over that, but someone seems to have moved it!",
                                        Path = "blob-pulling-hair-out-in-desperation-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "What To Do Blob",
                                        Chat = "Well I've looked all over and your stuff is nowhere to be found... what do i do know?",
                                        Path = "blob-what-do-i-do-now-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "Investigative Blob",
                                        Chat = "Looking at your request, I'm confident to say that we cannot service it at the moment.",
                                        Path = "blob-with-magnifying-glass-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "French Blob",
                                        Chat = "Sacre Bleu we do not 'ave anyfink to show you eeere.",
                                        Path = "blob-with-pencil-moustache-and-goatee-clipart.gif"
                                    },
                                new Blob
                                    {
                                        Name = "Worried Blob",
                                        Chat = "I hope you're not angry but we cannot provide you with what you are asking for. Please don't shout!",
                                        Path = "blob-worried-clasping-hands-clipart.gif"
                                    }
                            };
        }
        public Blob GiveMeOne()
        {
            return blobs[0.RandomFromTo(blobs.Count - 1)];
        }
    }
}