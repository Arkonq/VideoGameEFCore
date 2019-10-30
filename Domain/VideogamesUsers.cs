using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class VideogamesUsers : Entity
	{
		public virtual Videogame Videogame { get; set; }

		public virtual User User { get; set; }		
	}
}
