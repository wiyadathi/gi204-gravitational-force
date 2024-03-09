using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private const float G = 6.674f; //Gravitational constant
    
    //create a list to store other planets to be attracted
    public static List<Attractor> Attractors;

    private void FixedUpdate()
    {
        //loop through all elements in the List
        foreach (var attractor in Attractors)
        {
            //ไม่ดูดตัวเอง
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }

    private void OnEnable()
    {
        //initiallize the Attractors List
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        
        Attractors.Add(this);
    }

    void Attract(Attractor other)
    {
        // F = G*(m1*m2)/d^2)
        //1. get the distance between 2 objects
        //1.1 get RB of the other object to attract 
        Rigidbody rbOther = other.rb;
        
        //1.2 get the direction between the Planet and the other 
        Vector3 direction = rb.position - rbOther.position;
        
        //1.3 get only the distance (the length) of the direction vector
        float distance = direction.magnitude;

        //2. apply the gravitation Force formular: F = G*(m1*m2)/d^2)
        //2.1 get force magnitude
        float forceMagnitude = G *(rb.mass * rbOther.mass) / Mathf.Pow(distance, 2);
        
        //2.2 create the final force vector
        //เอาทิศของ object 2 ตัวที่ดึงดูดกัน คูณ กับ ขนาด(strength)ของ Force ที่คำนวณจากสูตรของนิวตัน
        Vector3 force = direction.normalized * forceMagnitude;
        
        //3. Apply the final force to the other object
        rbOther.AddForce(force);
        // ณ จุดนี้ยังไม่มีการ call *Attract method จึงยังไม่เกิดผลอะไร  
        // ต้องไป call *Attract method ใน FixedUpdate
    }
}
