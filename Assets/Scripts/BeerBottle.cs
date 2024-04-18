using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();

    public void Explode()
    {
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
            part.useGravity = true;
            part.AddExplosionForce(1000, transform.position, 10);
        }
    }
}
