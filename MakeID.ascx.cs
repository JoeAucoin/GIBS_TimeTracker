
using System;
using System.IO;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Services.Exceptions;
using GIBS.Modules.GIBS_TimeTracker.Components;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PdfSharp;
using Spire.Barcode;

//using BarcodeStandard;


namespace GIBS.Modules.GIBS_TimeTracker
{
    public partial class MakeID : PortalModuleBase
    {

        public int ttUserID = 0;

        string _ClientPhoto = "";
        string _ClientPhotoRS = "";
        string _BarCodeImage = "";
        string _PdfFilename = "";
        string _IDCardImagePath = "";
        string _CertWatermark = "";


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
            //     JavaScript.RequestRegistration(CommonJs.jQueryUI);
            //    JavaScript.RequestRegistration(CommonJs.DnnPlugins);
            //  Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CameraScript", (this.TemplateSourceDirectory + "/JavaScript/Camera.js"));


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cid"] != null)
            {
                ttUserID = Int32.Parse(Request.QueryString["cid"]);
            }
            LoadSettings();
            FillClientRecord(ttUserID);

            //AutoIDCard

            if (Request.QueryString["AutoIDCard"] != null)
            {
                if (Request.QueryString["AutoIDCard"] == "true")
                {
                    ButtonPhotoID.Visible = false;
                    ButtonReturnToClientManager.Visible = false;
                    ButtonNoPhotoID.Visible=false;
                    MakeIDCard2(ttUserID.ToString());
                    DeleteImages();
                }
            }
        }

        public void FillClientRecord(int clientID)
        {

            try
            {
                //load the item
                
                TimeTrackerController controller = new TimeTrackerController();
                TimeTrackerInfo item = controller.GetPhotoByUserID(clientID);

                if (item != null)
                {

                    LabelClientInfo.Text = item.FirstName + ' ' + item.LastName.Substring(0,1).ToString();
                    HiddenFieldFirstName.Value = item.FirstName;
                    //  LabelClientInfo.Text += item.ClientAddress + ", " + item.ClientTown + ", " + item.ClientState + " " + item.ClientZipCode;
                    if (item.IDPhoto != null)
                    {
                        ImageIDClient.Visible = true;
                        byte[] imagem = (byte[])(item.IDPhoto);
                        var PROFILE_PIC = Convert.ToBase64String(imagem);
                        HiddenFieldClientPicture.Value = item.IDPhoto.ToString();
                        ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        ImageIDClient.AlternateText = item.FirstName + ' ' + item.LastName;

                        MemoryStream ms = new MemoryStream(imagem);
                        //write to file
                        //string myidLogo = PortalSettings.HomeDirectoryMapPath + "IDCard1.png"; // "Images\\Logo.png";
                        _ClientPhoto = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + clientID.ToString() + ".jpg";

                        if (File.Exists(_ClientPhoto))
                        {
                            File.Delete(_ClientPhoto);
                        }

                        FileStream file = new FileStream(_ClientPhoto.ToString(), FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);
                        ms.Close();
                        file.Close();
                        file.Dispose();
                        

                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                    }

                    // CREATE THE BARCODE
                    string s = clientID.ToString().PadLeft(11, '0');
                    CreateBarCode(s.ToString());

                }
                else
                {
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(), true);
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void CreateBarCode(string barcodeText)
        {

            try
            {
                ////start here
                //BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                //System.Drawing.Image imgBarCode = b.Encode(BarcodeLib.TYPE.CODE128, barcodeText.ToString(), System.Drawing.Color.Black, System.Drawing.Color.White, 290, 30);

                //FileStream fileStreamBarCode = new FileStream(PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + ttUserID.ToString() + "BarCode.jpg", FileMode.Create); //I use file stream instead of Memory stream here
                //imgBarCode.Save(fileStreamBarCode, ImageFormat.Jpeg);
                //fileStreamBarCode.Close();

          //      BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, barcodeText.ToString());
          //      gen.Save(PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + ttUserID.ToString() + "BarCode.jpg", BarCodeImageFormat.Jpeg);

                BarcodeSettings bs = new BarcodeSettings();

                bs.Type = BarCodeType.Code128;
                bs.Data = barcodeText.ToString();
                bs.ShowText = false;
                bs.AutoResize = false;
                bs.Unit = GraphicsUnit.Millimeter;
                bs.BarHeight = 9;
                
                bs.ImageWidth = 290;
                bs.ImageHeight = 9;
                
                BarCodeGenerator bg = new BarCodeGenerator(bs);

                bg.GenerateImage().Save(PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + ttUserID.ToString() + "BarCode.jpg");


                _BarCodeImage = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + ttUserID.ToString() + "BarCode.jpg";
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void ButtonPhotoID_Click(object sender, EventArgs e)
        {
            MakeIDCard(ttUserID.ToString());

            DeleteImages();
        }

        protected void ButtonNoPhotoID_Click(object sender, EventArgs e)
        {
            MakeIDCard2(ttUserID.ToString());

            DeleteImages();
        }



        public void MakeIDCard(string itemID)
        {
            try
            {


                string myPortalName = this.PortalSettings.PortalName.ToString();



                Document document = new Document();
                document.Info.Author = "Joseph Aucoin";
                document.Info.Keywords = "Family Pantry ID Card";
                document.Info.Title = myPortalName.ToString() + " ID Card";

                // Get the A4 page size
                MigraDoc.DocumentObjectModel.Unit width, height;
                PageSetup.GetPageSize(PageFormat.A6, out width, out height);

                width = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(86));
                height = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(54));

                // Add a section to the document and configure it such that it will be in the centre
                // of the page
                Section section = document.AddSection();
                section.PageSetup.PageHeight = height;
                section.PageSetup.PageWidth = width;
                section.PageSetup.LeftMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.RightMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.TopMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.BottomMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));


                //++++++++++++++++++++++++++++++
                //++++++++++++++++++++++++++++++
                //++++++++++++++++++++++++++++++
                // CREATE THE TABLE  HeaderTable
                //  HeaderTable


                MigraDoc.DocumentObjectModel.Tables.Table HeaderTable = new MigraDoc.DocumentObjectModel.Tables.Table();
                HeaderTable.Borders.Width = 0; // Default to show borders 1 pixel wide Column
                HeaderTable.LeftPadding = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(0);
                HeaderTable.RightPadding = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(0);
                HeaderTable.Borders.Left.Visible = true;
                HeaderTable.Borders.Right.Visible = true;



                Column column0 = HeaderTable.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromMillimeter(41));
                Column column1 = HeaderTable.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromMillimeter(41));


                MigraDoc.DocumentObjectModel.Tables.Row row1 = HeaderTable.AddRow();
                //      row1.Borders.Width = 1;
                row1.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(8);
                MigraDoc.DocumentObjectModel.Tables.Cell cell1 = row1.Cells[0];
                MigraDoc.DocumentObjectModel.Tables.Cell cell2 = row1.Cells[1];
                //      row1.KeepWith = 1;
                cell1.Format.Alignment = ParagraphAlignment.Center;
                cell1.Format.Font.Color = Colors.Blue;
                cell1.Format.Font.Size = 16;

                cell1.AddParagraph(LabelClientInfo.Text.ToString());


                cell2.Format.Alignment = ParagraphAlignment.Center;
                cell2.Format.Font.Color = Colors.Blue;
                cell2.AddParagraph(ttUserID.ToString());



                /////// ROW 2 ////////
                ///////

                Row row2 = HeaderTable.AddRow();
                row2.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(31.75);
                //  row2.KeepWith = 2;
                MigraDoc.DocumentObjectModel.Tables.Cell idPhotoCell = row2.Cells[0];
                MigraDoc.DocumentObjectModel.Tables.Cell idLogoCell = row2.Cells[1];


                idPhotoCell.Format.Alignment = ParagraphAlignment.Center;
                //     idPhotoCell.Borders.Width = 1;
                idPhotoCell.Borders.Left.Visible = true;
                idPhotoCell.Borders.Right.Visible = true;
                idPhotoCell.Borders.Bottom.Visible = true;

                idLogoCell.Format.Alignment = ParagraphAlignment.Center;
                idLogoCell.Borders.Width = 0;
                idLogoCell.Borders.Left.Visible = true;
                idLogoCell.Borders.Right.Visible = true;
                idLogoCell.Borders.Bottom.Visible = true;

                string myidLogo = PortalSettings.HomeDirectoryMapPath + "IDCard.png";



                //   XImage myNewimage = XImage.FromFile(_ClientPhoto.ToString())
                //      myNewimage.Width = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(24);

                //Image img = new Image(ImageDataFactory.create(_ClientPhoto.ToString()));
                //img.scaleToFit(PageSize.A4.getWidth(), PageSize.A4.getHeight());

                MigraDoc.DocumentObjectModel.Shapes.Image imagePhoto = idPhotoCell.AddImage(_ClientPhoto.ToString());
                imagePhoto.Width = "3.96875cm";
                imagePhoto.LockAspectRatio = true;
                //    MigraDoc.DocumentObjectModel.Shapes.Image MyName Image;


                // LockAspectRatio = true;
                MigraDoc.DocumentObjectModel.Shapes.Image imageLogo = idLogoCell.AddParagraph().AddImage(myidLogo.ToString());
                imageLogo.Width = "3.5cm";
                imageLogo.LockAspectRatio = true;


                Row row3 = HeaderTable.AddRow();

                MigraDoc.DocumentObjectModel.Tables.Cell barCodeCell = row3.Cells[0];

                barCodeCell.MergeRight = 1;
                barCodeCell.AddParagraph().AddImage(_BarCodeImage.ToString());
                barCodeCell.Format.Alignment = ParagraphAlignment.Center;

                barCodeCell.Borders.Width = 0;
                barCodeCell.Borders.Left.Visible = true;
                barCodeCell.Borders.Right.Visible = true;
                barCodeCell.Borders.Bottom.Visible = true;



                section.Add(HeaderTable);


                ///// SPACING
                //MigraDoc.DocumentObjectModel.Paragraph paragraph = section.AddParagraph();
                //paragraph.Format.LineSpacingRule = MigraDoc.DocumentObjectModel.LineSpacingRule.Exactly;
                //paragraph.Format.LineSpacing = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(02.0);



                // Create a renderer
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

                // Associate the MigraDoc document with a renderer
                pdfRenderer.Document = document;

                // Layout and render document to PDF
                pdfRenderer.RenderDocument();


                _PdfFilename = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + "ID" + ttUserID.ToString() + ".pdf";

                if (File.Exists(_PdfFilename))
                {
                    File.Delete(_PdfFilename);
                }

                pdfRenderer.PdfDocument.Save(_PdfFilename);








                bool watermarkIsNeeded = false;
                if (_CertWatermark.ToString().Trim().Length > 1)
                {
                    watermarkIsNeeded = true;
                }

                if (watermarkIsNeeded)
                {
                    PdfDocument pdfIn = PdfReader.Open(_PdfFilename, PdfDocumentOpenMode.Import);
                    PdfDocument pdfOut = new PdfDocument();

                    for (int i = 0; i < pdfIn.PageCount; i++)
                    {
                        PdfPage pg = pdfIn.Pages[i];
                        pg = pdfOut.AddPage(pg);
                        // WATERMARK TEXT
                        string draftFlagStr = _CertWatermark.ToString();

                        // Get an XGraphics object for drawing beneath the existing content
                        XGraphics gfx = XGraphics.FromPdfPage(pg, XGraphicsPdfPageOptions.Prepend);

                        // Get the size (in point) of the text
                        XFont fontWM = new XFont("Verdana", 62, XFontStyle.Bold);
                        XSize size = gfx.MeasureString(draftFlagStr, fontWM);

                        // Define a rotation transformation at the center of the page
                        gfx.TranslateTransform(pg.Width / 2, pg.Height / 2);
                        gfx.RotateTransform(-Math.Atan(pg.Height / pg.Width) * 180 / Math.PI);
                        gfx.TranslateTransform(-pg.Width / 2, -pg.Height / 2);

                        // Create a string format
                        XStringFormat format = new XStringFormat();
                        format.Alignment = XStringAlignment.Near;
                        format.LineAlignment = XLineAlignment.Near;

                        // Create a dimmed red brush
                        XBrush brush = new XSolidBrush(XColor.FromArgb(32, 0, 0, 255));

                        // Draw the string
                        gfx.DrawString(draftFlagStr, fontWM, brush,
                            new XPoint((pg.Width - size.Width) / 2, (pg.Height - size.Height) / 2),
                            format);
                    }
                    pdfOut.Save(_PdfFilename);
                }


                string format1 = "Mddyyyyhhmmsstt";
                var myTimeStamp = String.Format("{0}", DateTime.Now.ToString(format1));

                HyperLinkPDF.Visible = true;
                HyperLinkPDF.NavigateUrl = PortalSettings.HomeDirectory + _IDCardImagePath.ToString() + "ID" + ttUserID.ToString() + ".pdf?v=" + myTimeStamp.ToString();



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);

            }
        }

        public void MakeIDCard2(string itemID)
        {
            try
            {


                string myPortalName = this.PortalSettings.PortalName.ToString();

                 

                Document document = new Document();
                document.Info.Author = "Joseph Aucoin";
                document.Info.Keywords = "Family Pantry ID Card";
                document.Info.Title = myPortalName.ToString() + " ID Card";

                // Get the A4 page size
                MigraDoc.DocumentObjectModel.Unit width, height;
                PageSetup.GetPageSize(PageFormat.A6, out width, out height);

                width = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(86));
                height = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(54));

                // Add a section to the document and configure it such that it will be in the centre
                // of the page
                Section section = document.AddSection();
                section.PageSetup.PageHeight = height;
                section.PageSetup.PageWidth = width;
                section.PageSetup.LeftMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.RightMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.TopMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.BottomMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));


                //++++++++++++++++++++++++++++++
                //++++++++++++++++++++++++++++++
                //++++++++++++++++++++++++++++++
                // CREATE THE TABLE  HeaderTable
                //  HeaderTable


                MigraDoc.DocumentObjectModel.Tables.Table HeaderTable = new MigraDoc.DocumentObjectModel.Tables.Table();
                HeaderTable.Borders.Width = 0; // Default to show borders 1 pixel wide Column
                HeaderTable.LeftPadding = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(0);
                HeaderTable.RightPadding = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(0);
                HeaderTable.Borders.Left.Visible = true;
                HeaderTable.Borders.Right.Visible = true;



                Column column0 = HeaderTable.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromMillimeter(82));
             //   Column column1 = HeaderTable.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromMillimeter(41));


                MigraDoc.DocumentObjectModel.Tables.Row row1 = HeaderTable.AddRow();
                //      row1.Borders.Width = 1;
                row1.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(11);
                MigraDoc.DocumentObjectModel.Tables.Cell cell1 = row1.Cells[0];
             //   MigraDoc.DocumentObjectModel.Tables.Cell cell2 = row1.Cells[1];
                //      row1.KeepWith = 1;
                cell1.Format.Alignment = ParagraphAlignment.Center;
                cell1.Borders.Visible = false;
                cell1.Format.Font.Color = Colors.Blue;
                cell1.Format.Font.Size = 26;

              //  cell1.AddParagraph(HiddenFieldFirstName.Value.ToString());
                cell1.AddParagraph(LabelClientInfo.Text.ToString());


                //      cell2.Format.Alignment = ParagraphAlignment.Center;
                //      cell2.AddParagraph(ttUserID.ToString());



                /////// ROW 2 ////////
                ///////

                Row row2 = HeaderTable.AddRow();
                row2.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(24.00);
              
                MigraDoc.DocumentObjectModel.Tables.Cell idLogoCell = row2.Cells[0];

                idLogoCell.Format.Alignment = ParagraphAlignment.Center;
                idLogoCell.Borders.Width = 0;
                idLogoCell.Borders.Left.Visible = true;
                idLogoCell.Borders.Right.Visible = true;
                idLogoCell.Borders.Bottom.Visible = true;

                string myidLogo = PortalSettings.HomeDirectoryMapPath + "IDCard2.png";


                // LockAspectRatio = true;
                MigraDoc.DocumentObjectModel.Shapes.Image imageLogo = idLogoCell.AddParagraph().AddImage(myidLogo.ToString());
                imageLogo.Width = "6.0cm";
                imageLogo.LockAspectRatio = true;

                /////// ROW 3 ////////
                ///////
                Row row3 = HeaderTable.AddRow();

                MigraDoc.DocumentObjectModel.Tables.Cell barCodeCell = row3.Cells[0];

            //    barCodeCell.MergeRight = 1;
                barCodeCell.AddParagraph().AddImage(_BarCodeImage.ToString());
                barCodeCell.Format.Alignment = ParagraphAlignment.Center;

                barCodeCell.Borders.Width = 0;
                barCodeCell.Borders.Left.Visible = false;
                barCodeCell.Borders.Right.Visible = false;
                barCodeCell.Borders.Bottom.Visible = false;

                /////// ROW 4 ////////
                ///////
                Row row4 = HeaderTable.AddRow();
                row4.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(4);
                MigraDoc.DocumentObjectModel.Tables.Cell ClientIDCell = row4.Cells[0];

                ClientIDCell.Format.Alignment = ParagraphAlignment.Center;
                ClientIDCell.Format.Font.Size = 12;

                ClientIDCell.AddParagraph(ttUserID.ToString());

                section.Add(HeaderTable);


         

                // Create a renderer
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

                // Associate the MigraDoc document with a renderer
                pdfRenderer.Document = document;

                // Layout and render document to PDF
                pdfRenderer.RenderDocument();



                _PdfFilename = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + "ID" + ttUserID.ToString() + ".pdf";

                if (File.Exists(_PdfFilename))
                {
                    File.Delete(_PdfFilename);
                }

                pdfRenderer.PdfDocument.Save(_PdfFilename);






                string format = "Mddyyyyhhmmsstt";
                var myTimeStamp = String.Format("{0}", DateTime.Now.ToString(format));
                // ?v=" + myTimeStamp.ToString();

                HyperLinkPDF.Visible = true;
                HyperLinkPDF.NavigateUrl = PortalSettings.HomeDirectory + _IDCardImagePath.ToString() + "ID" + ttUserID.ToString() + ".pdf?v=" + myTimeStamp.ToString();



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);

            }
        }


        public static void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Times New Roman";

            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.

            style = document.Styles["Heading1"];
            style.Font.Name = "Tahoma";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Color = Colors.DarkBlue;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading3"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called TextBox based on style Normal
            style = document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            // Create a new style called TOC based on style Normal
            style = document.Styles.AddStyle("TOC", "Normal");
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Blue;
        }

        public void LoadSettings()
        {
            try
            {



                if (Settings.Contains("iDCardImagePath"))
                {
                    _IDCardImagePath = (Settings["iDCardImagePath"].ToString());
                }
                else
                { _IDCardImagePath = ""; }




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void DeleteImages()
        {

            try
            {
                //  _ClientPhoto
                if (File.Exists(_ClientPhoto))
                {
                    File.Delete(_ClientPhoto);
                }

                if (File.Exists(_ClientPhotoRS))
                {
                    File.Delete(_ClientPhotoRS);
                }

                if (File.Exists(_BarCodeImage))
                {
                    File.Delete(_BarCodeImage);
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ButtonReturnToClientManager_Click(object sender, EventArgs e)
        {

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "Edit", "mid=" + ModuleId.ToString() + "&ttUserId=" + ttUserID.ToString()));

        }
        static string MigraDocFilenameFromByteArray(byte[] image)
        {
            return "base64:" +
                   Convert.ToBase64String(image);
        }

        static byte[] LoadImage(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                    throw new ArgumentException("No resource with name " + name);

                int count = (int)stream.Length;
                byte[] data = new byte[count];
                stream.Read(data, 0, count);
                stream.Close();
                return data;
            }
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            System.Drawing.Image newImage = new System.Drawing.Bitmap(image, newWidth, newHeight); // I specify the new image from the original together with the new width and height
            using (Graphics graphicsHandle = Graphics.FromImage(image))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(newImage, 0, 0, newWidth, newHeight);
                graphicsHandle.Dispose();
            }

            return newImage;
        }


    }
}