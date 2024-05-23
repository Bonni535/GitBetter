using GitBetter.Dtos;
using GitBetter.Models;
using Microsoft.EntityFrameworkCore;

namespace GitBetter.API
{
    public class AppointmentAPI
    {
        public static void Map(WebApplication app)
        {
            
            //Get all the Appointments
            app.MapGet("/appointments", (GitBetterDbContext db) =>
            {
                var appointments = db.Appointments
                                                  .Select(appointment => new
                                                  {
                                                      appointment.Id,
                                                      appointment.PatientId,
                                                      appointment.ProviderId,
                                                      appointment.FacilityId,
                                                      appointment.Date,
                                                      appointment.IsScheduled,
                                                      appointment.Notes
                                                  }).ToList();

                return Results.Ok(appointments);
            });

            
            //Get Appointment by Id
            app.MapGet("/appointments/{id}", (GitBetterDbContext db, int id) =>
            {
                var appointment = db.Appointments
                                    .Where(a => a.Id == id)
                                    .Select(a => new
                                    {
                                        a.Id,
                                        a.PatientId,
                                        a.ProviderId,
                                        a.FacilityId,
                                        a.Date,
                                        a.IsScheduled,
                                        a.Notes
                                    }).ToList();

                if (appointment == null)
                {
                    return Results.NotFound("Specialization not found.");
                }

                return Results.Ok(appointment);
            });


            //Create an Appointment
            app.MapPost("/appointments", (GitBetterDbContext db, Appointment createAppointment) =>
            {
                db.Appointments.Add(createAppointment);
                db.SaveChanges();
                return Results.Created($"/api/addAppointment/{createAppointment.Id}", createAppointment);
            });

            //Update an Appointment
            app.MapPatch("/appointments/{id}", (GitBetterDbContext db, int id, UpdateAppointmentDTO updateAppointmentDTO) =>
            {
                var appointment = db.Appointments.FirstOrDefault(a => a.Id == id);

                if (appointment == null)
                {
                    return Results.NotFound($" The appointment {id} was not found.");
                }

                //update appointment details
                appointment.Date = updateAppointmentDTO.Date;
                appointment.IsScheduled = updateAppointmentDTO.IsScheduled;
                appointment.Notes = updateAppointmentDTO.Notes;

                db.SaveChanges();

                return Results.Ok($"The appointment {id} was updated successfully.");
            });

            //Delete an Appointment
            app.MapDelete("/appointments", (GitBetterDbContext db, int id) =>
            {
                var appointmentToDelete = db.Appointments.FirstOrDefault(a => a.Id == id);

                if (appointmentToDelete == null)
                {
                    return Results.NotFound("There was an issue deleting this appointment.");
                }
                else
                {
                    db.Appointments.Remove(appointmentToDelete);

                    db.SaveChanges();
                }

                return Results.Ok($"The appointment {id} has been canceled.");
            });

        }
    }
}