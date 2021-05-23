using System;
using System.Collections.Generic;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class AmazonService
    {

        public  List<PriceSourceItemOld> SearchItemLookupOperationOld(string term)
        {
            return SearchItemLookupOperationOld(term, null);
        }
        public List<PriceSourceItemOld> SearchItemLookupOperationOld(string term, string endpoint)
        {
            throw new NotImplementedException();
            //term = term.Trim();
            //List<PriceSourceItemOld> results = new List<PriceSourceItemOld>();
            //List<Endpoint> endpointsToSearch = new List<Endpoint>();
            //if (endpoint != null)
            //    endpointsToSearch.Add(Endpoints.Get(endpoint.Value));
            //else
            //    endpointsToSearch.AddRange(Endpoints.EndpointsList);

            //foreach (var endpointItem in endpointsToSearch)
            //{
            //    var authentication = new AmazonAuthentication();
            //    authentication.AccessKey = endpointItem.accessKey;
            //    authentication.SecretKey = endpointItem.secreteKey;

            //    endpoint = (AmazonEndpoint)Enum.Parse(typeof(AmazonEndpoint), endpointItem.endpointCode);
            //    var wrapper = new AmazonWrapper(authentication, endpoint.Value, endpointItem.merchantId);
            //    var searchOperation = wrapper.ItemLookupOperation(new List<string>() { term });
            //    ExtendedWebResponse xmlResponse = wrapper.Request(searchOperation);
            //    var itemLookupResponse = XmlHelper.ParseXml<ItemLookupResponse>(xmlResponse.Content);
            //    if (itemLookupResponse?.Items?.Item?.FirstOrDefault() != null)
            //    {
            //        string itemJson = JsonConvert.SerializeObject(itemLookupResponse.Items.Item.FirstOrDefault());
            //    }
            //    results.AddRange(itemLookupResponse.ToPriceSourceItemsOld());
            //}
            //return results;







        }
    }

}