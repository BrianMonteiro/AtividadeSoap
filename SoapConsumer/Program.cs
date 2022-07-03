using System;
using System.IO;
using System.Net;
using System.Xml;

namespace UsingSOAPRequest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            Console.WriteLine("Operações disponíveis:");
            Console.WriteLine("1. Subtração");
            Console.WriteLine("2. Adição");
            Console.WriteLine("3. Multiplicação");
            Console.WriteLine("4. Divisão");
            int operacao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite dois valores:");

            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());

            //Calling InvokeService method  
            obj.InvokeService(operacao, a, b);
        }
        public void InvokeService(int operacao, int a, int b)
        {
            
            HttpWebRequest request = CreateSOAPWebRequest(operacao);

            XmlDocument SOAPReqBody = OperacaoXML(operacao, a, b);

            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }
            //Geting response from request  
            using (WebResponse Serviceres = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                {
                    //reading stream  
                    var ServiceResult = rd.ReadToEnd();
                    //writting stream result on console  
                    Console.WriteLine(ServiceResult);
                    Console.ReadLine();
                }
            }
        }

        private XmlDocument OperacaoXML(int operacao, int a, int b)
        {
            XmlDocument SOAPReqBody = new XmlDocument();
            switch (operacao)
            {
                case 1:
                    //SOAP Body Request  
                    SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>" + "\n" +
                    @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" + "\n" +
                    @"  <soap:Body>" + "\n" +
                    @"    <Subtract xmlns=""http://tempuri.org/"">" + "\n" +
                    @"      <intA>" + a + "</intA>" + "\n" +
                    @"      <intB>" + b + "</intB>" + "\n" +
                    @"    </Subtract>" + "\n" +
                    @"  </soap:Body>" + "\n" +
                    @"</soap:Envelope>");
                    break;
                case 2:
                    //SOAP Body Request  
                    SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>" + "\n" +
                    @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" + "\n" +
                    @"  <soap:Body>" + "\n" +
                    @"    <Add xmlns=""http://tempuri.org/"">" + "\n" +
                    @"      <intA>" + a + "</intA>" + "\n" +
                    @"      <intB>" + b + "</intB>" + "\n" +
                    @"    </Add>" + "\n" +
                    @"  </soap:Body>" + "\n" +
                    @"</soap:Envelope>");
                    break;
                case 3:
                    //SOAP Body Request  
                    SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>" + "\n" +
                    @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" + "\n" +
                    @"  <soap:Body>" + "\n" +
                    @"    <Multiply xmlns=""http://tempuri.org/"">" + "\n" +
                    @"      <intA>" + a + "</intA>" + "\n" +
                    @"      <intB>" + b + "</intB>" + "\n" +
                    @"    </Multiply>" + "\n" +
                    @"  </soap:Body>" + "\n" +
                    @"</soap:Envelope>");
                    break;
                case 4:
                    //SOAP Body Request  
                    SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>" + "\n" +
                    @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" + "\n" +
                    @"  <soap:Body>" + "\n" +
                    @"    <Divide xmlns=""http://tempuri.org/"">" + "\n" +
                    @"      <intA>" + a + "</intA>" + "\n" +
                    @"      <intB>" + b + "</intB>" + "\n" +
                    @"    </Divide>" + "\n" +
                    @"  </soap:Body>" + "\n" +
                    @"</soap:Envelope>");
                    break;
                default:
                    break;
            }
            return SOAPReqBody;
        }

        public HttpWebRequest CreateSOAPWebRequest(int operacao)
        {
            string soapAction = "http://tempuri.org/Divide";
            switch (operacao)
            {
                case 1:
                    soapAction = "http://tempuri.org/Subtract";
                break;
                case 2:
                    soapAction = "http://tempuri.org/Add";
                break;
                case 3:
                    soapAction = "http://tempuri.org/Multiply";
                break;
                case 4:
                    soapAction = "http://tempuri.org/Divide";
               break;
            }
            //Making Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://www.dneonline.com/calculator.asmx");
            //SOAPAction  
            Req.Headers.Add(@"SOAPAction:" + soapAction);
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";
            //return HttpWebRequest  
            return Req;
        }
    }
}