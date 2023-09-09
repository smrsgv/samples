using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using sample_grpc.Data;
using sample_grpc.Models;

namespace sample_grpc.Services;

public class ToDoService : ToDo.ToDoBase
{
    private readonly AppDbContext _context;

    public ToDoService(AppDbContext context)
    {
        _context = context;
    }

    public override async Task<ToDoItemCreateResponse> Create(ToDoItemCreateRequest request, ServerCallContext context)
    {
        var item = new ToDoItem
        {
            Title = request.Title,
            Description = request.Description
        };

        await _context.ToDoItems.AddAsync(item);
        await _context.SaveChangesAsync();

        return await Task.FromResult(new ToDoItemCreateResponse
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Status = item.Status
        });
    }

    public override async Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
    {
        var item = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (item == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"There is no item with Id = {request.Id}"));
        }

        return await Task.FromResult(new GetByIdResponse()
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Status = item.Status
        });
    }

    public override async Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
    {
        var getAllResponse = new GetAllResponse();
        var items = await _context.ToDoItems.ToListAsync();

        if (items.Any())
        {
            foreach (var item in items)
            {
                getAllResponse.ToDoItems.Add(new GetByIdResponse()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Status = item.Status
                });
            }
        }

        return await Task.FromResult(getAllResponse);
    }

    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        var item = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == request.Id);
        item.Status = request.Status;

        _context.Update(item);
        await _context.SaveChangesAsync();

        return new UpdateResponse()
        {
            Id = item.Id,
            Status = item.Status
        };;
    }

    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var item = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (item == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"There is no item with Id = {request.Id}"));
        }

        _context.ToDoItems.Remove(item);
        await _context.SaveChangesAsync();

        return await Task.FromResult(new DeleteResponse() { Id = item.Id });
    }
}