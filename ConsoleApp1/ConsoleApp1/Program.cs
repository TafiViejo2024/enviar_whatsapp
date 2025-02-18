using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main()
    {
        string url = "https://graph.facebook.com/v22.0/569259382937171/messages"; // Endpoint de Meta API
        string token = "EAASYXrmdB08BO1auQOFJQzqAq3zySezgHUG4Q2KNrcvLlYM9xZCqtRSg9b1DZBSfBUZBCrh12zR6IJwSSJZCCSvgjhirq7J3HPosQzXGN5OGTs0llgzZAf0TxAO8I9zTfpck1xcDudtvVx1dlC4bq5fsZCkyxBcRBMa0mJz0R6VgWo5slUm2egLpq6gbS92MclKoBTcxO0TX6TWsLURfMEqKZC0ZCGER5jvEBngu89dD";
        string toNumber = "+541132542221"; // Número de destino con código de país
        string jsonData = "{ \"messaging_product\": \"whatsapp\", \"to\": \"" + toNumber + "\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }";

        try
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Crear la solicitud HTTP POST
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers["Authorization"] = "Bearer " + token;

            // Convertir el JSON a bytes
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
            request.ContentLength = byteArray.Length;

            // Escribir el JSON en la solicitud
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            // Obtener la respuesta
            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string responseText = reader.ReadToEnd();
                Console.WriteLine("Respuesta de WhatsApp API: " + responseText);
            }
        }
        catch (WebException ex)
        {
            using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
            {
                string errorResponse = reader.ReadToEnd();
                Console.WriteLine("Error: " + errorResponse);
            }
        }
    }
}
