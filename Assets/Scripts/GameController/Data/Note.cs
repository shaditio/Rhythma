[System.Serializable]
public class Note
{
    public Coordinate coordinate;
    public float beat;

    public override string ToString(){
        return $"Coordinate: {coordinate.ToString()} Beat: {beat.ToString()}";
    }
}