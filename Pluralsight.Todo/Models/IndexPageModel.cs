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



        public EnumCompletionSelectionOption CompletionSelectionOption { get; set; }


        public bool IncludeOnlyVacationEntries { get; set; }
        

    }

}
