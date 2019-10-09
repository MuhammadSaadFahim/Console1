using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService service = ConnecttoCRM();
            if (service != null)
            {
                QueryExpression qe = new QueryExpression("contacting");
                EntityCollection enColl = service.RetrieveMultiple(qe);
                Console.WriteLine("Total Contacts in the System=" + enColl.Entities.Count);
                Console.ReadLine();
                //code your logic here
            }
        }

        public static IOrganizationService ConnecttoCRM()
        {
            IOrganizationService organizationService = null;
            String username = "developer.two@nayanazimabad.onmicrosoft.com";//eg: abc@xyz.onmicrosoft.com
            String password = "Bul95341";//eg: password@123

            String url = "https://nnjcldev.api.crm4.dynamics.com/XRMServices/2011/Organization.svc"; //eg: https://<yourorganisationname>.api.crm8.dynamics.com/XRMServices/2011/Organization.svc
            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = username;
                clientCredentials.UserName.Password = password;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri(url), null, clientCredentials, null);
                if (organizationService != null)
                {
                    Guid gOrgId = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).OrganizationId;
                    if (gOrgId != Guid.Empty)
                    {
                        Console.WriteLine("Connection Established Successfully...");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to Established Connection!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured - " + ex.Message);
            }
            return organizationService;

        }
    }
}