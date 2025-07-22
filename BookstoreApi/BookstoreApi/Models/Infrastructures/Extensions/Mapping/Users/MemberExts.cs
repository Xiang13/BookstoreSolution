using BookstoreApi.Models.DTOs.Users;
using BookstoreApi.Models.EFModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookstoreApi.Models.Infrastructures.Extensions.Mapping.Users
{
    public static class MemberExts
    {
        // ToDTO
        public static MemberDTO ToMemberDTO(this User user, List<string> roles)
        {
            return new MemberDTO
            {
                //UserId = user.UserId,
                DisplayName = user.DisplayName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Roles = roles,
            };
        }

        public static MemberOrderListDTO ToMemberOrderDTO(this Order order)
        {
            return new MemberOrderListDTO
            {
                //UserId = order.UserId,
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                TotalQuantity = order.TotalQuantity,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                PickupMethod = order.PickupMethod,
                BookImages = order.OrderDetails.Take(3).Select(d => d.ProductImage).ToList()
            };
        }

        public static MemberOrderDTO ToMemberOrderDetailDTO(this Order order)
        {
            return new MemberOrderDTO
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                TotalQuantity = order.TotalQuantity,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                PickupMethod = order.PickupMethod,
                OrderDetails = order.OrderDetails.Select(d => new MemberOrderDetailDTO
                {
                    OrderDetailId = d.OrderDetailId,
                    BookId = d.BookId,
                    BookTitle = d.BookTitle,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    Subtotal = d.Quantity * d.UnitPrice,
                    ProductImage = d.ProductImage,
                }).ToList()
            };
        }

        public static IEnumerable<MemberCartDTO> ToMemberCartItemlDTO(this IEnumerable<CartItem> item)
        {
            var totalAmount = item.Sum(i => i.Quantity * i.UnitPrice);
            return item.Select(item => new MemberCartDTO
            {
                CartId = item.CartId,
                BookId = item.BookId,
                BookTitle = item.Book.Title,
                ProductImage = item.Book.CoverImage,                
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                SubTotal = item.Quantity * item.UnitPrice,
                TotalAmount = totalAmount,
            }).ToList();
        }
    }
}
