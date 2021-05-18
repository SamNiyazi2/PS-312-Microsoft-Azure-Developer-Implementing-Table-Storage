using System.Collections.Generic;
using Pluralsight.Todo.Models;

namespace Pluralsight.Todo.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<TodoEntity> All(EnumCompletionSelectionOption completionSelectionOption, bool IncludeOnlyVacationEntries);
        void CreateOrUpdate(TodoEntity entity);
        void Delete(TodoEntity entity);
        TodoEntity Get(string partitionKey, string rowKey);

        EnumAzureTableTypes AzureTableTypes { get; set; }
        void DO_TodoRepository();

    }
}