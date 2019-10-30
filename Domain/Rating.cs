using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Rating : Entity
	{
		public int Rate { get; set; }
		public virtual User User { get; set; }
		public virtual Videogame Videogame { get; set; }
	}
}
