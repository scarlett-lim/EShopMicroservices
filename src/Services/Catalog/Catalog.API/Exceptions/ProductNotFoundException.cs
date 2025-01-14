using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

    public class ProductNotFoundException : NotFoundException
    {
    
        // the base keyword here is used to call the ctor of Exception class
        // and passed in the string "Product not found!"
        public ProductNotFoundException(Guid Id) : base("Product", Id)
        {
            
        }
    }

