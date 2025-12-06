using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.InfrastructureExceptions
{
    public class DatabaseInitializationException : Exception
    {
        public DatabaseInitializationException(string message, Exception? inner = null)
            : base(message, inner) { }
    }
}
