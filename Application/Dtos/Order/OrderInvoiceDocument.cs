using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

namespace Application.Dtos.Order;
public class OrderInvoiceDocument : IDocument
{
    private readonly OrderDto order;

    public OrderInvoiceDocument(OrderDto order)
    {
        this.order = order;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(40);
            page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().AlignRight().Text(text =>
            {
                text.Span("فاتورة متجر الهانم")
                    .FontSize(22)
                    .FontColor(Colors.Green.Darken1)
                    .Bold()
                    .DirectionFromRightToLeft();
            });

            column.Item().AlignRight().Text(text =>
            {
                text.Span("شكراً لتسوقك معنا")
                    .FontSize(11)
                    .FontColor(Colors.Grey.Darken1)
                    .DirectionFromRightToLeft();
            });

            column.Item().AlignRight().Text($"رقم الطلب: {order.OrderNumber}")
                .FontSize(12).FontColor(Colors.Grey.Darken2)
                .DirectionFromRightToLeft();

            column.Item().AlignRight().Text($"تاريخ الطلب: {order.CreatedAt:yyyy/MM/dd}")
                .FontSize(11).FontColor(Colors.Grey.Darken1)
                .DirectionFromRightToLeft();

            column.Item().PaddingVertical(10).LineHorizontal(0.75f).LineColor(Colors.Grey.Lighten2);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            // 📍 معلومات الشحن
            column.Item().AlignRight().PaddingBottom(5).Text(text =>
            {
                text.Span("📍 معلومات الشحن")
                    .FontSize(14).Bold()
                    .DirectionFromRightToLeft();
            });

            column.Item().Column(inner =>
            {
                inner.Spacing(2);
                void AddRow(string label, string value)
                {
                    inner.Item().AlignRight().Text(txt =>
                    {
                        txt.Span(label)
                           .Bold()
                           .DirectionFromRightToLeft();

                        txt.Span(" ").DirectionFromRightToLeft();

                        txt.Span(value)
                           .DirectionFromRightToLeft();
                    });
                }

                AddRow("الاسم:", order.Location?.Name ?? "غير معروف");
                AddRow("العنوان:", $"{order.Location?.Address}, {order.Location?.StreetAddress}, {order.Location?.Building}, شقة {order.Location?.Apartment}");
                AddRow("رقم الهاتف:", order.Location?.PhoneNumber1 ?? "غير متوفر");
            });

            column.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten2);

            // 🛒 تفاصيل المنتجات
            column.Item().AlignRight().PaddingBottom(5).Text(text =>
            {
                text.Span("🛒 تفاصيل المنتجات")
                    .FontSize(14).Bold()
                    .DirectionFromRightToLeft();
            });

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(4); // المنتج
                    columns.RelativeColumn(1); // الكمية
                    columns.RelativeColumn(2); // السعر
                });

                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignRight()
                        .Text(text => text.Span("المنتج").SemiBold().DirectionFromRightToLeft());

                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignCenter()
                        .Text(text => text.Span("الكمية").SemiBold().DirectionFromRightToLeft());

                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignLeft()
                        .Text(text => text.Span("السعر").SemiBold().DirectionFromRightToLeft());
                });

                foreach (var item in order.Items)
                {
                    table.Cell().PaddingVertical(2).AlignRight().Text(item.ProductName).DirectionFromRightToLeft();
                    table.Cell().AlignCenter().Text(item.Quantity.ToString());
                    table.Cell().AlignLeft().Text($"{item.Price:F2} جنيه");
                }
            });

            column.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten3);

            // 💳 ملخص الطلب
            column.Item().AlignRight().PaddingBottom(5).Text(text =>
            {
                text.Span("💳 ملخص الطلب")
                    .FontSize(14).Bold()
                    .DirectionFromRightToLeft();
            });

            column.Item().Column(inner =>
            {
                inner.Spacing(3);
                void AddSummaryRow(string label, string value, bool bold = false)
                {
                    inner.Item().AlignRight().Text(txt =>
                    {
                        var labelSpan = txt.Span(label).DirectionFromRightToLeft();
                        if (bold) labelSpan.Bold();

                        txt.Span(" ").DirectionFromRightToLeft();

                        var valueSpan = txt.Span(value).DirectionFromRightToLeft();
                        if (bold) valueSpan.Bold();
                    });
                }

                //AddSummaryRow("حالة الطلب:", order.OrderStatus);
                //AddSummaryRow("طريقة التوصيل:", order.DeliveryStatus);
                AddSummaryRow("رسوم الشحن:", $"{order.ShippingFee:F2} جنيه");
                AddSummaryRow("الإجمالي:", $"{order.Total:F2} جنيه", bold: true);
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(text =>
        {
            text.Span("شكراً لطلبك! ").SemiBold().DirectionFromRightToLeft();
            text.Span($"تاريخ التوليد: {DateTime.Now:yyyy/MM/dd}")
                .FontSize(10).FontColor(Colors.Grey.Medium)
                .DirectionFromRightToLeft();
        });
    }
}