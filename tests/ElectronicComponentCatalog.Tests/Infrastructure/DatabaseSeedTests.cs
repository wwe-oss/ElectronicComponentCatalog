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
        [Trait("Category", "Infrastructure")]
        public async Task Database_Seeding_Success()
        {
            try
            {
                var cat = new Category("Diodes", "Rectifiers");
                var spec = new ComponentSpecification(1, "N4007");
                var comp = new Component("1N4007", "Diode", cat, spec, 200);

                _context.Components.Add(comp);
                await _context.SaveChangesAsync();

                var total = await _context.Components.CountAsync();
                total.Should().BeGreaterThan(0);

                _helper.Diagnostics.WriteInfo($"Database seeding verified. Total records: {total}");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Database_Seeding_Success), ex);
                throw;
            }
        }

        [Fact(DisplayName = "Database handles failure gracefully during seeding")]
        [Trait("Category", "Infrastructure")]
        public async Task Database_Seeding_Failure_Graceful()
        {
            try
            {
                var invalidComp = new Component("Invalid", "Res", null!, null!, -1);
                await _context.AddAsync(invalidComp);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Database_Seeding_Failure_Graceful), ex);
                ex.Should().BeOfType<InvalidOperationException>()
                    .Or.BeOfType<DbUpdateException>();
            }
        }
    }
}
