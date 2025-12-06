using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Repositories;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Infrastructure
{
    public class RepositoryTests
    {
        private readonly TestHelper _helper;
        private readonly AppDbContext _context;
        private readonly ComponentRepository _repo;

        public RepositoryTests(ITestOutputHelper output)
        {
            _helper = new TestHelper(output);
            _context = _helper.Resolve<AppDbContext>();
            _repo = new ComponentRepository(_context);
        }

        [Fact(DisplayName = "Repository AddAsync inserts component successfully")]
        [Trait("Category", "Infrastructure")]
        public async Task Repository_AddAsync_ShouldInsertComponent()
        {
            try
            {
                var category = new Category("Capacitors", "Electrolytics");
                var spec = new ComponentSpecification(10, "uF");
                var comp = new Component("10uF Capacitor", "Capacitor", category, spec, 25);

                await _repo.AddAsync(comp);
                await _context.SaveChangesAsync();

                var found = await _repo.GetByIdAsync(comp.Id);
                found.Should().NotBeNull();
                found!.Name.Should().Be("10uF Capacitor");

                _helper.Diagnostics.WriteInfo("Component successfully inserted via repository.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Repository_AddAsync_ShouldInsertComponent), ex);
                throw;
            }
        }

        [Fact(DisplayName = "Repository DeleteAsync removes a component")]
        [Trait("Category", "Infrastructure")]
        public async Task Repository_DeleteAsync_ShouldRemoveComponent()
        {
            try
            {
                var category = new Category("Resistors", "Carbon Film");
                var spec = new ComponentSpecification(100, "Ohm");
                var comp = new Component("100 Ohm Resistor", "Resistor", category, spec, 50);
                await _repo.AddAsync(comp);
                await _context.SaveChangesAsync();

                await _repo.DeleteAsync(comp.Id);
                await _context.SaveChangesAsync();

                var result = await _repo.GetByIdAsync(comp.Id);
                result.Should().BeNull();

                _helper.Diagnostics.WriteInfo("Component successfully deleted from repository.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Repository_DeleteAsync_ShouldRemoveComponent), ex);
                throw;
            }
        }
    }
}
