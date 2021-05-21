﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Resources;

namespace TopGpuMining.Domain.Models
{
    //Todo: Rename To ChipsetAlgorithmHashrate
    public class Hashrate : BaseEntity
    {
        [ForeignKey("AlogrthimId")]
        public Algorithm Algorithm { get; set; }
        public string AlogrthimId { get; set; }
      
        public Model Model { get; set; }
        public string ModelId { get; set; }


        [Display(Name = nameof(CommonText.HashrateValue), ResourceType = typeof(CommonText))]
        public string HashrateValueMhz { get; set; }

        [Display(Name = nameof(CommonText.Description), ResourceType = typeof(CommonText))]

        public string Description { get; set; }

        


        public void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Hashrate;
            this.HashrateValueMhz = updateData.HashrateValueMhz;
            this.Description = updateData.Description;
            this.AlogrthimId = updateData.AlogrthimId;
            this.ModelId = updateData.ModelId;

        }


    }
}
