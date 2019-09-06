using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private void Start()
    {
        if (gameManager == null)
            gameManager = this;
    }
}
