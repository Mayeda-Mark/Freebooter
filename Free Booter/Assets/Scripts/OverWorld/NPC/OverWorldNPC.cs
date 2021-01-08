using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldNPC : MonoBehaviour
{
    [SerializeField] bool isAttacker = false;
    [SerializeField] GameObject projectile;
    OverworldNPCController controller;
    // Start is called before the first frame update
    public bool vulnerable = false;
    float spawnInvincibility = 2f;
    private void Awake()
    {
        controller = GetComponent<OverworldNPCController>();
    }
    public IEnumerator Spawn() {
        vulnerable = false;
        yield return new WaitForSeconds(spawnInvincibility);
        vulnerable = true;
    }
    /*public void Kill() {
        if(vulnerable) {
            controller.Kill();
            //Destroy(gameObject);
            this.gameObject.SetActive(false);
        }
    }*/
    public bool IsVulnerable() { return vulnerable; }
    public bool IsNPCAttacker()   { return isAttacker; }
}
