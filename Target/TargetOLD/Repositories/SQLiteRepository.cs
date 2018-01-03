using Akavache;
using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Newtonsoft.Json;
using Plugin.GoogleAnalytics;

namespace Target.Repositories
{
    public class SQLiteRepository : ISQLiteRepository
    {
        public async Task<Unit> Create<T>(string name, T obj)
        {
            BlobCache.ApplicationName = Constants.AppName;
            Unit returnval;
            try
            {
                returnval = await BlobCache.UserAccount.InsertObject(name, obj);
            }
            catch (KeyNotFoundException ex)
            {
                GoogleAnalytics.Current.Tracker.SendException(ex.Message, false);
                returnval = Unit.Default;
            }
            catch (Exception e)
            {
                GoogleAnalytics.Current.Tracker.SendException(e.Message, false);
                returnval = Unit.Default;
            }
            return returnval;
        }
        public async Task<T> Get<T>(string name)
        {
            BlobCache.ApplicationName = Constants.AppName;
            return await BlobCache.UserAccount.GetObject<T>(name);
        }
        public async Task<IEnumerable<T>> GetAll<T>()
        {
            BlobCache.ApplicationName = Constants.AppName;
            return await BlobCache.UserAccount.GetAllObjects<T>();
        }
    }
}
