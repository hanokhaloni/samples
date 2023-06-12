﻿using System.Text.Json;

namespace kafka.pubsub.console
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}