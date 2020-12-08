using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dab.Dtos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dab.Controllers {
    [Route("[controller]")]
    public class OutputsController : Controller {
        [HttpGet("ns/sum/{applicationid}")]
        public async Task<IActionResult> Convert(string applicationid)
        {
            /////////////////////////////////////////////////////////////////////////
            //Get application data
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var httpResponseMessage =
                    await client.GetAsync($"https://localhost:44312/Application/ns/sum/{applicationid}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var contentData = JsonConvert.DeserializeObject<ReservedNameDto>(content);

                    if (contentData != null)
                    {
                        var renderer = new IronPdf.HtmlToPdf();

                        List<string> htmlList = new List<string>();

                        string style = "<style>" +
                                       "body {" +
                                       "       background: white;" +
                                       "    }" +
                                       "    div {" +
                                       "        padding: 40px;" +
                                       "        padding-top: 10px;" +
                                       "        padding-bottom: 16%;" +
                                       "    }" +
                                       "</style>";

                        htmlList.Add(style);

                        string html = $"<body>" +
                                      $"<page size='A4'>" +
                                      $"    <div>" +
                                      $"       <p style='text-align: center;'><img src='Views/Assets/img/2000px-Coat_of_arms_of_Zimbabwe.svg.png'/></p>" +
                                      $"       <p style='text-align: center;'> &nbsp;</p>" +
                                      $"        <center style='text-align: center; font-size:18pt;'><b>ZIMBABWE</b></center>" +
                                      $"        <br>" +
                                      $"        <p style='text-align: center; font-size:16pt;'>" +
                                      $"            COMPANIES AND OTHER BUSINESS ENTITIES ACT <br>[CHAPTER 24:31] <br><br>Form C.V. 4" +
                                      $"        </p>" +
                                      $"        <p style='text-align: center;'> &nbsp;</p>" +
                                      $"        <p style='font-size:14pt;'>COMPANIES REGISTRATION OFFICE<br> P.O.BOX CY 117<br>" +
                                      $"            CAUSEWAY<br> HARARE<br> TEL 0242775544-6<br> <br></p>" +
                                      $"        <p style='font-size:14pt;'> Mr.TAWANDA FOHLO<br> 5 Egeler Lane<br>" +
                                      $"            HARARE<br> ZIMBABWE<br> <br>" +
                                      $"        <p>Dear Sir/Madam</p>" +
                                      $"        <p style='font-size:16pt;'><b>REF: {contentData.NameSearchRef} {contentData.Value} P/L </b></p>" +
                                      $"        <p style='font-size:14pt;'> I acknowledge your application for company name search and do" +
                                      $"            hereby confirm that the above choice has been reserved up to {contentData.ExpiryDate} </p>" +
                                      $"        <p style='text-align: center; '>&nbsp;</p>" +
                                      $"        <p style='font-size:14pt;'>Yours faithfully,</p>" +
                                      $"        <p style='text-align: center; '>&nbsp;</p>" +
                                      $"        <p style='font-size:14pt;'> For Registrar </p>" +
                                      $"        <h></h>" +
                                      $"    </div>" +
                                      $"</page>" +
                                      $"</body>";
                        htmlList.Add(html);


                        string[] htmlArray = htmlList.ToArray();
                        string finalhtml = string.Concat(htmlArray);

                        var pdf = renderer.RenderHtmlAsPdf(finalhtml);
                        string OutputPath = "C:/My/HtmlToPDFRAW.pdf";

                        pdf.SaveAs(OutputPath);


                        renderer.RenderHtmlAsPdf(finalhtml).SaveAs(OutputPath);
                        System.Net.WebClient webClient = new System.Net.WebClient();
                        Byte[] byteArray = webClient.DownloadData(OutputPath);
                        ViewBag.title = "New Search";
                        return new FileContentResult(byteArray, "application/pdf");
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }
    }
}