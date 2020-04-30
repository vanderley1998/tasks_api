using LubyTasks.Domain.Utils.Interfaces;
using System;

namespace LubyTasks.Domain.Utils.Classes
{
    public class Task : ICommonEntityProperties
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Concluded { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool Removed { get; set; }
    }
}
