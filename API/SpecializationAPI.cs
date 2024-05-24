using GitBetter.Models;
using Microsoft.EntityFrameworkCore;

namespace GitBetter.API
{
    public class SpecializationAPI
    {
        public static void Map(WebApplication app)
        {
            //Get all the Specializations
            app.MapGet("/specializations", (GitBetterDbContext db) =>
            {
                return db.Specializations.ToList();
            });

            //Get Specialization by Id
            app.MapGet("/specializations/{id}", (GitBetterDbContext db, int id) =>
            {
                var specializationId = db.Specializations.FirstOrDefault(s => s.Id == id);

                if (specializationId == null)
                {
                    return Results.NotFound("No Specialization was Found.");
                }

                return Results.Ok(specializationId);
            });

            //Create a New Specialization
            app.MapPost("/specializations", (GitBetterDbContext db, Specialization createSpecialization) =>
            {
                db.Specializations.Add(createSpecialization);
                db.SaveChanges();
                return Results.Created($"/api/addSpecialization/{createSpecialization.Id}", createSpecialization);
            });

            //Delete a Specialization
            app.MapDelete("/specializations/{id}", (GitBetterDbContext db, int id) =>
            {
                var specializationToDelete = db.Specializations.FirstOrDefault(s => s.Id == id);

                if (specializationToDelete == null)
                {
                    return Results.NotFound("There was an issue with deleting this specialization.");
                }
                else
                {
                    db.Specializations.Remove(specializationToDelete);

                    db.SaveChanges();
                }

                return Results.Ok("The Specialization was deleted.");
            });
        
        }
    }
}