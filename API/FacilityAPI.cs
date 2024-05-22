using GitBetter.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace GitBetter.API
{
    public class FacilityAPI
    {
        public static void Map(WebApplication app)
        {
            // Get all the Facilities
            app.MapGet("/facilities",  (GitBetterDbContext db) =>
            {
                return db.Facilities
                                              .Include(f => f.Appointments)
                                              .Include(f => f.FacilitySpecializations)
                                              .ToList();

            });

            // Get a Single Facility
            app.MapGet("/facilities/{facilityId}", (GitBetterDbContext db, int facilityId) =>
            {
                var facility = db.Facilities
                                            //.Include(f => f.Users)
                                            .Include(f => f.Appointments)
                                            .Include(f => f.FacilitySpecializations)
                                            .FirstOrDefault(f => f.Id == facilityId);
                if (facility == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(facility);

            });

            // Create a New Facility
           app.MapPost("/facilities", async (Facility facility, GitBetterDbContext db) =>
            {
              db.Facilities.Add(facility);
               await db.SaveChangesAsync();
               return Results.Created($"/facilities/{facility.Id}", facility);
            });

            //Delete a Facility
            app.MapDelete("/facilities/{facilityId}/specializations/{specializationId}", (GitBetterDbContext db, int facilityId, int specializationId) =>
            {
                var facility = db.Facilities.Include(f => f.Appointments).FirstOrDefault(f => f.Id == facilityId);

                if (facility == null)
                {
                    return Results.NotFound("Facility not found.");
                }

                var specializationToRemove = facility.FacilitySpecializations.FirstOrDefault(s => s.Id == specializationId);

                if (specializationToRemove == null)
                {
                    return Results.NotFound("Specialization not found.");
                }

                facility.FacilitySpecializations.Remove(specializationToRemove);

                db.SaveChanges();

                return Results.Ok("Specialization removed from the facility ");
            });

          
        }
    }
}