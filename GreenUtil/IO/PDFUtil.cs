//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GreenUtil.IO
{
    //public static class PDFUtil
    //{
    //    /// <summary>
    //    /// Insere uma imagem em um determinado documento
    //    /// </summary>
    //    /// <param name="pdfOriginal">bytes do pdf original</param>
    //    /// <param name="imgInserir">bytes da Imagem que vai ser inserida no documento</param>
    //    /// <param name="ponto">Ponto em que a imagem deve ser inserida</param>
    //    /// <param name="paginaAssinatura">Página do documento em que a imagem deve ser inserida</param>
    //    /// <returns>Novo documento contendo a imagem inserida</returns>
    //    public static void CriarPorImagem(string arquivoSaida, List<Stream> listaArquivos)
    //    {
    //        var document = new Document();
    //        document.AddAuthor("GreenConcept");

    //        using (var fileStream = new FileStream(arquivoSaida, FileMode.Create))
    //        {
    //            using (var writer = PdfWriter.GetInstance(document, fileStream))
    //            {
    //                document.Open();

    //                foreach (var imagem in listaArquivos)
    //                {
    //                    Rectangle pageSize = PageSize.A4;
    //                    imagem.Seek(0, System.IO.SeekOrigin.Begin);

    //                    Image iTextImage = Image.GetInstance(imagem);
    //                    AjustarImagemPagina(document, iTextImage, PageSize.A4);
    //                    //iTextImage.SetAbsolutePosition(0, 0);

    //                    iTextImage.SetAbsolutePosition((pageSize.Width - iTextImage.ScaledWidth) / 2, (pageSize.Height - iTextImage.ScaledHeight) / 2);


    //                    //document.SetPageSize(new Rectangle(iTextImage.Width, iTextImage.Height));
    //                    document.NewPage();
    //                    document.Add(iTextImage);
    //                }

    //                document.Close();
    //            }
    //        }
    //    }

    //    public static void AjustarImagemPagina(Document document, Image image, Rectangle pageSize)
    //    {

    //        float maxWidth = pageSize.Width - document.RightMargin - document.LeftMargin;

    //        float maxHeight = pageSize.Height - document.TopMargin - document.BottomMargin;

    //        float scalePercent = maxWidth / image.Width;

    //        while (image.Height * scalePercent > maxHeight)
    //        {
    //            scalePercent -= 0.01f;
    //        }

    //        image.ScaleAbsolute(image.Width * scalePercent, image.Height * scalePercent);
    //    }

    //    public static void FillFields(Dictionary<string, string> values, string inputFile, string outputFile)
    //    {
    //        var pdfReader = new PdfReader(inputFile);
    //        var pdfStamper = new PdfStamper(pdfReader, new FileStream(outputFile, FileMode.Create));
    //        foreach (var field in values.Where(f => f.Value != null))
    //        {
    //            pdfStamper.AcroFields.SetField(field.Key, field.Value);
    //        }
    //        pdfStamper.FormFlattening = true;
    //        pdfStamper.Close();
    //        pdfStamper.Save(outputFile);
    //    }
    //}
}
