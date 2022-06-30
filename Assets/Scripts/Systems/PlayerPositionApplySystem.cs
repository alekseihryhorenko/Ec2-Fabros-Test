using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

public class PlayerPositionApplySystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var players = ecsWorld.Filter<PlayerTag>().Inc<Circle>().Inc<TransformComponent>().End();

        var circles = ecsWorld.GetPool<Circle>();
        var transforms = ecsWorld.GetPool<TransformComponent>();

        foreach (var player in players)
        {
            var transform = transforms.Get(player);
            var center = circles.Get(player).Center;
            transform.Value.position = new Vector3(center.x, 0.0f, center.y);
        }
    }
}
