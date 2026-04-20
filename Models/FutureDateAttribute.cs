using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group5Flight.Models
{
    public class FutureDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private int _years;

        public FutureDateAttribute(int years)
        {
            _years = years;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            if (value == null)
                return ValidationResult.Success;

            DateTime date = (DateTime)value;

            if (date.Date <= DateTime.Today)
                return new ValidationResult(
                    base.ErrorMessage ?? "Date must be in the future.");

            if (date.Date > DateTime.Today.AddYears(_years))
                return new ValidationResult(
                    base.ErrorMessage ?? $"Date cannot be more than {_years} years ahead.");

            return ValidationResult.Success;
        }


        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");

            // "years" matches the adapter name in futuredate.js
            ctx.Attributes.Add("data-val-futuredate-years", _years.ToString());
            ctx.Attributes.Add("data-val-futuredate",
                base.ErrorMessage ??
                $"Date must be in the future and within {_years} years.");
        }
    }
}
