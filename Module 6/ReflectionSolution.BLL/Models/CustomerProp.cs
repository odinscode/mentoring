using CustomReflectionLibrary.Attributes;
using ReflectionSolution.DAL.Models;
using ReflectionSolution.DAL.Services;

namespace ReflectionSolution.BLL.Models
{
    public class CustomerProp
    {
        [Import]
        public ICustomer CustomerDAL { get; set; }

        [Import]
        public Logger Logger { get; set; }
    }
}
