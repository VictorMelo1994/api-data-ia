namespace TechMentor.Entities
{   

    public class GifData
    {
        public GifData(string label, string url)
        {
            Label =  label;
            Url = url;
        }
        public string Label {get; set;}
        public string Url {get; set;}

    }
}