using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    float lowest = -10f;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PlatformCreated += (s, e) => MoveDeath(e.CustomVariable.transform);
    }

    void MoveDeath(Transform Transform)
    {
        if ((Transform.position.y - 10f) < lowest) lowest = (Transform.position.y - 10f);

        transform.localScale = new Vector2(Transform.position.x, transform.localScale.y);
		transform.position = new Vector2(Transform.position.x, lowest);
    }
}
