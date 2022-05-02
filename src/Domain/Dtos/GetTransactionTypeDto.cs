using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class GetTransactionTypeDto
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Nature { get; private set; }
        public string Signal { get; set; }
    }
}
