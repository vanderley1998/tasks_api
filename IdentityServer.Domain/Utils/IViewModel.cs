using System;

namespace LubyTasks.Domain.Utils
{
    public interface IViewModel
    {
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public bool? Removed { get; set; }
    }
}
