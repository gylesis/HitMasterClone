using System;
using Project.PlayerLogic;

namespace Project
{
    public class WaypointsPlayerMoveService
    {
        private readonly PlayerFacade _playerFacade;
        private Action _onArrived;


        public WaypointsPlayerMoveService(PlayerFacade playerFacade)
        {
            _playerFacade = playerFacade;
        }

        public void Move(PlayerWaypoint waypoint, Action onArrived = null)
        {
            _playerFacade.Move(waypoint.transform.position, onArrived);
        }
    }
}