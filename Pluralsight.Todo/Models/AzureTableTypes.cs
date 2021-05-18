using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Pluralsight.Todo.Models
{

    public enum EnumAzureTableTypes
    {
        [Description("Azure Storage Table")]
        AzureStorageTable,

        [Description("Azure CosmoBD Table")]
        AzureCosmoDBTable
    }

}