using Microsoft.AspNetCore.Mvc;
using PeopleAPI.Data;
using PeopleAPI.Dto;

namespace PeopleAPI.Common;

public static class ValidationHelper
{
    /// Validates ModelState and returns BadRequest with errors if invalid
    public static IActionResult? ValidateModelState(ControllerBase controller)
    {
        if (!controller.ModelState.IsValid)
        {
            var errors = controller.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage));
            return controller.BadRequest(new { Errors = errors });
        }
        return null;
    }
    
    /// Validates ID parameter and returns BadRequest if invalid
    public static IActionResult? ValidateId(ControllerBase controller, int id)
    {
        if (!InputGuards.IsValidId(id))
        {
            return controller.BadRequest("Invalid ID provided");
        }
        return null;
    }

    /// Sanitizes string properties in PersonAddEditDto
    public static void SanitizePersonDto(PersonAddEditDto dto)
    {
        dto.FirstName = InputGuards.SanitizeString(dto.FirstName);
        dto.LastName = InputGuards.SanitizeString(dto.LastName);
        if (dto.Comments != null)
            dto.Comments = InputGuards.SanitizeString(dto.Comments);
    }

    /// Sanitizes string properties in HobbyAddEditDto
    public static void SanitizeHobbyDto(HobbyAddEditDto dto)
    {
        dto.Name = InputGuards.SanitizeString(dto.Name);
    }

    /// Sanitizes string properties in ProfessionAddEditDto
    public static void SanitizeProfessionDto(ProfessionAddEditDto dto)
    {
        dto.Name = InputGuards.SanitizeString(dto.Name);
    }

    /// Validates business rules for Person creation/editing
    public static async Task<IActionResult?> ValidatePersonBusinessRules(
        ControllerBase controller, PersonAddEditDto dto, PeopleDbContext context)
    {
        // Age validation
        if (!InputGuards.IsValidAge(dto.DateOfBirth))
        {
            return controller.BadRequest("Invalid date of birth. Date must be in the past and represent a realistic age.");
        }

        // Profession validation
        if (dto.ProfessionId.HasValue && dto.ProfessionId.Value > 0)
        {
            if (!await ResourceGuards.ProfessionExistsAsync(context, dto.ProfessionId.Value))
            {
                return controller.BadRequest($"Profession with ID {dto.ProfessionId} does not exist.");
            }
        }

        // Hobby validation
        foreach (var hobbyId in dto.HobbyIds.Where(id => id > 0))
        {
            if (!await ResourceGuards.HobbyExistsAsync(context, hobbyId))
            {
                return controller.BadRequest($"Hobby with ID {hobbyId} does not exist.");
            }
        }

        return null;
    }

    /// Complete validation pipeline for Person operations
    public static async Task<IActionResult?> ValidatePerson(
        ControllerBase controller, PersonAddEditDto dto, PeopleDbContext context)
    {
        // 1. Model validation
        var modelValidation = ValidateModelState(controller);
        if (modelValidation != null) return modelValidation;

        // 2. Input sanitization
        SanitizePersonDto(dto);

        // 3. Business rules validation
        return await ValidatePersonBusinessRules(controller, dto, context);
    }

    /// Complete validation pipeline for Hobby operations
    public static IActionResult? ValidateHobby(ControllerBase controller, HobbyAddEditDto dto)
    {
        // 1. Model validation
        var modelValidation = ValidateModelState(controller);
        if (modelValidation != null) return modelValidation;

        // 2. Input sanitization
        SanitizeHobbyDto(dto);

        return null;
    }

    /// Complete validation pipeline for Profession operations
    public static IActionResult? ValidateProfession(ControllerBase controller, ProfessionAddEditDto dto)
    {
        // 1. Model validation
        var modelValidation = ValidateModelState(controller);
        if (modelValidation != null) return modelValidation;

        // 2. Input sanitization
        SanitizeProfessionDto(dto);

        return null;
    }
}