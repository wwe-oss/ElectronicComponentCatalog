using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
