using System.Collections.Generic;

namespace RunAndHikeStore.Services.Data
{
    public interface ISettingsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
