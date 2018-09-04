using System;
using System.Collections.Generic;
using System.Linq;

namespace Bizland.Core
{
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;
        private bool _isNotValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool IsNotValid
        {
            get
            {
                return _isNotValid;
            }
            set
            {
                _isNotValid = value;
                RaisePropertyChanged(() => IsNotValid);
            }
        }

        public string ValidationDescriptions => string.Join(Environment.NewLine, Validations.Select(v => v.ValidationMessage));

        public ValidatableObject()
        {
            _isValid = true;
            _isNotValid = false;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();
            IsNotValid = !IsValid;

            return this.IsValid;
        }
    }
}
