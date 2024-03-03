namespace Character
{
    public class CharacterDataModel 
    {
        public float ForwardMoveSpeed { get; }
        public float SideMoveSpeed { get; }
        public float RotationSpeed { get; }
        public float RotationMaxAngle { get; }

        public CharacterDataModel()
        {
            ForwardMoveSpeed = 5f;
            SideMoveSpeed = 5f;
            RotationSpeed = 15f;
            RotationMaxAngle = 23f;
        }
    }
}
