using Akavache;
using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Newtonsoft.Json;
using Xamarin.Forms;
//using Plugin.GoogleAnalytics;

namespace Target.Repositories
{
    public class SQLiteRepository : ISQLiteRepository
    {
        private readonly IDefaultsFactory defaultsFactory;
        public SQLiteRepository(IDefaultsFactory defaultsFactory)
        {
            this.defaultsFactory = defaultsFactory;
            var dbpath = DependencyService.Get<IPlatformStuff>().GetLocalFilePath(defaultsFactory.GetAppName() + ".db3");
        }
        public async Task<Unit> Create<T>(string name, T obj)
        {
            BlobCache.ApplicationName = defaultsFactory.GetAppName();
            Unit returnval;
            try
            {
                returnval = await BlobCache.UserAccount.InsertObject(name, obj);
            }
            catch (KeyNotFoundException ex)
            {
                //GoogleAnalytics.Current.Tracker.SendException(ex.Message, false);
                returnval = Unit.Default;
            }
            catch (Exception e)
            {
                //GoogleAnalytics.Current.Tracker.SendException(e.Message, false);
                returnval = Unit.Default;
            }
            return returnval;
        }
        public async Task<T> Get<T>(string name)
        {
            BlobCache.ApplicationName = defaultsFactory.GetAppName();
            return await BlobCache.UserAccount.GetObject<T>(name);
        }
        public async Task<IEnumerable<T>> GetAll<T>()
        {
            BlobCache.ApplicationName = defaultsFactory.GetAppName();
            return await BlobCache.UserAccount.GetAllObjects<T>();
        }
    }
}
