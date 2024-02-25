using AccountDeletion.Models;

namespace AccountDeletion.Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<App> IsIdExist(string appid);
        Task<AppConfig> GetAppConfig(int app_pk);
        Task<int> SaveUserDetails(string appid, string mobilenumber);
        Task<App> GetAppById(string appid);
        int LoginUser(string username, string password);
        List<AppConfig> GetData();
        Task<int> SaveConfing(string appid, string bannercolor, string formcolor, string logo, string inputlevel, string phonenumber, string buttoncolor);
        bool DeleteConfingData(int id);
        Task<bool> Updateconfingdata(int id, string bannercolor, string formcolor, string logo, string inputlevel, string phonenumber, string buttoncolor);
    }
}
