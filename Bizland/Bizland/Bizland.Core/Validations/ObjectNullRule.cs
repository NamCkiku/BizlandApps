namespace Bizland.Core
{
    public class ObjectNullRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            else { return true; }
        }
    }
}
