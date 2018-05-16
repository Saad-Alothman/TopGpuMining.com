namespace CreaDev.Framework.Core.Models
{
    public enum CrudOperationType
    {
        Add = 1,
        //Update,Delete
        Modify = 2,
        Read = 3,
        Search = 4,

        IncludeInAnotherCommand=5,
        Delete = 6
    }
}