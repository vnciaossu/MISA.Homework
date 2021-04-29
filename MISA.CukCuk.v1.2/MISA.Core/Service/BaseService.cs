using MISA.Core.AttributeCustom;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
            CustomValidate(entity);
            return _baseRepository.Insert(entity);
        }

        private void Validate(MISAEntity entity)
        {
            //Lấy ra tất cả các property của class
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var property in properties)
            {
                var requiredProperties = property.GetCustomAttributes(typeof(MISARequired), true);
                var maxLengthProperties = property.GetCustomAttributes(typeof(MISAMaxLength), true);

                //Check thông tin khách hàng
                if (requiredProperties.Length > 0)
                {
                    //Lấy giá trị:
                    var propertyValue = property.GetValue(entity);

                    //Kiểm tra giá trị:
                    if (string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        var msgError = (requiredProperties[0] as MISARequired).MsgError;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            var msgDefault = Properties.Resources.Msg_Error_Required;
                            var sb = new StringBuilder();
                            var name = (requiredProperties[0] as MISARequired).Name.Length > 0 ? (requiredProperties[0] as MISARequired).Name : property.Name;
                            sb.AppendFormat(msgDefault, name);
                            msgError = sb.ToString();
                        }
                        
                        throw new CustomerException(msgError);
                    }
                }

                //Check maxlength
                if(maxLengthProperties.Length > 0)
                {
                    //Get Value
                    var propertyValue = property.GetValue(entity);
                    var maxLength = (maxLengthProperties[0] as MISAMaxLength).MaxLength;
                    //Check Value 
                    if (propertyValue.ToString().Length > maxLength) 
                    {
                        var msgError = (maxLengthProperties[0] as MISAMaxLength).MsgError;
                        throw new CustomerException(msgError);
                    }
                }
            }
            CustomValidate(entity);
        }

        protected virtual void CustomValidate(MISAEntity entity)
        {
        }

        public int Update(MISAEntity entityId)
        {
            return _baseRepository.Update(entityId);
        }
    }
}