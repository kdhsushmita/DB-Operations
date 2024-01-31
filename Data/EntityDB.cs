using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace entitiyauthentication.Data
{
    public class EntityDB: IdentityDbContext
    {
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public EntityDB(DbContextOptions options) : base(options) { }
    }
}
