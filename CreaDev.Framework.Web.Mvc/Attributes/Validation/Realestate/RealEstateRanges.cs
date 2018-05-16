using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Web.Mvc.Attributes.Validation.Realestate
{

    public static class RealEstateRanges
    {
        
        internal const int RealEstateNumberDefaultMax = 1000000000;
        internal const int RealEstateNumberMin = 0;
        internal const int PositiveMustExistMin = 1;
        public static readonly RangeModel<double> Area = new RangeModel<double>(RealEstateNumberMin, 100000000);
        public static readonly RangeModel<double> Bedrooms = new RangeModel<double>(RealEstateNumberMin, 30);
        public static readonly RangeModel<double> Bathrooms = new RangeModel<double>(RealEstateNumberMin, 30);

        internal const double LatMin = -85;
        internal const double LatMax = 85;
        internal const double LngMin = -180;
        internal const double LngMax = 180;

        internal const double PriceMax = 1000000000000;

        internal const int FloorsMax = 1000;
        internal const int RoomCountMax = 1000;
    }
}
   
