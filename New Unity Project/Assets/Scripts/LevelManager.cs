using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{

    // Un prefabricado para la creacion de mosaicos
    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize
    {
        ///Calculamos que tan grandes son nuestros mosaicos, esto lo usamos para colocarlos en la pocison correcta
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Use this for initialization
    void Start()
    {
        //Point p = new Point(0, 0);
        //Debug.Log(p.X);
        //TestValue(p);
        //Debug.Log(p.X);

        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void TestValue(Point p)
    //{
    //    Debug.Log("Changig value");

    //    p.X = 3;


    //}


    private void CreateLevel()
    {
        string[] mapData = ReadLeveltext();
       

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        //float tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {

                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }

        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        ///Creamos un nuevo mosaico y los usamos de referencia en la variable newTile
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);

        ///Usamos la nueva variable de los mosaicos para cambiar la pocision de los mosaicos siguientes
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }

    private string[] ReadLeveltext()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
