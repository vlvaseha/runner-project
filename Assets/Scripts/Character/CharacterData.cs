namespace Character
{
    public class CharacterData 
    {
        public float ForwardMoveSpeed { get; }
        public float SideMoveSpeed { get; }
        public float YRotationSpeed { get; }
        public float YRotationMaxAngle { get; }
        public float FlyingHeight { get; }
        public float ZRotationSpeed { get; }
        public float ZRotationMaxAngle { get; }
        public float FlyingForwardMoveSpeed { get; }

        public CharacterData()
        {
            ForwardMoveSpeed = 8f;
            SideMoveSpeed = 18f;
            YRotationSpeed = 90;
            YRotationMaxAngle = 12;
            FlyingHeight = 1f;
            ZRotationSpeed = 18f;
            ZRotationMaxAngle = 12;
            FlyingForwardMoveSpeed = 13f;
        }
    }
}
