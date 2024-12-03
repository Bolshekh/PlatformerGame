using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    [SerializeField] List<RawImage> layers;
    [SerializeField] float speed;
    [SerializeField] Transform player;
    [SerializeField] float speedDecrease;
    // Update is called once per frame
    void Update()
    {
        
    }
}
