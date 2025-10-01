namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);


    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                        async (CreateProductRequest request, ISender sender) =>
               {


                   try
                   {
                       var command = request.Adapt<CreateProductCommand>();

                       var result = await sender.Send(command);

                       var response = result.Adapt<CreateProductResponse>();

                       return Results.Created($"/product/{response.Id}", response);
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.ToString());
                       throw; // rilancia per vedere stack completo
                   }

               })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
