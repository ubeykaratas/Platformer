using System.Collections;
using UnityEditor;
using UnityEngine;

public abstract class Base : MonoBehaviour
{
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
       
    }

    public virtual void FixedUpdate()
    {
        
    }

}

public abstract class DeathHandler : MonoBehaviour
{
    protected float respawnTime = 0f;
    public abstract void HandleDeath();
}
