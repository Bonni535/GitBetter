using GitBetter.Models;
using Microsoft.EntityFrameworkCore;

namespace GitBetter.API
{
    public class UserAPI
    {
        public static void Map(WebApplication app)
        {
            //Create a User
            app.MapPost("users/register", (GitBetterDbContext db, User user) =>
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Results.Created($"/user/{user.Id}", user);
            });

            //Get User by Id
            app.MapGet("/users/{id}", (GitBetterDbContext db, int id) =>
            {
               var user = db.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(user);
            });

            //Update a User
            app.MapPut("users/{id}", (GitBetterDbContext db, int id, User users) =>
            {
                DateTime changeDate = DateTime.Now;
                User updateUser = db.Users.SingleOrDefault(u => u.Id == id);
                if (updateUser == null)
                {
                    return Results.NotFound("User not found.");
                }
                updateUser.Id = users.Id;
                updateUser.Name = users.Name;
                updateUser.Email = users.Email;


                db.SaveChanges();
                return Results.Ok(users);
            });

            //Delete a User
            app.MapDelete("/users/{id}", (GitBetterDbContext db, int id) =>
            {
                var userToDelete = db.Users.Include(u => u.Appointments).FirstOrDefault(u => u.Id == id);
                if (userToDelete == null)
                {
                    return Results.NotFound("No User was found.");
                }

                db.Users.Remove(userToDelete);
                db.SaveChanges();
                return Results.Ok(db.Users);
            });
        }
    }
}