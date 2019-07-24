using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool IsPassed = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")){
            IsPassed = true;

            // 一旦、地面に埋める
//            var pos = transform.position;
//            pos.y = -100;
//            transform.position = pos;
        }
    }

    public void Reset()
    {
        IsPassed = false;

        // 一旦、地面に埋める
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;
    }

}
