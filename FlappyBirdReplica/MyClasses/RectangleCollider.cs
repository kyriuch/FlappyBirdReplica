using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdReplica.MyClasses
{
	class RectangleCollider : Collider
	{
		public Rectangle rectangle { get; set; }
		public bool ShouldPoint { get; set; }
		public bool AlreadyPointed { get; set; }
	}
}
