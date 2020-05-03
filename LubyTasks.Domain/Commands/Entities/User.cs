using LubyTasks.Domain.Commands.Entities;
using System;
using System.Collections.Generic;

namespace LubyTasks.Domain.Commands.Auth.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Task> Tasks { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool Removed { get; set; }

        public User()
        {
            CreateDate = DateTimeOffset.Now;
            LastModified = DateTimeOffset.Now;
        }
        public void SetData(string name, string login)
        {
            Name = name ?? Name;
            Login = login ?? Login;
            LastModified = DateTimeOffset.Now;
        }

        public void Remove()
        {
            Removed = true;
            foreach(var task in Tasks)
            {
                task.Remove();
            }
            LastModified = DateTimeOffset.Now;
        }
    }
}