//DataAccesLayer - Модели базы
namespace DALS.Model
{
    //Библиотеки 
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    ////Таблица где сохраняются ShortUrl
    public class ShortUrl
    {

        //Id    
        [Key]
        [ForeignKey("Url")]
        public int Id { get; set; }
        //Сокращенный URL для проверки
        public string ShortUrlPath { get; set; }
        public Url Url { get; set; }
    }
}
