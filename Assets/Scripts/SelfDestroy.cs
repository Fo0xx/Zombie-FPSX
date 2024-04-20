using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float TimeForDestruction = 5f;
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(DestroySelf(TimeForDestruction));  
    }

    private IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
