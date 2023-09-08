using Microsoft.Xna.Framework;

namespace RunningGame.Entities
{
	public interface ICollisionEntity
	{
		Rectangle CollisionBox { get; }
	}
}

