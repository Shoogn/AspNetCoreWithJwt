using AspNetCoreWithJwt.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWithJwt.Models
{
    public class JwtDataContext : IdentityDbContext<User>
    {
       public JwtDataContext(DbContextOptions<JwtDataContext> options): base(options) { }
    }
}
