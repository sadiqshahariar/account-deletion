using AccountDeletion.Models;
using AccountDeletion.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace AccountDeletion.Repositories.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly AccountDeletionDbContext _dbContext;

        public AccountRepository(AccountDeletionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<App> IsIdExist(string appid)
        {

            var data = await _dbContext.Apps.FirstOrDefaultAsync(x => x.AppId == appid);

            if (data == null)
            {
                return null;
            }
            return data;
        }

        public async Task<AppConfig> GetAppConfig(int app_pk)
        {
            var appConfigData = await _dbContext.AppConfigs.FirstOrDefaultAsync(a => a.AppId == app_pk);
            if (appConfigData == null)
            {
                return null;
            }
            return appConfigData;
        }

        public async Task<int> SaveUserDetails(string appid, string mobilenumber)
        {
            try
            {
                var userDetail = new UserRequest
                {
                    AppId = Int32.Parse(appid),
                    Data = mobilenumber,
                };
                await _dbContext.AddAsync(userDetail);
                await _dbContext.SaveChangesAsync();
                return userDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<App> GetAppById(string appid)
        {
            var app = await _dbContext.Apps.FirstOrDefaultAsync(x => x.Id == Int32.Parse(appid));
            return app;
        }

        public int LoginUser(string username, string password)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
                if (user == null)
                {
                    return -1;
                }
                return user.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<AppConfig> GetData()
        {
            List<AppConfig> users = _dbContext.AppConfigs.OrderByDescending(u => u.Id).ToList();
            return users;
        }
        public async Task<int> SaveConfing( string appid, string bannercolor, string formcolor, string logo, string inputlevel, string phonenumber, string buttoncolor)
        {
            try
            {
                var userDetail = new AppConfig
                {
                    AppId = Int32.Parse(appid),
                    BannerColor = bannercolor,
                    FormColor = formcolor,
                    Logo = logo,
                    InputLabel = inputlevel,
                    Placeholder = phonenumber,
                    ButtonColor = buttoncolor,
                   // CreatedAt = DateTime.UtcNow

                };
                await _dbContext.AddAsync(userDetail);
                await _dbContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool DeleteConfingData(int id)
        {
            var user = _dbContext.AppConfigs.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dbContext.AppConfigs.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> Updateconfingdata(int id, string bannercolor, string formcolor, string logo, string inputlevel, string phonenumber, string buttoncolor)
        {
            try
            {
                var user =  _dbContext.AppConfigs.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.BannerColor = bannercolor;
                    user.FormColor = formcolor;
                    user.Logo = logo;
                    user.InputLabel = inputlevel;
                    user.Placeholder = phonenumber;
                    user.ButtonColor = buttoncolor;

                    // Save changes to the database
                      _dbContext.AppConfigs.Update(user);
                      await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }



}
