using System;

namespace CreaDev.Framework.Core.Models
{
    public interface IUpdatable<T>
    {
        void Update(T modelWithNewdata);
    }
    public interface IAuditableBase
    {
        AuditData AuditData { get; }
        
        string CreatedByUserId { get; set; }
        DateTime? CreatedDate { get; set; }
        bool IsOwner { get; }
        
        string ModifiedByUserId { get; set; }
        DateTime? ModifiedDate { get; set; }

        void InsertAudit();
        void UpdateAudit();
    }
    public interface IAuditableCommon<TUser>: IAuditableBase
    {
        TUser CreatedByUser { get; set; }
        TUser ModifiedByUser { get; set; }
    }
    public interface IAuditableStringId<TUser> :IAuditableCommon<TUser>,IEntityBaseStringId
    {
        
    }

    public interface IAuditable<TUser> : IAuditableCommon<TUser>, IEntityBase
    {
        
    }
}