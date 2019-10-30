using System;
using System.Collections.Generic;

namespace Domain
{
	public class Videogame : Entity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
		public virtual ICollection<VideogamesUsers> Users { get; set; } = new List<VideogamesUsers>();
	}
}
