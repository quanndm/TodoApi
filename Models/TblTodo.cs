using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApi.Models
{
    public partial class TblTodo
    {
        public long Id { get; set; }
        public string Todo { get; set; }
        public long StatusId { get; set; }

        public virtual TblStatus Status { get; set; }
    }
}
