using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupProcess : MonoBehaviour {

    private Inventory inventory;
    public GameObject item;
    private AudioSource source;
    

    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            source.Play();
            for (int i = 0; i < inventory.slots.Length; i++) {
                if(inventory.isFull[i] == false) {
                    inventory.isFull[i] = true;
                    inventory.fluxProcess = true;
                    Instantiate(item, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }

            }
        }
    }
}


