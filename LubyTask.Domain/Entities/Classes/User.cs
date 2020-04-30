using LubyTasks.Domain.Utils.Interfaces;
using System;

namespace LubyTasks.Domain.Utils.Classes
{
    public class User : ICommonEntityProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CredentialUser Credential { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool Removed { get; set; }
    }
}
