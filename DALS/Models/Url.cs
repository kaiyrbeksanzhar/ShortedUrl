//DataAccesLayer - Модели базы
namespace DALS.Model
{
    //Библиотеки 
    using System;

    //Таблица где сохраняются URL
    public class Url
    {
        //Id  
        public int Id { get; set; }
        //Длинный URL
        public string FullUrl { get; set; }
        //Сокращенный URL
        public string ShortUrlPath { get; set; }
        //Дата создания URL-a
        public DateTime CreateDate { get; set; }
        //Кол-во переходов
        public int ClickCount { get; set; }
        public  ShortUrl ShortUrl { get; set; }
    }
}
