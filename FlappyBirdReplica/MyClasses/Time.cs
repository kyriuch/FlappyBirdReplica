using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdReplica.MyClasses
{
	class Time
	{
		public static float DeltaTime;

		public static void UpdateTime(GameTime gameTime)
		{
			DeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}
