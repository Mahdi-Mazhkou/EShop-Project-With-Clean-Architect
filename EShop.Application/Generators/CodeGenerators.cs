using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Generators
{
    public  class CodeGenerators
    {
        public static string GetUniqueCode()
        {
            Random rnd = new Random();

            int randomNumber = rnd.Next(100_000, 999_999);

            return randomNumber.ToString();
        }
    }
}
