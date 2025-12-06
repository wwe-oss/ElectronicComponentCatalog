using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Integration
{
    public class SearchIntegrationTests
    {
        [Fact]
        public async Task Search_By_CommonName_Should_Return_Matching_Component()
        {
            using var context = MockDbContextFactory.CreateInMemoryContext();
            await context.Components.AddRangeAsync(TestDataBuilder.GetComponents());
            await context.SaveChangesAsync();

            var result = context.Components.FirstOrDefault(c => c.CommonName == "R10K");
            result.Should().NotBeNull();
            result!.Name.Should().Contain("Resistor");
        }

        [Fact]
        public async Task Search_By_Category_Should_Filter_Correctly()
        {
            using var context = MockDbContextFactory.CreateInMemoryContext();
            await context.Categories.AddRangeAsync(TestDataBuilder.GetCategories());
            await context.Components.AddRangeAsync(TestDataBuilder.GetComponents());
            await context.SaveChangesAsync();

            var catId = context.Categories.First(c => c.Name == "Resistors").Id;
            var result = context.Components.Where(c => c.CategoryId == catId).ToList();

            result.Should().OnlyContain(c => c.CategoryId == catId);
        }
    }
}
