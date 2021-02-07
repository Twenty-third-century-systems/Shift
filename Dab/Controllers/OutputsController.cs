using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dab.Dtos;
using IdentityModel.Client;
using IronPdf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dab.Controllers {
    [Route("[controller]")]
    public class OutputsController : Controller {
        [HttpGet("ns/sum/{applicationId}")]
        public async Task<IActionResult> Convert(string applicationId)
        {
            /////////////////////////////////////////////////////////////////////////
            //Get application data
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var httpResponseMessage =
                    await client.GetAsync($"https://localhost:44312/Application/ns/sum/{applicationId}");

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
                        string OutputPath = "C:/Backgrounds/_generated_certificate.pdf";

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

        [HttpGet("pvt/sum/{applicationId}")]
        public async Task<IActionResult> PvtSummaryDoc(string applicationId)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var httpResponseMessage =
                    await client.GetAsync($"https://localhost:44312/Application/pvt/sum/{applicationId}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var docDto = await httpResponseMessage.Content.ReadAsAsync<PvtEntitySummaryDocDto>();
                    using (var renderer = new IronPdf.HtmlToPdf())
                    {
                        var htmlList = new List<string>();

                        var header =
                            @"<html>" +
                            "<head>" +
                            "<meta http-equiv='content-type' content='text/html; charset=utf-8'/>" +
                            "<title></title>" +
                            "<style>" +
                            "body {" +
                            "background: transparent;" +
                            "}" +
                            "table {" +
                            "border-collapse: separate;" +
                            "border-spacing: 0;" +
                            "color: #4a4a4d;" +
                            "width: 100%;" +
                            "font: 14px/1.4 'Helvetica Neue', Helvetica, Arial, sans-serif;" +
                            "}th, td {" +
                            "padding: 10px 15px;" +
                            "vertical-align: middle;" +
                            "}" +
                            "thead {" +
                            "background: rgba(57, 88, 112, 0.75);" +
                            "color: #fff;" +
                            "}" +
                            "th:first-child {" +
                            "text-align: left;" +
                            "}" +
                            "tbody tr:nth-child(even) {" +
                            "background: #f0f0f2;" +
                            "}" +
                            "td {" +
                            "border-bottom: 1px solid #cecfd5;" +
                            "border-right: 1px solid #cecfd5;" +
                            "}" +
                            "td:first-child {" +
                            "border-left: 1px solid #cecfd5;" +
                            "}tfoot {" +
                            "text-align: right;" +
                            "}tfoot tr:last-child {" +
                            "background: #f0f0f2;" +
                            "}" +
                            "</style>" +
                            "</head>";
                        htmlList.Add(header);

                        var companySpecifics =
                            $"<body>" +
                            $"<table style='font-size:16px'>" +
                            $"<col style=\"width:15 % \">" +
                            $"<col style=\"width:85 % \">" +
                            $"<tbody>" +
                            $"<td>Entity No.&nbsp;&nbsp;&nbsp;&nbsp;</td>" +
                            $"<td>{docDto.EntitySummary.Ref}</td>" +
                            $"</tr>" +
                            $"<tr>" +
                            $"<td>Entity Name</td>" +
                            $"<td>{docDto.EntitySummary.Name} Pvt (Ltd)</td>" +
                            $"</tr>" +
                            $"<tr>" +
                            $"<td>Date of Incorporation<span></span></td>" +
                            $"<td>{docDto.EntitySummary.DateOfIncorporation}</td>" +
                            $"</tr>" +
                            $"</tbody>" +
                            $"</table>" +
                            $"<hr>";
                        htmlList.Add(companySpecifics);

                        var pageDescriptors =
                            $"<p>Section 31 and 241 of Act</p>" +
                            "<p>Section 10, 11, 13, 14, 15, 17, 18, 20 of Regulations</p>" +
                            "<p>" +
                            "<b>" +
                            "Situation and Postal Address of a Company’s Registered Office or " +
                            "of a Foreign Company’s Principal Place of Business" +
                            "</b>" +
                            "</p>" +
                            "<hr>" +
                            "<br>";
                        htmlList.Add(pageDescriptors);

                        var addressDetails =
                            $"<p><b>CURRENT ADDRESS</b></p>" +
                            "<hr>" +
                            "<table>" +
                            "<col style=\"width:15%\">" +
                            "<col style=\"width:2%\">" +
                            "<col style=\"width:83%\">" +
                            "<tbody>" +
                            "<tr>" +
                            "<td>Situated at</td>" +
                            "<td>:</td>" +
                            $"<td>{docDto.EntityAddress.SituatedAt}.</td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td>Postal Address</td>" +
                            "<td>:</td>" +
                            $"<td>{docDto.EntityAddress.PostalAddress}.</td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td>Email Address</td>" +
                            "<td>:</td>" +
                            $"<td>{docDto.EntityAddress.EmailAddress}.</td>" +
                            "</tr>" +
                            "</tbody>" +
                            "</table>" +
                            "<hr>" +
                            "<p>" +
                            "NOTES:- (a) In the case of address, online update must be submitted to the registrar BEFORE" +
                            " the proposed change takes place." +
                            "</p>" +
                            "<p style='page-break-before:always'></p>";
                        htmlList.Add(addressDetails);

                        var directors =
                            "<p><b>DIRECTORS</b></p><br>" +
                            "<table>" +
                            "<col style=\"width:15%\">" +
                            "<col style=\"width:20%\">" +
                            "<col style=\"width:10%\">" +
                            "<col style=\"width:15%\">" +
                            "<col style=\"width:40%\">" +
                            "<thead>" +
                            "<tr>" +
                            "<th>Date of Appointment</th>" +
                            "<th>Present Christian Names, Surnames</th>" +
                            "<th>ID/Passport#s</th>" +
                            "<th>Nationality</th>" +
                            "<th>Full Residential or Business Address and Postal Address</th>" +
                            "</tr>" +
                            "</thead>" +
                            "<tbody>";

                        foreach (var director in docDto.Directors)
                        {
                            directors +=
                                "<tr>" +
                                $"<td>{director.DateOfIncorporation}</td>" +
                                $"<td>{director.ChristianNames}</td>" +
                                $"<td>{director.Ids}</td>" +
                                $"<td>{director.Nationality}.</td>" +
                                $"<td>{director.ResidentialAddress}</td>" +
                                "</tr>";
                        }

                        directors =
                            directors +
                            "</tbody>" +
                            "</table>" +
                            "<br><br>";
                        htmlList.Add(directors);

                        var principalOfficer =
                            $"<p><b>SECRETARY or PRINCIPAL OFFICER</b></p><br>" +
                            "<table>" +
                            "<col style=\"width:15%\">" +
                            "<col style=\"width:20%\">" +
                            "<col style=\"width:10%\">" +
                            "<col style=\"width:15%\">" +
                            "<col style=\"width:40%\">" +
                            "<thead>" +
                            "<tr>" +
                            "<th>Date ofAppointment</th>" +
                            "<th>Present Christian Names, Surnames</th>" +
                            "<th>ID/Passport#s</th>" +
                            "<th>Nationality</th>" +
                            "<th>Full Residential or Business Address and Postal Address</th>" +
                            "</tr>" +
                            "</thead>" +
                            "<tbody>";

                        foreach (var principal in docDto.Secretary)
                        {
                            principalOfficer +=
                                "<tr>" +
                                $"<td>{principal.DateOfIncorporation}</td>" +
                                $"<td>{principal.ChristianNames}</td>" +
                                $"<td>{principal.Ids}</td>" +
                                $"<td>{principal.DateOfIncorporation}.</td>" +
                                $"<td>{principal.ResidentialAddress}</td>" +
                                "</tr>";
                        }

                        principalOfficer +=
                            "</tbody>" +
                            "</table>" +
                            "<br>" +
                            "<hr>" +
                            "<p style='page-break-before:always'>";
                        htmlList.Add(principalOfficer);

                        var majorObject =
                            "<p><b>MAJOR OBJECT</b></p>" +
                            $"<p>{docDto.MajorObject}</p>";
                        htmlList.Add(majorObject);

                        var liabilityClause =
                            "<p><b>LIABILITY CLAUSE</b></p>" +
                            $"<p>{docDto.LiabilityClause}</p>" +
                            "<br><br>";
                        htmlList.Add(liabilityClause);

                        var shareCapitalClause =
                            "<p><b>SHARE CAPITAL CLAUSE</b></p>" +
                            $"<p>{docDto.ShareCapital}</p>" +
                            "<br><br>";
                        htmlList.Add(shareCapitalClause);

                        var articlesOfAssociation =
                            "<p><b>ARTICLES OF ASSOCIATION</b></p>";
                        foreach (var article in docDto.ArticlesOfAssociation)
                        {
                            articlesOfAssociation += $"<p>{article}</p>";
                        }

                        articlesOfAssociation +=
                            "<hr>" +
                            "<p style='page-break-before:always'></p>";
                        htmlList.Add(articlesOfAssociation);

                        var members =
                            "<p><b> MEMBERS</b></p><br>" +
                            "<table>" +
                            "<col style=\"width:60%\">" +
                            "<col style=\"width:10%\">" +
                            "<col style=\"width:10%\">" +
                            "<col style=\"width:20%\">" +
                            "<thead>" +
                            "<tr>" +
                            "<th>Full names, IDs & Occupation of Subscribers</th>" +
                            "<th>Ordinary Shares</th>" +
                            "<th>Preference Shares</th>" +
                            "<th>Total Shares</th>" +
                            "</tr>" +
                            "</thead>" +
                            "<tbody>";

                        foreach (var subscriber in docDto.Subscribers)
                        {
                            members +=
                                "<tr>" +
                                $"<td>{subscriber.FullNamesAndIds}</td>" +
                                $"<td>{subscriber.OrdinaryShares}</td>" +
                                $"<td>{subscriber.PreferenceShares}</td>" +
                                $"<td>{subscriber.TotalShares}</td>" +
                                "</tr>";
                        }

                        members +=
                            "</tbody>" +
                            "</table>" +
                            "<hr>" +
                            "</body>" +
                            "</html>";
                        htmlList.Add(members);

                        return await ConstructSummaryDocAndSend(renderer,htmlList);
                    }
                }
                else
                {
                    return NotFound("Application not found");
                }
            }            
        }
        
        [HttpGet("pvt/cert/{applicationId}")]
        public async Task<IActionResult> PvtCertificate(string applicationId)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var httpResponseMessage =
                    await client.GetAsync($"https://localhost:44312/Application/pvt/cert/{applicationId}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    //TODO add code to bind html with data here
                    ConstructCertificateAndSend();
                }
                else
                {
                    return NotFound("Application not found");
                }
            }
            
            return null;
        }

        private void ConstructCertificateAndSend()
        {
            throw new NotImplementedException();
        }

        private async Task<IActionResult> ConstructSummaryDocAndSend(HtmlToPdf renderer, List<string> htmlList)
        {
            string finalhtml = string.Concat(htmlList.ToArray());

            string DocPath = @"C:/Backgrounds/" + $"_generated.pdf";
            renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;

            renderer.PrintOptions.EnableJavaScript = true;
            renderer.PrintOptions.RenderDelay = 500; //milliseconds
            renderer.PrintOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;

            renderer.PrintOptions.MarginTop = 60;
            renderer.PrintOptions.MarginBottom = 60;
            renderer.PrintOptions.MarginLeft = 15;
            renderer.PrintOptions.MarginRight = 10;
            
            var bg = renderer.RenderHtmlAsPdf(finalhtml);
            bg.AddBackgroundPdf(@"C:\\Backgrounds\\summary_bg.pdf");
            bg.SaveAs(DocPath);
            
            var pdf = PdfDocument.FromFile(DocPath);

            System.Net.WebClient client = new System.Net.WebClient();
            Byte[] byteArray = client.DownloadData(DocPath);
            
            return new FileContentResult(byteArray, "application/pdf");
        }
    }
}