using CreaDev.Framework.Core;

namespace GpuMiningInsights.Application.Services
{
    public static class GmiApp
    {

        public static void Initialize()
        {
            InitializeServiceLocator();
        }
        private static void InitializeServiceLocator()
        {
         //   ServiceLocator.Add<IEmployeeService>(EmployeeService.Instance);
        }
    }
}