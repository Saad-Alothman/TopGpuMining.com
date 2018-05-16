using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Core.Resources;

namespace GpuMiningInsights.Domain.Models
{
    public class User : CreaDevUser
    {
        public override string UserName
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }
        public int? CompanyId { get; set; }
        
        public int? CompanyBranchId { get; set; }

        public AuditData AuditData { get; }
        public string CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsOwner { get; }
        public string ModifiedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public LocalizableTextRequired FirstName { get; set; }

        public LocalizableText MiddleName { get; set; }

        public LocalizableTextRequired LastName { get; set; }


        [Display(Name = nameof(Common.Birthdate), ResourceType = typeof(Common))]
        public DateTime? Birthdate { get; set; }

        [NotMapped]
        public LocalizableText FullName
        {
            get
            {

                var arabic = $"{this.FirstName?.Arabic} {this.MiddleName?.Arabic} {this.LastName?.Arabic}";
                var english = $"{this.FirstName?.English} {this.MiddleName?.English} {this.LastName?.English}";
                return new LocalizableText(arabic, english);
            }
        }


        public string About { get; set; }


        public void InsertAudit()
        {
            throw new NotImplementedException();
        }

        public void UpdateAudit()
        {
            throw new NotImplementedException();
        }


        public User()
        {
            this.CreatedDate = DateTime.Now;
        }

        public bool IsActive { get; set; }

        
    
        public void Update(User user)
        {
            this.Email = user.UserName;
            this.UserName = user.UserName;
            this.CompanyId = user.CompanyId;
            this.CompanyBranchId = user.CompanyBranchId;

        }


        
    }
}