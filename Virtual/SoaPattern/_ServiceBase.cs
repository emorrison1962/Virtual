using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Services
{
    abstract public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected IRepositoryBase<T> Repository { get; set; }

        public ServiceBase(IRepositoryBase<T> repository)
        {
            this.Repository = repository;
        }

        virtual public void Delete(int id)
        {
            try
            {
                this.Repository.Delete(id);
                this.Repository.Commit();
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168
        }

        virtual public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        virtual public IEnumerable<T> GetAll()
        {
            var result = new List<T>();
            try
            {
                result = this.Repository.GetAll().ToList();
                result.Sort();

            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168
            return result;
        }

        virtual public IEnumerable<T> GetAll(object filter)
        {
            throw new NotImplementedException();
        }

        virtual public T GetById(int id)
        {
            T result = null;
            try
            {
                result = this.Repository.GetById(id);
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168
            return result;
        }

        virtual public T GetFullObject(object id)
        {
            T result = null;
            try
            {
                result = this.Repository.GetFullObject(id);
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168
            return result;
        }

        virtual public IEnumerable<T> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
        {
            throw new NotImplementedException();
        }

        virtual public T Insert(T entity)
        {
            if (null == entity)
                throw new ArgumentNullException("Parameter {{entity}} is null.");

            try
            {
                this.Repository.Insert(entity);
                this.Repository.Commit();
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168
            return entity;
        }

        virtual public T Update(T entity)
        {
            try
            {
                this.Repository.Update(entity);
                this.Repository.Commit();

            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168

            return entity;
        }

        public T Detach(T entity)
        {
            T result = null;
            try
            {
                result = this.Repository.Detach(entity);

            }
#pragma warning disable 168
            catch (Exception ex)
            {
                throw;
            }
#pragma warning restore 168

            return result;
        }
    }//class

}//ns



