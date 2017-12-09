using MusicIdentificationSystem.Backoffice.Models.ApplicationSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.Backoffice.Validators
{
    //public class SettingValidator : AbstractValidator<SettingModel>
    //{
    //    //TODO NAME LENGTH VALIDATION
    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="SettingValidator"/> class.
    //    /// </summary>
    //    public SettingValidator()
    //    {
    //        RuleFor(x => x.Name).NotNull().WithMessage(Resources.Resources.DELVR_Setting_Name_Required);
    //        RuleFor(x => x.Value).NotNull().WithMessage(Resources.Resources.DELVR_Setting_Value_Required);
    //        RuleFor(x => x.DataTypeId).NotNull().WithMessage(Resources.Resources.DELVR_Setting_DataTypeId_Required);

    //        Custom(x =>
    //        {
    //            var dataTypesService = EngineContext.Current.Resolve<IvwAttributeDataTypes>();

    //            var dictionary =
    //                dataTypesService.GetAllAttributeDataTypes(isActive: true, useCache: true)
    //                    .ToDictionary(dataType => dataType.AttributeDataTypeId,
    //                        dataType => dataType.AttributeDataTypeName);

    //            switch (dictionary[x.DataTypeId])
    //            {
    //                case "BIT":
    //                    if (!x.Value.IsNullOrWhiteSpace() && !(x.Value == "1" || x.Value == "0"))
    //                        return new ValidationFailure("Value", Resources.Resources.DELVR_Setting_Rules_BIT_Format);
    //                    return null;
    //                case "INT":
    //                    int intValue;
    //                    if (!Int32.TryParse(x.Value, out intValue))
    //                        return new ValidationFailure("Value", Resources.Resources.DELVR_Setting_Rules_INT_Format);
    //                    if (intValue < x.MinValue || intValue > x.MaxValue)
    //                        return new ValidationFailure("Value",
    //                            Resources.Resources.DELVR_Setting_Rules_INT_Validation);
    //                    return null;
    //                case "BIGINT":
    //                    long longValue;
    //                    if (!Int64.TryParse(x.Value, out longValue))
    //                        return new ValidationFailure("Value", Resources.Resources.DELVR_Setting_Rules_BIGINT_Format);
    //                    if (longValue < x.MinValue || longValue > x.MaxValue)
    //                        return new ValidationFailure("Value",
    //                            Resources.Resources.DELVR_Setting_Rules_BIGINT_Validation);
    //                    return null;
    //                case "DECIMAL":
    //                    decimal decimalValue;
    //                    if (!Decimal.TryParse(x.Value, NumberStyles.Number, Thread.CurrentThread.CurrentCulture, out decimalValue))
    //                        return new ValidationFailure("Value",
    //                            Resources.Resources.DELVR_Setting_Rules_DECIMAL_Format);
    //                    if (String.Equals(x.Name, "mobileareasettings.deliverymodule.receipt.maxcollectedamount") && (decimalValue < 0))
    //                        return new ValidationFailure("Value",
    //                            Resources.Resources.DELVR_Setting_Rules_MaxCollectedAmount);
    //                    if (decimalValue < x.MinValue || decimalValue > x.MaxValue)
    //                        return new ValidationFailure("Value",
    //                            Resources.Resources.DELVR_Setting_Rules_DECIMAL_Validation);
    //                    return null;
    //                case "DATETIME":
    //                    DateTime datetimeValue;
    //                    if (!DateTime.TryParseExact(x.Value, ApplicationConstantSettings.DateFormatUnescaped,
    //                            Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out datetimeValue))
    //                        return new ValidationFailure("Value", Resources.Resources.DELVR_Setting_Rules_DATETIME_Format);
    //                    return null;
    //                case "TEXT":

    //                    if (!string.IsNullOrEmpty(x.Value) && x.Value.Length > x.Length)
    //                        return new ValidationFailure("Value", Resources.Resources.DELVR_Setting_Rules_TEXT_Format);
    //                    return null;
    //                case "LINK":
    //                    return null;
    //                case "UINT":
    //                    uint uintValue;
    //                    if (!UInt32.TryParse(x.Value, out uintValue))
    //                        return new ValidationFailure("Value", Resources.Resources.DELVR_Setting_Rules_UINT_Format);
    //                    if (uintValue < x.MinValue || uintValue > x.MaxValue)
    //                        return new ValidationFailure("Value",
    //                            Resources.Resources.DELVR_Setting_Rules_UINT_Validation);
    //                    return null;
    //                default:
    //                    return null;

    //            }
    //        });
    //    }
    //}
}