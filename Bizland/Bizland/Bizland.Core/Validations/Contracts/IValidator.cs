using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core.Validations
{
    public interface IValidator
    {
        string Message { get; set; }
        bool Check(string value);
    }
}
