using Leopotam.EcsLite;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;

    public void Init(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var playerEntity = ecsWorld.NewEntity();

        var player = GameObject.FindGameObjectWithTag("Player");
        ref var transform = ref ecsWorld.GetPool<TransformComponent>().Add(playerEntity);
        transform.Value = player.transform;
        ecsWorld.GetPool<PlayerTag>().Add(playerEntity);
        ref var circle = ref ecsWorld.GetPool<Circle>().Add(playerEntity);
        circle.Radius = player.transform.GetChild(0).transform.localScale.x / 2;
        circle.Center = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);
    }
}
