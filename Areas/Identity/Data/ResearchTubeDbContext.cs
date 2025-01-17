﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResearchTube.Areas.Identity.Data;
using ResearchTube.Models;

namespace ResearchTube.Data
{
    public class ResearchTubeDbContext : IdentityDbContext
    {
        public DbSet<ResearchTubeUser> ResearchTubeUsers { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Video> Video { get; set; }
        public ResearchTubeDbContext(DbContextOptions<ResearchTubeDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ResearchTubeUser>()
                .HasOne<Payment>(user => user.Payment)
                .WithOne(payment => payment.Users)
                .HasForeignKey<Payment>(payment => payment.ResearchTubeUserId);

            builder.Entity<PaymentType>()
                .HasOne<Payment>(paymentType => paymentType.Payment)
                .WithMany(payment => payment.PaymentTypes)
                .HasForeignKey(paymentType => paymentType.CurrentPaymentId);
        }
    }
}
