using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;

namespace TechMentor.Entities
{
    public class GifDataSpeaker
    {
        public Guid Id {get; set;}
        public string? TalkTitle {get; set;}
        public string? TalkGifUrl {get; set;}
        List<GifDataSpeaker>? Speakers {get; set;}

        public Guid GifDataId {get; set;}
    }
}