﻿using System.Text.Json.Serialization;

namespace APICleanArchi.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<GradleEntity> Gradles { get; set; }
    }
}