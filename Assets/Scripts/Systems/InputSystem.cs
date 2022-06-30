using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

public class InputSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
   //TODO separed class Input event and Player Start Movement  
    public void Run(EcsSystems ecsSystem)
    {
        ecsWorld = ecsSystem.GetWorld();
        var datas = ecsWorld.Filter<PlayerMovingData>().End();
        if (datas.GetEntitiesCount() > 0)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                return;
            }
            var entity = ecsWorld.NewEntity(); 
            ref var data = ref ecsWorld.GetPool<PlayerMovingData>().Add(entity);
            data.Target = new Vector2(hit.point.x, hit.point.z);
        }
    }
}