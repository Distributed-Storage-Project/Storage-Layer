using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;

namespace Repository
{
	public class AppDbContext: DbContext
	{
        // 将应用程序的配置传递给DbContext
        public AppDbContext(DbContextOptionsBuilder<AppDbContext> options) {
            options.UseSqlite("Data Source=temp335.db");
        }

    }
}

