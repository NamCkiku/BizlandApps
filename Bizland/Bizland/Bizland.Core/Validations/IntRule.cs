using System;

namespace Bizland.Core
{
    public class IntRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            if (Convert.ToInt32(value) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
