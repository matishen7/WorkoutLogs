using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Middleware
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation failures occurred.")
        {
            Errors = new Dictionary<string, string[]>();

            foreach (var failure in failures)
            {
                if (!Errors.ContainsKey(failure.PropertyName))
                {
                    Errors.Add(failure.PropertyName, new[] { failure.ErrorMessage });
                }
                else
                {
                    var existingMessages = Errors[failure.PropertyName];
                    var updatedMessages = existingMessages.Append(failure.ErrorMessage).ToArray();
                    Errors[failure.PropertyName] = updatedMessages;
                }
            }
        }
    }
}
