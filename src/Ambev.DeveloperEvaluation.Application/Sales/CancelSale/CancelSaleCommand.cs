﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for cancel a new sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for cancel a sale
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CancelSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CancelSaleCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CancelSaleCommand : IRequest<CancelSaleResult>
{
    public Guid Id { get; set; }
    public bool IsCancelled { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CancelSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}