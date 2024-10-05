﻿using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infra.Data.Context
{
    public class MyEShopContext : DbContext
    {
        public MyEShopContext(DbContextOptions<MyEShopContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}