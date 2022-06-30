using Leopotam.EcsLite;
using UnityEngine;

public class ButtonUpdateSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;

    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var buttons = ecsWorld.Filter<ButtonTag>().Inc<Circle>().Inc<Colored>().End();
        var circles = ecsWorld.GetPool<Circle>();
        var players = ecsWorld.Filter<PlayerTag>().Inc<Circle>().End();
        foreach(var player in players)
        {
            var playerCircle = circles.Get(player);

            foreach(var button in buttons)
            {
                var circle = circles.Get(button);
                if (Vector2.Distance(circle.Center, playerCircle.Center) <= circle.Radius + playerCircle.Radius)
                {
                    ChangeFlagsIfNeed<UnPressedTag, PressedTag>(button);
                    break;
                } else {
                    ChangeFlagsIfNeed<PressedTag, UnPressedTag>(button);
                }
            }

        }
    }

    void ChangeFlagsIfNeed<SRC, DIST>(int entity) where SRC  : struct
                                                  where DIST : struct 
    {
        var src = ecsWorld.GetPool<SRC>();
        var dist = ecsWorld.GetPool<DIST>();
        if (!dist.Has(entity))
        {
            src.Del(entity);
            dist.Add(entity);
        }
    } 
}
