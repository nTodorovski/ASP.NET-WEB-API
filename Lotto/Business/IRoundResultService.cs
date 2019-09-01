using DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IRoundResultService
    {
        IEnumerable<RoundResult> GetAll();
        void Draw();
        List<int> Draw7Numbers();
        void ClaimAwards(int roundId, List<int> winningCombination);
    }
}
