using System;
using System.Collections.Generic;
using System.Text;

namespace CommercePlatform.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
        protected void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }


}
