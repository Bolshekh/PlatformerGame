using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField] TMP_Text Text;
    // Start is called before the first frame update
    void Start()
    {
        //TODO: SAVES
        //GlobalScore.SetScore(Save.BestScore);

        Text.text = GlobalScore.Score.ToString();
    }


}
