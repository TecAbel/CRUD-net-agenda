using Microsoft.EntityFrameworkCore;
using Mxss.Schedules.Service.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mxss.Schedules.Service.Entities
{
    public partial class MxChavosSql
    {
        private readonly string _connection;

        public MxChavosSql(AppSettings appSettings)
        {
            _connection = appSettings.MxChavosSql;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.
                    UseSqlServer(_connection).
                    UseLazyLoadingProxies();
            }
        }
    }
}
