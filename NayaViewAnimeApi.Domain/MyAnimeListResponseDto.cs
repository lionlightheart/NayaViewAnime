
namespace NayaViewAnimeApi.Domain
{
    public class MyAnimeListResponseDto
    {
        public List<Node>? Node { get; set; }
    }

   public class Node
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public List<MainPicture>? MainPictures { get; set; }
    }

   public class MainPicture
    {
        public required string Medium { get; set; }
        public required string Large { get; set; }
    }
}
