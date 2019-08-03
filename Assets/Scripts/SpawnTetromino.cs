using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] tetrominoes;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewTetromino();
    }

    public void SpawnNewTetromino()
    {
        GameObject tetromino = Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], transform.position, Quaternion.identity);
        if(!tetromino.GetComponent<Block>().ValidMove()) // if blocked on spawn
        {
            Destroy(tetromino);
            Debug.Log("Game Over!");
            this.enabled = false;
        }
    }
}
