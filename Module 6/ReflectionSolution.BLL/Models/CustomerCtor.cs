using CustomReflectionLibrary.Attributes;
using ReflectionSolution.DAL.Services;
using ICustomerDAL = ReflectionSolution.DAL.Models.ICustomer;
using CustomerDAL = ReflectionSolution.DAL.Models.Customer;

namespace ReflectionSolution.BLL.Models
{
    [ImportConstructor]
    public class CustomerCtor
    {
        public CustomerCtor(ICustomerDAL dal, Logger logger) { }

        // TODO: check if CustomReflectionLibrary can create an instance using that particular ctor
        public CustomerCtor(CustomerDAL dal, Logger logger) { }
    }
}
