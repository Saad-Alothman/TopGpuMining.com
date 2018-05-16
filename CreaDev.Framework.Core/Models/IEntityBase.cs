namespace CreaDev.Framework.Core.Models
{
    public interface IEntityBaseCommon
{
        bool IsActive { get; set; }

        bool IsUserCanModify();
        bool IsUserCanView();
        
        void Validate();
        void ValidateAdd();
        void ValidateEdit();
        void ValidateUpdate();
    }
    
    public interface IEntityBase: IEntityBaseCommon
    {
        int Id { get; set; }
       
    }
    public interface IEntityBaseStringId: IEntityBaseCommon
    {
        string Id { get; set; }
      
    }
}