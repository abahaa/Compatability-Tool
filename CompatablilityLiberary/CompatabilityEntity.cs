using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompatablilityLiberary
{
    public class CompatabilityEntity : DbContext
    {
            private static bool Migrated = false;
            public CompatabilityEntity()
                : base("name=InterviewManagmentConnectionString")
            {
                if (!Migrated)
                {
                    Database.SetInitializer<CompatabilityEntity>(new
                       MigrateDatabaseToLatestVersion<CompatabilityEntity, InterviewSystem.Migrations.Configuration>());
                }
                Migrated = true;
            }

    }
