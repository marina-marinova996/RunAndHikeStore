using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Services.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace RunAndHikeStore.Services.Data
{
    public class SettingsService : ISettingsService
    {
        private readonly IRepository settingsRepository;

        public SettingsService(IRepository settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.AsNoTracking<Setting>().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All<Setting>().To<T>().ToList();
        }
    }
}
