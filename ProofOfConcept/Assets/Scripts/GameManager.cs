using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public Text winning;
    public static GameManager gameManager;
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this.gameObject.GetComponent<GameManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (enemies.Count == 0)
        {
            StartCoroutine(DelayWin());
        }
    }
    IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(0.5f);
        winning.enabled = true;
    }
}