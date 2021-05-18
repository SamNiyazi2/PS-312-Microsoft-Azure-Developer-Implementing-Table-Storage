using Microsoft.Azure.Cosmos.Table;
using Pluralsight.Todo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluralsight.Todo.Repositories
{



    public class TodoRepository : ITodoRepository
    {
        private CloudTable todoTable = null;
        public EnumAzureTableTypes AzureTableTypes { get; set; }


        // public TodoRepository()
        public void DO_TodoRepository()
        {


            CloudStorageAccount storageAccount = null;

            // 05/16/2021 10:52 am - SSN - [20210516-1011] - [002] - M03-02 - Introducing Azure table storage in a .NET application
             

            if (this.AzureTableTypes == EnumAzureTableTypes.AzureStorageTable)
            {
                storageAccount = CloudStorageAccount.Parse(MvcApplication.ps312AzureTableConnectionString_azureTable);
                // storageAccount = CloudStorageAccount.Parse(tableConnectionString);
            }

            if (this.AzureTableTypes == EnumAzureTableTypes.AzureCosmoDBTable)
            {
                storageAccount = CloudStorageAccount.Parse(MvcApplication.ps312AzureTableConnectionString_cosmboDBTable);                // storageAccount = CloudStorageAccount.Parse(tableConnectionString);
            }


            if (storageAccount != null)
            {
                var tableClient = storageAccount.CreateCloudTableClient();
                todoTable = tableClient.GetTableReference("Todo");
            }
            else
            {
                todoTable = null;
            }


            // todoTable.CreateIfNotExists();

        }


        public IEnumerable<TodoEntity> All(EnumCompletionSelectionOption completionSelectionOption, bool IncludeOnlyVacationEntries)
        { 

            var query = new TableQuery<TodoEntity>();

            string completedFilter = null;

            if (completionSelectionOption != EnumCompletionSelectionOption.Both)
            {

                completedFilter = TableQuery.GenerateFilterConditionForBool(nameof(TodoEntity.Completed),
                                               QueryComparisons.Equal,
                                               completionSelectionOption == EnumCompletionSelectionOption.Completed);

            }

            var isVacation = TableQuery.GenerateFilterCondition(nameof(TodoEntity.PartitionKey),
                                        QueryComparisons.Equal, "Vacation");

            if (IncludeOnlyVacationEntries && !string.IsNullOrWhiteSpace(completedFilter))
            {

                query = query.Where(TableQuery.CombineFilters(
                    isVacation,
                    TableOperators.And,
                    completedFilter));

            }
            else if (IncludeOnlyVacationEntries)
            {
                query = query.Where(isVacation);

            }
            else if (!string.IsNullOrWhiteSpace(completedFilter))
            {
                query = query.Where(completedFilter);

            }






            var entities = todoTable.ExecuteQuery(query);

            return entities;
        }

        public void CreateOrUpdate(TodoEntity entity)
        {
            var operation = TableOperation.InsertOrReplace(entity);

            todoTable.Execute(operation);
        }

        public void Delete(TodoEntity entity)
        {
            var operation = TableOperation.Delete(entity);

            todoTable.Execute(operation);
        }

        public TodoEntity Get(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TodoEntity>(partitionKey, rowKey);

            var result = todoTable.Execute(operation);

            return result.Result as TodoEntity;
        }
    }

    public class TodoEntity : TableEntity
    {
        public string Content { get; set; }
        public bool Completed { get; set; }
        public string Due { get; set; }

        public DateTimeOffset? CompletedDate { get; set; }
    }
}