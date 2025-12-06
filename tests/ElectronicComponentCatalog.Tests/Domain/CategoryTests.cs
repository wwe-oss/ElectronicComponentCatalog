using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Domain
{
    public class CategoryTests
    {
        private readonly TestHelper _helper;

        public CategoryTests(ITestOutputHelper output) => _helper = new TestHelper(output);

        [Fact(DisplayName = "Category initializes correctly")]
        [Trait("Category", "Domain")]
        public void Category_Create_Success()
        {
            try
            {
                var cat = new Category("Switches", "Mechanical toggles");
                cat.Name.Should().Be("Switches");
                cat.Description.Should().Contain("Mechanical");
                _helper.Diagnostics.WriteInfo("Category created successfully and verified.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Category_Create_Success), ex);
                throw;
            }
        }

        [Fact(DisplayName = "Category equality works as expected")]
        [Trait("Category", "Domain")]
        public void Category_Equality_Checks()
        {
            try
            {
                var c1 = new Category("Diodes", "Rectifiers");
                var c2 = new Category("Diodes", "Rectifiers");
                c1.Should().NotBeSameAs(c2);
                c1.Name.Should().Be(c2.Name);
                _helper.Diagnostics.WriteInfo("Category equality verified.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Category_Equality_Checks), ex);
                throw;
            }
        }
    }
}
