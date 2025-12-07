using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Infrastructure
{
    public class DatabaseSeedTests
    {
        private readonly TestHelper _helper;
        private readonly AppDbContext _context;

        public DatabaseSeedTests(ITestOutputHelper output)
        {
            _helper = new TestHelper(output);
            _context = _helper.Resolve<AppDbContext>();
        }

        [Fact(DisplayName = "Database seeding inserts sample data")]
        [Trait(name: "Category", value: "Infrastructure")]
        public async Task Database_Seeding_Success()
        {
            try
            {
                var cat = new Category(name: "Diodes", description: "Rectifiers");
                var spec = new ComponentSpecification(value: "1", unit: "N4007");
                var comp = new Component(name: "1N4007", commonName: "Diode", category: cat, specification: spec, quantityOnHand: 200);

                _context.Components.Add(comp);
                await _context.SaveChangesAsync();

                var total = await _context.Components.CountAsync();
                total.Should().BeGreaterThan(expected: 0);

                _helper.Diagnostics.WriteInfo(message: $"Database seeding verified. Total records: {total}");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(testName: nameof(Database_Seeding_Success), ex: ex);
                throw;
            }
        }

        [Fact(DisplayName = "Database handles failure gracefully during seeding")]
        [Trait(name: "Category", value: "Infrastructure")]
        public void Database_Seeding_Failure_Graceful()
        {
            var invalidCat = new Category(name: "Invalid", description: "Invalid");
            var invalidSpec = new ComponentSpecification(value: "10", unit: "Ohm");
            Action act = () => new Component(name: "Invalid", commonName: "Res", category: invalidCat, specification: invalidSpec, quantityOnHand: -1);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
