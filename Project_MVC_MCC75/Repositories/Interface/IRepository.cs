using Project_MVC_MCC75.ViewModels;

namespace Project_MVC_MCC75.Repositories.Interface;

interface IRepository<Key, Entity> where Entity : class
{ 
    int Insert(Entity entity);//nanya
    List<Entity> GetAll();
    Entity GetById(Key key);//nanya
    int Update(Entity entity);//nanya
    int Delete(Key key);//nanya
}
