//Created by ThaiQ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class master : MonoBehaviour {

    static int currentlevel = 0;

	public static void NewLevel()
    {
        if (currentlevel < 3)
        {
            currentlevel++;
            //Application.LoadLevel(1);
            SceneManager.LoadScene(currentlevel);

        }
        else
        {
            print("Game Over");
        }
    }
}
