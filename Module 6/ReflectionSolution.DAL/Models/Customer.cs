using CustomReflectionLibrary.Attributes;

namespace ReflectionSolution.DAL.Models
{
    [Export(typeof(ICustomer))]
    public class Customer : ICustomer
    {
    }
}
