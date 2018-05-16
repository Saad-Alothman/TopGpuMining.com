using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Persistance;
using User = GpuMiningInsights.Domain.Models.User;

namespace REME.Persistance
{
    public class GmiUnitOfWork:UnitOfWork<GmiContext,User>
    {
        private AccountRepository _accountRepositry;

        public AccountRepository AccountRepository
        {
            get
            {
                if (_accountRepositry == null)
                    _accountRepositry = new AccountRepository(Context);

                return _accountRepositry;
            }
        }
    }
}
