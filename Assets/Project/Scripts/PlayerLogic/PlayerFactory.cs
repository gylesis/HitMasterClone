using UnityEngine.AI;
using Zenject;

namespace Project.PlayerLogic
{
    public class PlayerFactory : PlaceholderFactory<PlayerSpawnContext, Player>
    {
        public override Player Create(PlayerSpawnContext context)
        {
            Player player = base.Create(context);

            if (NavMesh.SamplePosition(context.SpawnPoint.position, out var hit, 5, NavMesh.AllAreas))
            {
                player.transform.position = hit.position;
                player.transform.rotation = context.SpawnPoint.rotation;
            }
          
            return player;
        }
    }
}