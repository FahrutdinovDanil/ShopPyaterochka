using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopPyaterochka.BD;

namespace ShopPyaterochka
{
    public class db_connection
    {
        public static ShopEntities connection = new ShopEntities();
    }
}