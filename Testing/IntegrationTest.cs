using Infrastructure.Data;

namespace Testing;

public abstract class IntegrationTest : IClassFixture<PostgresTestFixture>
{
    protected readonly AppDbContext Context;

    protected IntegrationTest(PostgresTestFixture fixture)
    {
        Context = new AppDbContext(fixture.DbOptions);
    }
}