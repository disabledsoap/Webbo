using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class puzzleScript : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI text;

    private Vector3Int location;
    private int[,] rotations = new int[7, 7];

    public static bool solved = false;

    void Start()
    {
        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
                rotations[i, j] = Random.Range(-1, 1);

        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
            {
                Vector3Int location = new Vector3Int(i - 8, 2 - j);
                Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rotations[i, j] * 90f), Vector3.one);
                tilemap.SetTransformMatrix(location, matrix);
            }

        rotations[0, 0] = 0;
        rotations[0, 6] = 0;
        rotations[6, 0] = 0;
        rotations[6, 6] = 0;
    }

    public bool isSolved()
    {
        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 7; j++)
            {
                int rot = rotations[i, j];
                if (rot % 4 != 0 && (i % 6 != 0 && j % 6 != 0))
                {
                    Debug.Log($"{i}, {j}");
                    return false;
                }
            }

        return true;
    }

    void Update()
    {
        solved = isSolved();
        if (solved)
        {
            title.text = "description";
            title.fontSize = 26;
            text.text = "Jupiter is a world of extremes, with mammoth storms, superfast winds and unbelievably cold temperatures. Webb is capturing the stunning details of our Solar System�s largest planet with NIRCam�s three specialized infrared filters";
            text.fontSize = 14;
        }
        if (solved && (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.R)))
        {
            string currPath = Directory.GetCurrentDirectory();
            string checkpointPath = currPath + "\\Assets\\Checkpoints.txt";

            string[] lines = { "1" };
            File.WriteAllLines(checkpointPath, lines);

            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetMouseButtonDown(0) && !solved)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            location = tilemap.WorldToCell(mousePosition);

            if (!tilemap.GetTile(location))
                return;

            int i = 8 + location.x, j = 2 - location.y;
            rotations[i, j] += 1;
            Debug.Log($"I: {i}, J: {j}, R: {rotations[i, j]}");

            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rotations[i, j] * 90f), Vector3.one);
            tilemap.SetTransformMatrix(location, matrix);
        }
    }
}
