using FluentValidation;
using ProtobufCRUDServer;

public class GetPersonRequestValidator: AbstractValidator<GetPersonRequest> {
    public GetPersonRequestValidator()
    {
        RuleFor(person => person.Id).NotEmpty().WithMessage("id is required. ").GreaterThan(0).WithMessage("id must be greater than 0");
    }
}