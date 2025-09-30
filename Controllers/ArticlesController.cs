using _5.HaftaGorev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _5.HaftaGorev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        static List<Article> _articles = new List<Article>();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Article>))]
        public IActionResult GetList()
        {
            return Ok(_articles); //otomatik json
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Consumes(typeof(int), "application/json")]
        public IResult GetArticle(int id)
        {
            Article? article = _articles.FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                return Results.NotFound("Bu ID'de bir makale yok");
            }

            return Results.Ok(article);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [Consumes(typeof(Article), "application/json")]
        public IResult AddArticle(CreateArticle article)
        {
            if (string.IsNullOrEmpty(article.Title))
            {
                return Results.BadRequest("Makalenin Tittle'ı(Başlığı) boş bırakılamaz");
            }

            Article newArticle = new Article
            {
                Id = _articles.Count + 1,
                Title = article.Title,
                Content = article.Content
            };

            _articles.Add(newArticle);

            return Results.Created($"/{newArticle.Id}", "Yeni makale oluşturuldu");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [Consumes(typeof(Article), "application/json")]
        public IResult ChangeArticle(Article article)
        {
            Article? changeArticle = _articles.FirstOrDefault(x => x.Id == article.Id);

            if (changeArticle == null)
            {
                return Results.BadRequest("Bu ID'de bir makale yok");
            }
            else
            {
                if (string.IsNullOrEmpty(changeArticle.Title))
                {
                    return Results.BadRequest("Makalenin Tittle'ı(Başlığı) boş bırakılamaz");
                }

                changeArticle.Title = article.Title;
                changeArticle.Content = article.Content;
                return Results.Ok("Makale güncellendi");

            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]        
        [Consumes(typeof(int), "application/json")]
        public IResult DelArticle(int id) 
        {
            Article? silinecekArticle = _articles.FirstOrDefault(x => x.Id == id);

            if(silinecekArticle == null)
            {
                return Results.NoContent();
            }

            _articles.Remove(silinecekArticle);

            return Results.Ok("Heeeey dostum makaleyi sildim.");
        }


    }
}
