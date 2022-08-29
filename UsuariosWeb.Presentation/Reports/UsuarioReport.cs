using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using UsuariosWeb.Domain.Entities;

namespace UsuariosWeb.Presentation.Reports
{
    public class UsuarioReport
    {
        public static byte[] GerarPDF(List<Usuario> usuarios)
        {
            var memoryStream = new MemoryStream();
            var pdfDocument = new PdfDocument(new PdfWriter(memoryStream));
            using (var document = new Document(pdfDocument))
            {
                document.Add(new Paragraph($"Relatório").AddStyle(tituloStyle()));
                document.Add(new Paragraph($"Usuários cadastrados").AddStyle(subTituloStyle()));
                document.Add(new Paragraph($"Data: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}").AddStyle(textoStyle()));
                document.Add(new Paragraph($"\n"));
                document.Add(new Paragraph($"Quantidade de usuários: {usuarios.Count}").AddStyle(textoStyle()));
                document.Add(new Paragraph($"\n"));

                //criando uma tabela para exibir os usuarios no relatório
                var table = new Table(3);
                //títulos da tabela
                table.AddHeaderCell("NOME").AddStyle(headerCellStyle());
                table.AddHeaderCell("EMAIL").AddStyle(headerCellStyle());
                table.AddHeaderCell("DATA/HORA").AddStyle(headerCellStyle());
                //corpo da tabela
                foreach (var item in usuarios)
                {
                    table.AddCell(new Paragraph(item.Nome).AddStyle(cellStyle()));

                    table.AddCell(new Paragraph(item.Email).AddStyle(cellStyle()));

                    table.AddCell(new Paragraph(item.DataCadastro.ToString("dd/MM/yyyy HH: mm")).AddStyle(cellStyle()));

}
                //adicionando a tabela do documento pdf
                document.Add(table);
                document.Add(new Paragraph($"\n"));
                document.Add(new Paragraph($"C# WebDeveloper - COTI Informática").AddStyle(textoStyle()));
                document.Add(new Paragraph($"Sistema UsuáriosWeb!").AddStyle(textoStyle()));
            }
            return memoryStream.ToArray();
        }
        private static Style tituloStyle()
        {
            var style = new Style();
            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            style.SetFontSize(32);
            style.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

            return style;
        }
        private static Style subTituloStyle()
        {
            var style = new Style();
            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
            style.SetFontSize(14);
            style.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

            return style;
        }
        private static Style textoStyle()
        {
            var style = new Style();
            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
            style.SetFontSize(12);

            return style;
        }
        private static Style headerCellStyle()
        {
            var style = new Style();
            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
            style.SetFontSize(11);
            style.SetFontColor(Color.ConvertRgbToCmyk(new DeviceRgb(0, 102, 204)));

            return style;
        }
        private static Style cellStyle()
        {
            var style = new Style();
            style.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
            style.SetFontSize(10);

            return style;
        }
    }
}
