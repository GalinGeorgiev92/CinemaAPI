using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemAPI.Models.Input.Ticket
{
    public class TicketCreationModel
    {
        public int ProjectionId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}