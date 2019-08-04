using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] tetrominoes;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnNewTetromino();
    }

    public void SpawnNewTetromino(int controllingPlayerId)
    {
        GameObject tetromino = Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], transform.position, Quaternion.identity);
        tetromino.GetComponent<Block>().controllingPlayerId = controllingPlayerId;
        if(!tetromino.GetComponent<Block>().ValidMove()) // if blocked on spawn
        {
            Destroy(tetromino);
            GameManager.instance.GameOver();
            this.enabled = false;
        }
    }
}
