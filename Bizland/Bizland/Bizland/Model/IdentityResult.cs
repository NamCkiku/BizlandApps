using System.Collections.Generic;

namespace Bizland.Model
{
    //
    // Summary:
    //     Represents the result of an identity operation
    public class IdentityResult
    {
        //
        // Summary:
        //     True if the operation was successful
        public bool Succeeded { get; }
        //
        // Summary:
        //     List of errors
        public IEnumerable<string> Errors { get; }
    }
}
