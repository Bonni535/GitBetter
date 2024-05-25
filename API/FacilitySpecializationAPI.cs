using GitBetter.Models;
using Microsoft.EntityFrameworkCore;

namespace GitBetter.API
{
    public class FacilitySpecializationAPI
    {
        public static void Map(WebApplication app)
        {
           

            app.MapPost("/facilities/{facilityId}/specializations/{specializationId}", async (GitBetterDbContext db, int facilityId, int specializationId) =>
            {
                // Check if the Facility is in the Database
                var facility = db.Facilities.FirstOrDefault(a => a.Id == facilityId);
                if (facility == null)
                {
                    return Results.NotFound("The Facility was not found.");
                }

                // Check to see if the Specialization exists
                var specialization = db.Specializations.FirstOrDefault(t => t.Id == specializationId);
                if (specialization == null)
                {
                    return Results.NotFound("The Specialization was not found");
                }

                // Check if the Specialization is already attached
                var existingFacilitySpecialization = db.FacilitySpecializations.Where(fs => (fs.FacilityId == facilityId) && (fs.SpecializationId == specializationId)).FirstOrDefault();
                if (existingFacilitySpecialization == null)
                {
                    var facilitySpecialization = new FacilitySpecialization
                    {
                        FacilityId = facilityId,
                        SpecializationId = specializationId
                    };
                    db.FacilitySpecializations.Add(facilitySpecialization);
                    db.SaveChanges();

                    var specializationTitle = specialization.Title;

                    return Results.Ok($"{specializationTitle} has been added to the Facility {facilityId}.");
                }
                else
                {
                    return Results.NotFound("The Specialization was already added");
                }

            });

            //Delete a Specialization from a Facility
            app.MapDelete("/facilities/{facilityId}/specializations/{facilitySpecializationId}", async (GitBetterDbContext db, int facilityId, int facilitySpecializationId) =>
            {
                var removeSpecialization = await db.FacilitySpecializations
                .Include(fs => fs.Specialization)
                .Where(fs => fs.FacilityId == facilityId && fs.Id == facilitySpecializationId)
                .FirstOrDefaultAsync();

                if (removeSpecialization == null)
                {
                    return Results.NotFound("This Specialization was not found in this Facility.");
                }

                //Now Fetch the specific Facility
                var facility = db.Facilities.FindAsync(facilityId);
                if(facility == null)
                {
                    return Results.NotFound("Facility not found.");
                }

                //Remove the FacilitySpecialization from the DataBase
                db.FacilitySpecializations.Remove(removeSpecialization);
                db.SaveChangesAsync();

                return Results.Ok(new { Message = $"{removeSpecialization.Specialization.Title} has been removed from the Facility {facilityId}." });
            });
        }
    }
}