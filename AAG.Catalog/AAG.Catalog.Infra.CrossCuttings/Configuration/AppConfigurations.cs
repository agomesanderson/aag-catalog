using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAG.Catalog.Infra.CrossCuttings.Configuration;

public class AppConfigurations
{
    public string ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public CatalogSettings CatalogSettings { get; set; }
}
