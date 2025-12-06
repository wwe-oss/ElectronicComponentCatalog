using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string message) : base(message) { }
    }
}
