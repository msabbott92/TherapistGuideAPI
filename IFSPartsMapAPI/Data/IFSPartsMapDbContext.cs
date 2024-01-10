using IFSPartsMapAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace IFSPartsMapAPI.Data
{
    public class IFSPartsMapDbContext : DbContext // Inherit from DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IFSPartsMapDbContext(DbContextOptions<IFSPartsMapDbContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options) 
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<IFSPart> Parts { get; set; }
        public DbSet<PartCategory> PartCategories { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
