using System;

namespace LubyTasks.Domain.Queries.ViewModels
{
    public class CredentialUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public bool? Removed { get; set; }
    }
}