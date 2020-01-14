using UnityEngine;

[System.Serializable]
public class Coordinate
{
    public float x;
    public float y;
    public float z;

    public override string ToString(){
        return $"x = {this.x}, y = {this.y}, z = {this.z}";
    }

    public Vector3 toVector3(){
        return new Vector3(x,y,z);
    }

    // Will be used for the implementation of variable speed
    public Vector3 toVector3(float offset){
        return new Vector3(x,y,z+offset);
    }
}