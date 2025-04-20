namespace ProtobufCRUDServer.Infrastructures;

using FluentValidation;
using FluentValidation.Results;
using Google.Protobuf;

public static class Validator
{
    public static ValidationResult Validation(this IMessage message)
    {
        Type messageType = message.GetType();
        Type genericValidatorType = typeof(AbstractValidator<>);
        Type closedValidatorType = genericValidatorType.MakeGenericType(messageType);

        // Find the validator type (same as before)
        Type validatorType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly =>
            {
                try { return assembly.GetTypes(); }
                catch { return Array.Empty<Type>(); }
            })
            .FirstOrDefault(type =>
                !type.IsAbstract &&
                closedValidatorType.IsAssignableFrom(type)
            );

        if (validatorType == null)
            throw new InvalidOperationException($"Validator for {messageType.Name} not found.");

        // Create validator instance
        object validatorInstance = Activator.CreateInstance(validatorType);

        // Get the SPECIFIC `Validate` method that accepts the message type as a parameter
        var validateMethod = closedValidatorType.GetMethod(
            "Validate",
            new[] { messageType } // Specify parameter types to resolve ambiguity
        );

        // Invoke the method
        return (ValidationResult)validateMethod.Invoke(validatorInstance, new[] { message });
    }
}