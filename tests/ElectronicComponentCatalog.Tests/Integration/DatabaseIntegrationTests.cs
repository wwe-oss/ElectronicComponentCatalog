using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Integration
{
    public class DatabaseIntegrationTests
    {
        [Fact]
        public async Task Database_Should_Create_And_Seed_Properly()
        {
            using var context = MockDbContextFactory.CreateInMemoryContext();
            context.Database.IsInMemory().Should().BeTrue();

            await context.Categories.AddRangeAsync(TestDataBuilder.GetCategories());
            await context.SaveChangesAsync();

            var count = await context.Categories.CountAsync();
            count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task SaveChanges_Should_Persist_Data()
        {
            using var context = MockDbContextFactory.CreateInMemoryContext();
            var component = TestDataBuilder.GetComponents()[0];

            await context.Components.AddAsync(component);
            await context.SaveChangesAsync();

            var loaded = await context.Components.FirstOrDefaultAsync(c => c.Id == component.Id);
            loaded.Should().NotBeNull();
        }
    }
}
