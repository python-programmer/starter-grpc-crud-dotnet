using FluentValidation;
using ProtobufCRUDServer;

public class GetAllPersonRequestValidator: AbstractValidator<GetAllPersonRequest> {
    public GetAllPersonRequestValidator()
    {
        RuleFor(person => person.Page).GreaterThanOrEqualTo(0).WithMessage("page must be greater than or equal 0");
        RuleFor(person => person.PageSize).GreaterThanOrEqualTo(0).WithMessage("page_size must be greater than or equal 0");
        RuleFor(person => person.SearchTerm).MaximumLength(32).WithMessage("search_term must be at max 32 characters");
    }
}