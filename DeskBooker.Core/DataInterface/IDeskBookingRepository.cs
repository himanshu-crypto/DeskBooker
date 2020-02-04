using DeskBooker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBooker.Core.DataInterface
{
   public interface IDeskBookingRepository
    {
       public void Save(DeskBooking deskBooking);
        
    }
}
