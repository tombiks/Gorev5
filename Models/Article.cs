using static _5.HaftaGorev.Controllers.ArticlesController;

namespace _5.HaftaGorev.Models
{
    public class Article : CreateArticle, IID
    {
        public int Id { get; set; }

    }

}
