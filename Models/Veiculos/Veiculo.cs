using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Font_End.Models.Veiculos
{ 
    public class Veiculo
    {
        public int id { get; set; }
        public string veiculo { get; set; }
        public string descricao { get; set; }
        public string marca { get; set; }
        public int ano { get; set; }
        public bool vendido { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    
    }
}
