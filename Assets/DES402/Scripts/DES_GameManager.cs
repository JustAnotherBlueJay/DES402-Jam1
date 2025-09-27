using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DES_GameManager : MonoBehaviour
{
    //possible states a player can be in
    public enum PlayerState
    {
        Inactive,
        Active

    }

    //dictionary of player id and current state
    private Dictionary<int, PlayerState> playerStates = new Dictionary<int, PlayerState>
    {
        {0, PlayerState.Inactive},
        {1, PlayerState.Inactive},
        {2, PlayerState.Inactive},
        {3, PlayerState.Inactive}

    };

    //list of each isntance manager
    private List<InstanceManager> instanceManagers = new List<InstanceManager>();

    private void Awake()
    {
        //create 4 instances of the game
        for (int i = 0; i < playerStates.Count; i++)
        {
            CreateGameInstance(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //update player states to the correct state
        UpdatePlayerStates();
    }

    //checks for inactive players becoming active and active players becoming innactive
    private void UpdatePlayerStates()
    {
        for (int i = 0; i < playerStates.Count; i++)
        {
            if (playerStates[i] == PlayerState.Active)
            {
                //TODO
                //where the check for active players becoming inactive will go
                continue;   
            }

            //check for inactive players becoming active
            else if (playerStates[i] == PlayerState.Inactive && DidPlayMakeAnInput(i))
            {
                playerStates[i] = PlayerState.Active;
                //CreateGameInstance(i);
            }
        }


    }

    //returns if player i made an input this frame
    private bool DidPlayMakeAnInput(int i)
    {
        foreach (WhaleButton whaleButton in System.Enum.GetValues(typeof(WhaleButton)))
        {
            if (WhalesongInput.GetButton(i, whaleButton))
            {
                return true;
            }

        }

        return false;
    }

    //creates an instance of the game
    private void CreateGameInstance(int instanceNumber)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameInstance", LoadSceneMode.Additive);

        //when the gmae is finished loading RegisterInstance is called
        asyncLoad.completed += (AsyncOperation op) => { RegisterInstance(instanceNumber); };
    }

    //register the new instance manager with the necessary information to set itself up
    void RegisterInstance(int instanceNumber)
    {

        //find all instance managers
        GameObject[] managers = GameObject.FindGameObjectsWithTag("InstanceManager");

        //iterate over the array until finding the unregistered one
        foreach (GameObject manager in managers)
        {
            if (!manager.GetComponent<InstanceManager>().IsRegistered)
            {
                //add the unregistered instance to instance managers and register it
                instanceManagers.Add(manager.GetComponent<InstanceManager>());
                manager.GetComponent<InstanceManager>().RegisterSelf(instanceNumber);
            }
        }
    }

}

