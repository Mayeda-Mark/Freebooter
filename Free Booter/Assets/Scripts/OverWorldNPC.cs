using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldNPC : MonoBehaviour
{
    [SerializeField] bool isAttacker = false;
    [SerializeField] GameObject projectile;
    // Start is called before the first frame update
    bool vulnerable = false;
    float spawnInvincibility = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Spawn() {
        yield return new WaitForSeconds(spawnInvincibility);
        vulnerable = true;
    }
    public void Kill() {
        if(vulnerable) {
            Destroy(gameObject);
        }
    }
}
