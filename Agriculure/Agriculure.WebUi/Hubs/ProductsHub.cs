using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Agriculure.WebUi.Hubs
{
    public class ProductsHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void AnnounceProduct(string ProdName, string OwnerName)
        {
            Clients.All.announce(ProdName, OwnerName);
        }
    }
}