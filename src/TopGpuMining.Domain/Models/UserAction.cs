using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Core.Entities;

namespace TopGpuMining.Domain.Models
{
    public enum UserActionType
    {
        Unknow =0,
        BuyNowClick=1,
        Visit = 1,
    }
    public class UserAction: BaseEntity
    {
        public string IpAddress { get; set; }
        public UserActionType UserActionType { get; set; }
        public string Data { get; set; }
        public string ItemId { get; set; }
        
    }
}
