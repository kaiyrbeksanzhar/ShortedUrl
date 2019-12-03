//Сервис интерфейса IUrlMdl-a
namespace BLL.Services
{
    //библиотеки(libraries)
    using BLL.Interface;
    using System.Collections.Generic;
    using BLL.AdditionalModels;
    using System.Linq;
    using System.Data.Entity;
    using System.Web;
    using DALS.Model;
    using DALS.Context;

    //Реализация интерфейса
    public class UrlMdlService : IUrlMdl
    {
        private readonly ApplicationDbContext db;
        private readonly HttpContext accessor;

        public UrlMdlService(ApplicationDbContext db, HttpContext accessor)
        {
            this.db = db;
            this.accessor = accessor;
        }

        //Реализация(вернет из базы список Url моделя)
        public IEnumerable<UrlMdl> UrlMdlList
        {
            get
            {
                return (from u in db.Urls
                        select new UrlMdl
                        {
                            Id = u.Id,
                            ShortUrl = u.ShortUrlPath,
                            FullUrl = u.FullUrl,
                            ClickCount = u.ClickCount,
                            CreateDate = u.CreateDate,
                        }).OrderByDescending(o => o.CreateDate).ToList();
            }
        }

        //Реализация(удаляет по Id Url моделя)
        public RepositoryResult<bool> Delete(int Id)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                var model = db.Urls.Where(u => u.Id == Id).Include(u => u.ShortUrl).SingleOrDefault();
                if (model is null)
                {
                    return new RepositoryResult<bool>(success: false,
                        value: false,
                        message: "не удалось удалить(может быть из за нарушение интернета)!!");
                }
                db.Urls.Remove(model);
                db.SaveChanges();
                return new RepositoryResult<bool>(success: true,
                    value: true, message: "Данные успешно удалено!!!");
            }
            catch
            {
                return new RepositoryResult<bool>(success: false,
                    value: false, message: "не удалось удалить(может быть из за нарушение интернета)!!");
            }
        }

        //Реализация(изменение(Edit) Url моделя)
        public RepositoryResult<bool> Edit(UrlMdl model)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                string https = "https://";
                string www = "www.";
                string newUrltext = model.FullUrl.Replace(https, string.Empty);
                string UrlText = newUrltext.Replace(www, string.Empty);
                Url result = db.Urls
                       .Where(u => u.Id == model.Id)
                       .Include(m => m.ShortUrl)
                       .SingleOrDefault();
                result.FullUrl = UrlText;
                result.ShortUrlPath = string.Format("{0}://{1}/{2}", accessor.Request.Url.Scheme,
                 accessor.Request.Url.Authority, "redirect/" + model.ShortUrl);
                result.ShortUrl.ShortUrlPath = model.ShortUrl;

                if (result is null)
                    return new RepositoryResult<bool>(value: false, success: false, message: "не удалось изменить Url");

                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return new RepositoryResult<bool>(value: true, success: true, message: "Данные успешно изменилось");

            }
            catch
            {
                return new RepositoryResult<bool>(value: false, success: false, message: "не удалось изменить Url");
            }
        }
        //Реализация(Добавляет в таблицу Url моделя)(нужен модель UrlMdl)
        public RepositoryResult<UrlVmMdl> Insert(UrlMdl model)
        {
            string https = "https://";
            string www = "www.";
            try
            {
                if (model is null)
                    return new RepositoryResult<UrlVmMdl>(message: "не верно заполнили данные, проверьте еще раз", success: false);

                string newUrltext = model.FullUrl.Replace(https, string.Empty);
                string UrlText = newUrltext.Replace(www, string.Empty);

                var dbisNull = db.Urls.Where(u => u.FullUrl == UrlText).FirstOrDefault();
                if (dbisNull != null)
                {
                    db.Urls.Remove(dbisNull);
                    db.SaveChanges();
                }
                var RandomiText = Randomizer.GetRandomAlphanumericString((UrlText.Length / 2));
                var shortUrl = string.Format("{0}://{1}/{2}", accessor.Request.Url.Scheme,
                 accessor.Request.Url.Authority, "redirect/" + RandomiText);
                db.Urls.Add(new Url
                {
                    ShortUrlPath = shortUrl,
                    FullUrl = UrlText,
                    ClickCount = 0,
                    CreateDate = System.DateTime.Now,
                    ShortUrl = new ShortUrl
                    {
                        ShortUrlPath = RandomiText,
                    }
                });
                db.SaveChanges();
                return new RepositoryResult<UrlVmMdl>(true,
                    message: "Успешно  добавленно",
                    value: new UrlVmMdl
                    {
                        FullUrl = model.FullUrl,
                        ShortUrl = shortUrl,
                    });
            }
            catch
            {
                return new RepositoryResult<UrlVmMdl>(message: "не удалось сохнарить данные", success: false);
            }
        }

        //Реализация(Перенаправление через Url)
        public RepositoryResult<string> RedirectToUrl(string path)
        {
            try
            {
                if (path is null)
                    return new RepositoryResult<string>(success: false, message: "нет такого данные в базе", value: "Ваше url нет в базе");

                var ciphertext = path;
                var result = (from su in db.shortUrls
                              where su.ShortUrlPath == path
                              select su.Id).SingleOrDefault();
                var model = db.Urls.Where(u => u.Id == result).SingleOrDefault();
                model.ClickCount = model.ClickCount + 1;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                if (model is null)
                    return new RepositoryResult<string>(success: false, message: "нет такого данные в базе", value: "Ваше url нет в базе");

                return new RepositoryResult<string>(success: true, message: "Успешно нашли текущий Url", value: model.FullUrl);

            }
            catch
            {
                return new RepositoryResult<string>(success: false,
                    message: "нет такого данные в базе",
                    value: "Ваше url нет в базе");
            }
        }

        //Реализация(Получение одного Url-а)
        public RepositoryResult<UrlMdl> Select(int Id)
        {
            try
            {
                var shortUrl = string.Format("{0}://{1}/{2}", accessor.Request.Url.Scheme,
                 accessor.Request.Url.Authority, "redirect/");
                var model = db.Urls.Find(Id);
                if (model is null)
                    return new RepositoryResult<UrlMdl>(success: false, message: "нет такого элемента в базе", value: null);
                return new RepositoryResult<UrlMdl>(success: true,
                    message: "Данный успешно найдено",
                    value: new UrlMdl
                    {
                        Id = model.Id,
                        FullUrl = model.FullUrl,
                        ShortUrl = model.ShortUrlPath.Replace(shortUrl, string.Empty),
                        ClickCount = model.ClickCount,
                        CreateDate = model.CreateDate,
                    });
            }
            catch
            {
                return new RepositoryResult<UrlMdl>(success: false, message: "нет такого элемента в базе", value: null);
            }
        }
    }
}
