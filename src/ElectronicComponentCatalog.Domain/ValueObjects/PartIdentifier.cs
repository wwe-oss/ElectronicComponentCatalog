namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents manufacturer and part identification data.
    /// </summary>
    public record PartIdentifier(string Manufacturer, string PartNumber, string? DatasheetUrl = null);
}
