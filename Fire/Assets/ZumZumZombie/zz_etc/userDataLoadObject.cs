using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RoadUserData", menuName = "Examples/RoadUserData")]
public class userDataLoadObject : ScriptableObject
{
    // Start is called before the first frame update
    public UserData userdata = new UserData();
}
