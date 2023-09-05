namespace AAG.Catalog.Infra.Data.Queries;

public class CategoryQueries
{
    public const string Get =
        @"
            SELECT
                [Id],
                [Name],
                [CreatedAt],
                [UpdatedAt]
            FROM [Category] (NOLOCK)
            WHERE [Id] = @Id;
        ";

    public const string GetAll =
        @"
            SELECT
                [Id],
                [Name],
                [CreatedAt],
                [UpdatedAt]
                COUNT(1) OVER() as TotalItems,
                @QuantityByPage as QuantityByPage
            FROM [Category] (NOLOCK)
            ORDER BY [CreatedAt] DESC
            OFFSET @ActualPage ROWS
            FETCH NEXT @QuantityByPage ROWS ONLY
        ";

    public const string HasProductsWithCategory =
        @"
            SELECT
                Count(1)
            FROM [Product] (NOLOCK)
            WHERE [CategoryId] = @Id;
        ";

    public const string Insert =
        @"
            INSERT INTO [Category]
                ([Name],
                [CreatedAt])
            VALUES 
                (@Name,
                @CreatedAt);
            SELECT SCOPE_IDENTITY();
        ";

    public const string Update =
        @"
            UPDATE [Category]
               SET Name = @Name,
                   UpdatedAt = @UpdatedAt
            WHERE Id = @Id;
            SELECT @Id;
        ";

    public const string Delete =
        @"
            DELETE FROM [Category] WHERE Id = @Id;
            SELECT @Id;
        ";
}
