using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Updating_Text : MonoBehaviour
{

    public TMP_Text text;

    public PlayerCombat playercombat;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health = " + playercombat.playerHealth;
    }
}
