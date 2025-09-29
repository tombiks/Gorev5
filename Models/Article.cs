using static _5.HaftaGorev.Controllers.ArticlesController;

namespace _5.HaftaGorev.Models
{
    public class CreateArticle
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }

    public interface IID
    {
        public int Id { get; set; }
    }

    public class Article : CreateArticle, IID
    {
        public int Id { get; set; }

    }

}
