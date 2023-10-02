using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTerrain : MonoBehaviour
{
    [Header("Terrain")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject gridTile;
    [SerializeField] LayerMask mask;
    public float terrainSize = 2;
    
    void Start()
    {
        player = GetComponent<Transform>();

        Ray ray = new Ray(player.position, new Vector3(0, 0, 1));
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 5f, mask))
        {
            gridTile = hitInfo.collider.gameObject;
        } 

        //player.parent = gridTile.transform;
    }

    void Update()
    {
        Ray ray = new Ray(player.position, new Vector3(0, 0, 1));
        Debug.DrawRay(player.position, new Vector3(0, 0, 1),Color.white, 1.0f);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 5f, mask))
        {
            gridTile = hitInfo.collider.gameObject;
            CheckPosition(hitInfo.collider.gameObject);
        } 
        //Transform playerPos = player;
        //player.parent = gridTile.transform;
        //player.localPosition = Vector3.zero;
        //player.position = playerPos.position;
    }

    void CheckPosition(GameObject currentTile)
    {

        float centerDistX = player.position.x - currentTile.GetComponent<Transform>().position.x;
        float centerDistY = player.position.y - currentTile.GetComponent<Transform>().position.y;
        
        Vector2 direction = Vector2.zero;

        if(centerDistX >= terrainSize - terrainSize/2 - terrainSize/8)
        {
            direction.x = 1;
        }
        if(centerDistX <= -terrainSize - terrainSize/2 - terrainSize/8)
        {
            direction.x = -1;
        }
        if(centerDistY >= terrainSize - terrainSize/2 - terrainSize/8)
        {
            direction.y = 1;
        }
        if(centerDistY <= -terrainSize - terrainSize/2 - terrainSize/8)
        {
            direction.y = -1;
        }

        if(direction != Vector2.zero)
        {
            Debug.Log("o direction é " + direction);
            LoadTiles(currentTile, direction);
        }
    }

    void LoadTiles(GameObject currentTile, Vector2 direction)
    {
        float currentTilePosX = currentTile.GetComponent<Transform>().position.x;
        float currentTilePosY = currentTile.GetComponent<Transform>().position.y;
        //Vector2 currentTileCoord = currentTile.GetComponent<PerlinNoise>().tileCoord;

        if(direction.x != 0 && direction.y != 0)
        {
            //check the collider hitbox;

            //Diagonal
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * terrainSize, currentTilePosY + direction.y * terrainSize, 0), 0.5f, mask);
            foreach(Collider collider in hitColliders)
                {
                    Debug.Log("Pedro formiga é gay");
                }

            //check the collider hitbox;

            //X axis
            Collider[] hitColliders2 = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * terrainSize, currentTilePosY, 0), 0.5f, mask);
            foreach(Collider collider in hitColliders)
                {
                    Debug.Log("Gui Flowers é gay");
                }

            //check the collider hitbox;

            //Z axis
            Collider[] hitColliders3 = Physics.OverlapSphere(new Vector3(currentTilePosX, currentTilePosY + direction.y * terrainSize, 0), 0.5f, mask);
            foreach(Collider collider in hitColliders)
                {
                    Debug.Log("Severus White White Severus é gay");
                }
        }
        else
        {
            //check the collider hitbox;

            //X or Z axis
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(currentTilePosX + direction.x * terrainSize, currentTilePosY + direction.y * terrainSize, 0), 0.5f, mask);
            foreach(Collider collider in hitColliders)
                {
                    foreach (Transform test in collider.gameObject.transform) {
                        test.gameObject.GetComponent<NPCEnemySM>().Teste();
                    }
                }
        }
    }
}
