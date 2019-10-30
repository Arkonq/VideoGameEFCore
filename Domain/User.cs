using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class User : Entity
	{
		public string Name { get; set; }
		public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
		public virtual ICollection<VideogamesUsers> Videogames { get; set; } = new List<VideogamesUsers>();
	}
}
