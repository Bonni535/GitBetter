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

            //Delete a Specialization from a Facility
            app.MapDelete("/facilities/{facilityId}/specializations/{facilitySpecializationId}", async (GitBetterDbContext db, int facilityId, int facilitySpecializationId) =>
            {
                var removeSpecialization = await db.FacilitySpecializations
                .Include(fs => fs.Specialization)
                .Where(fs => fs.SpecializationId == facilityId && fs.Id == facilitySpecializationId)
                .FirstOrDefaultAsync();

                if (removeSpecialization == null)
                {
                    return Results.NotFound("This Specialization was not found in this Facility.");
                }

                //Now Fetch the specific Facility
                var facility = db.Facilities.Find(facilityId);
                if(facility == null)
                {
                    return Results.NotFound("Facility not found.");
                }

                //Remove the FacilitySpecialization from the DataBase
                db.FacilitySpecializations.Remove(removeSpecialization);
                db.SaveChanges();

                return Results.Ok(new { Message = $"{removeSpecialization.Specialization.Title} has been removed from the Facility {facilityId}." });
            });
        }
    }
}