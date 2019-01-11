using ReflectionSolution.DAL.Models;
using ReflectionSolution.DAL.Services;

namespace ReflectionSolution.BLL.Models
{
    public class CustomerProp
    {
        public ICustomer CustomerDAL { get; set; }

        public Logger Logger { get; set; }
    }
}
