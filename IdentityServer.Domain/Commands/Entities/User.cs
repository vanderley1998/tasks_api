﻿using System;

namespace IdentyServer.Domain.Commands.Auth.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool Removed { get; set; }
    }
}