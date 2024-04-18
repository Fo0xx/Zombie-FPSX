using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieHand zombieHand;

    public int damageAmount = 25;
    private void Start()
    {
        zombieHand.damageAmount = damageAmount;
    }
}
