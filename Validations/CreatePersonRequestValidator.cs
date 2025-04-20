using FluentValidation;
using ProtobufCRUDServer;

public class CreatePersonRequestValidator: AbstractValidator<CreatePersonRequest> {
    public CreatePersonRequestValidator()
    {
        RuleFor(person => person.FirstName).NotEmpty().WithMessage("first_name is required. ");
        RuleFor(person => person.LastName).NotEmpty().WithMessage("last_name is required. ");
        RuleFor(person => person.NationalCode)
                .NotEmpty()
                .WithMessage("national_code is required. ")
                .Length(10)
                .WithMessage("national_code must be 10 digits. ")
                .Custom((value, context) => {
                    if(!value.All(ch => char.IsDigit(ch))) {
                        context.AddFailure("national_code must be digits. ");
                    }
                });
        RuleFor(person => person.Birthday)
                .NotEmpty()
                .WithMessage("birthday is required. ")
                .Custom((value, context) => {
                            try
                            {
                                DateTime.Parse(value);
                            } catch (FormatException)
                            {
                                context.AddFailure("birthday is not a valid DateTime. (MM-DD-YYYY), 12-28-1988. ");
                            }
                });
    }
}