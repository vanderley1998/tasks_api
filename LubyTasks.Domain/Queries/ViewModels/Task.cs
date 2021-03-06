﻿using System;

namespace LubyTasks.Domain.Queries.ViewModels
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Concluded { get; set; }
        public User User { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public bool? Removed { get; set; }
    }
}
