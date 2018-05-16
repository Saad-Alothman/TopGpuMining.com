using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Web.Mvc.Attributes.Validation.Realestate
{
    public class RealEstateDiscreteNumberAttribute : RangeAttribute, IClientValidatable
    {



        public RealEstateDiscreteNumberAttribute(RealEstateNumberType realEstateNumberType) : base(GetAttributeValues(realEstateNumberType).Min, GetAttributeValues(realEstateNumberType).Max)
        {

            this.ErrorMessage = ErrorMessages.RealEstateNumberValidationMessage;

        }
        private RealEstateDiscreteNumberAttribute(RealEstateNumberType realEstateNumberType, Type type, string min, string max) : base(type, min, max)
        {
            SetupMessage(realEstateNumberType);
        }

        private void SetupMessage(RealEstateNumberType realEstateNumberType)
        {
            throw new NotImplementedException();
        }

        private static RealEstateDiscreteNumberAttributeValues GetAttributeValues(RealEstateNumberType realEstateNumberType)
        {
            Type type;
            int min;
            int max;
            switch (realEstateNumberType)
            {
                case RealEstateNumberType.Price:
                case RealEstateNumberType.Latitude:
                case RealEstateNumberType.Longtitude:
                case RealEstateNumberType.Area:
                    throw new NotSupportedException($"{realEstateNumberType} is not supported in this Attribute,consider using the RealEstateContinousNumberAttribute");


                case RealEstateNumberType.Floors:
                    type = typeof(int);
                    min = RealEstateRanges.PositiveMustExistMin;
                    max = RealEstateRanges.FloorsMax;
                    break;

                case RealEstateNumberType.RoomCount:
                    type = typeof(double);
                    min = RealEstateRanges.PositiveMustExistMin;
                    max = RealEstateRanges.RoomCountMax;
                    break;
                case RealEstateNumberType.UnitCount:
                    type = typeof(double);
                    min = RealEstateRanges.PositiveMustExistMin;
                    max = RealEstateRanges.RoomCountMax;
                    break;

                default:
                    type = typeof(int);
                    min = RealEstateRanges.PositiveMustExistMin;
                    max = RealEstateRanges.RealEstateNumberDefaultMax;
                    break;

            }
            RealEstateDiscreteNumberAttributeValues realEstateNumberAttributeValues = new RealEstateDiscreteNumberAttributeValues()
            {

                Min = min,
                Max = max
            };
            return realEstateNumberAttributeValues;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRangeRule(ErrorMessage, this.Minimum, this.Maximum);
            return new List<ModelClientValidationRangeRule> { modelClientValidationRule };
        }
    }
}