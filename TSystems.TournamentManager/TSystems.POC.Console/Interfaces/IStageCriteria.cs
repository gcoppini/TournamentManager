using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems
{
    public interface IStageCriteria 
    {
        int Order { get; set; }
        List<IFightMatch> Apply(List<ICompetitor> list);
        
    }
}
