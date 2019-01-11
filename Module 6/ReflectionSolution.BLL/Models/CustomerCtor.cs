using ReflectionSolution.DAL.Services;
using ICustomerDAL = ReflectionSolution.DAL.Models.ICustomer;

namespace ReflectionSolution.BLL.Models
{
    public class CustomerCtor
    {
        public CustomerCtor(ICustomerDAL dal, Logger logger)
        {

        }
    }
}
