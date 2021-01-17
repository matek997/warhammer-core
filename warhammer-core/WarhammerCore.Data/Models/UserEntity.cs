using System;

namespace WarhammerCore.Data.Models
{
    public partial class UserEntity
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}