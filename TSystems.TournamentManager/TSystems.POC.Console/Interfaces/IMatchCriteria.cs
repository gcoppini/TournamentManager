using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems
{
    public interface IMatchCriteria 
    {
        int Order { get; set; }
        List<IFightMatch> Apply(List<IFightMatch> list);
        
    }
}
