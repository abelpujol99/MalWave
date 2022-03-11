using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class EventManager : MonoBehaviour
    {
        public delegate void Action();

        public static event Action JumpAction;
        
        
    }    
}

