namespace MadCat
{
    public interface IGameObject
    {
        void Update(float deltaTime);
        void Collide(IGameObject other);
    }
}
