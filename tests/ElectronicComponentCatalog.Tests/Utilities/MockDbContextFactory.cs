using System;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities
{
    /// <summary>
    /// Provides an in-memory database context for testing.
    /// Each context uses a unique name to guarantee isolation.
    /// </summary>
    public static class MockDbContextFactory
    {
        public static AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .EnableSensitiveDataLogging()
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
