namespace Catalog.API.Exceptions;

    public class ProductNotFoundException : Exception
    {
    
        // the base keyword here is used to call the ctor of Exception class
        // and passed in the string "Product not found!"
        public ProductNotFoundException() : base("Product not found!")
        {
            
        }
    }

