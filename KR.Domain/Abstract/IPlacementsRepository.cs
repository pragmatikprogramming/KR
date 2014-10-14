using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR.Domain.Entities;

namespace KR.Domain.Abstract
{
    public interface IPlacementsRepository
    {
        void AddPlacement(Placement m_Placement);
        void EditPlacement(Placement m_Placement);
        void DeletePlacement(int id);
    }
}
