namespace NutEngine.Physics
{
    public class MassData
    {
        public float MassInv { get; set; }
        public float Mass {
            get => MassInv == 0 ? 0 : 1 / MassInv;
            set => MassInv = (value == 0 ? 0 : 1 / value);
        }
    }

    // Don't forget about inertia
}