using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using CreaDev.Framework.Core.Resources;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace CreaDev.Framework.Core.Models
{
    public abstract class AuditableEntity<TUser> : EntityBase, IAuditable<TUser>
    {
        [NotMapped]
        public AuditData AuditData
        {
            get
            {
                AuditData auditData = new AuditData()
                {
                    CreatedByUserId = CreatedByUserId,
                    CreatedDate = CreatedDate,
                    ModifiedByUserId = ModifiedByUserId,
                    ModifiedDate = ModifiedDate
                };
                return auditData;
            }
        }
        [Display(ResourceType = typeof(Common), Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedByUserId { get; set; }
        public string ModifiedByUserId { get; set; }
        [JsonIgnore]
        public TUser CreatedByUser { get; set; }
        [JsonIgnore]
        public TUser ModifiedByUser { get; set; }
        [NotMapped]
        public virtual bool IsOwner
        {
            get
            {
                if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    return false;
                string loggedInUserId = Thread.CurrentPrincipal.Identity.GetUserId();
                bool isCreatedByUser = CreatedByUserId == loggedInUserId;
                return isCreatedByUser;
            }
        }

        public void InsertAudit()
        {
            //TODO: this is for testing Only
            if (string.IsNullOrWhiteSpace(CreatedByUserId))
            {
                this.CreatedByUserId = Thread.CurrentPrincipal.Identity.GetUserId();
            }
            if (this.CreatedDate == null)
            {
            this.CreatedDate = DateTime.Now;
            }

        }

        public void UpdateAudit()
        {
            this.ModifiedDate = DateTime.Now;
            this.ModifiedByUserId = Thread.CurrentPrincipal.Identity.GetUserId();
        }


    }
}
