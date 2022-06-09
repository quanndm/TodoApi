using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApi.Models
{
    public partial class TblStatus
    {
        public TblStatus()
        {
            TblTodos = new HashSet<TblTodo>();
        }

        public long Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<TblTodo> TblTodos { get; set; }
    }
}
