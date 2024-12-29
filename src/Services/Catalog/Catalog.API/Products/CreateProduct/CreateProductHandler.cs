namespace Catalog.API.Products.CreateProduct;

    // param required to create the product
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

    // result returned if created successfully
    public record CreateProductResult(Guid Id);

    //business logic to handle CreateProductCommand
    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Business logic to create a product

            // Create Product Entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // Save to database

            // the session.store hereis to store the changes like update, insert, delete...., into session
            // it havent store in db
            // it can have multiple line of store code eg: 2nd session.store(product2)
            session.Store(product);
            
            // the savechangeasync will then execute all the actions written
            // eg : if there is store entity 1, update entity 2 and delete one of the entity 1
            await session.SaveChangesAsync(cancellationToken);

            // Return result
            return new CreateProductResult(product.Id);

        }
    }

