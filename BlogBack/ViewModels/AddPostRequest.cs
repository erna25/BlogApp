namespace BlogBack.ViewModels
{
    public class AddPostRequest
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public AddPostRequest(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}
