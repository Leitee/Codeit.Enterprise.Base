using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeit.NetStdLibrary.Base.BusinessLogic
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> ErrorsByProperty { get; }
        public List<string> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }

        public ValidationException(List<string> errors)
            : this()
        {
            Errors = errors;
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            ErrorsByProperty = new Dictionary<string, string[]>();

            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                ErrorsByProperty.Add(propertyName, propertyFailures);
                Errors.AddRange(propertyFailures);
            }
        }        
    }
}
