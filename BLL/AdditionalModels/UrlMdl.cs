namespace BLL.AdditionalModels
{
    using System;

    //Обьект Url-DAL-a
    public class UrlMdl
    {
        //Id 
        public int Id { get; set; }
        //Длинный URL
        public string FullUrl { get; set; }
        //Сокращенный URL
        public string ShortUrl { get; set; }
        //Кол-во переходов
        public int ClickCount { get; set; }
        //Дата создания URL-a
        public DateTime CreateDate { get; set; }
    }
}
