using disclone.Application.TodoLists.Queries.ExportTodos;

namespace disclone.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
