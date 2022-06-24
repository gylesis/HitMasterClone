using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class InputService : ITickable
    {
        public Subject<Vector2> Touched { get; } = new Subject<Vector2>();
            
        public Vector2 TouchDelta { get; private set; } 
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                
                Touched.OnNext(mousePosition);
            }

            if (Input.touches.Length > 0)
            {
                Touch touch = Input.touches[0];

                TouchDelta = touch.deltaPosition;
                
                if (touch.phase != TouchPhase.Began) return;
                
                Vector2 touchPos = touch.position;
                
                Touched.OnNext(touchPos);
            }
            
        }
    }
}