using AAG.Catalog.Domain.Commands.Input.Categories;
using Flunt.Notifications;
using Flunt.Validations;

namespace AAG.Catalog.Domain.Commands.Input.Products;

public class CreateProductCommand : Notifiable<Notification>
{
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<CreateProductCommand>()
            .IsNotNullOrEmpty(CategoryId, "Id da categoria não pode zero")
            .IsNotNullOrEmpty(Name, "Nome não pode ser vazio ou nulo")
            .IsNotNullOrEmpty(Description, "Description não pode ser vazio ou nulo")
            .IsGreaterOrEqualsThan(Price, 0, "Preço não pode zero")
        );
    }
}
