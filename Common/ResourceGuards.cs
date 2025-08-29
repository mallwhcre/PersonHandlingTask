using Microsoft.EntityFrameworkCore;
namespace PeopleAPI.Common;
using PeopleAPI.Data;

public static class ResourceGuards
{
    public static async Task<bool> HobbyExistsAsync(PeopleDbContext context, int hobbyId)
    {
        return await context.Hobbies.AnyAsync(h => h.Id == hobbyId);
    }
    
    public static async Task<bool> ProfessionExistsAsync(PeopleDbContext context, int professionId)
    {
        return await context.Professions.AnyAsync(p => p.Id == professionId);
    }
}