using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class UI5 : MonoBehaviour
{
    public Transform square;

    public void Update()
    {
        if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.R)) && puzzleScript.solved)
        {
            leave();
        }
    }

    void leave()
    {
        string currPath = Directory.GetCurrentDirectory();
        string checkpointPath = currPath + "\\Assets\\Checkpoints.txt";

        string[] lines = { "1" };
        File.WriteAllLines(checkpointPath, lines);

        Destroy(square.gameObject);
        Destroy(this.gameObject);

        SceneManager.LoadScene("GameScene");
    }
}