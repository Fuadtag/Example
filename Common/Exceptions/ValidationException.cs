using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Common.Exceptions
{
    public class ValidationException:Exception
    {
        public ValidationException() : base("Bir və ya daha çox doğrulama xətası baş verdi")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures) : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}