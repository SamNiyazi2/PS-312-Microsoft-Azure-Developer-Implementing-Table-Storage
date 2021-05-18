using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 05/16/2021 11:46 am - SSN - [20210516-1011] - [004] - M03-02 - Introducing Azure table storage in a .NET application

namespace Pluralsight.Todo.Models
{
    public class IndexPageModel
    {
        public IEnumerable<TodoModel> todoModel { get; set; }
        public IList<AzureTableOption> azureTableOptions { get; set; }



        public EnumCompletionSelectionOption CompletionSelectionOption { get; set; }


        public bool IncludeOnlyVacationEntries { get; set; }
        public EnumAzureTableTypes AzureTableOptionSelected { get; set; }

        //public bool UseAzure_Storage_Table { get; set; }
        //public bool UseAzure_CosmoDB_Table { get; set; }



        public IndexPageModel()
        { 

            azureTableOptions = new List<AzureTableOption>();

            this.azureTableOptions.Add(new AzureTableOption { OptionTitle = "Azure Storage Table", OptionValue = "Storage" });
            this.azureTableOptions.Add(new AzureTableOption { OptionTitle = "Azure CosmoDB Table", OptionValue = "CosmoDB" });
        }
    }


    public class AzureTableOption
    {
        public string OptionTitle { get; set; }
        public string OptionValue { get; set; }
    }
}
