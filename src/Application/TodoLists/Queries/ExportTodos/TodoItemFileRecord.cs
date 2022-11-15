using disclone.Application.Common.Mappings;
using disclone.Domain.Entities;

namespace disclone.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
