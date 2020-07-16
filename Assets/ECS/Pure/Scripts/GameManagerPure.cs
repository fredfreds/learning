
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using Unity.Transforms;


public class GameManagerPure : Manager<GameManagerPure>
{
    #region Common Code

    [Header("Screen Bounds")]
    public float upperBound = -4.5f;
    public float lowerBound = 3.5f;
    public float leftBound = -9.17f;
    public float rightBound = 9.0f;

    [Header("Block Prefab")]
    public GameObject PS_Block;
    public GameObject firstBlock;

    [Header("Block Spawning")]
    public int numberToSpawn;

    [Header("Stats")]
    public Text blockCountDisplay;
    public Text fpsDisplay;

    public readonly Vector3[] rotDirections = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    private int numBlocks;

    private int frameCount = 0;
    private float dt = 0.0f;
    private float fps = 0.0f;
    private readonly float updateRate = 4.0f;  // 4 updates per sec.

    #endregion

    private EntityManager manager;
    private float rotationSpeed = 1;

    private void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void Update()
    {
        updateFPS();

        if (Input.GetKeyDown("space"))
        {
            if (null != firstBlock)
                Destroy(firstBlock);

            AddMoreBlocks();
        }
    }

    private void AddMoreBlocks()
    {
        NativeArray<Entity> entities = new NativeArray<Entity>(numberToSpawn, Allocator.Temp);
        manager.Instantiate(PS_Block, entities);
        for (int i = 0; i < numberToSpawn; i++)
        {
            float yPos = Random.Range(upperBound, lowerBound);
            float xPos = Random.Range(leftBound, rightBound);
            float zPos = Random.Range(0, 8);

            Translation spawnPos = new Translation { Value = new Vector3(xPos, yPos, zPos) };
            Rotation spawnRot = new Rotation { Value = Quaternion.identity };

            BlockControllerData blockData = new BlockControllerData
            {
                RotateSpeed = rotationSpeed,
                RotationDirection = rotDirections[Random.Range(0, 4)]
            };

            manager.SetComponentData(entities[i], spawnPos);
            manager.SetComponentData(entities[i], spawnRot);
            manager.SetComponentData(entities[i], blockData);
        }

        entities.Dispose();
        numBlocks += numberToSpawn;
    }

    void updateFPS()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0f / updateRate;

            fpsDisplay.text = string.Format("FPS: {0}", fps);
            blockCountDisplay.text = string.Format("Blocks: {0}", numBlocks);
        }
    }
}
