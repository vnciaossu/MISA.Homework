using System;
using System.Collections.Generic;

namespace MISA.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity> where MISAEntity: class
    {
        IEnumerable<MISAEntity> GetAll();

        MISAEntity GetById(Guid entityId);

        int Insert(MISAEntity entity);

        int Update(MISAEntity entityId);

        int Delete(Guid entityId);
    }
}