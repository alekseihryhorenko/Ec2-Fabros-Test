

using Leopotam.EcsLite;
using UnityEngine;

public class ButtonInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;

    public void Init(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var buutonsPool = ecsWorld.GetPool<ButtonTag>();
        var coloredPool = ecsWorld.GetPool<Colored>();
        var circles = ecsWorld.GetPool<Circle>();
        var upressedTags = ecsWorld.GetPool<UnPressedTag>();

        var buttons = GameObject.FindGameObjectsWithTag(Tags.BUTION);
        foreach (var button in buttons)
        {
            var entity = ecsWorld.NewEntity();
            buutonsPool.Add(entity);
            ref var colored = ref coloredPool.Add(entity);
            colored.Color = button.GetComponent<ColoredComponent>().color;
            ref var circle = ref circles.Add(entity);
            circle.Radius = button.transform.GetChild(0).transform.localScale.x / 2;
            circle.Center = new Vector2(button.transform.localPosition.x, button.transform.localPosition.z);
            upressedTags.Add(entity);
        }
    }
}
