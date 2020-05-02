using System;

namespace LubyTasks.Domain.Queries.ViewModels
{
    public class Action
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Concluded { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public bool? Removed { get; set; }
    }
}
