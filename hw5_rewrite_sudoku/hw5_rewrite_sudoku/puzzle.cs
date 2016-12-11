using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5_puzzle
{
    public abstract class puzzle
    {
        //protected IsolveBehavior solveBehavior;
        protected IpromptBehavior promptBehavior;
        public abstract void puzzleExecute();
    }
}
