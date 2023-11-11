using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;

/// RigidBodyData는 Rigidbody2D Component에 대한 정보를 담고 있습니다.
public class RigidbodyData : DefaultData
{
    private Rigidbody2D rb;
    public RigidbodyType2D rigidbodyType { get; private set; }

    private ValueFlag<float2> velocity;
    private ValueFlag<float2> force;
    private ValueFlag<float> angularVelocity;
    private ValueFlag<float> mass;
    private ValueFlag<float> friction;
    private ValueFlag<float> bounciness;


    public override void OnInit()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb.sharedMaterial == null) 
        { 
            rb.sharedMaterial = new PhysicsMaterial2D(hierarchyName.ToString()); 
            friction = new (rb.sharedMaterial.friction);
            bounciness = new (rb.sharedMaterial.bounciness);
        }
        else
        {
            var original = rb.sharedMaterial;
            var clone = new PhysicsMaterial2D(hierarchyName.ToString());
            clone.friction = original.friction;
            clone.bounciness = original.bounciness;
            rb.sharedMaterial = clone;
            friction = new (clone.friction);
            bounciness = new (clone.bounciness);
        }
        rigidbodyType = rb.bodyType;
        velocity = new (rb.velocity);
        angularVelocity = new (rb.angularVelocity);
        mass = new (rb.mass);
        force = new (0);
        ComponentManager.AddComponent(hierarchyName, this);
    }



    /// Velocity를 반환합니다. 
    public float2 GetVelocity()
    {
        return velocity.value;
    }

    /// Velocity를 변경합니다.
    public void SetVelocity(in float2 value, bool notifyChanged = true)
    {
        
        if(notifyChanged)
        {
            velocity.value = value;
        }
        else
        {
            bool changed_temp = velocity.isChanged;
            velocity.value = value;
            velocity.isChanged = changed_temp;
        }
    }



    /// AngularVelocity를 반환합니다.
    public float GetAngularVelocity()
    {
        return angularVelocity.value;
    }

    /// AngularVelocity를 변경합니다.
    public void SetAngularVelocity(float value, bool notifyChanged = true)
    {
        if(notifyChanged)
        {
            angularVelocity.value = value;
        }
        else
        {
            bool changed_temp = angularVelocity.isChanged;
            angularVelocity.value = value;
            angularVelocity.isChanged = changed_temp;
        }
    }


    /// Mass를 반환합니다.
    public float GetMass()
    {
        return mass.value;
    }

    /// Mass를 변경합니다.
    public void SetMass(float value)
    {
        mass.value = value;
    }


    /// Force를 반환합니다.
    public float2 GetForce()
    {
        return force.value;
    }

    /// Force를 변경합니다.
    public void SetForce(in float2 value)
    {
        force.value = value;
    }


    public bool IsChanged()
    {
        return velocity.isChanged || angularVelocity.isChanged || mass.isChanged || !force.value.Equals(float2(0));
    }


    public void Sync()
    {
        if(!force.value.Equals(float2(0)))
        { 
            rb.AddForce(force.value);
            force.value = 0;
        }

        if(velocity.isChanged) 
        {
            rb.velocity = velocity.value;
            velocity.isChanged = false; 
        }

        if(angularVelocity.isChanged) 
        { 
            rb.angularVelocity = angularVelocity.value;
            angularVelocity.isChanged = false; 
        }

        if(mass.isChanged) 
        { 
            rb.mass = mass.value;
            mass.isChanged = false; 
        }
    }


    public void ReadRigidbody2D(Rigidbody2D rb)
    {
        if(!velocity.isChanged) { SetVelocity(rb.velocity, false);}
        if(!angularVelocity.isChanged) { SetAngularVelocity(rb.angularVelocity, false);}
    }
}

