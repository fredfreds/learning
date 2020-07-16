using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Jobs;
using Unity.Jobs;

public class GameManagerJS : Manager<GameManagerJS>
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

    private float rotationSpeed = 1;
    private Vector3 rotationDirection;

    // 1
    TransformAccessArray blockAccessArray;
    BlockControllerJob blockJob;
    JobHandle blockJobHandle;

    private void Start()
    {
        // 2
        blockAccessArray = new TransformAccessArray(0);
    }

    private void Update()
    {
        updateFPS();

        // 3
        // Убедиться что все предыдущие работы завершены, перед тем как начинать новые.
        blockJobHandle.Complete();

        if (Input.GetKeyDown("space"))
        {
            if (null != firstBlock)
                Destroy(firstBlock);

            AddMoreBlocks();
        }


        // 4
        blockJob = new BlockControllerJob
        {
            UpperBound = upperBound,
            LowerBound = lowerBound,
            LeftBound = leftBound,
            RightBound = rightBound,
            RotateSpeed = rotationSpeed,
            RotationDirection = rotationDirection,
            DeltaTime = Time.deltaTime
        };

        // 5
        // Placing new job to the job queue
        blockJob.Schedule(blockAccessArray);

        // 6
        // Для выполнения работ
        JobHandle.ScheduleBatchedJobs();
    }

    private void AddMoreBlocks()
    {
        // 7
        blockAccessArray.capacity = blockAccessArray.length + numberToSpawn;

        rotationDirection = rotDirections[Random.Range(0, 4)];

        for (int i = 0; i < numberToSpawn; i++)
        {
            float xPos = Random.Range(leftBound, rightBound);
            float yPos = Random.Range(upperBound, lowerBound);
            float zPos = Random.Range(0, 8);

            Vector3 spawnPos = new Vector3(xPos, yPos, zPos);
            Quaternion spawnRotation = Quaternion.identity;

            GameObject block = Instantiate(PS_Block, spawnPos, spawnRotation);

            // 8
            blockAccessArray.Add(block.transform);
        }

        numBlocks += numberToSpawn;
    }

    private void OnDisable()
    {
        // 9
        // Finish all jobs
        blockJobHandle.Complete();

        // 10
        // Free allocated memory
        blockAccessArray.Dispose();
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
