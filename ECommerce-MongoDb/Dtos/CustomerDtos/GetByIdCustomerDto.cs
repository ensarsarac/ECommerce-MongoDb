﻿namespace ECommerce_MongoDb.Dtos.CustomerDtos
{
    public class GetByIdCustomerDto
    {
        public string CustomerId { get; set; }
        public string CustomerNameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
