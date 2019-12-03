//Интерфейс Business logic layer
namespace BLL.Interface
{
    //библиотеки(libraries)
    using BLL.AdditionalModels;
    using BLL.Services;
    using System.Collections.Generic;

    //Интерфейс Url моделя
    public interface IUrlMdl
    {
        //Интерфейс добавление Url-моделя
        RepositoryResult<UrlVmMdl> Insert(UrlMdl model);
        //Интерфейс удаление Url
        RepositoryResult<bool> Delete(int Id);
        //Интерфейс получение список Url моделя 
        IEnumerable<UrlMdl> UrlMdlList { get; }
        //Интерфейс Изменение  Url моделя 
        RepositoryResult<bool> Edit(UrlMdl model);
        //Интерфейс Перенаправление  Url моделя 
        RepositoryResult<string> RedirectToUrl(string path);

        //Интерфейс получение одного Url моделя 
        RepositoryResult<UrlMdl> Select(int Id);
    }
}
