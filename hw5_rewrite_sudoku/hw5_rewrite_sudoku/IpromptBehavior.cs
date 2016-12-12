using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5_puzzle
{
    public interface IpromptBehavior
    {
        String prompt(String userInput = "empty");
    }
}
