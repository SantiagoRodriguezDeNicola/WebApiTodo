using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using WebApiTodo.Data;
using WebApiTodo.Models;
namespace WebApiTodo.Controllers;

public static class ToDoEndpoints
{
    public static void MapToDoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ToDo").WithTags(nameof(ToDo));

        group.MapGet("/", async (WebApiTodoContext db) =>
        {
            return await db.ToDo.ToListAsync();
        })
            //.RequireAuthorization()
        .WithName("GetAllToDos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<ToDo>, NotFound>> (int id, WebApiTodoContext db) =>
        {
            return await db.ToDo.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is ToDo model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
            //.RequireAuthorization()
        .WithName("GetToDoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, ToDo toDo, WebApiTodoContext db) =>
        {
            var affected = await db.ToDo
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, toDo.Id)
                    .SetProperty(m => m.Title, toDo.Title)
                    .SetProperty(m => m.Description, toDo.Description)
                    .SetProperty(m => m.Condition, toDo.Condition)
                    .SetProperty(m => m.Creation_date, toDo.Creation_date)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
            //.RequireAuthorization()
        .WithName("UpdateToDo")
        .WithOpenApi();

        group.MapPost("/", async (ToDo toDo, WebApiTodoContext db) =>
        {
            db.ToDo.Add(toDo);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/ToDo/{toDo.Id}",toDo);
        })
            //.RequireAuthorization()
        .WithName("CreateToDo")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, WebApiTodoContext db) =>
        {
            var affected = await db.ToDo
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
            //.RequireAuthorization()
        .WithName("DeleteToDo")
        .WithOpenApi();
    }
}
