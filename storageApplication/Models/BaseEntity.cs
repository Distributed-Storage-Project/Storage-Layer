using System;
namespace Models
{
	public class BaseEntity
	{
		public BaseEntity()
		{
		
		}

		public long id { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

