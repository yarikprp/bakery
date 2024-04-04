using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery
{
    internal static class DbConnection
    {
        public readonly static string ConnectionString = "Host=localhost;Port=5432;Database=bakery;Username=postgres;Password=1";
    }
}
