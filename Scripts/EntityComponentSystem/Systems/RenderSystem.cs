using System.Collections.Generic;
using EntityComponentSystem;
using Godot;

public class RenderSystem : ISystem {
	private GameLoop _gameLoop;
	private GameObjectManager _gameObjectManager;
	private EntityManager _entityManager;

	public RenderSystem(GameLoop gameLoop) {
		_gameLoop = gameLoop;
		_entityManager = _gameLoop.EntityManager;
		_gameObjectManager = _gameLoop.GameObjectManager;
	}

	public void Update(double delta) {
		foreach (KeyValuePair<ulong, Node> kvp in _gameObjectManager.GameObjects) {
			if (kvp.Value is Node2D node2D) {
				Entity entity = _entityManager.GetEntity(kvp.Key);
				PositionComponent positionComponent = entity.GetComponent<PositionComponent>();
				if (positionComponent is null) {
					GD.PrintErr($"Entity {kvp.Key} does not have a PositionComponent");
					return;
				}

				node2D.Position = positionComponent.Position;
			}
		}
	}
}
