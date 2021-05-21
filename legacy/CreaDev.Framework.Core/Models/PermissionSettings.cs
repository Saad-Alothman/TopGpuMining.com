using System.Collections.Generic;
using System.Linq;

namespace CreaDev.Framework.Core.Models
{
    public class PermissionSettings
    {
        public List<CrudOperationSettings> CrudOperationSettingsList { get; set; }

        public CrudOperationSettings ReadSettings
            => CrudOperationSettingsList.FirstOrDefault(s => s.CrudOperationType == CrudOperationType.Read);

        public CrudOperationSettings SearchSettings
            => CrudOperationSettingsList.FirstOrDefault(s => s.CrudOperationType == CrudOperationType.Search);

        public CrudOperationSettings AddSettings
            => CrudOperationSettingsList.FirstOrDefault(s => s.CrudOperationType == CrudOperationType.Add);

        public CrudOperationSettings ModifySettings
            => CrudOperationSettingsList.FirstOrDefault(s => s.CrudOperationType == CrudOperationType.Modify);
        
        public static PermissionSettings SameCompanyModify
        {
            get
            {
                return new PermissionSettings()
                {
                    CrudOperationSettingsList = new List<CrudOperationSettings>()
                    {
                        new CrudOperationSettings(CrudOperationType.Add, false, false),
                        new CrudOperationSettings(CrudOperationType.Modify, true, true),
                        new CrudOperationSettings(CrudOperationType.Read, false, false),
                        new CrudOperationSettings(CrudOperationType.Search, false, false),
                        //Command : like Command /Query (create update,delete)
                        new CrudOperationSettings(CrudOperationType.IncludeInAnotherCommand, false,true),
                    }
                };
            }
        }
        public static PermissionSettings CompanyRoleCanModifyAndInclude
        {
            get
            {
                return new PermissionSettings()
                {
                    CrudOperationSettingsList = new List<CrudOperationSettings>()
                    {
                        new CrudOperationSettings(CrudOperationType.Add, false, false),
                        new CrudOperationSettings(CrudOperationType.Modify, false,false,true),
                        new CrudOperationSettings(CrudOperationType.Read, false, false),
                        new CrudOperationSettings(CrudOperationType.Search, false, false),
                        //Command : like Command /Query (create update,delete)
                        new CrudOperationSettings(CrudOperationType.IncludeInAnotherCommand, false,false,true),
                    }
                };
            }
        }


        public static PermissionSettings Default => SameCompanyModify;
        public PermissionSettings()
        {
            this.CrudOperationSettingsList = new List<CrudOperationSettings>();
        }
    }
}