using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity> where MISAEntity: class
    {
        IEnumerable<MISAEntity> GetAll();

        MISAEntity GetById(Guid entityId);

        int Insert(MISAEntity entity);

        int Update(MISAEntity entityId);

        int Delete(Guid entityId);
    }
}
