using System.Linq;

namespace Project.Level
{
    public class LevelsController
    {
        private int _currentLevel = 0;
        
        private Level[] _levels;

        private Level _current;

        public LevelsController(Level[] levels)
        {
            _levels = levels;
        }

        public bool DoesExistMoreLevels()
        {
            var potentialLevel = _currentLevel + 1;

            Level level = _levels.FirstOrDefault(x => x.Id == potentialLevel);

            return level != null;
        }
        
        public Level GetNextLevel()
        {
            _currentLevel++;

            Level level = _levels.FirstOrDefault(x => x.Id == _currentLevel);

            _current = level;
            
            return level;
        }

    }
}