using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Integration
{
    public class BulkAddIntegrationTests
    {
        [Fact]
        public async Task BulkAdd_Should_Insert_Multiple_Components()
        {
            using var context = MockDbContextFactory.CreateInMemoryContext();
            var data = TestDataBuilder.GetComponents();

            await context.Components.AddRangeAsync(data);
            await context.SaveChangesAsync();

            var count = context.Components.Count();
            count.Should().Be(data.Count);
        }
    }
}
