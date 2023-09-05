namespace AAG.Catalog.Infra.Data.Queries;

public class ProductQueries
{
    public const string Get =
        @"
            SELECT
                [Id],
                [CategoryId],
                [Name],
                [Description],
                [Price],
                [CreatedAt],
                [UpdatedAt]
            FROM [Product] (NOLOCK)
            WHERE [Id] = @Id;
        ";

    public const string GetAll =
        @"
            SELECT
                [Id],
                [CategoryId],
                [Name],
                [Description],
                [Price],
                [CreatedAt],
                [UpdatedAt]
                COUNT(1) OVER() as TotalItems,
                @QuantityByPage as QuantityByPage
            FROM [Product] (NOLOCK)
            ORDER BY [CreatedAt] DESC
            OFFSET @ActualPage ROWS
            FETCH NEXT @QuantityByPage ROWS ONLY
        ";

    public const string Insert =
        @"
            INSERT INTO [Product]
                ([CategoryId],
                [Name],
                [Description],
                [Price],
                [CreatedAt])
            VALUES 
                (@CategoryId,
                @Name,
                @Description,
                @Price,
                @CreatedAt);
            SELECT SCOPE_IDENTITY();
        ";

    public const string Update =
        @"
            UPDATE [Product]
               SET CategoryId = @CategoryId,
                   Name = @Name,
                   Description = @Description,
                   Price = @Price,
                   UpdatedAt = GETDATE()
            WHERE Id = @Id;
            SELECT @Id;
        ";

    public const string Delete =
        @"
            DELETE FROM [Product] WHERE Id = @Id;
            SELECT @Id;
        ";
}
