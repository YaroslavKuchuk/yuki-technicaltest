using System.ComponentModel.DataAnnotations;

namespace BloggingSystem.Tests.Infrastructure;

public static class ModelValidation
{
    public static IList<ValidationResult> Validate(object model)
    {
        var ctx = new ValidationContext(model);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, ctx, results, validateAllProperties: true);
        return results;
    }
}
