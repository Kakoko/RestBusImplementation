using Newtonsoft.Json;
using RestBus.Client;
using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var amqpUrl = "amqp://localhost:5672"; //AMQP URL for RabbitMQ installation
            var serviceName = "products"; //The unique identifier for the target service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);

            RestBusClient client = new RestBusClient(msgMapper);

            RequestOptions requestOptions = null;

            /* 
            * //Uncomment this section to get a response in JSON format
            * */
            requestOptions = new RequestOptions();
           requestOptions.Headers.Add("Accept", "application/json");
            


            //Send Request
            var uri = "api/products"; //Substitute "hello/random" for the ServiceStack example
            var response = client.GetAsync(uri, requestOptions).Result;
            //NOTE: You can use 'var response = await client.GetAsync(uri, requestOptions);' in your code


            var res = response.Content.ReadAsStringAsync().Result;


            // var result = JsonConvert.DeserializeObject<ProductModel>(res);




            dynamic json = JsonConvert.DeserializeObject(res);

            //Display response
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(res);

            //Dispose client
            client.Dispose();

        }
    }
}
