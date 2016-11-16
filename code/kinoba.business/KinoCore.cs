using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business
{
    public class KinoCore
    {
        static KinoCore()
        {
            EntitiesConnectionString = ConfigurationManager.AppSettings["kinobaEntities"];
        }

        public static string EntitiesConnectionString { get; set; }

        public static string UniqueCode()
        {
            string characters = "_abcd0efgh1ijkl2mnop3qrst4uvwx5yz-Q6WERT7YUIO8PASD9FGHJKLZXCVBNM";
            string ticks = DateTime.UtcNow.Ticks.ToString();
            var code = "";
            for (var i = 0; i < characters.Length; i += 2)
            {
                if ((i + 2) <= ticks.Length)
                {
                    var number = int.Parse(ticks.Substring(i, 2));
                    if (number > characters.Length - 1)
                    {
                        var one = double.Parse(number.ToString().Substring(0, 1));
                        var two = double.Parse(number.ToString().Substring(1, 1));
                        code += characters[Convert.ToInt32(one)];
                        code += characters[Convert.ToInt32(two)];
                    }
                    else
                        code += characters[number];
                }
            }
            return code;
        }
    }
}
