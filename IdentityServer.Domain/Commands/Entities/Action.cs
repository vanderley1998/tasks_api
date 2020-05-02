using LubyTasks.Domain.Commands.Auth.Entities;
using System;

namespace LubyTasks.Domain.Commands.Entities
{
    public class Action
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Concluded { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool Removed { get; set; }

        public void SetData(string title, string description)
        {
            Title = title ?? Title;
            Description = description ?? Description;
            LastModified = DateTimeOffset.Now;
        }

        public void Remove()
        {
            Removed = true;
            LastModified = DateTimeOffset.Now;
        }
    }
}
