using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
namespace WebApplication1;

public static class BrokerEndpoints
{
    public static void MapBrokerEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Broker", async (WebApplication1Context db) =>
        {
            return await db.Broker.ToListAsync();
        })
        .WithName("GetAllBrokers")
        .Produces<List<Broker>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Broker/{id}", async (int ID, WebApplication1Context db) =>
        {
            return await db.Broker.FindAsync(ID)
                is Broker model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetBrokerById")
        .Produces<Broker>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Broker/{id}", async (int ID, Broker broker, WebApplication1Context db) =>
        {
            var foundModel = await db.Broker.FindAsync(ID);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(broker);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateBroker")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Broker/", async (Broker broker, WebApplication1Context db) =>
        {
            db.Broker.Add(broker);
            await db.SaveChangesAsync();
            return Results.Created($"/Brokers/{broker.ID}", broker);
        })
        .WithName("CreateBroker")
        .Produces<Broker>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Broker/{id}", async (int ID, WebApplication1Context db) =>
        {
            if (await db.Broker.FindAsync(ID) is Broker broker)
            {
                db.Broker.Remove(broker);
                await db.SaveChangesAsync();
                return Results.Ok(broker);
            }

            return Results.NotFound();
        })
        .WithName("DeleteBroker")
        .Produces<Broker>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
