//DataAccesLayer - Модели базы
namespace DAL.Model
{
    //Библиотеки 
    using System;
    using DALS.Model;

    //Таблица где сохраняются URL
    public class Url
    {
        //Id 
        public int Id { get; set; }
        //Длинный URL
        public string FullUrl { get; set; }
        //Сокращенный URL
        public string ShortUrl { get; set; }
        //Дата создания URL-a
        public DateTime CreateDate { get; set; }
        //Кол-во переходов
        public int ClickCount { get; set; }

        public ShortUrl shortUrl { get; set; }
    }
}
