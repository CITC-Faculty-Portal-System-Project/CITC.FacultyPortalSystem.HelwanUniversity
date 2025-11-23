using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MissionAlreadyExist : Exception
    {
        public MissionAlreadyExist()
            :base("This Mission is Already Added to System!") {
        }
    }
}
