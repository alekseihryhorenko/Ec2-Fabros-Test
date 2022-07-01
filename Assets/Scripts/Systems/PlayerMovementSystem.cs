using System.Collections;
using Leopotam.EcsLite;
using UnityEngine;

public class PlayerMovementSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var gameData = ecsSystem.GetShared<GameData>();
        var players = ecsWorld.Filter<PlayerTag>().Inc<Circle>().End();

        var datas = ecsWorld.Filter<PlayerMovingData>().End();

        var circles = ecsWorld.GetPool<Circle>();
        var playerMovingDataPool = ecsWorld.GetPool<PlayerMovingData>();

        foreach (var player in players)
        {
            ref var playerCircle = ref circles.Get(player);

            foreach (var data in datas)
            {
                ref var playerMovingData = ref playerMovingDataPool.Get(data);
                var result = Vector2.MoveTowards(playerCircle.Center, playerMovingData.Target, Time.fixedDeltaTime * gameData.playerSpeed);

                if (result == playerMovingData.Target)
                {
                    playerMovingDataPool.Del(data);
                }
                playerCircle.Center = result;
            }
        }
    }
}

