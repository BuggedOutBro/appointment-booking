using appointment_booking.Enums;
using appointment_booking.Models.DTO;
using FluentValidation;
using System;
using System.Linq;

namespace appointment_booking.Validators
{
  public class CalendarRequestValidator : AbstractValidator<QueryRequest>
  {
    public CalendarRequestValidator()
    {
      RuleFor(data => data.Date).NotEmpty().NotNull();
      RuleFor(data => data.Date)
        .Must(BeAValidDate).WithMessage("Date is required");

      RuleFor(data => data.Products).NotEmpty().NotNull()
        .Must(x => x.All(p => !string.IsNullOrWhiteSpace(p)))
        .Must(products => AreAllStrictlyAllowedEnumValues<Products>(products))
       .WithMessage($"Invalid Products. Supported values are {GetAllowedEnumValues<Products>()}.");

      RuleFor(x => x.Language)
        .NotEmpty().NotNull()
        .Must(language => IsStrictlyAllowedEnumValue<Language>(language))
        .WithMessage($"Invalid Language. Supported values are {GetAllowedEnumValues<Language>()}");

      RuleFor(data => data.Rating).NotEmpty().NotNull()
          .Must(rating => IsStrictlyAllowedEnumValue<Ratings>(rating))
          .WithMessage($"Invalid Rating. Supported values are {GetAllowedEnumValues<Ratings>()}");
    }

    private bool BeAValidDate(DateTime date)
    {
      return !date.Equals(default(DateTime));
    }
    private bool IsStrictlyAllowedEnumValue<TEnum>(string value) where TEnum : Enum
    {
      return Enum.GetNames(typeof(TEnum)).Contains(value);
    }
    private bool AreAllStrictlyAllowedEnumValues<TEnum>(IEnumerable<string> values) where TEnum : Enum
    {
      // Ensure all items in the list strictly match enum names
      var allowedValues = Enum.GetNames(typeof(TEnum));
      return values.All(value => allowedValues.Contains(value));
    }
    private string GetAllowedEnumValues<TEnum>() where TEnum : Enum
    {
        // Join enum values into a string, separated by commas
        return string.Join(", ", Enum.GetNames(typeof(TEnum)));
    }

  }
}
