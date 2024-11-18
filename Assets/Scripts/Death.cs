using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PlatformCreated += (s, e) => MoveDeath(e.CustomVariable.transform);
    }

    void MoveDeath(Transform Transform)
    {
        transform.position = new Vector2(Transform.position.x, Transform.position.y - 10f);
    }
}
