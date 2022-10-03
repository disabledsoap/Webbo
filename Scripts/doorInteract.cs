using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorInteract : MonoBehaviour
{
    [SerializeField] float minDist = 2;
    [SerializeField] GameObject player;

    float dist;
    public Animator transition;

    bool wasInvoked = false;

    void Update()
    {
        dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist < minDist && Input.GetKey(KeyCode.F))
        {
            if (!wasInvoked)
            {
                Debug.Log("Scene transiotion");
                StartCoroutine(LoadMiniGame());
                wasInvoked = true;
            }
        }
        else
        {
            wasInvoked = false;
        }
    }

    IEnumerator LoadMiniGame()
    {
        transition.SetTrigger("switchTrig");

        yield return new WaitForSeconds(0.0f);
        SceneManager.LoadScene("SampleScene");
    }
}
