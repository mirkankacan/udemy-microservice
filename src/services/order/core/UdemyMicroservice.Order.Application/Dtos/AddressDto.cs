﻿namespace UdemyMicroservice.Order.Application.Dtos
{
    public record AddressDto(string Province, string District, string Street, string ZipCode, string Line);
}