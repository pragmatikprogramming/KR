using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.Domain.DataAccess;

namespace KR.Domain.Models
{
    public class PlacementRepository : IPlacementsRepository
    {
        public void AddPlacement(Placement m_Placement)
        {
            DBPlacement.AddPlacement(m_Placement);
        }

        public void EditPlacement(Placement m_Placement)
        {
            DBPlacement.EditPlacement(m_Placement);
        }

        public void DeletePlacement(int id)
        {
            DBPlacement.DeletePlacement(id);
        }
    }
}