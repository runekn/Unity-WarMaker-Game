using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void Play()
    {
        SceneManager.LoadScene("MapPlayer");
    }

    public void MapEditor()
    {
        SceneManager.LoadScene("MapEditor");
    }

    public void TileEditor()
    {
        SceneManager.LoadScene("TileEditor");
    }

}
