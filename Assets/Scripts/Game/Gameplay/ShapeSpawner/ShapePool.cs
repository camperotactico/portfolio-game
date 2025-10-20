using UnityEngine;

namespace Game.Gameplay.Shape_Spawner
{
    public class ShapePool: Pool<Shape>
    {
        private readonly SpawnedShapesRuntimeSet _spawnedShapesRuntimeSet;
        
        public ShapePool(SpawnedShapesRuntimeSet newSpawnedShapesRuntimeSet,Shape newInstancePrefab, Transform newInstancesParent, int newDefaultCapacity = 8, int newMaxSize = 32) : base(newInstancePrefab, newInstancesParent, newDefaultCapacity, newMaxSize)
        {
            _spawnedShapesRuntimeSet = newSpawnedShapesRuntimeSet;
        }

        protected override Shape OnCreate()
        {
            Shape createdShape = base.OnCreate();
            _spawnedShapesRuntimeSet.OnShapeSpawned(createdShape);
            return createdShape;
        }

        protected override void OnDestroy(Shape shapeToDestroy)
        {
            _spawnedShapesRuntimeSet.OnShapeDestroyed(shapeToDestroy);
            base.OnDestroy(shapeToDestroy);
        }
    }
}