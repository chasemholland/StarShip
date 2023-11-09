using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Level builder
/// </summary>
public class LevelBuilder : FloatEventInvoker
{
    #region Fields

    [SerializeField]
    GameObject prefabAlien1;

    [SerializeField]
    GameObject prefabAlien2;

    [SerializeField]
    GameObject prefabAlien3;

    [SerializeField]
    GameObject prefabShip1;

    // store alien width and height
    float alienWidth;
    float alienHeight;

    // chek for if any aliens remain alive
    GameObject[] aliens1Alive;
    GameObject[] aliens2Alive;
    GameObject[] aliens3Alive;

    // store rounds completed
    int roundValue = 0;
    int totalRounds = 0;

    // round timer
    Timer roundTimer;

    // alien spawn count
    int alienInitialCount = 4;
    int alienCount = 4;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // spawn player ship
        Instantiate(prefabShip1);

        // get alien dimensions
        GameObject alien = Instantiate(prefabAlien1);
        alienWidth = alien.GetComponent<BoxCollider2D>().size.x;
        alienHeight = alien.GetComponent<BoxCollider2D>().size.y;
        Destroy(alien);

        // generate aliens
        GenAliens(roundValue);

        // add as invoker for add money event
        unityEvents.Add(EventName.AddRoundEvent, new AddRoundEvent());
        EventManager.AddInvoker(EventName.AddRoundEvent, this);

        // set round timer
        roundTimer = gameObject.AddComponent<Timer>();
        roundTimer.Duration = 3;
        roundTimer.AddTimerFinishedListener(HandleRoundTimerFinishedEvent);

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // generate infinate rounds of enemies
        aliens1Alive = GameObject.FindGameObjectsWithTag("Alien1");
        aliens2Alive = GameObject.FindGameObjectsWithTag("Alien2");
        aliens3Alive = GameObject.FindGameObjectsWithTag("Alien3");
        if (aliens1Alive.Length == 0 && aliens2Alive.Length == 0 && aliens3Alive.Length == 0)
        {
            if (!roundTimer.Running)
            {
                // run round timer
                roundTimer.Run();
            }
            
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles spawning aliens
    /// </summary>
    /// <param name="round">current round</param>
    private void GenAliens(int round)
    {
        // get max spawn y
        float topSpawn = ScreenUtils.ScreenTop - (alienHeight / 2f) - 0.5f;

        // get min spawn y
        float bottomSpawn = ScreenUtils.ScreenTop - (alienHeight * 3f);

        // get max spawn x
        float leftSpawn = ScreenUtils.ScreenLeft + (alienWidth / 2f);

        // get min spawn y
        float rightSpawn = ScreenUtils.ScreenRight - (alienWidth / 2f);

        // if rounds get past 10, reset round for generator to 0 and increment initial alien count
        switch (round)
        {
            case 1:
                alienCount += 2;
                break;
            case 2:
                alienCount += 2;
                break;
            case 3:
                alienCount += 2;
                break;
            case 4:
                alienCount += 2;
                break;
            case 5:
                alienCount += 2;
                break;
            case 6:
                alienCount += 2;
                break;
            case 7:
                alienCount += 2;
                break;
            case 8:
                alienCount += 2;
                break;
            case 9:
                alienCount += 2;
                break;
            case 10:
                alienCount += 2;
                break;
            case 11:
                alienInitialCount += 2;
                alienCount = alienInitialCount;
                roundValue = 0;
                round = 0;
                break;
            default:
                break;
        }

        // get alien count
        if (round < 3)
        {

            // spawn aliens
            for (int i = 0; i < alienCount; i++)
            {
                Instantiate(prefabAlien1, new Vector3(Random.Range(leftSpawn, rightSpawn), Random.Range(bottomSpawn, topSpawn), 0f), Quaternion.identity);
            }

        }
        else if (round >= 3 && round < 5)
        {

            // spawn aliens
            for (int i = 0; i < alienCount; i++)
            {
                // get chance of alien type
                int alienType = Random.Range(0, 11);

                // spawn alien
                if (alienType >= 9)
                {
                    Instantiate(prefabAlien2, new Vector3(Random.Range(leftSpawn, rightSpawn), Random.Range(bottomSpawn, topSpawn), 0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(prefabAlien1, new Vector3(Random.Range(leftSpawn, rightSpawn), Random.Range(bottomSpawn, topSpawn), 0f), Quaternion.identity);
                } 
            }
        }
        else if (round >= 5 && round < 7)
        {

            // spawn aliens
            for (int i = 0; i < alienCount; i++)
            {
                // get chance of alien type
                int alienType = Random.Range(0, 11);

                // spawn alien
                if (alienType >= 7)
                {
                    Instantiate(prefabAlien2, new Vector3(Random.Range(leftSpawn, rightSpawn), Random.Range(bottomSpawn, topSpawn), 0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(prefabAlien1, new Vector3(Random.Range(leftSpawn, rightSpawn), Random.Range(bottomSpawn, topSpawn), 0f), Quaternion.identity);
                }
            }
        }
        else if (round >= 7 && round < 10)
        {

            // spawn aliens
            for (int i = 0; i < alienCount; i++)
            {
                // get chance of alien type
                int alienType = Random.Range(0, 11);

                // spawn alien
                if (alienType >= 5)
                {
                    Instantiate(prefabAlien2, new Vector3(Random.Range(leftSpawn, rightSpawn), Random.Range(bottomSpawn, topSpawn), 0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(prefabAlien1, new Vector3(Random.Range(leftSpawn, rightSpawn), Random.Range(bottomSpawn, topSpawn), 0f), Quaternion.identity);
                }
            }
        }
        else
        { 
            // spawn mother ship
            Instantiate(prefabAlien3, new Vector3(0f, topSpawn - 1.5f, 0f), Quaternion.identity);

        }
    }

    /// <summary>
    /// Handles round timer finished
    /// </summary>
    private void HandleRoundTimerFinishedEvent()
    {

        // add to rounds completed
        unityEvents[EventName.AddRoundEvent].Invoke(1);
        roundValue += 1;
        totalRounds += 1;

        // stop the round timer
        roundTimer.Stop();

        // generate aliens
        GenAliens(roundValue);
    }

    #endregion
}
