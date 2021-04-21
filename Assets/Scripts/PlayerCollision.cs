using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	public bool jumpAvailable = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision obj) {
    	if (obj.collider.tag == "Ground") {
    		jumpAvailable = true;
    	}
    }

    public void playerJumped() {
    	jumpAvailable = false;
    }
}
