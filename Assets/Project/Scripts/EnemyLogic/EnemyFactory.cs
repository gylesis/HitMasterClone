using Zenject;

namespace Project.EnemyLogic
{
    public class EnemyFactory : PlaceholderFactory<EnemySpawnContext, Enemy>
    {
        public override Enemy Create(EnemySpawnContext context)
        {
            Enemy enemy = base.Create(context);

            enemy.transform.position = context.SpawnPoint.transform.position;
            enemy.transform.rotation = context.SpawnPoint.transform.rotation;
            
            return enemy;
        }

    }
    
    public struct EnemySpawnContext
    {
        public EnemySpawnPoint SpawnPoint;
        public int Hp;
    }

}