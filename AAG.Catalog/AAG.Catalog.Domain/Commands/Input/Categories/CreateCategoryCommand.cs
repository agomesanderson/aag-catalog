using Flunt.Notifications;
using Flunt.Validations;

namespace AAG.Catalog.Domain.Commands.Input.Categories;

public class CreateCategoryCommand : Notifiable<Notification>
{
    public string Name { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<CreateCategoryCommand>()
            .IsNotNullOrEmpty(Name, "Nome não pode ser vazio ou nulo")
        );
    }
}
