using GitBetter.Models;
using Microsoft.EntityFrameworkCore;

namespace GitBetter.API
{
    public class FacilitySpecializationAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/facilities/{facilityId}/specializations/{specializationId}", async (int facilityId, int specializationId, GitBetterDbContext context) =>
            {
                var facility = await context.Facilities.FindAsync(facilityId);
                var specialization = await context.Specializations.FindAsync(specializationId);

                if (facility == null || specialization == null)
                {
                    return Results.NotFound();
                }

                var facilitySpecialization = new FacilitySpecialization
                {
                    FacilityId = facilityId,
                    SpecializationId = specializationId
                };

                context.FacilitySpecializations.Add(facilitySpecialization);
                await context.SaveChangesAsync();

                return Results.Created($"/facilities/{facilityId}/specializations/{specializationId}", facilitySpecialization);
            });
        }
    }
}