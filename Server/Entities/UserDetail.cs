﻿namespace WebAppBlazorLogin.Server.Entities
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

    }
}
