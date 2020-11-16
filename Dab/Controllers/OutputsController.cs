// using System;
// using System.Collections.Generic;
// using System.Net.Http;
// using Dab.Globals;
// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
//
// namespace Dab.Controllers {
//     public class OutputsController : Controller {
//         [HttpGet("ns/sum/{applicationid}")]
//         public ActionResult Convert(string applicationid)
//         {
//             /////////////////////////////////////////////////////////////////////////
//             //Using Iron PDF
//             //  IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
//             var clientw = new HttpClient();
//             var res = clientw.GetAsync($"{ApiUrls.NameSearchSummary}/{applicationid}").Result.Content.ReadAsStringAsync().Result;
//             var i = 0;
//
//             dynamic json_data = JsonConvert.DeserializeObject(res);
//             var data = json_data.data.value.searchNames;
//             List<mSearchNames> names = JsonConvert.DeserializeObject<List<mSearchNames>>(data.ToString());
//
//             var name = names.Where(z => z.Status == "Reserved").FirstOrDefault();
//
//             var dattta = json_data.data.value.searchInfo;
//             mSearchInfo searchInfo = JsonConvert.DeserializeObject<mSearchInfo>(dattta.ToString());
//
//             i = 2;
//
//             DateTime regDate = DateTime.Parse(searchInfo.ApprovedDate);
//             DateTime expDate = regDate.AddDays(30);
//             // using ironpdf
//
//
//
//             var Renderer = new IronPdf.HtmlToPdf();
//
//
//             List<string> HtmlList = new List<string>();
//             string[] HtmlArray;
//             // Render an HTML document or snippet as a string
//
//
//
//             string Dummy = "dummy data";
//
//             //HEADER STARTS HERE
//
//
//             string style = @"<style> body{ background: white); } div{ padding:40px; padding-top:10px; padding-bottom:16%; }  </style > ";
//
//             HtmlList.Add(style);
//
//             string html = " <body> <page size = 'A4'> <div> <p style = 'text-align: center;' ><img src = 'Views/Assets/img/2000px-Coat_of_arms_of_Zimbabwe.svg.png' /></p>     <p style = 'text-align: center;'> &nbsp;</p>" +
//                 "<center style = 'text-align: center; font-size:18pt;'><b>ZIMBABWE</b></center><br> <p style = 'text-align: center; font-size:16pt;'>  COMPANIES AND OTHER BUSINESS ENTITIES ACT <br>[CHAPTER 24:31] <br><br>Form C.V. 4 </p> <p style = 'text-align: center;' > &nbsp;</p> ";
//             HtmlList.Add(html);
//
//             string html2 = "<p style='font-size:14pt;'>COMPANIES REGISTRATION OFFICE<br> P.O.BOX CY 117<br> Causeway<br> Harare<br> Tel 0242775544-6<br> <br> </p>";
//             HtmlList.Add(html2);
//
//             var searcherId = searchInfo.Searcher_ID;
//             var user = db.AspNetUsersDetails.Where(z => z.email == searcherId).FirstOrDefault();
//
//             string html3 = $"<p style='font-size:14pt;'> {user.firstname} {user.lastname}<br> {user.address}<br> {user.city}<br> {user.country}<br> <br> <p style='font-size:14pt;'>Dear Sir/Madam</p></p> ";
//             HtmlList.Add(html3);
//
//             string html4 = $"<p style='font-size:16pt;'><b>REF: {searchInfo.SearchRef}  {name.Name} P/L </b> </p> ";
//             HtmlList.Add(html4);
//
//             string html5 = $" <p style='font-size:14pt;'> I acknowledge your application for company name search and do hereby confirm that the above choice has been reserved up to {expDate} </p> <p style='text - align: center; '>&nbsp;</p>  ";
//             HtmlList.Add(html5);
//
//             string html6 = @"<p style='font-size:14pt;'>Yours faithfully,</p> <p style='text - align: center; '>&nbsp;</p> <p style='font-size:14pt;'> For Registrar  </p >    <h></h>  </div> ";
//             HtmlList.Add(html6);
//             
//
//             //GeneratedBarcode QRWithLogo = QRCodeWriter.CreateQrCode($"Company Name: " +
//             //   $"Company Number:" + "url to app");
//             //QRWithLogo.ResizeTo(100, 50).SetMargins(1).ChangeBarCodeColor(Color.Black);
//             //QRWithLogo.SaveAsPng($"C:/My/QRCode.png").SaveAsPdf($"C:/My/QRCode.png.pdf");
//
//
//
//             //create the final html string
//             // HtmlList.Add(html11;          
//             HtmlArray = HtmlList.ToArray();
//             string finalhtml = string.Concat(HtmlArray);
//
//             var PDF = Renderer.RenderHtmlAsPdf(finalhtml);
//             string OutputPath = "C:/My/HtmlToPDFRAW.pdf";
//
//             PDF.SaveAs(OutputPath);
//
//
//             Renderer.RenderHtmlAsPdf(finalhtml).SaveAs(OutputPath);
//           //  QRWithLogo.StampToExistingPdfPage("C:/My/" + "HtmlToPDFRAW.pdf", 280, 725, 1);
//             System.Net.WebClient client = new System.Net.WebClient();
//             Byte[] byteArray = client.DownloadData(OutputPath);
//
//
//             //////////////////////////////////////////////////////////////////////
//             ViewBag.title = "New Search";
//             return new FileContentResult(byteArray, "application/pdf");
//
//         }
//     }
// }