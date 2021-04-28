using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace MISA.Core.Service
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : class
    {
        private IBaseRepository<MISAEntity> _baseRepository;

        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int Delete(Guid entityId)
        {
            return _baseRepository.Delete(entityId);
        }

        public IEnumerable<MISAEntity> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public MISAEntity GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        public int Insert(MISAEntity entity)
        {
            //Validate data
            Validate(entity);
            return _baseRepository.Insert(entity);
        }

        protected virtual void Validate(MISAEntity entity)
        {
        }

        public int Update(MISAEntity entityId)
        {
            return _baseRepository.Update(entityId);
        }
    }
}