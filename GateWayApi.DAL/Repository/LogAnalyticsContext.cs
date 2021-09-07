using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayApi.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace GateWayApi.DAL.Repository
{
    public class LogAnalyticsContext : DbContext
    {
        public LogAnalyticsContext(DbContextOptions<LogAnalyticsContext> options) : base(options)
        {}

        public DbSet<LogItem> LogItems { get; set; }

    }
}
