using System.Collections.Generic;

namespace CreaDev.Framework.Core.Models
{
    public class CrudOperationSettings
    {
        public CrudOperationSettings()
        {
            this.Roles = new List<string>();
        }
        public CrudOperationSettings(CrudOperationType crudOperationType, bool isOwnerOnly, bool isSameCompany,bool isMustHaveCompanyRole, List<string> roles = null) : this(crudOperationType,isOwnerOnly,isSameCompany,roles)
        {
            this.IsMustHaveCompanyRole=isMustHaveCompanyRole;
            Validate();
        }
        public CrudOperationSettings(CrudOperationType crudOperationType, bool isOwnerOnly, bool isSameCompany, List<string> roles = null) : this()
        {
            this.IsOwnerOnly = isOwnerOnly;
            this.IsSameCompany = isSameCompany;
            
            if (roles != null)
            {
                this.Roles = roles;
            }
            this.CrudOperationType = crudOperationType;
            Validate();
        }

        public CrudOperationType CrudOperationType { get; set; }

        private void Validate()
        {

        }

        public bool IsRolesMustMatchAll { get; set; }
        public bool IsOwnerOnly { get; set; }
        public bool IsSameCompany { get; set; }
        public bool IsMustHaveCompanyRole { get; set; }
        public List<string> Roles { get; set; }
    }
}