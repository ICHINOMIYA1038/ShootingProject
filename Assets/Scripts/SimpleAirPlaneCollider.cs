using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeneGames.Airplane
{
    public class SimpleAirPlaneCollider : MonoBehaviour
    {
        public bool collideSometing;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<SimpleAirPlaneCollider>() == null)
            {
                Debug.Log(other.gameObject.name);
                if(other.gameObject.Equals("Terrain"))
                collideSometing = true;
            }
        }
    }
}