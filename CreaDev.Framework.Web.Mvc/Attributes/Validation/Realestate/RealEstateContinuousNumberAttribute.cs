using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Web.Mvc.Attributes.Validation.Realestate
{
    public class RealEstateContinuousNumberAttribute : RangeAttribute, IClientValidatable
    {



        public RealEstateContinuousNumberAttribute(RealEstateNumberType realEstateNumberType) : base(GetAttributeValues(realEstateNumberType).Min, GetAttributeValues(realEstateNumberType).Max)
        {

            this.ErrorMessage = ErrorMessages.RealEstateNumberValidationMessage;

        }


        private void SetupMessage(RealEstateNumberType realEstateNumberType)
        {
            throw new NotImplementedException();
        }

        private static RealEstateContinuiousNumberAttributeValues GetAttributeValues(RealEstateNumberType realEstateNumberType)
        {
            //TODO: Create RealEstateRangesLocator and Get it From it to modify values from db and it can be configurable
            Type type;
            double min;
            double max;
            switch (realEstateNumberType)
            {
                case RealEstateNumberType.RoomCount:
                    min = RealEstateRanges.Bedrooms.From;
                    max = RealEstateRanges.Bedrooms.To;
                    break;
                case RealEstateNumberType.UnitCount:
                case RealEstateNumberType.Floors:
                    throw new NotSupportedException($"{realEstateNumberType} is not supported in this Attribute,consider using the RealEstateDiscreteNumberAttribute");


                case RealEstateNumberType.Area:
                    min = RealEstateRanges.RealEstateNumberMin;
                    max = RealEstateRanges.Area.To;
                    break;

                case RealEstateNumberType.Latitude:
                    min = RealEstateRanges.LatMin;
                    max = RealEstateRanges.LatMax;
                    break;
                case RealEstateNumberType.Longtitude:
                    min = RealEstateRanges.LngMin;
                    max = RealEstateRanges.LngMax;
                    break;

                case RealEstateNumberType.Price:
                    type = typeof(double);
                    min = RealEstateRanges.PositiveMustExistMin;
                    max = RealEstateRanges.PriceMax;
                    break;


                default:

                    min = RealEstateRanges.PositiveMustExistMin;
                    max = RealEstateRanges.RealEstateNumberDefaultMax;
                    break;

            }
            RealEstateContinuiousNumberAttributeValues realEstateNumberAttributeValues = new RealEstateContinuiousNumberAttributeValues()
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