using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pluralsight.Todo.Models
{
    public class TodoModel
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("Group Name")]
        public string Group { get; set; }

        [Required]
        public string Content { get; set; }

        public string Due { get; set; }

        public bool Completed { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}