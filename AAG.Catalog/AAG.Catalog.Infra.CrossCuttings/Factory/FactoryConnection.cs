using AAG.Catalog.Infra.CrossCuttings.Configuration;
using System.Data.SqlClient;

namespace AAG.Catalog.Infra.CrossCuttings.Factory
{
    public static class FactoryConnection
    {
        public static SqlConnection BuildConnection(AppConfigurations settings)
        {
            return new SqlConnection(settings.ConnectionString);
        }
    }
}
