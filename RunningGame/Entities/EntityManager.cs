using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RunningGame.Entities
{
	public class EntityManager
	{
		private readonly List<IEntity> _entities = new();

		private readonly List<IEntity> _entitiesAdd = new();
		private readonly List<IEntity> _entitiesRemove = new();

		public IEnumerable<IEntity> Entities => new ReadOnlyCollection<IEntity>(_entities);


		public void Update(GameTime gameTime)
		{
			foreach (IEntity entity in _entities)
			{
				if (_entitiesRemove.Contains(entity))
				{
					continue;
				}

				entity.Update(gameTime);
			}

			foreach (IEntity aEntity in _entitiesAdd)
			{
				_entities.Add(aEntity);
			}
			_entitiesAdd.Clear();

			foreach (IEntity rEntity in _entitiesRemove)
			{
				_entities.Remove(rEntity);
			}
			_entitiesRemove.Clear();
        }

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach (IEntity entity in _entities.OrderBy(ent => ent.DrawOrder))
			{
				entity.Draw(spriteBatch, gameTime);
			}
		}

		public void AddEntity(IEntity entity)
		{
			_entitiesAdd.Add(entity);
		}
        public void RemoveEntity(IEntity entity)
        {
            _entitiesRemove.Add(entity);
        }

		public void ClearEntities()
		{
			_entitiesRemove.AddRange(_entities);
		}



        public IEnumerable<T> GetEntitiesOfType<T>() where T : IEntity
		{
			return _entities.OfType<T>();
		}
	}
}

