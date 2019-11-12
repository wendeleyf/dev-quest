using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFluxProcess : MonoBehaviour {

    private Inventory inventory;
    public Sprite sprite;
    
    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (inventory.isFull[0]) {
            if(inventory.fluxProcess == true) {
                this.GetComponent<SpriteRenderer>().sprite = sprite;
                print(inventory.slots[0]);
                inventory.isFull[0] = false;
            }
        }
    }


}
