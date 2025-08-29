namespace PeopleAPI.Common;

public static class InputGuards
{
    public static string SanitizeString(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return input.Trim().Replace("<script", "&lt;script", StringComparison.OrdinalIgnoreCase);
    }
    
    public static bool IsValidAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthDate.Year;
        return age >= 0 && age <= 150 && birthDate <= today;
    }
    
    public static bool IsValidId(int id) => id > 0;
}