using Microsoft.Extensions.Configuration;
using RunAndHikeStore.Data.Common.Repositories;
using RunAndHikeStore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IConfiguration config;

        private readonly IRepository repo;

        public AdministrationService(IConfiguration config, IRepository repo)
        {
            this.config = config;
            this.repo = repo;   
        }
    }
}
