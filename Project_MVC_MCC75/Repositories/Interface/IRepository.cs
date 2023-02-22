using Project_MVC_MCC75.ViewModels;

namespace Project_MVC_MCC75.Repositories.Interface;

interface IRepository<Key, Entity> where Entity : class
{
    List<Entity> GetAll();
    Entity GetById(Key key);
    int Insert(Entity entity);
    int Update(Entity entity);
    int Delete(Key key);
}
