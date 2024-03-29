﻿namespace Training.EFCore
{
    public interface IRespotry<T> where T : class
    {
        void Add(T obj);
        void Del(T obj);
        T Find(object id);
        IQueryable<T> GetList();
        int Save();
        void Upt(T obj);
    }
}