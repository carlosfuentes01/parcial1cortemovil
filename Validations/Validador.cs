using Plugin.ValidationRules.Interfaces;

namespace SistemaInformacion.Views;

public class IsNotNullOrEmptyRule : IValidationRule<string>
{

    public string ValidationMessage { get; set; }
    public bool Check(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }



}