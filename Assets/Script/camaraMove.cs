using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class camaraMove : MonoBehaviour
{

    GameObject player;
  
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
