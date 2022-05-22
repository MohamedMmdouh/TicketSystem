using TicketSystemApi.Persistance.Data;

namespace TicketSystemApi.Helpers.ImageFormat
{
    public static class IMageTextFormat
    {
        public static string ImageGenerator(TicketViewModel ticket,int ticketnum)
        {
            string num = (ticketnum.ToString().Length > 3) ? ticketnum.ToString() : "00" + ticketnum;
            var html = @"<head>
                <meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>
            <style>
            .Center {
                display: block;
                width: 50%;
                text-align: center;
                padding: 0;
                margin: auto;
                font-family: Noto Naskh Arabic;
            }
            .main {
              height: 100%;
              width: 100%;
              background-color: #c0c0c0;
              display: table;
            }
            </style>
            </head>
              <div class='Center'>
              <h1>
                " + ticket.employeename + @"
             </h1>
             <h1>" +
                ticket.Location + @"
             </h1>
             <br>
             <h3>
               رقم ميعادك هو
             </h3>
             <hr>
             <h1>
               A-" + num + @"
             </h1>
             <hr>
              <br>
            <h1>
             ميعاد حجزك التقريبي هو
             </h1>
             <h1>
               " + TimeFormat.datetimechange(ticket.StartTime) + "  " + TimeFormat.datetimechange(ticket.EndTime) + @"
              </h1>
            </div>";
            return html;
        }

    }
}
