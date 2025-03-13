using System;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace Person.Routes;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        var route = app.MapGroup("person");

        route.MapPost("", async (PersonRequest req, PersonContext DbContext) =>
        {
            var person = new PersonModel(req.name);
            await DbContext.AddAsync(person);
            await DbContext.SaveChangesAsync();
            return Results.Ok(person);
        });

        route.MapPost("activate/{id:int}", async (int id, PersonContext DbContext) =>
        {
            var person = await DbContext.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return Results.NotFound();
            }
            if (person.IsActive)
            {
                return Results.BadRequest("Person is already active.");
            }

            person.IsActive = true;
            await DbContext.SaveChangesAsync();

            return Results.Ok(person);
        });

        route.MapGet("", async (PersonContext DbContext) =>
        {
            List<PersonModel>? people = await DbContext.People.ToListAsync();
            return Results.Ok(people);
        });

        route.MapPut("{id:int}", async (int id, PersonRequest req, PersonContext DbContext) =>
        {
            var person = await DbContext.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return Results.NotFound();
            }

            person.UpdateName(req.name);
            await DbContext.SaveChangesAsync();

            return Results.Ok(person);
        });

        // Soft Delete
        route.MapDelete("{id:int}", async (int id, PersonContext DbContext) =>
        {

            var person = await DbContext.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return Results.NotFound();
            }

            person.SetInactive();
            await DbContext.SaveChangesAsync();
            return Results.Ok();
        });
    }
}
