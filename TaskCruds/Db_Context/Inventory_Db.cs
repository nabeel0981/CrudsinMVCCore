using System.Collections.Generic;
using System;
using TaskCruds.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskCruds.Db_Context
{
    public class Inventory_Db : DbContext
    {
        public Inventory_Db(DbContextOptions<Inventory_Db> options) : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
    }
}

