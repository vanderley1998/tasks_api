using System;
using System.Collections.Generic;
using System.Text;

namespace LubyTasks.Entities.Interfaces
{
    public interface ICommonEntityProperties
    {
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool Removed { get; set; }
    }
}
