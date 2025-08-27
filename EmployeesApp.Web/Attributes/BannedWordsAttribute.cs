using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EmployeesApp.Web.Attributes;

public class BannedWordsAttribute(params string[] bannedWords) : ValidationAttribute
{
    //Vill ta in en dynamisk lista/array men går ej då attribut skapas vid kompilering, inte runtime.
    //Om jag vill göra det så gör man det i controllern, där kan man kolla med en dynamisk lista.

    //Attributets constructor kan då bara ta emot konstanta värden
    //Med "params" så kan man ta in värden som kompilatorn redan vet om.


    // Får eget felmeddelande till den här.
    // ValidationContext skickas med automatiskt

    //Finns andra sätt att göra så att "ValidationResult.Success" inte ger varning, var tvungen att skapa en egen då?
    //
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;
        var pattern = $@"\b({string.Join("|", bannedWords)}\b)";

        string text = value.ToString()!;

        if (Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase))
            return new ValidationResult("Texten innehåller förbjudna ord.");

        return ValidationResult.Success!;
    }

    public override bool IsValid(object? value)
    {
        if (value is null) 
            return false;

        var pattern = $@"\b({string.Join("|", bannedWords)}\b)";

        string text = value.ToString()!;


        return base.IsValid(value);
    }

}