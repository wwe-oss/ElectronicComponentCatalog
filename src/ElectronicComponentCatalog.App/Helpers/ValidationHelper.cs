namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Helpers
{
    /// <summary>
    /// Simple static validation helper.
    /// </summary>
    public static class ValidationHelper
    {
        public static bool HasValues(params string[] values)
        {
            foreach (var v in values)
            {
                if (string.IsNullOrWhiteSpace(v)) return false;
            }
            return true;
        }
    }
}
