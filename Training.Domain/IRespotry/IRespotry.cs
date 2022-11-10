namespace Training.EFCore
{
    public interface IRespotry<T> where T : class
    {
        void Add(T obj);
        void Del(T obj);
        T Find(object id);
        IQueryable<T> GetList();
        int Save(T obj);
        void Upt(T obj);
    }
}