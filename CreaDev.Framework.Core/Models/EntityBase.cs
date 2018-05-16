using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Core.Models
{

    public abstract class EntityBase: IEntityBase
    {
        [Display(ResourceType = typeof(Common), Name = "Id")]
        public virtual int Id { get; set; }

        public bool IsActive { get; set; }

        public virtual void ValidateAdd()
        {
            
        }
        public virtual void Validate()
        {

        }
        public virtual void ValidateUpdate()
        {

        }
        public virtual void ValidateEdit()
        {

        }

        public abstract void Update(object objectWithNewData);
        
        

        
        
        

        protected  bool? _userCanModify;

        public virtual bool IsUserCanModify()
        {
            return true;
        }
        public virtual bool IsUserCanModify(CrudOperationType operationType)
        {
            return true;
        }
        public virtual bool IsUserCanView()
        {
            return true;

        }


        protected bool? _userCanView;
        
        

    }
}
