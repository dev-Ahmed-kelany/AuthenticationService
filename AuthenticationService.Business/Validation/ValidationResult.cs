using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business.Validation
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<ValidationError> Errors { get; } = new();

        public void AddError(string propertyName, string errorMessage)
        {
            Errors.Add(new ValidationError(propertyName, errorMessage));
        }
    }

}
